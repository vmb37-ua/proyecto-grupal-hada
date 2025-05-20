using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
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
        public bool CrearPais(ENPais en)
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
        public bool EliminarPais(ENPais en)
        {
            try
            {
                using (SqlConnection c = new SqlConnection(constring))
                {
                    c.Open();

                    // Verificar existencia
                    string checkQuery = "SELECT id_pais FROM Pais WHERE id_pais = @id";
                    using (SqlCommand checkCmd = new SqlCommand(checkQuery, c))
                    {
                        checkCmd.Parameters.AddWithValue("@id", en.IdPais);
                        if (checkCmd.ExecuteScalar() == null)
                        {
                            return false;
                        }
                    }

                    // Eliminar por ID 
                    string deleteQuery = "DELETE FROM Pais WHERE id_pais = @id";
                    using (SqlCommand cmd = new SqlCommand(deleteQuery, c))
                    {
                        cmd.Parameters.AddWithValue("@id", en.IdPais);
                        return cmd.ExecuteNonQuery() == 1;
                    }
                }
            }
            catch (SqlException ex) when (ex.Number == 547)
            {
                return false;
            }
            catch (SqlException ex)
            {
                return false;
            }
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


