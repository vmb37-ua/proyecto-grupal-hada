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
                var notificaciones = new List<string>
                {
                    "¡Has ganado tu apuesta!",
                    "Tienes un bono disponible en tu cuenta",
                    "Tu perfil ha sido actualizado correctamente"
                };

                ListaNotificaciones.DataSource = notificaciones;
                ListaNotificaciones.DataBind();
            }
        }
    }
}

