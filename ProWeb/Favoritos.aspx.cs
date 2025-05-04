using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ProWeb
{
    public partial class Favoritos : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                List<EquipoFavorito> equipos = new List<EquipoFavorito>
                {
                    new EquipoFavorito { NombreEquipo = "FC Barcelona", CiudadEquipo = "Barcelona" },
                    new EquipoFavorito { NombreEquipo = "Inter de Milán", CiudadEquipo = "Milán" }
                };

                TablaFavoritos.DataSource = equipos;
                TablaFavoritos.DataBind();
            }
        }

        public class EquipoFavorito
        {
            public string NombreEquipo { get; set; }
            public string CiudadEquipo { get; set; }
        }

        protected void IrPerfil_Click(object sender, EventArgs e)
        {
            Response.Redirect("Perfil.aspx");
        }
    }
}
