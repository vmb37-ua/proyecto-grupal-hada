using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using library;

namespace ProWeb
{
    public partial class Site1 : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Login"] == null)
            {
                botonPerfil.Visible = false;
                botonSesion.Visible = true;
                if (mainMenu.FindItem("Administrar") != null)
                {
                    mainMenu.Items.Remove(mainMenu.FindItem("Administrar"));
                }
            }
            else { 
                ENUsuario usuario = new ENUsuario();
                usuario.ID = int.Parse(Session["Login"].ToString());
                usuario.Read();
                botonPerfil.ImageUrl = "~/Source/Images/"+usuario.Imagen;
                botonPerfil.Visible = true;
                botonSesion.Visible = false;

                ENRol rol = new ENRol();
                rol.Id_rol = usuario.Rol;
                rol.Read();
                if(rol.Nombre != "Administrador" && mainMenu.FindItem("Administrar") != null)
                {
                    mainMenu.Items.Remove(mainMenu.FindItem("Administrar"));
                }
            }
        }

        protected void EventoClickSesion(object sender, EventArgs e)
        {
            Response.Redirect("Login.aspx");
        }

        protected void EventoBotonPerfil(object sender, EventArgs e)
        {
            Response.Redirect("Perfil.aspx");
        }

        protected void EventoBotonLogo(object sender, EventArgs e) {
            Response.Redirect("Login.aspx");
        }
    }
}