using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace library
{
    class CADCategoria
    {
        private string constring { get; set; }

        public CADCategoria()
        {
            constring = ConfigurationManager.ConnectionStrings["miconex"].ToString();
        }

        public bool Create(ENCategoria categoria)
        {
            bool aux = true;
            SqlConnection c = new SqlConnection(constring);
            try
            {
                c.Open();
                SqlCommand com = new SqlCommand("INSERT INTO Categoria (Nombre) VALUES (@Nombre)", c);
                com.Parameters.AddWithValue("@Nombre", categoria.Nombre);
                com.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                Console.WriteLine("Failure to create Categoria: " + ex.Message);
                aux = false;
            }
            finally
            {
                c.Close();
            }
            return aux;
        }

        public bool Delete(ENCategoria categoria)
        {
            bool aux = true;
            SqlConnection c = new SqlConnection(constring);
            try
            {
                c.Open();
                SqlCommand com = new SqlCommand("DELETE FROM Categoria WHERE Nombre = @Nombre", c);
                com.Parameters.AddWithValue("@Nombre", categoria.Nombre);
                com.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Failure to delete Categoria: " + ex.Message);
                aux = false;
            }
            finally
            {
                c.Close();
            }
            return aux;
        }

        public bool Update(ENCategoria categoria)
        {
            bool aux = true;
            SqlConnection c = new SqlConnection(constring);
            try
            {
                c.Open();
                SqlCommand com = new SqlCommand("UPDATE Categoria SET Nombre = @NuevoNombre WHERE Nombre = @Nombre", c);
                com.Parameters.AddWithValue("@Nombre", categoria.Nombre);
                com.Parameters.AddWithValue("@NuevoNombre", categoria.Nombre); 
                int rows = com.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                Console.WriteLine("Failure to update Categoria: " + ex.Message);
                aux = false;
            }
            finally
            {
                c.Close();
            }
            return aux;
        }

        public bool Read(ENCategoria categoria)
        {
            bool aux = false;
            SqlConnection c = new SqlConnection(constring);
            try
            {
                c.Open();
                SqlCommand com = new SqlCommand("SELECT Nombre FROM Categoria WHERE Nombre = @Nombre", c);
                com.Parameters.AddWithValue("@Nombre", categoria.Nombre);
                SqlDataReader dr = com.ExecuteReader();
                if (dr.Read())
                {
                    categoria.Nombre = dr["Nombre"].ToString();
                    aux = true;
                }
                dr.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Failure to read Categoria: " + ex.Message);
            }
            finally
            {
                c.Close();
            }
            return aux;
        }

        public List<ENCategoria> ReadAll(ENCategoria categoria)
        {
            List<ENCategoria> lista = new List<ENCategoria>();
            SqlConnection c = new SqlConnection(constring);
            try
            {
                c.Open();
                SqlCommand com = new SqlCommand("SELECT Nombre FROM Categoria", c);
                SqlDataReader dr = com.ExecuteReader();
                while (dr.Read())
                {
                    ENCategoria cat = new ENCategoria();
                    cat.Nombre = dr["Nombre"].ToString();
                    lista.Add(cat);
                }
                dr.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Failure to read all Categorias: " + ex.Message);
            }
            finally
            {
                c.Close();
            }
            return lista;
        }
    }
}
