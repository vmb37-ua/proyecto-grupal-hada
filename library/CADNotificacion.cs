using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data.SqlClient;

namespace library
{
    public class CADNotificacion
    {
        private string conexion;

        public CADNotificacion()
        {
            conexion = ConfigurationManager.ConnectionStrings["miconex"].ToString();
        }

        public bool Create(ENNotificacion notificacion)
        {
            bool creada = false;
            try
            {
                using (SqlConnection conn = new SqlConnection(conexion))
                {
                    string query = "INSERT INTO notificacion (id_usuario, texto) VALUES (@id_usuario, @texto)";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@id_usuario", notificacion.IdUsuario);
                    cmd.Parameters.AddWithValue("@texto", notificacion.Mensaje);

                    conn.Open();
                    creada = cmd.ExecuteNonQuery() > 0;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al crear notificación: " + ex.Message);
            }
            return creada;
        }

        public bool Delete(ENNotificacion notificacion)
        {
            bool eliminada = false;
            try
            {
                using (SqlConnection conn = new SqlConnection(conexion))
                {
                    string query = "DELETE FROM notificacion WHERE id = @id AND id_usuario = @id_usuario";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@id", notificacion.Id);
                    cmd.Parameters.AddWithValue("@id_usuario", notificacion.IdUsuario);

                    conn.Open();
                    eliminada = cmd.ExecuteNonQuery() > 0;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al eliminar notificación: " + ex.Message);
            }
            return eliminada;
        }

        public bool Update(ENNotificacion notificacion)
        {
            bool actualizada = false;
            try
            {
                using (SqlConnection conn = new SqlConnection(conexion))
                {
                    string query = "UPDATE notificacion SET texto = @texto WHERE id = @id AND id_usuario = @id_usuario";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@texto", notificacion.Mensaje);
                    cmd.Parameters.AddWithValue("@id", notificacion.Id);
                    cmd.Parameters.AddWithValue("@id_usuario", notificacion.IdUsuario);

                    conn.Open();
                    actualizada = cmd.ExecuteNonQuery() > 0;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al actualizar notificación: " + ex.Message);
            }
            return actualizada;
        }

        public bool Read(ENNotificacion notificacion)
        {
            bool encontrada = false;
            try
            {
                using (SqlConnection conn = new SqlConnection(conexion))
                {
                    string query = "SELECT texto FROM notificacion WHERE id = @id AND id_usuario = @id_usuario";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@id", notificacion.Id);
                    cmd.Parameters.AddWithValue("@id_usuario", notificacion.IdUsuario);

                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        notificacion.Mensaje = reader["texto"].ToString();
                        encontrada = true;
                    }
                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al leer notificación: " + ex.Message);
            }
            return encontrada;
        }

        public List<ENNotificacion> ReadAll(ENNotificacion _)
        {
            List<ENNotificacion> lista = new List<ENNotificacion>();
            try
            {
                using (SqlConnection conn = new SqlConnection(conexion))
                {
                    string query = "SELECT id, id_usuario, texto FROM notificacion";
                    SqlCommand cmd = new SqlCommand(query, conn);

                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        ENNotificacion n = new ENNotificacion
                        {
                            Id = Convert.ToInt32(reader["id"]),
                            IdUsuario = Convert.ToInt32(reader["id_usuario"]),
                            Mensaje = reader["texto"].ToString()
                        };
                        lista.Add(n);
                    }
                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al leer todas las notificaciones: " + ex.Message);
            }
            return lista;
        }
    }
}
