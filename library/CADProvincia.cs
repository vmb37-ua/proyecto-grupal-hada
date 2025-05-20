
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Configuration;

namespace library
{
    public class CADProvincia
    {
        private string conexion;

        public CADProvincia()
        {
            conexion = ConfigurationManager.ConnectionStrings["miconex"].ToString();
        }

        public bool Create(ENProvincia provincia)
        {
            bool resultado = false;
            SqlConnection conn = new SqlConnection(conexion);

            try
            {
                conn.Open();
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

        public bool Delete(ENProvincia provincia)
        {
            bool resultado = false;
            SqlConnection conn = new SqlConnection(conexion);

            try
            {
                conn.Open();
                string query = "DELETE FROM provincia WHERE id = @id";
                SqlCommand cmd = new SqlCommand(query, conn);
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
