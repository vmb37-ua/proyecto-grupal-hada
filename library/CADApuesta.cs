using System;
using System.Collections.Generic;
using System.Configuration;
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
        private string constring { get; set; }
        public CADApuesta()
        {
            constring = ConfigurationManager.ConnectionStrings["miconex"].ToString();
        }

        public bool Create(ENApuesta apuesta)
        {
            bool aux = true;
            SqlConnection c = new SqlConnection(constring);
            try
            {
                    c.Open();

                    SqlCommand check = new SqlCommand("SELECT COUNT(*) FROM Apuesta WHERE estadio = @Estadio", c);
                    check.Parameters.AddWithValue("@Estadio", apuesta.Estadio.Nombre);

                    int count = (int)check.ExecuteScalar();
                    if (count > 0)
                    {
                        throw new Exception("Ya existe una apuesta para ese estadio.");
                    }

              
                    SqlCommand com = new SqlCommand(
                        "INSERT INTO Apuesta (Fecha, estadio, IdEquipo1, IdEquipo2, Cot1, Cot2, CotX) " +
                        "OUTPUT INSERTED.id_apuesta " +
                        "VALUES (@Fecha, @Estadio, @IdEquipo1, @IdEquipo2, @Cot1, @Cot2, @CotX)", c);

                    com.Parameters.AddWithValue("@Fecha", apuesta.Fecha);
                    com.Parameters.AddWithValue("@Estadio", apuesta.Estadio.Nombre);
                    com.Parameters.AddWithValue("@IdEquipo1", apuesta.Equipo1.Id_equipo);
                    com.Parameters.AddWithValue("@IdEquipo2", apuesta.Equipo2.Id_equipo);
                    com.Parameters.AddWithValue("@Cot1", apuesta.cot1);
                    com.Parameters.AddWithValue("@Cot2", apuesta.cot2);
                    com.Parameters.AddWithValue("@CotX", apuesta.cotX);

                    apuesta.Id_apuesta = (int)com.ExecuteScalar(); 

                    return true;
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
        public bool Delete(ENApuesta apuesta)
        {
            bool aux = true;
            SqlConnection c = new SqlConnection(constring);
            try
            {
                c.Open();
                SqlCommand com = new SqlCommand("DELETE FROM Apuesta WHERE id_apuesta = @IdApuesta", c);
                com.Parameters.AddWithValue("@IdApuesta", apuesta.Id_apuesta);
                com.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                Console.WriteLine("Failure to delete Apuesta: " + ex.Message);
                aux = false;
            }
            finally
            {
                c.Close();
            }
            return aux;
        }
        public bool Read(ENApuesta apuesta)
        {
            bool aux = false;
            SqlConnection c = new SqlConnection(constring);
            try
            {
                c.Open();
                SqlCommand com = new SqlCommand("SELECT Fecha, estadio, IdEquipo1, IdEquipo2, Cot1, Cot2, CotX FROM Apuesta WHERE id_apuesta = @IdApuesta", c);
                com.Parameters.AddWithValue("@IdApuesta", apuesta.Id_apuesta);
                SqlDataReader data = com.ExecuteReader();
                if (data.Read())
                {
                    apuesta.Fecha = Convert.ToDateTime(data["Fecha"]);
                    apuesta.Estadio = new ENEstadio { Nombre = Convert.ToString(data["estadio"]) };
                    apuesta.Equipo1 = new ENEquipo { Id_equipo = Convert.ToInt32(data["IdEquipo1"]) };
                    apuesta.Equipo2 = new ENEquipo { Id_equipo = Convert.ToInt32(data["IdEquipo2"]) };
                    apuesta.cot1 = Convert.ToDouble(data["Cot1"]);
                    apuesta.cot2 = Convert.ToDouble(data["Cot2"]);
                    apuesta.cotX = Convert.ToDouble(data["CotX"]);
                    aux = true;
                }
                data.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Failure to read Apuesta: " + ex.Message);
            }
            finally
            {
                c.Close();
            }
            return aux;
        }
        public bool Update(ENApuesta apuesta)
        {
            bool aux = true;
            SqlConnection c = new SqlConnection(constring);
            try
            {
                c.Open();
                SqlCommand com = new SqlCommand("UPDATE Apuesta SET Fecha = @Fecha, estadio = @estadio, IdEquipo1 = @IdEquipo1, " +
                                                "IdEquipo2 = @IdEquipo2, Cot1 = @Cot1, Cot2 = @Cot2, CotX = @CotX WHERE id_apuesta = @IdApuesta", c);

                com.Parameters.AddWithValue("@Fecha", apuesta.Fecha);
                com.Parameters.AddWithValue("@estadio", apuesta.Estadio.Nombre);
                com.Parameters.AddWithValue("@IdEquipo1", apuesta.Equipo1.Id_equipo);
                com.Parameters.AddWithValue("@IdEquipo2", apuesta.Equipo2.Id_equipo);
                com.Parameters.AddWithValue("@Cot1", apuesta.cot1);
                com.Parameters.AddWithValue("@Cot2", apuesta.cot2);
                com.Parameters.AddWithValue("@CotX", apuesta.cotX);
                com.Parameters.AddWithValue("@IdApuesta", apuesta.Id_apuesta);

                int rows = com.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Failure to update Apuesta: " + ex.Message);
                aux = false;
            }
            finally
            {
                c.Close();
            }
            return aux;
        }
        public List<ENApuesta> ReadAll(ENApuesta apuesta)
        {
            List<ENApuesta> lista = new List<ENApuesta>();
            SqlConnection c = new SqlConnection(constring);
            try
            {
                c.Open();
                SqlCommand com = new SqlCommand("SELECT id_apuesta, Fecha, IdEstadio, IdEquipo1, IdEquipo2, Cot1, Cot2, CotX FROM Apuesta", c);
                SqlDataReader data = com.ExecuteReader();
                while (data.Read())
                {
                    ENApuesta a = new ENApuesta();
                    a.Id_apuesta = Convert.ToInt32(data["id_apuesta"]);
                    a.Fecha = Convert.ToDateTime(data["Fecha"]);
                    a.Estadio = new ENEstadio { Nombre = Convert.ToString(data["estadio"]) };
                    a.Equipo1 = new ENEquipo { Id_equipo = Convert.ToInt32(data["IdEquipo1"]) };
                    a.Equipo2 = new ENEquipo { Id_equipo = Convert.ToInt32(data["IdEquipo2"]) };
                    a.cot1 = Convert.ToDouble(data["Cot1"]);
                    a.cot2 = Convert.ToDouble(data["Cot2"]);
                    a.cotX = Convert.ToDouble(data["CotX"]);
                    lista.Add(a);
                }
                data.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Failure to read all Apuestas: " + ex.Message);
            }
            finally
            {
                c.Close();
            }
            return lista;
        }
    }
}