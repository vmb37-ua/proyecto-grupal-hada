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
    /// Clase de acceso a datos para la entidad Estadio.
    /// Contiene métodos para realizar operaciones CRUD en la base de datos.
    /// </summary>
    public class CADEstadio
    {
        private string conexion;

        /// <summary>
        /// Constructor por defecto. Inicializa la cadena de conexión.
        /// </summary>
        public CADEstadio()
        {
            conexion = ConfigurationManager.ConnectionStrings["miconex"].ToString();
        }

        /// <summary>
        /// Crea un nuevo estadio en la base de datos.
        /// </summary>
        /// <param name="estadio">Objeto ENEstadio con los datos del estadio.</param>
        /// <returns>True si se creó correctamente, false en caso contrario.</returns>
        public bool Create(ENEstadio estadio)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(conexion))
                {
                    string query = "INSERT INTO estadio (nombre, capacidad, texto, id_municipio) VALUES (@nombre, @capacidad, @texto, @id_municipio)";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@nombre", estadio.Nombre);
                    cmd.Parameters.AddWithValue("@capacidad", estadio.Capacidad);
                    cmd.Parameters.AddWithValue("@texto", estadio.Texto);
                    cmd.Parameters.AddWithValue("@id_municipio", estadio.Id_municipio);

                    conn.Open();
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al crear estadio: " + ex.Message);
            }
        }

        /// <summary>
        /// Actualiza los datos de un estadio existente en la base de datos.
        /// </summary>
        /// <param name="estadio">Objeto ENEstadio con los nuevos datos.</param>
        /// <returns>True si se actualizó correctamente, false en caso contrario.</returns>
        public bool Update(ENEstadio estadio)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(conexion))
                {
                    string query = "UPDATE estadio SET capacidad = @capacidad, texto = @texto, id_municipio = @id_municipio WHERE nombre = @nombre";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@nombre", estadio.Nombre);
                    cmd.Parameters.AddWithValue("@capacidad", estadio.Capacidad);
                    cmd.Parameters.AddWithValue("@texto", estadio.Texto);
                    cmd.Parameters.AddWithValue("@id_municipio", estadio.Id_municipio);

                    conn.Open();
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Elimina un estadio de la base de datos por su nombre.
        /// </summary>
        /// <param name="estadio">Objeto ENEstadio con el nombre del estadio a eliminar.</param>
        /// <returns>True si se eliminó correctamente, false en caso contrario.</returns>
        public bool Delete(ENEstadio estadio)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(conexion))
                {
                    string query = "DELETE FROM estadio WHERE nombre = @nombre";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@nombre", estadio.Nombre);

                    conn.Open();
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Lee los datos de un estadio por su nombre.
        /// </summary>
        /// <param name="estadio">Objeto ENEstadio con el nombre del estadio. Se actualiza con los datos leídos.</param>
        /// <returns>True si se encontró el estadio, false en caso contrario.</returns>
        public bool Read(ENEstadio estadio)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(conexion))
                {
                    string query = "SELECT * FROM estadio WHERE nombre = @nombre";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@nombre", estadio.Nombre);

                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        estadio.Capacidad = Convert.ToInt32(reader["capacidad"]);
                        estadio.Texto = reader["texto"].ToString();
                        estadio.Id_municipio = Convert.ToInt32(reader["id_municipio"]);
                        return true;
                    }
                    return false;
                }
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Lee todos los estadios existentes en la base de datos.
        /// </summary>
        /// <param name="_">No se utiliza, puede pasarse null.</param>
        /// <returns>Lista de objetos ENEstadio.</returns>
        public List<ENEstadio> ReadAll(ENEstadio _)
        {
            List<ENEstadio> lista = new List<ENEstadio>();
            try
            {
                using (SqlConnection conn = new SqlConnection(conexion))
                {
                    string query = "SELECT * FROM estadio";
                    SqlCommand cmd = new SqlCommand(query, conn);

                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        lista.Add(new ENEstadio
                        {
                            Nombre = reader["nombre"].ToString(),
                            Capacidad = Convert.ToInt32(reader["capacidad"]),
                            Texto = reader["texto"].ToString(),
                            Id_municipio = Convert.ToInt32(reader["id_municipio"])
                        });
                    }
                }
            }
            catch { }
            return lista;
        }
    }
}
