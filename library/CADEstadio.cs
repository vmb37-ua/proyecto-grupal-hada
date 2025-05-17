using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace library
{
    public class CADEstadio
    {
        private string conexion;

        public CADEstadio()
        {
            conexion = ConfigurationManager.ConnectionStrings["miconex"].ToString();
        }

        public bool Create(ENEstadio estadio)
        {
            bool creado = false;
            try
            {
                using (SqlConnection conn = new SqlConnection(conexion))
                {
                    string query = "INSERT INTO estadio (nombre, ciudad, direccion) VALUES (@nombre, @ciudad, @direccion)";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@nombre", estadio.Nombre);
                    cmd.Parameters.AddWithValue("@ciudad", estadio.Ciudad);
                    cmd.Parameters.AddWithValue("@direccion", estadio.Direccion);

                    conn.Open();
                    creado = cmd.ExecuteNonQuery() > 0;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al crear estadio: " + ex.Message);
            }
            return creado;
        }

        public bool Delete(ENEstadio estadio)
        {
            bool eliminado = false;
            try
            {
                using (SqlConnection conn = new SqlConnection(conexion))
                {
                    string query = "DELETE FROM estadio WHERE id_estadio = @id_estadio";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@id_estadio", estadio.Id_estadio);

                    conn.Open();
                    eliminado = cmd.ExecuteNonQuery() > 0;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al eliminar estadio: " + ex.Message);
            }
            return eliminado;
        }

        public bool Update(ENEstadio estadio)
        {
            bool actualizado = false;
            try
            {
                using (SqlConnection conn = new SqlConnection(conexion))
                {
                    string query = "UPDATE estadio SET nombre = @nombre, ciudad = @ciudad, direccion = @direccion WHERE id_estadio = @id_estadio";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@nombre", estadio.Nombre);
                    cmd.Parameters.AddWithValue("@ciudad", estadio.Ciudad);
                    cmd.Parameters.AddWithValue("@direccion", estadio.Direccion);
                    cmd.Parameters.AddWithValue("@id_estadio", estadio.Id_estadio);

                    conn.Open();
                    actualizado = cmd.ExecuteNonQuery() > 0;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al actualizar estadio: " + ex.Message);
            }
            return actualizado;
        }

        public bool Read(ENEstadio estadio)
        {
            bool encontrado = false;
            try
            {
                using (SqlConnection conn = new SqlConnection(conexion))
                {
                    string query = "SELECT * FROM estadio WHERE id_estadio = @id_estadio";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@id_estadio", estadio.Id_estadio);

                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        estadio.Nombre = reader["nombre"].ToString();
                        estadio.Ciudad = reader["ciudad"].ToString();
                        estadio.Direccion = reader["direccion"].ToString();
                        encontrado = true;
                    }
                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al leer estadio: " + ex.Message);
            }
            return encontrado;
        }

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
                        ENEstadio estadio = new ENEstadio
                        {
                            Id_estadio = Convert.ToInt32(reader["id_estadio"]),
                            Nombre = reader["nombre"].ToString(),
                            Ciudad = reader["ciudad"].ToString(),
                            Direccion = reader["direccion"].ToString()
                        };
                        lista.Add(estadio);
                    }
                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al leer todos los estadios: " + ex.Message);
            }
            return lista;
        }
    }
}