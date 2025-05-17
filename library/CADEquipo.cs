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
            bool creado = false;
            try
            {
                using (SqlConnection conn = new SqlConnection(conexion))
                {
                    string query = "INSERT INTO equipo (id_estadio, nombre, ciudad) VALUES (@id_estadio, @nombre, @ciudad)";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@id_estadio", equipo.Id_estadio);
                    cmd.Parameters.AddWithValue("@nombre", equipo.Nombre);
                    cmd.Parameters.AddWithValue("@ciudad", equipo.Ciudad);

                    conn.Open();
                    creado = cmd.ExecuteNonQuery() > 0;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al crear equipo: " + ex.Message);
            }
            return creado;
        }

        public bool Delete(ENEquipo equipo)
        {
            bool eliminado = false;
            try
            {
                using (SqlConnection conn = new SqlConnection(conexion))
                {
                    string query = "DELETE FROM equipo WHERE id_equipo = @id_equipo";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@id_equipo", equipo.Id_equipo);

                    conn.Open();
                    eliminado = cmd.ExecuteNonQuery() > 0;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al eliminar equipo: " + ex.Message);
            }
            return eliminado;
        }

        public bool Update(ENEquipo equipo)
        {
            bool actualizado = false;
            try
            {
                using (SqlConnection conn = new SqlConnection(conexion))
                {
                    string query = "UPDATE equipo SET id_estadio = @id_estadio, nombre = @nombre, ciudad = @ciudad WHERE id_equipo = @id_equipo";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@id_equipo", equipo.Id_equipo);
                    cmd.Parameters.AddWithValue("@id_estadio", equipo.Id_estadio);
                    cmd.Parameters.AddWithValue("@nombre", equipo.Nombre);
                    cmd.Parameters.AddWithValue("@ciudad", equipo.Ciudad);

                    conn.Open();
                    actualizado = cmd.ExecuteNonQuery() > 0;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al actualizar equipo: " + ex.Message);
            }
            return actualizado;
        }

        public bool Read(ENEquipo equipo)
        {
            bool encontrado = false;
            try
            {
                using (SqlConnection conn = new SqlConnection(conexion))
                {
                    string query = "SELECT * FROM equipo WHERE id_equipo = @id_equipo";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@id_equipo", equipo.Id_equipo);

                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        equipo.Id_estadio = Convert.ToInt32(reader["id_estadio"]);
                        equipo.Nombre = reader["nombre"].ToString();
                        equipo.Ciudad = reader["ciudad"].ToString();
                        encontrado = true;
                    }
                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al leer equipo: " + ex.Message);
            }
            return encontrado;
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
                            Id_estadio = Convert.ToInt32(reader["id_estadio"]),
                            Nombre = reader["nombre"].ToString(),
                            Ciudad = reader["ciudad"].ToString()
                        };
                        lista.Add(equipo);
                    }
                    reader.Close();
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