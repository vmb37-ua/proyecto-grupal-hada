using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;

namespace ProWeb
{
    /// <summary>
    /// Página que muestra estadísticas de apuestas:
    /// - Los 5 partidos con más apuestas.
    /// - El partido con la cuota más alta.
    /// - Los 5 usuarios que más han ganado.
    /// </summary>
    public partial class WebForm3 : Page
    {
        /// <summary>
        /// Evento que se ejecuta al cargar la página.
        /// Consulta la base de datos y muestra:
        /// - Top 5 partidos más apostados.
        /// - Partido con la cuota más alta.
        /// - Top 5 ganadores por ganancias.
        /// </summary>
        /// <param name="sender">Objeto que genera el evento (la página).</param>
        /// <param name="e">Argumentos del evento de carga.</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            //Solo se ejecuta la primera vez que se carga la página (no en postbacks)
            if (!IsPostBack)
            {
                //Obtiene la cadena de conexión desde Web.config
                string connStr = ConfigurationManager.ConnectionStrings["miconex"].ToString();

                /// ================== TOP 5 PARTIDOS MÁS APOSTADOS ==================
                List<object> topPartidos = new List<object>(); //Lista para almacenar los resultados

                //Abre conexión a la base de datos
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    //Consulta que devuelve los 5 partidos más apostados
                    string query = @"
                        SELECT TOP 5 CONCAT(e1.nombre, ' vs ', e2.nombre) AS Partido,
                               COUNT(*) AS TotalApuestas
                        FROM apuesta_usu au
                        JOIN apuesta a ON au.id_apuesta = a.id_apuesta
                        JOIN equipo e1 ON a.id_equipo1 = e1.id_equipo
                        JOIN equipo e2 ON a.id_equipo2 = e2.id_equipo
                        GROUP BY e1.nombre, e2.nombre
                        ORDER BY TotalApuestas DESC";

                    SqlCommand cmd = new SqlCommand(query, conn);
                    conn.Open(); //Abre la conexión

                    SqlDataReader reader = cmd.ExecuteReader(); //Ejecuta la consulta

                    int pos = 1; //Contador de posición
                    while (reader.Read())
                    {
                        //Añade el resultado a la lista con posición, nombres de equipos y total de apuestas
                        topPartidos.Add(new
                        {
                            Posicion = pos++,
                            Partido = reader["Partido"].ToString(),
                            Apuestas = Convert.ToInt32(reader["TotalApuestas"])
                        });
                    }

                    reader.Close(); //Cierra el lector
                }

                //Muestra los datos en el GridView
                gvTopPartidos.DataSource = topPartidos;
                gvTopPartidos.DataBind();


                /// ================== PARTIDO CON MAYOR CUOTA ==================
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    //Consulta que obtiene el partido con la mayor cuota
                    string query = @"
                        SELECT TOP 1 CONCAT(e1.nombre, ' vs ', e2.nombre) AS Partido, MAX(au.cuota) AS Cuota
                        FROM apuesta_usu au
                        JOIN apuesta a ON au.id_apuesta = a.id_apuesta
                        JOIN equipo e1 ON a.id_equipo1 = e1.id_equipo
                        JOIN equipo e2 ON a.id_equipo2 = e2.id_equipo
                        GROUP BY e1.nombre, e2.nombre
                        ORDER BY Cuota DESC";

                    SqlCommand cmd = new SqlCommand(query, conn);
                    conn.Open();

                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        //Muestra el partido con mayor cuota en un Label
                        lblMayorCuota.Text = $"{reader["Partido"]} - Cuota: {Convert.ToDecimal(reader["Cuota"]):0.00}";
                    }
                    reader.Close();
                }

                /// ================== TOP 5 GANADORES ==================
                blTopGanadores.Items.Clear(); //Limpia la lista visual antes de llenarla

                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    //Consulta para obtener los 5 usuarios con mayores ganancias
                    string query = @"
                        SELECT TOP 5 u.nombre, SUM(au.cuota * au.dinero_apostado) AS Ganancia
                        FROM apuesta_usu au
                        JOIN usuario u ON au.id_usuario = u.id
                        GROUP BY u.nombre
                        ORDER BY Ganancia DESC";

                    SqlCommand cmd = new SqlCommand(query, conn);
                    conn.Open();

                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        //Formatea el nombre y la ganancia y los añade a la lista
                        string texto = $"{reader["nombre"]} - {Convert.ToDecimal(reader["Ganancia"]):N0}€";
                        blTopGanadores.Items.Add(texto);
                    }

                    reader.Close();
                }
            }
        }
    }
}
