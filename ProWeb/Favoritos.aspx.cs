using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using library;

namespace ProWeb
{
    public partial class Favoritos : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["Login"] != null)
                {
                    int idUsuario = Convert.ToInt32(Session["Login"]);

                    ENFavoritos fav = new ENFavoritos
                    {
                        IdUsuario = idUsuario
                    };

                    List<ENFavoritos> favoritos = fav.ReadAllUsuario();

                    TablaFavoritos.DataSource = favoritos;
                    TablaFavoritos.DataBind();
                }
                else
                {
                    Response.Redirect("Login.aspx");
                }
            }
        }

        protected void IrPerfil_Click(object sender, EventArgs e)
        {
            Response.Redirect("Perfil.aspx");
        }
    }
}