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
    /// <summary>
    /// Clase que representa un objeto de Pais en la base de datos.
    /// Contiene los métodos necesarios para las operaciones Crear, Eliminar y Leer.
    /// </summary>
    public class CADPais
    {
        private string constring;

        /// <summary>
        /// Constructor por defecto.
        /// Inicializa la cadena de conexión para la base de datos.
        /// </summary>
        public CADPais()
        {
            constring = ConfigurationManager.ConnectionStrings["miconex"].ConnectionString;
        }

        /// <summary>
        /// Método que crea un pais en la base de datos.
        /// </summary>
        /// <param name="pais">Objeto entidad Pais que contiene todos los datos del pais a crear en la base de datos.</param>
        /// <returns>True si la creación es exitosa. False si hubo alguna excepción o no se pudo insertar.</returns>
        public bool Create(ENPais pais)
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
                        checkCmd.Parameters.AddWithValue("@nombre", pais.NombrePais);
                        if ((int)checkCmd.ExecuteScalar() > 0)
                            return false;
                    }

                    // Insertar nuevo país
                    string insertQuery = @"INSERT INTO Pais(nombre) VALUES(@nombre);
                                    SELECT SCOPE_IDENTITY();";

                    using (SqlCommand cmd = new SqlCommand(insertQuery, c))
                    {
                        cmd.Parameters.AddWithValue("@nombre", pais.NombrePais);
                        object result = cmd.ExecuteScalar();

                        if (result != null && result != DBNull.Value)
                        {
                            pais.IdPais = Convert.ToInt32(result);
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

        /// <summary>
        /// Método que elimina un pais en la base de datos.
        /// </summary>
        /// <param name="pais">Objeto entidad Pais que contiene, el ID del pais a eliminar.</param>
        /// <returns>True si la eliminación es exitosa. False si hubo alguna excepción o no se pudo borrar.</returns>
        public bool Delete(ENPais pais)
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
                WHERE p.id_pais = @idPaisParam"; 

                    SqlCommand cmdMunicipios = new SqlCommand(queryMunicipios, conn, transaction);
                    cmdMunicipios.Parameters.AddWithValue("@idPaisParam", pais.IdPais); 
                    cmdMunicipios.ExecuteNonQuery();

                    
                    string queryProvincias = "DELETE FROM provincia WHERE id_pais = @idPaisParam"; 
                    SqlCommand cmdProvincias = new SqlCommand(queryProvincias, conn, transaction);
                    cmdProvincias.Parameters.AddWithValue("@idPaisParam", pais.IdPais); 
                    cmdProvincias.ExecuteNonQuery();

                    
                    string queryPais = "DELETE FROM pais WHERE id_pais = @idPaisParam";
                    SqlCommand cmdPais = new SqlCommand(queryPais, conn, transaction);
                    cmdPais.Parameters.AddWithValue("@idPaisParam", pais.IdPais); 

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


        /// <summary>
        /// Método que lee todos los municipios de la base de datos de una misma provincia.
        /// </summary>
        /// <returns>Lista de entidades pais leídas de la base de datos.</returns>
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


