
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Configuration;
using System.Diagnostics;

namespace library
{ 
    /// <summary>
    /// Clase encargada de realizar las operaciones de base de datos
    /// </summary>
    public class CADProvincia
    {
        private string conexion;
        /// <summary>
        /// Constructor por defecto que obtiene la cadena de conexión.
        /// </summary>
        public CADProvincia()
        {
            conexion = ConfigurationManager.ConnectionStrings["miconex"].ToString();
        }
        /// <summary>
        /// Inserta una nueva provincia en la base de datos.
        /// </summary>
        /// <param name="provincia">Objeto <c>ENProvincia</c> con los datos a guardar.</param>
        /// <returns><c>true</c> si se insertó correctamente, <c>false</c> en caso contrario.</returns>
        public bool Create(ENProvincia provincia)
        {
            bool resultado = false;
            SqlConnection conn = new SqlConnection(conexion);

            try
            {
                conn.Open();

                string checkQuery = @"SELECT COUNT(*) FROM provincia WHERE LOWER(nombre) = LOWER(@nombre)AND id_pais = @idPais";
                using (SqlCommand checkCmd = new SqlCommand(checkQuery, conn))
                {
                    checkCmd.Parameters.AddWithValue("@nombre", provincia.Nombre);
                    checkCmd.Parameters.AddWithValue("@idPais", provincia.IdPais);

                    if ((int)checkCmd.ExecuteScalar() > 0)
                        return false;
                }


                string query = "INSERT INTO provincia (nombre, id_pais) VALUES (@nombre, @id_pais)";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@nombre", provincia.Nombre);
                cmd.Parameters.AddWithValue("@id_pais", provincia.IdPais);
                cmd.ExecuteNonQuery();
                resultado = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                conn.Close();
            }

            return resultado;
        }
        /// <summary>
        /// Modifica los datos de una provincia ya registrada.
        /// </summary>
        /// <param name="provincia">Provincia con los nuevos datos.</param>
        /// <returns><c>true</c> si al menos una fila fue afectada, <c>false</c> si no.</returns>
        public bool Update(ENProvincia provincia)
        {
            bool resultado = false;
            SqlConnection conn = new SqlConnection(conexion);

            try
            {
                conn.Open();
                string query = "UPDATE provincia SET nombre = @nombre, id_pais = @id_pais WHERE id = @id";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@nombre", provincia.Nombre);
                cmd.Parameters.AddWithValue("@id_pais", provincia.IdPais);
                cmd.Parameters.AddWithValue("@id", provincia.IdProvincia);
                int affectedRows = cmd.ExecuteNonQuery();
                resultado = affectedRows > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                conn.Close();
            }

            return resultado;
        }
        /// <summary>
        /// Elimina una provincia y todos sus municipios asociados.
        /// </summary>
        /// <param name="en">Provincia a eliminar.</param>
        /// <returns><c>true</c> si la operación fue exitosa, <c>false</c> en caso de fallo.</returns>
        public bool Delete(ENProvincia en)
        {
            bool resultado = false;

            using (SqlConnection conn = new SqlConnection(conexion))
            {
                conn.Open();
                SqlTransaction transaction = conn.BeginTransaction(); // Iniciar transacción

                try
                {
                    // 1. Eliminar todos los municipios de la provincia
                    string queryMunicipios = "DELETE FROM municipio WHERE id_provincia = @idProvincia";
                    SqlCommand cmdMunicipios = new SqlCommand(queryMunicipios, conn, transaction);
                    cmdMunicipios.Parameters.AddWithValue("@idProvincia", en.IdProvincia);
                    cmdMunicipios.ExecuteNonQuery();

                    // 2. Eliminar la provincia
                    string queryProvincia = "DELETE FROM provincia WHERE id_provincia = @idProvincia";
                    SqlCommand cmdProvincia = new SqlCommand(queryProvincia, conn, transaction);
                    cmdProvincia.Parameters.AddWithValue("@idProvincia", en.IdProvincia);

                    int affectedRows = cmdProvincia.ExecuteNonQuery();
                    resultado = affectedRows > 0;

                    transaction.Commit(); // Confirmar cambios si todo va bien
                }
                catch (Exception ex)
                {
                    transaction.Rollback(); // Revertir en caso de error
                    Debug.WriteLine($"Error eliminando provincia: {ex.Message}");
                    throw;
                }
            }

            return resultado;
        }

        /// <summary>
        /// Busca una provincia en la base de datos a partir de su identificador.
        /// </summary>
        /// <param name="provincia">Provincia a buscar.</param>
        /// <returns><c>true</c> si se encontró, <c>false</c> si no existe.</returns>
        public bool Read(ENProvincia provincia)
        {
            bool resultado = false;
            SqlConnection conn = new SqlConnection(conexion);

            try
            {
                conn.Open();
                string query = "SELECT * FROM provincia WHERE id = @id";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@id", provincia.IdProvincia);
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    provincia.Nombre = reader["nombre"].ToString();
                    provincia.IdPais = int.Parse(reader["id_pais"].ToString());
                    resultado = true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al leer la provincia: " + ex.Message);
            }
            finally
            {
                conn.Close();
            }

            return resultado;
        }

        /// <summary>
        /// Recupera todas las provincias existentes en la tabla, ordenadas por nombre.
        /// </summary>
        /// <returns>Lista de objetos <c>ENProvincia</c> con la información completa.</returns>
        /// 
        public List<ENProvincia> ReadAll()
        {
            var provincias = new List<ENProvincia>();

            using (var c = new SqlConnection(conexion))
            {
                try
                {
                    c.Open();
                    string sql = "SELECT id_provincia, nombre, id_pais FROM Provincia ORDER BY nombre";

                    using (var cmd = new SqlCommand(sql, c))
                    using (var dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            provincias.Add(new ENProvincia
                            {
                                IdProvincia = (int)dr["id_provincia"],
                                Nombre = dr["nombre"].ToString(),
                                IdPais = (int)dr["id_pais"]
                            });
                        }
                    }
                }
                catch (SqlException ex)
                {
                    Console.WriteLine($"Error al leer provincias: {ex.Message}");
                }
            }

            return provincias;
        }
        /// <summary>
        /// Obtiene todas las provincias correspondientes a un país concreto.
        /// </summary>
        /// <param name="provincia">Objeto con el ID del país como filtro.</param>
        /// <returns>Lista de provincias pertenecientes al país indicado.</returns>
        public List<ENProvincia> ReadAllByPais(ENProvincia provincia)
        {
            var provincias = new List<ENProvincia>();

            using (var c = new SqlConnection(conexion))
            {
                try
                {
                    c.Open();
                    string sql = "SELECT id_provincia, id_pais, nombre FROM Provincia WHERE id_pais = @idPais ORDER BY nombre";

                    using (var cmd = new SqlCommand(sql, c))
                    {
                        cmd.Parameters.AddWithValue("@idPais", provincia.IdPais);

                        using (var dr = cmd.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                provincias.Add(new ENProvincia(
                                    idProvincia: (int)dr["id_provincia"],
                                    idPais: (int)dr["id_pais"],
                                    nombre: dr["nombre"].ToString()
                                ));
                            }
                        }
                    }
                }
                catch (SqlException ex)
                {
                    Console.WriteLine($"Error al leer provincias por país: {ex.Message}");
                }
            }

            return provincias;
        }

        /*public List<ENProvincia> ReadAll(ENProvincia en)
        {
            var provincias = new List<ENProvincia>();

            using (var c = new SqlConnection(conexion))
            {
                try
                {
                    c.Open();
                    string sql = "SELECT id_provincia, nombre, id_pais FROM Provincia ORDER BY nombre";

                    using (var cmd = new SqlCommand(sql, c))
                    using (var dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            provincias.Add(new ENProvincia
                            {
                                IdProvincia = (int)dr["id_provincia"],
                                Nombre = dr["nombre"].ToString(),
                                IdPais = (int)dr["id_pais"]
                            });
                        }
                    }
                }
                catch (SqlException ex)
                {
                    Console.WriteLine($"Error al leer provincias: {ex.Message}");
                }
            }

            return provincias;
        }

        public List<ENProvincia> ReadAllByPais(ENProvincia provincia)
        {
            var provincias = new List<ENProvincia>();

            using (var c = new SqlConnection(conexion))
            {
                try
                {
                    c.Open();
                    string sql = "SELECT id_provincia, id_pais, nombre FROM Provincia WHERE id_pais = @idPais ORDER BY nombre";

                    using (var cmd = new SqlCommand(sql, c))
                    {
                        cmd.Parameters.AddWithValue("@idPais", provincia.IdPais);

                        using (var dr = cmd.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                provincias.Add(new ENProvincia(
                                    idProvincia: (int)dr["id_provincia"],
                                    idPais: (int)dr["id_pais"],
                                    nombre: dr["nombre"].ToString()
                                ));
                            }
                        }
                    }
                }
                catch (SqlException ex)
                {
                    Console.WriteLine($"Error al leer provincias por país: {ex.Message}");
                }
            }
            return lista;
        }*/
    }
}
