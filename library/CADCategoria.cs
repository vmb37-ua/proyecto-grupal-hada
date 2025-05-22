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
    /// <summary>
    /// Clase de acceso a datos para la entidad Categoría.
    /// </summary>
    class CADCategoria
    {
        private string constring { get; set; }
        /// <summary>
        /// Inicializa una nueva instancia de <see cref="CADCategoria"/>.
        /// </summary>
        /// <remarks>
        /// Consigue la cadena de conexión del archivo de configuración.
        /// </remarks>
        public CADCategoria()
        {
            constring = ConfigurationManager.ConnectionStrings["miconex"].ToString();
        }

        /// <summary>
        /// Crea una nueva categoría en la base de datos.
        /// </summary>
        /// <param name="categoria">Objeto <see cref="ENCategoria"/> con los datos.</param>
        /// <returns>
        /// <c>true</c> si la operación fue exitosa, <c>false</c> si no.
        /// </returns>
        /// <exception cref="SqlException">Error al ejecutar el comando SQL.</exception>
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
        /// <summary>
        /// Elimina una categoría.
        /// </summary>
        /// <param name="categoria">Objeto <see cref="ENCategoria"/> con los datos para eliminar.</param>
        /// <returns>
        /// <c>true</c> si la operación fue exitosa, <c>false</c> si no.
        /// </returns>
        /// <exception cref="SqlException">Error al ejecutar el comando SQL.</exception>
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

        /// <summary>
        /// Actualiza una categoría en la base de datos.
        /// </summary>
        /// <param name="categoria">Objeto <see cref="ENCategoria"/> con los datos a actualizar.</param>
        /// <returns>
        /// <c>true</c> si la operación fue exitosa, <c>false</c> si no.
        /// </returns>
        /// <exception cref="SqlException">Error al ejecutar el comando SQL.</exception>
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

        /// <summary>
        /// Coge los datos de una categoría desde la base de datos.
        /// </summary>
        /// <param name="categoria">Objeto <see cref="ENCategoria"/> donde se almacenarán los datos.</param>
        /// <returns>
        /// <c>true</c> si se encontró la categoría, <c>false</c> si no.
        /// </returns>
        /// <exception cref="SqlException">Error al ejecutar el comando SQL.</exception>
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

        /// <summary>
        /// Obtiene todas las categorías que haya en la base de datos.
        /// </summary>
        /// <returns>
        /// Lista de objetos <see cref="ENCategoria"/> con todas las categorías.
        /// </returns>
        /// <exception cref="SqlException">Error al ejecutar el comando SQL.</exception>
        public List<ENCategoria> ReadAll()
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
