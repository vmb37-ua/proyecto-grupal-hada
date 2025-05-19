using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace library
{
    public class CADMunicipio
    {
        private string conexion;

        public CADMunicipio() {
            conexion = ConfigurationManager.ConnectionStrings["miconex"].ToString();
        }
        public bool Create(ENMunicipio municipio)
        {
            bool resultado = false;
            SqlConnection conn = new SqlConnection(conexion);
            SqlCommand cmd = new SqlCommand("INSERT INTO municipio (id_provincia, nombre) VALUES (@id_provincia, @nombre)", conn);

            cmd.Parameters.AddWithValue("@id_provincia", municipio.Id_provincia);
            cmd.Parameters.AddWithValue("@nombre", municipio.Nombre);

            try
            {
                conn.Open();
                int filasAfectadas = cmd.ExecuteNonQuery();
                resultado = filasAfectadas != 0;
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                conn.Close();
            }

            return resultado;
        }
        public bool Delete(ENMunicipio municipio) {
            bool resultado = false;
            SqlConnection conn = new SqlConnection(conexion);
            SqlCommand cmd = new SqlCommand("DELETE FROM municipio WHERE id_municipio = @id_municipio", conn);

            cmd.Parameters.AddWithValue("@id_municipio", municipio.Id_municipio);

            try
            {
                conn.Open();
                int filasAfectadas = cmd.ExecuteNonQuery();
                resultado = filasAfectadas != 0;
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                conn.Close();
            }

            return resultado;
        }
        public bool Update(ENMunicipio municipio) {
            bool resultado = false;
            string query = "UPDATE municipio SET id_provincia = @id_provincia, nombre = @nombre WHERE id_municipio = @id";

            SqlConnection conn = new SqlConnection(conexion);
            conn.Open();

            try
            {
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@id", municipio.Id_municipio);
                cmd.Parameters.AddWithValue("@imagen", municipio.Id_provincia);
                cmd.Parameters.AddWithValue("@contrasenya", municipio.Nombre);

                int filasAfectadas = cmd.ExecuteNonQuery();
                resultado = filasAfectadas != 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                conn.Close();
            }

            return true;
        }
        public bool Read(ENMunicipio municipio) {
            SqlConnection conn = new SqlConnection(conexion);
            bool resultado = false;
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM municipio WHERE id_municipio = @id", conn);
                cmd.Parameters.AddWithValue("@id", municipio.Id_municipio);
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    municipio.Id_provincia = int.Parse(reader["id_provincia"].ToString());
                    municipio.Nombre = reader["nombre"].ToString();
                    resultado = true;
                }
                else
                {
                    resultado = false;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                resultado = false;
            }
            finally
            {
                conn.Close();
            }
            return resultado;
        }
        public List<ENMunicipio> ReadAll(ENMunicipio en)
        {
            List<ENMunicipio> lista = new List<ENMunicipio>();

            using (SqlConnection conn = new SqlConnection(conexion))
            {
                
                string query = "SELECT id_municipio, nombre FROM municipio WHERE id_provincia = @idProvincia";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@idProvincia", en.Id_provincia);

                try
                {
                    conn.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            // Crear nueva instancia para cada municipio
                            ENMunicipio municipio = new ENMunicipio
                            {
                                Id_municipio = Convert.ToInt32(reader["Id_municipio"]), 
                                Nombre = Convert.ToString(reader["Nombre"]),      
                                Id_provincia = en.Id_provincia 
                            };
                            lista.Add(municipio);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error en ReadAll (Municipio): {ex.ToString()}");
                    
                }
            }

            return lista;
        }
    }
}
