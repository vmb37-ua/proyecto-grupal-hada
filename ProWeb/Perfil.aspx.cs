using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.DynamicData;
using System.Web.UI;
using System.Web.UI.WebControls;
using library;

namespace ProWeb
{
    public partial class Perfil : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Login"] == null)
            {
                Response.Redirect("Login.aspx");
            }

            ENUsuario usuario = new ENUsuario();
            usuario.ID = int.Parse(Session["Login"].ToString());
            usuario.Read();
            FotoPerfil.ImageUrl = "~/Source/Images/"+usuario.Imagen;
            CampoNombre.Text = usuario.Nombre;
            CampoDireccion.Text = usuario.Direccion;
            CampoSaldo.Text = usuario.Saldo.ToString() + " €";
            CampoTelefono.Text = usuario.Telefono;
        }

        protected void EventoEditarPerfil(object sender, EventArgs e)
        {
            Response.Redirect("AdminUsuario.aspx");
        }

        protected void EventoBotonSaldo(object sender, EventArgs e)
        {
            Response.Redirect("Cartera.aspx");
        }
        protected void EventoBotonApuestas(object sender, EventArgs e)
        {
            Response.Redirect("ApuestasUsuario.aspx");
        }
        protected void EventoBotonFavoritos(object sender, EventArgs e)
        {
            Response.Redirect("Favoritos.aspx");
        }
        protected void EventoBotonNotificaciones(object sender, EventArgs e)
        {
            Response.Redirect("Notificaciones.aspx");
        }
        protected void EventoCerrarSesion(object sender, EventArgs e)
        {
            Session["Login"] = null;
            Response.Redirect("Juegos.aspx");
        }
        protected void EventoEliminarCuenta(object sender, EventArgs e)
        {
            ENUsuario usuario = new ENUsuario();
            usuario.ID = int.Parse(Session["Login"].ToString());
            usuario.Delete();
            Session["Login"] = null;
            Response.Redirect("Juegos.aspx");
        }
    }
}