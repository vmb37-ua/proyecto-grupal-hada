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
    /// Clase que representa la capa de acceso a datos de patrocinador.
    /// Maneja las diferentes operaciones de la BD.
    /// </summary>
    public class CADPatrocinador
    {
        private string constring { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        public CADPatrocinador()
        {
            constring = ConfigurationManager.ConnectionStrings["miconex"].ToString();
        }
        public bool Create(ENPatrocinador patrocinador)
        {
            bool result = true;
            SqlConnection c = new SqlConnection(constring);
            try
            {
                c.Open();
                SqlCommand com = new SqlCommand("INSERT INTO patrocinador (id_patrocinador, dinero, texto) VALUES (@Id, @Dinero, @Texto)", c);
                com.Parameters.AddWithValue("@Id", patrocinador.Id_patrocinador);
                com.Parameters.AddWithValue("@Dinero", patrocinador.Dinero);
                com.Parameters.AddWithValue("@Texto", patrocinador.Texto);
                com.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                Console.WriteLine("Patrocinador operation has failed. Error: {0}", ex.Message);
                result = false;
            }
            finally
            {
                c.Close();
            }
            return result;
        }
        public bool Update(ENPatrocinador patrocinador)
        {
            bool result = true;
            SqlConnection c = new SqlConnection(constring);
            try
            {
                c.Open();
                SqlCommand com = new SqlCommand("UPDATE patrocinador SET dinero=@Dinero, texto=@Texto WHERE id_patrocinador=@Id", c);
                com.Parameters.AddWithValue("@Id", patrocinador.Id_patrocinador);
                com.Parameters.AddWithValue("@Dinero", patrocinador.Dinero);
                com.Parameters.AddWithValue("@Texto", patrocinador.Texto);
                com.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Patrocinador Update failed. Error: {0}", ex.Message);
                result = false;
            }
            finally
            {
                c.Close();
            }
            return result;
        }
        public bool Delete(ENPatrocinador patrocinador)
        {
            bool result = true;
            SqlConnection c = new SqlConnection(constring);
            try
            {
                c.Open();
                SqlCommand com = new SqlCommand("DELETE FROM patrocinador WHERE id_patrocinador=@Id", c);
                com.Parameters.AddWithValue("@Id", patrocinador.Id_patrocinador);
                com.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Patrocinador Delete failed. Error: {0}", ex.Message);
                result = false;
            }
            finally
            {
                c.Close();
            }
            return result;
        }
        public bool Read(ENPatrocinador patrocinador)
        {
            bool result = true;
            SqlConnection c = new SqlConnection(constring);
            try
            {
                c.Open();
                SqlCommand com = new SqlCommand("SELECT * FROM patrocinador WHERE id_patrocinador=@Id", c);
                com.Parameters.AddWithValue("@Id", patrocinador.Id_patrocinador);
                SqlDataReader dr = com.ExecuteReader();
                if (dr.Read())
                {
                    patrocinador.Dinero = float.Parse(dr["dinero"].ToString());
                    patrocinador.Texto = dr["texto"].ToString();
                }
                else
                {
                    result = false;
                }
                dr.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Patrocinador Read failed. Error: {0}", ex.Message);
                result = false;
            }
            finally
            {
                c.Close();
            }
            return result;
        }
        public bool ReadFirst(ENPatrocinador patrocinador)
        {
            bool result = true;
            SqlConnection c = new SqlConnection(constring);
            try
            {
                c.Open();
                SqlCommand com = new SqlCommand("SELECT TOP 1 * FROM patrocinador ORDER BY id_patrocinador ASC", c);
                SqlDataReader dr = com.ExecuteReader();
                if (dr.Read())
                {
                    patrocinador.Id_patrocinador = int.Parse(dr["id_patrocinador"].ToString());
                    patrocinador.Dinero = float.Parse(dr["dinero"].ToString());
                    patrocinador.Texto = dr["texto"].ToString();
                }
                else
                {
                    result = false;
                }
                dr.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("ReadFirst failed. Error: {0}", ex.Message);
                result = false;
            }
            finally
            {
                c.Close();
            }
            return result;
        }
        public bool ReadNext(ENPatrocinador patrocinador)
        {
            bool result = true;
            SqlConnection c = new SqlConnection(constring);
            try
            {
                c.Open();
                SqlCommand com = new SqlCommand("SELECT TOP 1 * FROM patrocinador WHERE id_patrocinador > @Id ORDER BY id_patrocinador ASC", c);
                com.Parameters.AddWithValue("@Id", patrocinador.Id_patrocinador);
                SqlDataReader dr = com.ExecuteReader();
                if (dr.Read())
                {
                    patrocinador.Id_patrocinador = int.Parse(dr["id_patrocinador"].ToString());
                    patrocinador.Dinero = float.Parse(dr["dinero"].ToString());
                    patrocinador.Texto = dr["texto"].ToString();
                }
                else
                {
                    result = false;
                }
                dr.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("ReadNext failed. Error: {0}", ex.Message);
                result = false;
            }
            finally
            {
                c.Close();
            }
            return result;
        }
        public bool ReadPrev(ENPatrocinador patrocinador)
        {
            bool result = true;
            SqlConnection c = new SqlConnection(constring);
            try
            {
                c.Open();
                SqlCommand com = new SqlCommand("SELECT TOP 1 * FROM patrocinador WHERE id_patrocinador < @Id ORDER BY id_patrocinador DESC", c);
                com.Parameters.AddWithValue("@Id", patrocinador.Id_patrocinador);
                SqlDataReader dr = com.ExecuteReader();
                if (dr.Read())
                {
                    patrocinador.Id_patrocinador = int.Parse(dr["id_patrocinador"].ToString());
                    patrocinador.Dinero = float.Parse(dr["dinero"].ToString());
                    patrocinador.Texto = dr["texto"].ToString(); 
                }
                else
                {
                    result = false;
                }
                dr.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("ReadPrev failed. Error: {0}", ex.Message);
                result = false;
            }
            finally
            {
                c.Close();
            }
            return result;
        }
        public List<ENPatrocinador> ReadAll(ENPatrocinador patrocinador)
        {
            List<ENPatrocinador> lista = new List<ENPatrocinador>();
            SqlConnection c = new SqlConnection(constring);
            try
            {
                c.Open();
                SqlCommand com = new SqlCommand("SELECT * FROM patrocinador", c);
                SqlDataReader dr = com.ExecuteReader();
                while (dr.Read())
                {
                    ENPatrocinador p = new ENPatrocinador
                    {
                        Id_patrocinador = int.Parse(dr["id_patrocinador"].ToString()),
                        Dinero = float.Parse(dr["dinero"].ToString()),
                        Texto = dr["texto"].ToString()
                    };
                    lista.Add(p);
                }
                dr.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Patrocinador ReadAll failed. Error: {0}", ex.Message);
            }
            finally
            {
                c.Close();
            }
            return lista;
        }
    }
}