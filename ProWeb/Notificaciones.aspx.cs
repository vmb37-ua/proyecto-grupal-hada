using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ProWeb
{
    public partial class Notificaciones : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                List<string> notificaciones = new List<string>
                {
                    "¡Has ganado tu apuesta!",
                    "Nuevo bono disponible en tu cuenta"
                };

                ListaNotificaciones.DataSource = notificaciones;
                ListaNotificaciones.DataBind();
            }
        }
    }
}
