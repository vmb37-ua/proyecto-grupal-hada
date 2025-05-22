using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace library
{
    public class CADPais
    {
        private string constring;

        public CADPais()
        {
            constring = ConfigurationManager.ConnectionStrings["miconex"].ConnectionString;
        }

        // Crear Pais
        public bool Create(ENPais en)
        {
            using (SqlConnection c = new SqlConnection(constring))
            {
                try
                {
                    c.Open();

                    // Validar si ya existe el país
                    string checkQuery = "SELECT COUNT(*) FROM Pais WHERE LOWER(nombre) = LOWER(@nombre)";
                    using (SqlCommand checkCmd = new SqlCommand(checkQuery, c))
                    {
                        checkCmd.Parameters.AddWithValue("@nombre", en.NombrePais);
                        if ((int)checkCmd.ExecuteScalar() > 0)
                            return false;
                    }

                    // Insertar nuevo país
                    string insertQuery = @"INSERT INTO Pais(nombre) VALUES(@nombre);
                                    SELECT SCOPE_IDENTITY();";

                    using (SqlCommand cmd = new SqlCommand(insertQuery, c))
                    {
                        cmd.Parameters.AddWithValue("@nombre", en.NombrePais);
                        object result = cmd.ExecuteScalar();

                        if (result != null && result != DBNull.Value)
                        {
                            en.IdPais = Convert.ToInt32(result);
                            return true;
                        }
                        return false;
                    }
                }
                catch (SqlException ex)
                {
                    Console.WriteLine($"Error al crear país: {ex.Message}");
                    return false;
                }
            }
        }

        // Eliminar Pais
        public bool Delete(ENPais en)
        {
            bool resultado = false;

            using (SqlConnection conn = new SqlConnection(constring))
            {
                conn.Open();
                SqlTransaction transaction = conn.BeginTransaction();

                try
                {
                    // 1. Eliminar municipios (usando JOIN)
                    string queryMunicipios = @"
                DELETE m
                FROM municipio m
                INNER JOIN provincia p ON m.id_provincia = p.id_provincia
                WHERE p.id_pais = @idPaisParam";  // Cambiado a @idPaisParam

                    SqlCommand cmdMunicipios = new SqlCommand(queryMunicipios, conn, transaction);
                    cmdMunicipios.Parameters.AddWithValue("@idPaisParam", en.IdPais); // Nombre consistente
                    cmdMunicipios.ExecuteNonQuery();

                    // 2. Eliminar provincias
                    string queryProvincias = "DELETE FROM provincia WHERE id_pais = @idPaisParam"; // Mismo nombre
                    SqlCommand cmdProvincias = new SqlCommand(queryProvincias, conn, transaction);
                    cmdProvincias.Parameters.AddWithValue("@idPaisParam", en.IdPais); // Mismo nombre
                    cmdProvincias.ExecuteNonQuery();

                    // 3. Eliminar el país
                    string queryPais = "DELETE FROM pais WHERE id_pais = @idPaisParam"; // Mismo nombre
                    SqlCommand cmdPais = new SqlCommand(queryPais, conn, transaction);
                    cmdPais.Parameters.AddWithValue("@idPaisParam", en.IdPais); // Mismo nombre

                    resultado = cmdPais.ExecuteNonQuery() > 0;
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    Debug.WriteLine($"Error eliminando país: {ex.Message}");
                    throw new Exception("Error al eliminar el país. Detalles: " + ex.Message);
                }
            }

            return resultado;
        }


        // Leer todos los paises
        public List<ENPais> ReadAll()
        {
            var paises = new List<ENPais>();

            using (var c = new SqlConnection(constring))
            {
                try
                {
                    c.Open();
                    string sql = "SELECT id_pais, nombre FROM Pais ORDER BY nombre";

                    using (var cmd = new SqlCommand(sql, c))
                    using (var dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            paises.Add(new ENPais(
                                idPais: (int)dr["id_pais"],
                                nombre: dr["nombre"].ToString()
                            ));
                        }
                    }
                }
                catch (SqlException ex)
                {
                    Console.WriteLine($"Error al leer países: {ex.Message}");
                }
            }

            return paises;
        }
    }
}


