using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using library;

namespace ProWeb
{
    public partial class Site1 : System.Web.UI.MasterPage
    {
        /// <summary>
        /// Función que se ejecuta con la carga de la página. Verifica la identidad del usuario e inicializa la cabecera.
        /// </summary>
        /// <param name="sender">Instacia de la página cargada</param>
        /// <param name="e">Argumentos del evento</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            HttpCookie cookie = Request.Cookies["UsuarioID"];
            if (cookie == null || cookie.Value == null || cookie.Value == "")
            {
                Inicializar();
            }
            else
            {
                int valor;
                if (int.TryParse(cookie.Value, out valor))
                {
                    Session["Login"] = valor;
                    Inicializar();
                }
            }
        }
        /// <summary>
        /// Función auxiliar que inicializa la cabecera de la página, objetos del menu y foto de perfil.
        /// </summary>
        protected void Inicializar() {
            if (Session["Login"] == null)
            {
                botonPerfil.Visible = false;
                botonSesion.Visible = true;
                if (mainMenu.FindItem("Administrar") != null)
                {
                    mainMenu.Items.Remove(mainMenu.FindItem("Administrar"));
                }
            }
            else
            {
                ENUsuario usuario = new ENUsuario();
                usuario.ID = int.Parse(Session["Login"].ToString());
                usuario.Read();
                botonPerfil.ImageUrl = "~/Source/Images/" + usuario.Imagen;
                botonPerfil.Visible = true;
                botonSesion.Visible = false;

                ENRol rol = new ENRol();
                rol.Id_rol = usuario.Rol;
                rol.Read();
                if (rol.Nombre != "Administrador" && mainMenu.FindItem("Administrar") != null)
                {
                    mainMenu.Items.Remove(mainMenu.FindItem("Administrar"));
                }
            }
        }
        /// <summary>
        /// Evento lanzado por el botón de inicio de sesión. Redirije a la página login.
        /// </summary>
        /// <param name="sender">Página objeto que lanza el evento</param>
        /// <param name="e">Argumentos del evento</param>
        protected void EventoClickSesion(object sender, EventArgs e)
        {
            Response.Redirect("Login.aspx");
        }
        /// <summary>
        /// Evento lanzado por el botón del perfil. Redirije a la página de perfil.
        /// </summary>
        /// <param name="sender">Página objeto que lanza el evento</param>
        /// <param name="e">Argumentos del evento</param>
        protected void EventoBotonPerfil(object sender, EventArgs e)
        {
            Response.Redirect("Perfil.aspx");
        }
        /// <summary>
        /// Evento lanzado por el botón del logo de la página.
        /// </summary>
        /// <param name="sender">Página que lanza el evento</param>
        /// <param name="e">Argumentos del evento</param>
        protected void EventoBotonLogo(object sender, EventArgs e) {
            Response.Redirect("Juegos.aspx");
        }
    }
}