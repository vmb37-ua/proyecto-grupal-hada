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
                    // Verificar si ya existe un equipo con el mismo nombre
                    string checkQuery = "SELECT COUNT(*) FROM equipo WHERE nombre = @nombre";
                    SqlCommand checkCmd = new SqlCommand(checkQuery, conn);
                    checkCmd.Parameters.AddWithValue("@nombre", equipo.Nombre);

                    conn.Open();
                    int count = (int)checkCmd.ExecuteScalar();

                    if (count > 0)
                    {
                        throw new Exception("Ya existe un equipo con ese nombre.");
                    }

                    // Si no existe, insertar
                    string insertQuery = "INSERT INTO equipo (escudo, nombre, categoria) VALUES (@escudo, @nombre, @categoria)";
                    SqlCommand insertCmd = new SqlCommand(insertQuery, conn);
                    insertCmd.Parameters.AddWithValue("@escudo", equipo.Escudo);
                    insertCmd.Parameters.AddWithValue("@nombre", equipo.Nombre);
                    insertCmd.Parameters.AddWithValue("@categoria", equipo.Categoria);

                    return insertCmd.ExecuteNonQuery() > 0;
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
                    string query = "DELETE FROM equipo WHERE nombre = @nombre";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@nombre", equipo.Nombre);

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
                    string query = "UPDATE equipo SET escudo = @escudo, categoria = @categoria WHERE nombre = @nombre";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@nombre", equipo.Nombre);
                    cmd.Parameters.AddWithValue("@escudo", equipo.Escudo);
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
                    string query = "SELECT * FROM equipo WHERE id_equipo = @id_equipo";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@nombre", equipo.Nombre);

                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        equipo.Id_equipo = Convert.ToInt32(reader["id_equipo"]);
                        equipo.Escudo = reader["escudo"].ToString();
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

        public List<ENEquipo> ReadAllbyCategoria(ENEquipo en)
        {
            List<ENEquipo> lista = new List<ENEquipo>();
            try
            {
                
                using (SqlConnection conn = new SqlConnection(conexion))
                {
                    string query = "SELECT * FROM equipo WHERE categoria = @categoria";
                    SqlCommand cmd = new SqlCommand(query, conn);

                    cmd.Parameters.AddWithValue("@categoria", en.Categoria);

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