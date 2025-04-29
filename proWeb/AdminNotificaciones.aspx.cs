using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ProWeb
{
    public partial class AdminNotificaciones : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                IdEliminar.Attributes["placeholder"] = "Escriba el id aquí";

            }
        }

        protected void ListaUsuarios_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}