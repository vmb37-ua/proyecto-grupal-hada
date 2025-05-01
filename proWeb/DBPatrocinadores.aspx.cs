using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ProWeb
{
	public partial class DBPatrocinadores : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
            if (!IsPostBack) 
            {
                ListaPatrocinadores.Items.Add(new ListItem("Patrocinador A", "1"));
                ListaPatrocinadores.Items.Add(new ListItem("Patrocinador B", "2"));
                ListaPatrocinadores.Items.Add(new ListItem("Patrocinador C", "3"));
            }

        }
        
        protected void ListaPatrocinadores_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}