using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data.SqlClient;

namespace library
{
    public class CADTransaccion
    {
        private string conexion;

        public CADTransaccion()
        {
            conexion = ConfigurationManager.ConnectionStrings["miconex"].ToString();
        }

        public bool Create(ENTransaccion transaccion)
        {
            bool creada = false;
            try
            {
                using (SqlConnection conn = new SqlConnection(conexion))
                {
                    string query = "INSERT INTO transaccion (dinero, metodo, id_usu) VALUES (@dinero, @metodo, @id_usu)";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@dinero", transaccion.Cantidad);
                    cmd.Parameters.AddWithValue("@metodo", transaccion.MetodoPago);
                    cmd.Parameters.AddWithValue("@id_usu", transaccion.IdUsuario);

                    conn.Open();
                    creada = cmd.ExecuteNonQuery() > 0;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al crear transacción: " + ex.Message);
            }
            return creada;
        }

        public bool Delete(ENTransaccion transaccion)
        {
            bool eliminada = false;
            try
            {
                using (SqlConnection conn = new SqlConnection(conexion))
                {
                    string query = "DELETE FROM transaccion WHERE id = @id";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@id", transaccion.Id);

                    conn.Open();
                    eliminada = cmd.ExecuteNonQuery() > 0;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al eliminar transacción: " + ex.Message);
            }
            return eliminada;
        }

        public bool Update(ENTransaccion transaccion)
        {
            bool actualizada = false;
            try
            {
                using (SqlConnection conn = new SqlConnection(conexion))
                {
                    string query = "UPDATE transaccion SET dinero = @dinero, metodo = @metodo WHERE id = @id";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@dinero", transaccion.Cantidad);
                    cmd.Parameters.AddWithValue("@metodo", transaccion.MetodoPago);
                    cmd.Parameters.AddWithValue("@id", transaccion.Id);

                    conn.Open();
                    actualizada = cmd.ExecuteNonQuery() > 0;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al actualizar transacción: " + ex.Message);
            }
            return actualizada;
        }

        public bool Read(ENTransaccion transaccion)
        {
            bool encontrada = false;
            try
            {
                using (SqlConnection conn = new SqlConnection(conexion))
                {
                    string query = "SELECT dinero, metodo, id_usu FROM transaccion WHERE id = @id";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@id", transaccion.Id);

                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        transaccion.Cantidad = float.Parse(reader["dinero"].ToString());
                        transaccion.MetodoPago = reader["metodo"].ToString();
                        transaccion.IdUsuario = Convert.ToInt32(reader["id_usu"]);
                        encontrada = true;
                    }
                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al leer transacción: " + ex.Message);
            }
            return encontrada;
        }

        public List<ENTransaccion> ReadAll(ENTransaccion transaccion)
        {
            List<ENTransaccion> lista = new List<ENTransaccion>();
            try
            {
                using (SqlConnection conn = new SqlConnection(conexion))
                {
                    string query = "SELECT id, dinero, metodo, id_usu FROM transaccion WHERE id_usu = @id_usu";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@id_usu", transaccion.IdUsuario);

                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        ENTransaccion t = new ENTransaccion
                        {
                            Id = Convert.ToInt32(reader["id"]),
                            Cantidad = float.Parse(reader["dinero"].ToString()),
                            MetodoPago = reader["metodo"].ToString(),
                            IdUsuario = Convert.ToInt32(reader["id_usu"])
                        };
                        lista.Add(t);
                    }
                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al leer transacciones del usuario: " + ex.Message);
            }
            return lista;
        }
    }
}
