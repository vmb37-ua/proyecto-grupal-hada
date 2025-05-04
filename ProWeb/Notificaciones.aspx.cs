using System;
using System.Collections.Generic;
using System.Web.UI;

namespace ProWeb
{
    public partial class Notificaciones : Page
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

        protected void BotonVolver_Click(object sender, EventArgs e)
        {
            Response.Redirect("Perfil.aspx");
        }
    }
}

