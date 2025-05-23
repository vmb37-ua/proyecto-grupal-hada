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



        /// <summary>
        /// Evento que pasa al cargar la página.
        /// Si no es postback, carga las apuestas del usuario.
        /// </summary>
        protected void Page_Load(object sender, EventArgs e)
        {
  

            if (!IsPostBack)
            {
                CargarApuestasUsuario();
            }
        }

        /// <summary>
        /// Carga todas las apuestas realizadas por el usuario y las muestra en el GridView.
        /// Para cada apuesta, consigue datos como equipos, estadio, predicción y cotización.
        /// </summary>
        private void CargarApuestasUsuario()
        {

            var apuestasUsuario = new ENApuesta_usuario().ReadAll();

            var apuestasMostrar = new List<ApuestaFinal>();

            foreach (var apuestaUsuario in apuestasUsuario)
            {
                var apuesta = new ENApuesta { Id_apuesta = apuestaUsuario.IdApuesta };

                apuesta.Read();
                    var equipoLocal = new ENEquipo { Id_equipo = apuesta.Equipo1.Id_equipo };
                    var equipoVisitante = new ENEquipo { Id_equipo = apuesta.Equipo2.Id_equipo };
                    equipoLocal.Read();
                    equipoVisitante.Read();

                    var estadio = new ENEstadio { Nombre = apuesta.Estadio.Nombre };
                    estadio.Read();
                    double cot=1;

                    // Se determina la cotización dependiendo de la predicción del usuario 
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

            GridViewApuestasUsuario.DataSource = apuestasMostrar;
            GridViewApuestasUsuario.DataBind();


        }

        /// <summary>
        /// Clase creada ad hoc para almacenar los datos completos de una apuesta
        /// para luego presentarlos en el GridView.
        /// </summary>
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
        /// <summary>
        /// Evento que se ejecuta para cada fila 
        /// Cambia el color de fondo de la fila según si el usuario ha acertado o no
        /// </summary>
        protected void GridViewJuegos_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                ApuestaFinal apuesta = (ApuestaFinal)e.Row.DataItem;
                if (apuesta.Resultado_apuesta == apuesta.Resultado_predicho && apuesta.Resultado_apuesta != string.Empty)
                    e.Row.BackColor = System.Drawing.Color.LightGreen;
                else if (apuesta.Resultado_apuesta!=string.Empty)
                    e.Row.BackColor = System.Drawing.Color.LightCoral;
                else
                    e.Row.BackColor = System.Drawing.Color.LightYellow;


            }
        }
    }
}