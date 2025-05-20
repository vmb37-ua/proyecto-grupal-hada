using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic;

namespace library
{
    public class CADApuesta
    {
        private string conexion;

        public CADApuesta()
        {
            conexion = ConfigurationManager.ConnectionStrings["miconex"].ToString();
        }

        public bool Create(ENApuesta apuesta)
        {
            bool aux = true;
            SqlConnection c = new SqlConnection(constring);
            try
            {
                c.Open();
                SqlCommand com = new SqlCommand("INSERT INTO Apuesta (IdApuesta, Fecha, IdEstadio, IdEquipo1, IdEquipo2, Cot1, Cot2, CotX) " +
                                                "VALUES (@IdApuesta, @Fecha, @IdEstadio, @IdEquipo1, @IdEquipo2, @Cot1, @Cot2, @CotX)", c);

                com.Parameters.AddWithValue("@IdApuesta", apuesta.Id_apuesta);
                com.Parameters.AddWithValue("@Fecha", apuesta.Fecha);
                com.Parameters.AddWithValue("@estadio", apuesta.Estadio.Nombre);
                com.Parameters.AddWithValue("@IdEquipo1", apuesta.Equipo1.Id_equipo);
                com.Parameters.AddWithValue("@IdEquipo2", apuesta.Equipo2.Id_equipo);
                com.Parameters.AddWithValue("@Cot1", apuesta.cot1);
                com.Parameters.AddWithValue("@Cot2", apuesta.cot2);
                com.Parameters.AddWithValue("@CotX", apuesta.cotX);

            }
            catch (Exception ex)
            {
                Console.WriteLine("Failure to create Apuesta", ex.Message);
                aux = false;
            }
            finally
            {
                c.Close();
            }
            return aux;
        }

        public bool Update(ENApuesta apuesta)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(conexion))
                {
                    string query = "UPDATE apuesta SET resultado = @resultado, fecha = @fecha, " +
                                   "id_equipo1 = @eq1, id_equipo2 = @eq2, estadio = @estadio WHERE id_apuesta = @id";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@id", apuesta.Id_apuesta);
                    cmd.Parameters.AddWithValue("@resultado", apuesta.Resultado);
                    cmd.Parameters.AddWithValue("@fecha", apuesta.Fecha);
                    cmd.Parameters.AddWithValue("@eq1", apuesta.Equipo1.Id_equipo);
                    cmd.Parameters.AddWithValue("@eq2", apuesta.Equipo2.Id_equipo);
                    cmd.Parameters.AddWithValue("@estadio", apuesta.Estadio.Nombre);

                    conn.Open();
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al actualizar apuesta: " + ex.Message);
            }
        }

        public bool Delete(ENApuesta apuesta)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(conexion))
                {
                    string query = "DELETE FROM apuesta WHERE id_apuesta = @id";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@id", apuesta.Id_apuesta);

                    conn.Open();
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al eliminar apuesta: " + ex.Message);
            }
        }

        public bool Read(ENApuesta apuesta)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(conexion))
                {
                    string query = "SELECT * FROM apuesta WHERE id_apuesta = @id";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@id", apuesta.Id_apuesta);

                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        apuesta.Resultado = Convert.ToInt32(reader["resultado"]);
                        apuesta.Fecha = Convert.ToDateTime(reader["fecha"]);
                        apuesta.Equipo1 = new ENEquipo { Id_equipo = Convert.ToInt32(reader["id_equipo1"]) };
                        apuesta.Equipo2 = new ENEquipo { Id_equipo = Convert.ToInt32(reader["id_equipo2"]) };
                        apuesta.Estadio = new ENEstadio { Nombre = reader["estadio"].ToString() };
                        return true;
                    }
                    return false;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al leer apuesta: " + ex.Message);
            }
        }

        public List<ENApuesta> ReadAll(ENApuesta _)
        {
            List<ENApuesta> lista = new List<ENApuesta>();
            try
            {
                using (SqlConnection conn = new SqlConnection(conexion))
                {
                    string query = "SELECT * FROM apuesta";
                    SqlCommand cmd = new SqlCommand(query, conn);

                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        var ap = new ENApuesta
                        {
                            Id_apuesta = Convert.ToInt32(reader["id_apuesta"]),
                            Resultado = Convert.ToInt32(reader["resultado"]),
                            Fecha = Convert.ToDateTime(reader["fecha"]),
                            Equipo1 = new ENEquipo { Id_equipo = Convert.ToInt32(reader["id_equipo1"]) },
                            Equipo2 = new ENEquipo { Id_equipo = Convert.ToInt32(reader["id_equipo2"]) },
                            Estadio = new ENEstadio { Nombre = reader["estadio"].ToString() }
                        };
                        lista.Add(ap);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al leer todas las apuestas: " + ex.Message);
            }
            return lista;
        }
    }
}