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
    /// Clase de acceso a datos para la entidad Apuesta_usuario.
    /// </summary>
    public class CADApuesta_usuario
    {
        private string constring;

        /// <summary>
        /// Constructor por defecto.
        /// Inicializa la cadena de conexión para la base de datos.
        /// </summary>
        public CADApuesta_usuario()
        {
            constring = ConfigurationManager.ConnectionStrings["miconex"].ConnectionString;
        }

        /// <summary>
        /// Método que crea un apuesta de un usuario en la base de datos.
        /// </summary>
        /// <param name="apuesta">Objeto entidad Apuesta_usuario que contiene todos los datos de la apuesta a crear en la base de datos.</param>
        /// <returns>True si la creación es exitosa. False si hubo alguna excepción o no se pudo insertar.</returns>
        public bool CrearApuesta(ENApuesta_usuario apuesta)
        {
            string query = @"
            INSERT INTO apuesta_usu
            (id_usuario, id_apuesta, prediccion, dinero_apostado, cuota)
            VALUES 
            (@idUsuario, @idApuesta, @prediccion, @cantidad, @cuota)";

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

        /// <summary>
        /// Método que lee todos los municipios de la base de datos de una misma provincia.
        /// </summary>
        /// <returns>Lista de entidades Apuesta_usuario leídas de la base de datos.</returns>
        public List<ENApuesta_usuario> ReadAll()
        {
            var apuestas_usuario = new List<ENApuesta_usuario>();

            using (var c = new SqlConnection(constring))
            {
                try
                {
                    c.Open();
                    string sql = "SELECT id_usuario, id_apuesta, prediccion, cantidad, cuota FROM apuesta_usu";

                    using (var cmd = new SqlCommand(sql, c))
                    using (var dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            ENApuesta_usuario apuesta = new ENApuesta_usuario();
                            apuesta.IdApuesta = Convert.ToInt32(dr["id_apuesta"]);
                            apuesta.IdUsuario = Convert.ToInt32(dr["id_usuario"]);
                            apuesta.Prediccion = Convert.ToString(dr["prediccion"]);
                            apuesta.Cantidad = (float)(dr["cantidad"]);
                            apuesta.Cuota = (float)(dr["cuota"]);

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
