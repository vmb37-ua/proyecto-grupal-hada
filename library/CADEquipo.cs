
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace library
{
    /// <summary>
    /// Clase de acceso a datos para la entidad Equipo.
    /// Contiene métodos para realizar operaciones CRUD en la base de datos.
    /// </summary>
    public class CADEquipo
    {
        private string conexion;

        /// <summary>
        /// Constructor por defecto. Inicializa la cadena de conexión.
        /// </summary>
        public CADEquipo()
        {
            conexion = ConfigurationManager.ConnectionStrings["miconex"].ToString();
        }

        /// <summary>
        /// Crea un nuevo equipo en la base de datos si no existe uno con el mismo nombre.
        /// </summary>
        /// <param name="equipo">Objeto ENEquipo con los datos del equipo a crear.</param>
        /// <returns>True si se creó correctamente, false en caso contrario.</returns>
        public bool Create(ENEquipo equipo)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(conexion))
                {
                    string checkQuery = "SELECT COUNT(*) FROM equipo WHERE nombre = @nombre";
                    SqlCommand checkCmd = new SqlCommand(checkQuery, conn);
                    checkCmd.Parameters.AddWithValue("@nombre", equipo.Nombre);

                    conn.Open();
                    int count = (int)checkCmd.ExecuteScalar();

                    if (count > 0)
                        throw new Exception("Ya existe un equipo con ese nombre.");

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

        /// <summary>
        /// Elimina un equipo de la base de datos según su ID.
        /// </summary>
        /// <param name="equipo">Objeto ENEquipo con el ID del equipo a eliminar.</param>
        /// <returns>True si se eliminó correctamente, false en caso contrario.</returns>
        public bool Delete(ENEquipo equipo)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(conexion))
                {
                    string query = "DELETE FROM equipo WHERE id_equipo = @id_equipo";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@id_equipo", equipo.Id_equipo);

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

        /// <summary>
        /// Actualiza los datos de un equipo existente en la base de datos.
        /// </summary>
        /// <param name="equipo">Objeto ENEquipo con los nuevos datos.</param>
        /// <returns>True si se actualizó correctamente, false en caso contrario.</returns>
        public bool Update(ENEquipo equipo)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(conexion))
                {
                    string query = "UPDATE equipo SET nombre = @nombre, escudo = @escudo, categoria = @categoria WHERE id_equipo = @id_equipo";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@id_equipo", equipo.Id_equipo);
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

        /// <summary>
        /// Lee los datos de un equipo por su ID.
        /// </summary>
        /// <param name="equipo">Objeto ENEquipo con el ID del equipo. Se actualiza con los datos leídos.</param>
        /// <returns>True si se encontró el equipo, false en caso contrario.</returns>
        public bool Read(ENEquipo equipo)
        {
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
                        equipo.Nombre = reader["nombre"].ToString();
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

        /// <summary>
        /// Lee todos los equipos existentes en la base de datos.
        /// </summary>
        /// <param name="_">No se utiliza, puede pasarse null.</param>
        /// <returns>Lista de objetos ENEquipo.</returns>
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

        /// <summary>
        /// Lee todos los equipos filtrados por una categoría específica.
        /// </summary>
        /// <param name="en">Objeto ENEquipo con la categoría deseada.</param>
        /// <returns>Lista de objetos ENEquipo que pertenecen a esa categoría.</returns>
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
                Console.WriteLine("Error al leer equipos por categoría: " + ex.Message);
            }
            return lista;
        }
    }
}
