using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ProWeb
{
    public partial class Perfil : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void EventoEditarPerfil(object sender, EventArgs e)
        {
            Response.Redirect("AdminUsuario.aspx");
        }
    }
}