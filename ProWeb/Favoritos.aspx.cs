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
                    new EquipoFavorito { NombreEquipo = "C.D. Thader", CiudadEquipo = "Rojales" },
                    new EquipoFavorito { NombreEquipo = "Orihuela CF", CiudadEquipo = "Orihuela" }
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
    }
}
