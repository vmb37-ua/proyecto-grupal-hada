using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using static ProWeb.ApuestasUsuario;

namespace ProWeb
{
	public partial class Juegos : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
            if (!IsPostBack)
            {
                var juegos = new List<Juego>
                {
                    new Juego { EquipoLocal = "Real Madrid", EquipoVisitante = "Barcelona", Estadio = "Santiago Bernabéu", Fecha = new DateTime(2025, 5, 15), Categoria="Futbol", Hora=new TimeSpan(14, 30, 0)},
                    new Juego { EquipoLocal = "Manchester City", EquipoVisitante = "Liverpool", Estadio = "Etihad Stadium", Fecha = new DateTime(2025, 5, 16), Categoria="Futbol", Hora=new TimeSpan(20, 0, 0)}
                };

                rptJuegos.DataSource = juegos;
                rptJuegos.DataBind();
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

        }
    }
}