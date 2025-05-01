using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ProWeb
{
    public partial class Register : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                Nameregister.Attributes["placeholder"] = "Nombre Completo";
                Numberregister.Attributes["placeholder"] = "Número de telefono";
                Emailregister.Attributes["placeholder"] = "Correo electrónico";
                Passregister.Attributes["placeholder"] = "Contraseña";
                Passrepregister.Attributes["placeholder"] = "Repetir Contraseña";

            }
        }
    }
}