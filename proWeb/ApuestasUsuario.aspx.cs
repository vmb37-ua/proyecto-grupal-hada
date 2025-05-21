using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using library;

namespace ProWeb
{
    public partial class ApuestasUsuario : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarApuestasUsuario();
            }
        }

        private void CargarApuestasUsuario()
        {

            //Cojo todas las apuestas
            var apuestasUsuario = new ENApuesta_usuario().ReadAll();

            var apuestasMostrar = new List<Apuesta>();

            foreach (var apuestaUsuario in apuestasUsuario)
            {
                //Obtengo lo que haga falta de ENApuesta
                var apuesta = new ENApuesta { Id_apuesta = apuestaUsuario.IdApuesta };
                if (apuesta.Read())
                {
                    var equipoLocal = new ENEquipo { Id_equipo = apuesta.Equipo1.Id_equipo };
                    var equipoVisitante = new ENEquipo { Id_equipo = apuesta.Equipo2.Id_equipo };
                    equipoLocal.Read();
                    equipoVisitante.Read();

                    var estadio = new ENEstadio { Nombre = apuesta.Estadio.Nombre };
                    estadio.Read();

                    apuestasMostrar.Add(new Apuesta
                    {
                        IdApuesta = apuesta.Id_apuesta,
                        EquipoLocal = equipoLocal.Nombre,
                        EquipoVisitante = equipoVisitante.Nombre,
                        //Resultado_apuesta = apuestaUsuario.ResultadoApuesta,
                        Cotizacion = apuesta.cot1,
                        Estadio = estadio.Nombre,
                        Fecha = apuesta.Fecha
                    });
                }
            }

            GridViewApuestasUsuario.DataSource = apuestasMostrar;
            GridViewApuestasUsuario.DataBind();


        }

        public class Apuesta
        {
            public int IdApuesta { get; set; }
            public string EquipoLocal { get; set; }
            public string EquipoVisitante { get; set; }
            public string Resultado_apuesta { get; set; } //Ganada/Perdida
            public double Cotizacion { get; set; }
            public string Estadio { get; set; }
            public DateTime Fecha { get; set; }

        }

        protected void GridViewJuegos_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Apuesta apuesta = (Apuesta)e.Row.DataItem;

                if (apuesta.Resultado_apuesta == "Ganada")
                    e.Row.BackColor = System.Drawing.Color.LightGreen;
                else if (apuesta.Resultado_apuesta == "Perdida")
                    e.Row.BackColor = System.Drawing.Color.LightCoral;
                else
                    e.Row.BackColor = System.Drawing.Color.LightYellow;
            }
        }
    }
}