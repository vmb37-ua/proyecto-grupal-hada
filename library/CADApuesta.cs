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
    /// <summary>
    /// Clase de acceso a datos para Apuesta.
    /// </summary>
    public class CADApuesta
    {

        /// <summary>
        /// Cadena de conexión a la BD.
        /// </summary>
        private string constring { get; set; }
        /// <summary>
        /// Constructor por defecto
        /// </summary>
        public CADApuesta()
        {
            constring = ConfigurationManager.ConnectionStrings["miconex"].ToString();
        }
        /// <summary>
        /// Crea una nueva apuesta en la BD.
        /// </summary>
        /// <param name="apuesta"> ENApuesta con los datos.</param>
        /// <returns>True si la operación fue exitosa; si no, false.</returns>
        public bool Create(ENApuesta apuesta)
        {
            bool aux = true;
            SqlConnection c = new SqlConnection(constring);
            try
            {
                    c.Open();

                    SqlCommand check = new SqlCommand("SELECT COUNT(*) FROM Apuesta WHERE estadio = @Estadio AND fecha = @Fecha", c);
                    check.Parameters.AddWithValue("@Estadio", apuesta.Estadio.Nombre);
                    check.Parameters.AddWithValue("@Fecha", apuesta.Fecha);


                    int count = (int)check.ExecuteScalar();
                    if (count > 0)
                    {
                        throw new Exception("Ya existe una apuesta para ese estadio en esa fecha.");

                    }

              
                    SqlCommand com = new SqlCommand(
                        "INSERT INTO Apuesta (fecha, estadio, id_equipo1, id_equipo2, resultado, cot1, cot2, cotX) " +
                        "OUTPUT INSERTED.id_apuesta " +
                        "VALUES (@Fecha, @Estadio, @IdEquipo1, @IdEquipo2, @Resultado, @Cot1, @Cot2, @CotX)", c);

                    com.Parameters.AddWithValue("@Fecha", apuesta.Fecha);
                    com.Parameters.AddWithValue("@Estadio", apuesta.Estadio.Nombre);
                    com.Parameters.AddWithValue("@IdEquipo1", apuesta.Equipo1.Id_equipo);
                    com.Parameters.AddWithValue("@IdEquipo2", apuesta.Equipo2.Id_equipo);
                    com.Parameters.AddWithValue("@Resultado", string.IsNullOrEmpty(apuesta.Resultado) ? DBNull.Value : (object)apuesta.Resultado);
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
        /// <summary>
        /// Elimina una apuesta de la base de datos.
        /// </summary>
        /// <param name="apuesta">ENApuesta con el ID de la apuesta a eliminar.</param>
        /// <returns>True si se eliminó correctamente; si no, false.</returns>
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
        /// <summary>
        /// Lee una apuesta determinada desde la base de datos.
        /// </summary>
        /// <param name="apuesta">Objeto ENApuesta para leer su ID.</param>
        /// <returns>True si se encontró; si no, false.</returns>
        public bool Read(ENApuesta apuesta)
        {
            bool aux = false;
            SqlConnection c = new SqlConnection(constring);
            try
            {
                c.Open();
                SqlCommand com = new SqlCommand("SELECT fecha, estadio, id_equipo1, id_equipo2, resultado, cot1, cot2, cotX FROM Apuesta WHERE id_apuesta = @IdApuesta", c);
                com.Parameters.AddWithValue("@IdApuesta", apuesta.Id_apuesta);
                SqlDataReader data = com.ExecuteReader();
                if (data.Read())
                {
                    apuesta.Fecha = Convert.ToDateTime(data["fecha"]);
                    apuesta.Estadio = new ENEstadio { Nombre = Convert.ToString(data["estadio"]) };
                    apuesta.Equipo1 = new ENEquipo { Id_equipo = Convert.ToInt32(data["id_equipo1"]) };
                    apuesta.Equipo2 = new ENEquipo { Id_equipo = Convert.ToInt32(data["id_equipo2"]) };
                    apuesta.Resultado = Convert.ToString(data["resultado"]);
                    apuesta.cot1 = Convert.ToDouble(data["cot1"]);
                    apuesta.cot2 = Convert.ToDouble(data["cot2"]);
                    apuesta.cotX = Convert.ToDouble(data["cotX"]);
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
        /// <summary>
        /// Actualiza los datos de una apuesta.
        /// </summary>
        /// <param name="apuesta">Objeto ENApuesta con los datos actualizados.</param>
        /// <returns>True si se actualizó, si no, false.</returns>
        public bool Update(ENApuesta apuesta)
        {
            bool aux = true;
            SqlConnection c = new SqlConnection(constring);
            try
            {
                c.Open();
                SqlCommand com = new SqlCommand("UPDATE Apuesta SET fecha = @Fecha, estadio = @estadio, id_equipo1 = @IdEquipo1, " +
                                                "id_equipo2 = @IdEquipo2, resultado = @Resultado, cot1 = @Cot1,cot2 = @Cot2, cotX = @CotX WHERE id_apuesta = @IdApuesta", c);

                com.Parameters.AddWithValue("@Fecha", apuesta.Fecha);
                com.Parameters.AddWithValue("@estadio", apuesta.Estadio.Nombre);
                com.Parameters.AddWithValue("@IdEquipo1", apuesta.Equipo1.Id_equipo);
                com.Parameters.AddWithValue("@IdEquipo2", apuesta.Equipo2.Id_equipo);
                com.Parameters.AddWithValue("@Resultado", string.IsNullOrEmpty(apuesta.Resultado) ? DBNull.Value : (object)apuesta.Resultado);
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

        /// <summary>
        /// Lee todas las apuestas existentes en la BD.
        /// </summary>
        /// <returns>Lista de objetos ENApuesta con sus daros correspondientes.</returns>
        public List<ENApuesta> ReadAll()
        {
            List<ENApuesta> lista = new List<ENApuesta>();
            SqlConnection c = new SqlConnection(constring);
            try
            {
                c.Open();
                SqlCommand com = new SqlCommand("SELECT id_apuesta, fecha, estadio, id_equipo1, id_equipo2, resultado, cot1, cot2, cotX FROM Apuesta", c);
                SqlDataReader data = com.ExecuteReader();
                while (data.Read())
                {
                    ENApuesta a = new ENApuesta();
                    a.Id_apuesta = Convert.ToInt32(data["id_apuesta"]);
                    a.Fecha = Convert.ToDateTime(data["fecha"]);
                    a.Estadio = new ENEstadio { Nombre = Convert.ToString(data["estadio"]) };
                    a.Equipo1 = new ENEquipo { Id_equipo = Convert.ToInt32(data["id_equipo1"]) };
                    a.Equipo2 = new ENEquipo { Id_equipo = Convert.ToInt32(data["id_equipo2"]) };
                    a.Resultado = Convert.ToString(data["resultado"]);
                    a.cot1 = Convert.ToDouble(data["cot1"]);
                    a.cot2 = Convert.ToDouble(data["cot2"]);
                    a.cotX = Convert.ToDouble(data["cotX"]);
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