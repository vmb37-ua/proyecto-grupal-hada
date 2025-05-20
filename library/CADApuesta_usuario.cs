using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace library
{
    public class CADApuesta_usuario
    {
        private string constring;

        public CADApuesta_usuario()
        {
            constring = ConfigurationManager.ConnectionStrings["miconex"].ConnectionString;
        }
        public bool CrearApuesta(ENApuesta_usuario apuesta)
        {
            string query = @"
            INSERT INTO apuesta_usuario 
            (id_usuario, id_apuesta, prediccion, cantidad, cuota, fecha)
            VALUES 
            (@idUsuario, @idApuesta, @prediccion, @cantidad, @cuota, GETDATE())";

            using (SqlConnection conexion = new SqlConnection(constring))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand(query, conexion);

                    cmd.Parameters.AddWithValue("@idUsuario", apuesta.IdUsuario);
                    cmd.Parameters.AddWithValue("@idApuesta", apuesta.IdApuesta);
                    cmd.Parameters.AddWithValue("@prediccion", apuesta.Prediccion);
                    cmd.Parameters.AddWithValue("@cantidad", apuesta.Cantidad);
                    cmd.Parameters.AddWithValue("@cuota", apuesta.Cuota);

                    conexion.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
                catch (SqlException ex)
                {
                    
                    System.Diagnostics.Debug.WriteLine($"SQL Error creando apuesta: {ex.Number} - {ex.Message}");
                    return false;
                }
            }
        }

        public List<ENApuesta_usuario> ReadAll()
        {
            var apuestas_usuario = new List<ENApuesta_usuario>();

            using (var c = new SqlConnection(constring))
            {
                try
                {
                    c.Open();
                    string sql = "SELECT id_usuario, id_apuesta, prediccion, cantidad, cuota FROM ApuestaUsuario";

                    using (var cmd = new SqlCommand(sql, c))
                    using (var dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            ENApuesta_usuario apuesta = new ENApuesta_usuario();
                            apuesta.IdApuesta = Convert.ToInt32(dr["id_apuesta"]);
                            apuesta.IdUsuario = Convert.ToInt32(dr["id_usuario"]);
                            apuesta.Prediccion = Convert.ToString(dr["prediccion"]);
                            apuesta.Cantidad = Convert.ToDecimal(dr["cantidad"]);
                            apuesta.Cuota = Convert.ToDecimal(dr["cuota"]);

                            apuestas_usuario.Add(apuesta);
                        }
                    }
                }
                catch (SqlException ex)
                {
                    Console.WriteLine($"Error al leer apuestas_usuario: {ex.Message}");
                }
            }

            return apuestas_usuario;
        }
    }
}
