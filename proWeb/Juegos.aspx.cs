

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
        protected void timer1_Tick(object sender, EventArgs e)
        {
            CargarJuegosDesdeBD();
        }

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
                            EquipoVisitante = equipoLocal.Nombre,
                            Estadio = estadio.Nombre,
                            Fecha = apuesta.Fecha.Date,
                            Hora = apuesta.Fecha.TimeOfDay,
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
        //Creamos clase de Juego para almacenar los datos
        public class Juego
        {
            public string EquipoLocal { get; set; }
            public string EquipoVisitante { get; set; }
            public int IdApuesta { get; set; }
            public string Estadio { get; set; }
            public DateTime Fecha { get; set; }
            public TimeSpan Hora { get; set; }
            public string Categoria { get; set; }
       };

        protected void EventoJuegoClick(object sender, CommandEventArgs e)
        {

            if (e.CommandName == "Apostar")
            {   //Mandamos el ID de la apuesta en la ULR para que la otra página pueda identificarla
                string idApuesta = e.CommandArgument.ToString();
                Response.Redirect($"ApuestaUsuario.aspx?idApuesta={idApuesta}");
            }

        }
    }

}