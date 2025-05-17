using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace library
{
    public class CADFavoritos
    {
        private string conexion;

        public CADFavoritos()
        {
            conexion = ConfigurationManager.ConnectionStrings["miconex"].ToString();
        }

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
