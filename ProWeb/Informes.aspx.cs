using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.UI;

namespace ProWeb
{
    /// <summary>
    /// Página que genera informes estadísticos sobre apuestas, usuarios y eventos.
    /// Incluye:
    /// - Partidos más apostados
    /// - Mayor cuota
    /// - Usuarios con más ganancias
    /// - Ganancias totales
    /// - Próximo evento
    /// </summary>
    public partial class WebForm3 : Page
    {
        /// <summary>
        /// Evento que se ejecuta al cargar la página.
        /// Solo se ejecuta si no es un postback.
        /// Realiza todas las consultas necesarias para mostrar los informes.
        /// </summary>
        protected void Page_Load(object sender, EventArgs e)
        {
            //Verifica si es la primera carga de la página
            if (!IsPostBack)
            {
                //Obtiene la cadena de conexión desde Web.config
                string connStr = ConfigurationManager.ConnectionStrings["miconex"].ToString();

                try
                {
                    //Abre una conexión a la base de datos
                    using (SqlConnection conn = new SqlConnection(connStr))
                    {
                        conn.Open();

                        //Top 5 partidos más apostados
                        List<object> topPartidos = new List<object>();

                        string queryPartidos = @"
                            SELECT TOP 5 CONCAT(e1.nombre, ' vs ', e2.nombre) AS Partido,
                                   COUNT(*) AS TotalApuestas
                            FROM apuesta_usu au
                            JOIN apuesta a ON au.id_apuesta = a.id_apuesta
                            JOIN equipo e1 ON a.id_equipo1 = e1.id_equipo
                            JOIN equipo e2 ON a.id_equipo2 = e2.id_equipo
                            GROUP BY a.id_apuesta, e1.nombre, e2.nombre
                            ORDER BY TotalApuestas DESC";

                        using (SqlCommand cmdPartidos = new SqlCommand(queryPartidos, conn))
                        using (SqlDataReader reader = cmdPartidos.ExecuteReader())
                        {
                            int pos = 1;

                            //Lee cada partido y lo agrega a la lista
                            while (reader.Read())
                            {
                                topPartidos.Add(new
                                {
                                    Posicion = pos++,
                                    Partido = reader["Partido"].ToString(),
                                    Apuestas = Convert.ToInt32(reader["TotalApuestas"])
                                });
                            }
                        }

                        //Muestra los datos en el GridView
                        gvTopPartidos.DataSource = topPartidos;
                        gvTopPartidos.DataBind();

                        //Partido con mayor cuota
                        string queryCuota = @"
                            SELECT TOP 1 CONCAT(e1.nombre, ' vs ', e2.nombre) AS Partido, MAX(au.cuota) AS Cuota
                            FROM apuesta_usu au
                            JOIN apuesta a ON au.id_apuesta = a.id_apuesta
                            JOIN equipo e1 ON a.id_equipo1 = e1.id_equipo
                            JOIN equipo e2 ON a.id_equipo2 = e2.id_equipo
                            GROUP BY e1.nombre, e2.nombre
                            ORDER BY Cuota DESC";

                        using (SqlCommand cmdCuota = new SqlCommand(queryCuota, conn))
                        using (SqlDataReader reader = cmdCuota.ExecuteReader())
                        {
                            //Muestra el partido con la mayor cuota
                            if (reader.Read())
                            {
                                float cuota = Convert.ToSingle(reader["Cuota"]);
                                string partido = reader["Partido"].ToString();
                                lblMayorCuota.Text = partido + " - Cuota: " + cuota.ToString("0.00");
                            }
                        }

                        //Top 5 usuarios con más ganancias netas
                        blTopGanadores.Items.Clear();

                        string queryGanadores = @"
                            SELECT TOP 5 u.nombre, 
                                SUM((au.cuota * au.dinero_apostado) - au.dinero_apostado) AS GananciaNeta
                            FROM apuesta_usu au
                            JOIN usuario u ON au.id_usuario = u.id
                            JOIN apuesta a ON au.id_apuesta = a.id_apuesta
                            WHERE au.prediccion = a.resultado
                            GROUP BY u.nombre
                            ORDER BY GananciaNeta DESC";

                        using (SqlCommand cmdGanadores = new SqlCommand(queryGanadores, conn))
                        using (SqlDataReader reader = cmdGanadores.ExecuteReader())
                        {
                            //Muestra cada usuario y su ganancia
                            while (reader.Read())
                            {
                                string nombre = reader["nombre"].ToString();
                                float ganancia = Convert.ToSingle(reader["GananciaNeta"]);
                                blTopGanadores.Items.Add(nombre + " - " + ganancia.ToString("N0") + "€");
                            }
                        }

                        //Ganancias totales de todos los usuarios (MODIFICADO A GANANCIA NETA)
                        using (SqlCommand cmdGanancias = new SqlCommand(@"
                            SELECT SUM((cuota * dinero_apostado) - dinero_apostado)
                            FROM apuesta_usu au
                            JOIN apuesta a ON au.id_apuesta = a.id_apuesta
                            WHERE au.prediccion = a.resultado", conn))
                        {
                            object totalGan = cmdGanancias.ExecuteScalar();

                            //Muestra total si hay datos, o 0€ si no hay
                            if (totalGan != DBNull.Value && totalGan != null)
                            {
                                float total = Convert.ToSingle(totalGan);
                                lblGanancias.Text = total.ToString("N0") + "€";
                            }
                            else
                            {
                                lblGanancias.Text = "0€";
                            }
                        }

                        //Próximo evento (partido futuro más cercano)
                        using (SqlCommand cmdProx = new SqlCommand(@"
                            SELECT TOP 1 CONCAT(e1.nombre, ' vs ', e2.nombre)
                            FROM apuesta a
                            JOIN equipo e1 ON a.id_equipo1 = e1.id_equipo
                            JOIN equipo e2 ON a.id_equipo2 = e2.id_equipo
                            WHERE a.fecha > GETDATE()
                            ORDER BY a.fecha ASC", conn))
                        {
                            object proxEvento = cmdProx.ExecuteScalar();

                            if (proxEvento != null && proxEvento != DBNull.Value)
                            {
                                lblProximoEvento.Text = proxEvento.ToString();
                            }
                            else
                            {
                                lblProximoEvento.Text = "No hay eventos programados";
                            }
                        }
                    }
                }
                catch (Exception)
                {
                    //Si ocurre un error, muestra mensajes de error en todos los controles
                    lblProximoEvento.Text = "Error al cargar informes";
                    lblGanancias.Text = "Error";
                    lblMayorCuota.Text = "Error";
                    blTopGanadores.Items.Clear();
                    blTopGanadores.Items.Add("Error al obtener datos");
                    gvTopPartidos.DataSource = null;
                    gvTopPartidos.DataBind();
                }
            }
        }
    }
}
