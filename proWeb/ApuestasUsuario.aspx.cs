using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ProWeb
{
	public partial class ApuestasUsuario : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
            if (!IsPostBack)
            {
                var apuestas = new List<Apuesta>
                {
                    new Apuesta { EquipoLocal = "Real Madrid", EquipoVisitante = "Barcelona", Resultado_partido="1-0", Resultado_apuesta="x", Cotizacion=1.3, Estadio = "Santiago Bernabéu", Fecha = new DateTime(2025, 5, 15), Categoria="Futbol" },
                    new Apuesta { EquipoLocal = "Manchester City", EquipoVisitante = "Liverpool", Resultado_partido="3-2", Resultado_apuesta="1", Cotizacion=2, Estadio = "Etihad Stadium", Fecha = new DateTime(2025, 5, 16), Categoria="Futbol" }
                };

                GridViewApuestasUsuario.DataSource = apuestas;
                GridViewApuestasUsuario.DataBind();
            }
        }

        public class Apuesta
        {
            public string EquipoLocal { get; set; }
            public string EquipoVisitante { get; set; }
            public string Resultado_partido { get; set; }
            public string Resultado_apuesta { get; set; } //1 ganas, 0 pierdes y x empate
            public double Cotizacion { get; set; }
            public string Estadio { get; set; }
            public DateTime Fecha { get; set; }
            public string Categoria { get; set; }

        }

        protected void GridViewJuegos_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Apuesta apuesta = (Apuesta)e.Row.DataItem;

                if (apuesta.Resultado_apuesta == "1")
                    e.Row.BackColor = System.Drawing.Color.LightGreen;
                else if (apuesta.Resultado_apuesta == "0")
                    e.Row.BackColor = System.Drawing.Color.LightCoral;
                else
                    e.Row.BackColor = System.Drawing.Color.LightYellow;
            }
        }
    }
}