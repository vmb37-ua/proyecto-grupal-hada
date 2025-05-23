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
        /// <summary>
        /// Función que se ejecuta cuando se carga la página. Verifica la identidad del usuario y rellena los campos necesarios.
        /// </summary>
        /// <param name="sender">Objeto de la página</param>
        /// <param name="e">Argumentos del evento</param>
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
        /// <summary>
        /// Evento que se lanza al pulsar el botón de editar perfil.
        /// Redirije a Admin usuario.
        /// </summary>
        /// <param name="sender">Página objeto que lanza el evento</param>
        /// <param name="e">Argumentos del evento</param>
        protected void EventoEditarPerfil(object sender, EventArgs e)
        {
            Response.Redirect("AdminUsuario.aspx");
        }
        /// <summary>
        /// Evento que se lanza al pulsar el botón de saldo.
        /// Redirije a cartera.
        /// </summary>
        /// <param name="sender">Página objeto que lanza el evento</param>
        /// <param name="e">Argumentos del evento</param>
        protected void EventoBotonSaldo(object sender, EventArgs e)
        {
            Response.Redirect("Cartera.aspx");
        }
        /// <summary>
        /// Evento que se lanza al pulsar el botón de apuestas.
        /// Redirije a la página de apuestas.
        /// </summary>
        /// <param name="sender">Página objeto que lanza el evento</param>
        /// <param name="e">Argumentos del evento</param>
        protected void EventoBotonApuestas(object sender, EventArgs e)
        {
            Response.Redirect("ApuestasUsuario.aspx");
        }
        /// <summary>
        /// Evento que se lanza al pulsar el botón de favoritos.
        /// Redirije a la página de favoritos.
        /// </summary>
        /// <param name="sender">Página objeto que lanza el evento</param>
        /// <param name="e">Argumentos del evento</param>
        protected void EventoBotonFavoritos(object sender, EventArgs e)
        {
            Response.Redirect("Favoritos.aspx");
        }
        /// <summary>
        /// Evento que se lanza al pulsar el botón de notificaciones.
        /// Redirije a la página de notificaciones.
        /// </summary>
        /// <param name="sender">Página objeto que lanza el evento</param>
        /// <param name="e">Argumentos del evento</param>
        protected void EventoBotonNotificaciones(object sender, EventArgs e)
        {
            Response.Redirect("Notificaciones.aspx");
        }
        /// <summary>
        /// Evento que se lanza al pulsar el botón de cerrar sesión.
        /// Cierra la sesión y elimina las cookies del navegador.
        /// </summary>
        /// <param name="sender">Página objeto que lanza el evento</param>
        /// <param name="e">Argumentos del evento</param>
        protected void EventoCerrarSesion(object sender, EventArgs e)
        {
            Session["Login"] = null;
            HttpCookie cookie = new HttpCookie("UsuarioID");
            cookie.Value = "";
            Response.Cookies.Add(cookie);
            Response.Redirect("Login.aspx");
        }
        /// <summary>
        /// Evento que se lanza al pulsar el botón de eliminar cuenta.
        /// Cierra la sesión, borra el usuario y elimina las cookies.
        /// </summary>
        /// <param name="sender">Página objeto que lanza el evento</param>
        /// <param name="e">Argumentos del evento</param>
        protected void EventoEliminarCuenta(object sender, EventArgs e)
        {
            ENUsuario usuario = new ENUsuario();
            usuario.ID = int.Parse(Session["Login"].ToString());
            usuario.Delete();
            Session["Login"] = null;
            HttpCookie cookie = new HttpCookie("UsuarioID");
            cookie.Value = "";
            Response.Cookies.Add(cookie);
            Response.Redirect("Login.aspx");
        }
    }
}