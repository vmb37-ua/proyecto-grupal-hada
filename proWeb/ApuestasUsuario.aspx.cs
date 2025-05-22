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

            var apuestasMostrar = new List<ApuestaFinal>();

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
                    //La cotización depende de la predicción
                    double cot=1;
                    if (string.Equals(apuestaUsuario.Prediccion, "1"))
                    {
                        cot = apuesta.cot1;
                    }
                    if (string.Equals(apuestaUsuario.Prediccion, "2"))
                    {
                        cot = apuesta.cot2;
                    }
                    if (string.Equals(apuestaUsuario.Prediccion, "X"))
                    {
                        cot = apuesta.cotX;
                    }


                    apuestasMostrar.Add(new ApuestaFinal
                        {
                            IdApuesta = apuesta.Id_apuesta,
                            EquipoLocal = equipoLocal.Nombre,
                            EquipoVisitante = equipoVisitante.Nombre,
                            Resultado_apuesta = apuesta.Resultado,
                            Resultado_predicho = apuestaUsuario.Prediccion,
                            Cotizacion = cot,
                            Estadio = estadio.Nombre,
                            Fecha = apuesta.Fecha
                        });
                }
            }

            GridViewApuestasUsuario.DataSource = apuestasMostrar;
            GridViewApuestasUsuario.DataBind();


        }

        public class ApuestaFinal
        {
            public int IdApuesta { get; set; }
            public string EquipoLocal { get; set; }
            public string EquipoVisitante { get; set; }
            public string Resultado_apuesta { get; set; } //1, 2 o X
            public string Resultado_predicho { get; set; } //1, 2 o X
            public double Cotizacion { get; set; }
            public string Estadio { get; set; }
            public DateTime Fecha { get; set; }

        }

        protected void GridViewJuegos_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                ApuestaFinal apuesta = (ApuestaFinal)e.Row.DataItem;
                //El color cambia conforme has ganado o perdido
                if (apuesta.Resultado_apuesta == apuesta.Resultado_predicho)
                    e.Row.BackColor = System.Drawing.Color.LightGreen;
                else 
                    e.Row.BackColor = System.Drawing.Color.LightCoral;
           

            }
        }
    }
}