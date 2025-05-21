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
    public partial class WebForm3 : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string connStr = ConfigurationManager.ConnectionStrings["miconex"].ToString();

                List<object> topPartidos = new List<object>();
                using (SqlConnection conn = new SqlConnection(connStr))
                {
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
                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    int pos = 1;
                    while (reader.Read())
                    {
                        topPartidos.Add(new
                        {
                            Posicion = pos++,
                            Partido = reader["Partido"].ToString(),
                            Apuestas = Convert.ToInt32(reader["TotalApuestas"])
                        });
                    }
                    reader.Close();
                }
                gvTopPartidos.DataSource = topPartidos;
                gvTopPartidos.DataBind();

                using (SqlConnection conn = new SqlConnection(connStr))
                {
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
                        lblMayorCuota.Text = $"{reader["Partido"]} - Cuota: {Convert.ToDecimal(reader["Cuota"]):0.00}";
                    }
                    reader.Close();
                }

                blTopGanadores.Items.Clear();
                using (SqlConnection conn = new SqlConnection(connStr))
                {
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
                        string texto = $"{reader["nombre"]} - {Convert.ToDecimal(reader["Ganancia"]):N0}€";
                        blTopGanadores.Items.Add(texto);
                    }
                    reader.Close();
                }
            }
        }
    }
}