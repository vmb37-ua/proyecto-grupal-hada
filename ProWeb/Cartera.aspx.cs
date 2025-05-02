using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ProWeb
{
    public partial class Cartera : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            DineroDisponible.Text = "100";
        }
    }
}
