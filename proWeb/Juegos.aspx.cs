using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using library;
using static ProWeb.ApuestasUsuario;

namespace ProWeb
{
	public partial class Juegos : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
            if (!IsPostBack)
            {
                CargarJuegosDesdeBD();
            }
        }

        private void CargarJuegosDesdeBD()
        {
            try
            {
                var cadApuesta = new CADApuesta();
                var apuestas = cadApuesta.ReadAll(new ENApuesta());
                apuestas = apuestas.OrderBy(a => a.Fecha).ToList();

                var juegos = new List<Juego>();

                foreach (var apuesta in apuestas)
                {
                    var equipoLocal = new ENEquipo { Id_equipo = apuesta.Equipo1.Id_equipo };
                    var equipoVisitante = new ENEquipo { Id_equipo = apuesta.Equipo2.Id_equipo };

                    var estadio = new ENEstadio { Id_estadio = apuesta.Estadio.Id_estadio };

                    if (equipoLocal.Read() && equipoVisitante.Read() && estadio.Read())
                    {
                        juegos.Add(new Juego
                        {
                            EquipoLocal = equipoLocal.Nombre,
                            EquipoVisitante = equipoVisitante.Nombre,
                            Estadio = estadio.Nombre,
                            Fecha = apuesta.Fecha.Date,
                            Hora = apuesta.Fecha.TimeOfDay,
                        });
                    }
                }

                //Placeholder
                if (juegos.Count == 0)
                {
                    juegos.Add(new Juego
                    {
                        EquipoLocal = "Real Madrid",
                        EquipoVisitante = "Barcelona",
                        Estadio = "Santiago Bernabéu",
                        Fecha = DateTime.Today,
                        Hora = new TimeSpan(21, 0, 0),
                    });
                    juegos.Add(new Juego
                    {
                        EquipoLocal = "Manchester City",
                        EquipoVisitante = "Liverpool",
                        Estadio = "Mestalla",
                        Fecha = DateTime.Today,
                        Hora = new TimeSpan(21, 0, 0),
                    });
                }

                rptJuegos.DataSource = juegos;
                rptJuegos.DataBind();
            }
            catch (Exception ex)
            {
                Response.Write($"<script>alert('Error al cargar los partidos: {ex.Message}');</script>");
            }
        }

        public class Juego
        {
            public string EquipoLocal { get; set; }
            public string EquipoVisitante { get; set; }
            public string Estadio { get; set; }
            public DateTime Fecha { get; set; }
            public TimeSpan Hora { get; set; }
            public string Categoria { get; set; }

        };

        protected void EventoJuegoClick(object sender, EventArgs e)
        {
            Response.Redirect("ApuestaUsuario.aspx");
        }
    }
    
}