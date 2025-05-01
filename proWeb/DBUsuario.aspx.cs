using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ProWeb
{
	public partial class DBUsuario : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
            if (!IsPostBack)
            {
                ListaDBUsuarios.Items.Add(new ListItem("Usuario A", "2"));
                ListaDBUsuarios.Items.Add(new ListItem("Usuario B", "3"));
                ListaDBUsuarios.Items.Add(new ListItem("Usuario C", "4"));
            }
        }

        protected void ListaDBUsuarios_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}