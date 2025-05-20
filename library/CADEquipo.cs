using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace library
{
    public class CADEquipo
    {
        private string conexion;

        public CADEquipo()
        {
            conexion = ConfigurationManager.ConnectionStrings["miconex"].ToString();
        }

        public bool Create(ENEquipo equipo)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(conexion))
                {
                    string query = "INSERT INTO equipo (id_equipo, escudo, nombre, categoria) VALUES (@id, @escudo, @nombre, @categoria)";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@id", equipo.Id_equipo);
                    cmd.Parameters.AddWithValue("@escudo", equipo.Escudo);
                    cmd.Parameters.AddWithValue("@nombre", equipo.Nombre);
                    cmd.Parameters.AddWithValue("@categoria", equipo.Categoria);

                    conn.Open();
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al crear equipo: " + ex.Message);
            }

        }

        public bool Delete(ENEquipo equipo)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(conexion))
                {
                    string query = "DELETE FROM equipo WHERE id_equipo = @id";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@id", equipo.Id_equipo);

                    conn.Open();
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al eliminar equipo: " + ex.Message);
                return false;
            }
        }

        public bool Update(ENEquipo equipo)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(conexion))
                {
                    string query = "UPDATE equipo SET escudo = @escudo, nombre = @nombre, categoria = @categoria WHERE id_equipo = @id";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@id", equipo.Id_equipo);
                    cmd.Parameters.AddWithValue("@escudo", equipo.Escudo);
                    cmd.Parameters.AddWithValue("@nombre", equipo.Nombre);
                    cmd.Parameters.AddWithValue("@categoria", equipo.Categoria);

                    conn.Open();
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al actualizar equipo: " + ex.Message);
                return false;
            }
        }

        public bool Read(ENEquipo equipo)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(conexion))
                {
                    string query = "SELECT * FROM equipo WHERE id_equipo = @id";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@id", equipo.Id_equipo);

                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        equipo.Escudo = reader["escudo"].ToString();
                        equipo.Nombre = reader["nombre"].ToString();
                        equipo.Categoria = reader["categoria"].ToString();
                        return true;
                    }
                    return false;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al leer equipo: " + ex.Message);
                return false;
            }
        }

        public List<ENEquipo> ReadAll(ENEquipo _)
        {
            List<ENEquipo> lista = new List<ENEquipo>();
            try
            {
                using (SqlConnection conn = new SqlConnection(conexion))
                {
                    string query = "SELECT * FROM equipo";
                    SqlCommand cmd = new SqlCommand(query, conn);

                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        ENEquipo equipo = new ENEquipo
                        {
                            Id_equipo = Convert.ToInt32(reader["id_equipo"]),
                            Escudo = reader["escudo"].ToString(),
                            Nombre = reader["nombre"].ToString(),
                            Categoria = reader["categoria"].ToString()
                        };
                        lista.Add(equipo);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al leer todos los equipos: " + ex.Message);
            }
            return lista;
        }
    }
}