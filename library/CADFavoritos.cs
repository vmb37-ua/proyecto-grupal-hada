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
        public bool Create(ENFavoritos favoritos)
        {
            return true;
        }



        public bool Delete(ENFavoritos favoritos)
        {
            return true;

        }

        public List<ENFavoritos> ReadAll(ENFavoritos favoritos)
        {
            List<ENFavoritos> lista = new List<ENFavoritos>();

            try
            {
                using (SqlConnection conn = new SqlConnection(conexion))
                {
                    string query = @"
                SELECT f.idEquipo, e.nombre AS NombreEquipo, p.nombre AS NombreProvincia
                FROM Favoritos f
                JOIN Equipo e ON f.idEquipo = e.idEquipo
                JOIN Provincia p ON e.idProvincia = p.idProvincia
                WHERE f.idFavorito = @idFavorito";

                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@idFavorito", favoritos.IdFavorito);

                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        ENFavoritos fav = new ENFavoritos
                        {
                            IdFavorito = favoritos.IdFavorito,
                            IdEquipo = Convert.ToInt32(reader["idEquipo"]),
                            NombreEquipo = reader["NombreEquipo"].ToString(),
                            NombreProvincia = reader["NombreProvincia"].ToString()
                        };

                        lista.Add(fav);
                    }

                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return lista;
        }



    }
}