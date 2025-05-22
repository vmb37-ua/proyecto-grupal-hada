using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using library;

namespace ProWeb
{
    /// <summary>
    /// Página que permite al usuario autenticado ver una tabla con todos sus elementos marcados como favoritos.
    /// Si el usuario no ha iniciado sesión, será redirigido a la página de inicio de sesión.
    /// </summary>
    public partial class Favoritos : Page
    {
        /// <summary>
        /// Evento que se ejecuta al cargarse la página por primera vez (no en recargas por interacción del usuario).
        /// Si el usuario ha iniciado sesión, se consulta la base de datos para obtener sus favoritos
        /// y se cargan en un control tipo tabla (GridView).
        /// </summary>
        /// <param name="sender">Objeto que desencadenó el evento.</param>
        /// <param name="e">Argumentos del evento de carga de página.</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            //Verifica que la carga de la página no sea una recarga por acción del usuario (como un botón).
            if (!IsPostBack)
            {
                //Comprueba si hay sesión activa. Si no la hay redirige al login.
                if (Session["Login"] != null)
                {
                    //Obtiene el ID del usuario desde la sesión.
                    int idUsuario = Convert.ToInt32(Session["Login"]);

                    //Crea una instancia del objeto ENFavoritos y asigna el ID del usuario.
                    ENFavoritos fav = new ENFavoritos
                    {
                        IdUsuario = idUsuario
                    };

                    //Llama al método ReadAllUsuario(), que consulta y devuelve todos los favoritos del usuario desde la base de datos.
                    List<ENFavoritos> favoritos = fav.ReadAllUsuario();

                    //Asigna la lista de favoritos como fuente de datos del control GridView.
                    TablaFavoritos.DataSource = favoritos;

                    //Enlaza los datos al control para que se muestren en la interfaz.
                    TablaFavoritos.DataBind();
                }
                else
                {
                    //Si no hay sesión activa, redirige al login.
                    Response.Redirect("Login.aspx");
                }
            }
        }

        /// <summary>
        /// Evento que se ejecuta cuando el usuario hace clic en el botón "Ir al perfil".
        /// Redirige al usuario a la página de perfil (Perfil.aspx).
        /// </summary>
        /// <param name="sender">Objeto que generó el evento (el botón).</param>
        /// <param name="e">Datos asociados al evento de clic.</param>
        protected void IrPerfil_Click(object sender, EventArgs e)
        {
            //Redirige a la página de perfil del usuario.
            Response.Redirect("Perfil.aspx");
        }
    }
}
