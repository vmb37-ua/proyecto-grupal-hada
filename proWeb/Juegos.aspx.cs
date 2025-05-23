

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
    /// <summary>
    /// Página que muestra la lista de juegos que hay según la base de datos.
    /// Incluye botón de apostar en cada juego.
    /// </summary>
    public partial class Juegos : System.Web.UI.Page
    {
        /// <summary>
        /// Evento que se ejecuta al cargarse la página, si no es un postback, se cargan los juegos desde la base de datos.
        /// </summary>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarJuegosDesdeBD();
            }
        }

        /// <summary>
        /// Evento que se ejecuta cuando pasa un tiempo determinado por el aspx y recarga los juegos
        /// </summary>
        protected void timer1_Tick(object sender, EventArgs e)
        {
            CargarJuegosDesdeBD();
        }
        /// <summary>
        /// Carga todas las apuestas desde la base de datos, y las transforma en una lista de objetos Juego para mostrarlos
        /// </summary>
        private void CargarJuegosDesdeBD()
        {
            try
            {   //Ordenamos por fecha
                var cadApuesta = new CADApuesta();
                var apuestas = cadApuesta.ReadAll();
                apuestas = apuestas.OrderBy(a => a.Fecha).ToList();

                var juegos = new List<Juego>();

                foreach (var apuesta in apuestas)
                {   //Usamos el ID y el nombre para obtener el resto de datos de las entidades
                    var equipoLocal = new ENEquipo { Id_equipo = apuesta.Equipo1.Id_equipo };
                    var equipoVisitante = new ENEquipo { Id_equipo = apuesta.Equipo2.Id_equipo };

                    var estadio = new ENEstadio { Nombre = apuesta.Estadio.Nombre };

                    bool leyoLocal = equipoLocal.Read();
                    bool leyoVisitante = equipoVisitante.Read();
                    bool leyoEstadio = estadio.Read();

                        juegos.Add(new Juego
                        {
                            EquipoLocal = equipoLocal.Nombre,
                            EquipoVisitante = equipoVisitante.Nombre,
                            EscudoVisitante = equipoVisitante.Escudo,
                            EscudoLocal = equipoLocal.Escudo,
                            Estadio = estadio.Nombre,
                            Fecha = apuesta.Fecha.Date,
                            IdApuesta=apuesta.Id_apuesta,
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

        /// <summary>
        /// Clase que representa un partido para mostrarlo
        /// </summary>
        public class Juego
        {
            public string EquipoLocal { get; set; }
            public string EquipoVisitante { get; set; }
            public string EscudoVisitante { get; set; }
            public string EscudoLocal { get; set; }
            public int IdApuesta { get; set; }
            public string Estadio { get; set; }
            public DateTime Fecha { get; set; }
            public string Categoria { get; set; }
       };

        /// <summary>
        /// Evento que se lanza cuando el usuario hace clic en el botón "Apostar" de un juego y
        /// redirige a la página ApuestaUsuario.aspx mandando además el ID de la apuesta
        /// </summary>
        protected void EventoJuegoClick(object sender, CommandEventArgs e)
        {

            if (Session["Login"] == null)
            {
                Response.Redirect("Login.aspx");
                return;
            }

            if (e.CommandName == "Apostar")
            {  
                string idApuesta = e.CommandArgument.ToString();
                Response.Redirect($"ApuestaUsuario.aspx?idApuesta={idApuesta}");
            }

        }
    }

}