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
                Console.WriteLine( ex.Message);
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
                Console.WriteLine( ex.Message);
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
                Console.WriteLine( ex.Message);
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

        public List<ENProvincia> ReadAll(ENProvincia provincia)
        {
            List<ENProvincia> lista = new List<ENProvincia>();
            SqlConnection conn = new SqlConnection(conexion);

            try
            {
                conn.Open();
                string query = "SELECT * FROM provincia WHERE id_pais = @id_pais";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@id_pais", provincia.IdPais);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    ENProvincia p = new ENProvincia();
                    p.IdProvincia = int.Parse(reader["id"].ToString());
                    p.Nombre = reader["nombre"].ToString();
                    p.IdPais = int.Parse(reader["id_pais"].ToString());
                    lista.Add(p);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                conn.Close();
            }

            return lista;
        }
    }
}
