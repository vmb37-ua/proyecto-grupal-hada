using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ProWeb
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Emaillogin.Attributes["placeholder"] = "Correo electrónico";
                Passlogin.Attributes["placeholder"] = "Contraseña";
            }
        }

        protected void EventoRegistrar(object sender, EventArgs e)
        {
            Response.Redirect("Register.aspx");
        }

        protected void EventoMainPage(object sender, EventArgs e)
        {
            Response.Redirect("Juegos.aspx");
        }
    }
}