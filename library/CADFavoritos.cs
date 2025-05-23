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
    /// Clase encargada de manejar la interacción con la base de datos
    /// para operaciones relacionadas con los favoritos.
    /// </summary>
    public class CADFavoritos
    {
        private string conexion;

        public CADFavoritos()
        {
            conexion = ConfigurationManager.ConnectionStrings["miconex"].ToString();
        }
        /// <summary>
        /// Añade un nuevo favorito a la base de datos.
        /// </summary>
        /// <param name="fav">Objeto ENFavoritos con los datos del usuario y equipo.</param>
        /// <returns><c>true</c> si la operación fue exitosa, <c>false</c> en caso contrario.</returns>
        public bool Create(ENFavoritos fav)
        {
            bool creado = false;

            try
            {
                using (SqlConnection conn = new SqlConnection(conexion))
                {
                    string query = "INSERT INTO favoritos (id_usuario, id_equipo) VALUES (@idUsuario, @idEquipo)";

                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@idUsuario", fav.IdUsuario);
                    cmd.Parameters.AddWithValue("@idEquipo", fav.IdEquipo);

                    conn.Open();
                    int filas = cmd.ExecuteNonQuery();

                    creado = filas > 0;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al crear favorito: " + ex.Message);
            }

            return creado;
        }

        /// <summary>
        /// Elimina un favorito de la base de datos.
        /// </summary>
        /// <param name="fav">Objeto ENFavoritos con el ID del favorito a eliminar.</param>
        /// <returns><c>true</c> si se eliminó correctamente, <c>false</c> si ocurrió algún error.</returns>
        public bool Delete(ENFavoritos fav)
        {
            bool eliminado = false;

            try
            {
                using (SqlConnection conn = new SqlConnection(conexion))
                {
                    string query = "DELETE FROM favoritos WHERE id = @idFavorito";

                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@idFavorito", fav.IdFavorito);

                    conn.Open();
                    int filas = cmd.ExecuteNonQuery();

                    eliminado = filas > 0;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al eliminar favorito: " + ex.Message);
            }

            return eliminado;
        }
        /// <summary>
        /// Obtiene todos los favoritos de un usuario específico.
        /// </summary>
        /// <param name="idUsuario">Identificador del usuario cuyos favoritos se quieren consultar.</param>
        /// <returns>Lista con las instancias de <c>ENFavoritos</c> asociadas a ese usuario.</returns>

        public List<ENFavoritos> ReadAllUsuario(int idUsuario)
        {
            List<ENFavoritos> lista = new List<ENFavoritos>();

            try
            {
                using (SqlConnection conn = new SqlConnection(conexion))
                {
                    string query = @"
                SELECT f.id AS IdFavorito, e.id_equipo AS IdEquipo, e.nombre AS NombreEquipo, e.escudo AS Escudo, e.categoria AS Categoria
                FROM favoritos f
                JOIN equipo e ON f.id_equipo = e.id_equipo
                WHERE f.id_usuario = @idUsuario";

                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@idUsuario", idUsuario);

                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        ENFavoritos fav = new ENFavoritos
                        {
                            IdFavorito = int.Parse(reader["IdFavorito"].ToString()),
                            IdEquipo = int.Parse(reader["IdEquipo"].ToString()),
                            NombreEquipo = reader["NombreEquipo"].ToString(),
                            Escudo = reader["Escudo"].ToString(),
                            Categoria = reader["Categoria"].ToString()
                        };

                        lista.Add(fav);
                    }

                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al leer favoritos: " + ex.Message);
            }

            return lista;
        }
    }
}
