using System;
using System.Collections.Generic;
using System.Web.UI;
using library;

namespace ProWeb
{
    /// <summary>
    /// Página web que permite al usuario autenticado visualizar sus notificaciones.
    /// Si se pasa una ID de notificación por la URL, se muestra únicamente esa notificación,
    /// siempre que pertenezca al usuario. Si no se pasa ninguna ID, se listan todas sus notificaciones.
    /// </summary>
    public partial class Notificaciones : Page
    {
        /// <summary>
        /// Evento que se ejecuta al cargarse la página por primera vez (no por interacción del usuario).
        /// Comprueba si el usuario ha iniciado sesión. Si es así:
        /// - Si se recibe una ID de notificación por la URL, intenta mostrar esa notificación específica.
        /// - Si no se recibe ninguna ID, muestra todas las notificaciones del usuario.
        /// </summary>
        /// <param name="sender">Objeto que lanzó el evento (normalmente, la propia página).</param>
        /// <param name="e">Argumentos del evento de carga de página.</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            //Ejecutar solo si la página se está cargando por primera vez
            if (!IsPostBack)
            {
                //Verifica si el usuario ha iniciado sesión (existe una sesión activa con clave "Login")
                if (Session["Login"] != null)
                {
                    //Recupera el ID del usuario desde la sesión
                    int idUsuario = Convert.ToInt32(Session["Login"]);

                    //Intenta obtener el parámetro "id" desde la URL (QueryString)
                    string idQuery = Request.QueryString["id"];

                    //Si hay un parámetro "id" y es un número válido, intenta buscar esa notificación específica
                    if (!string.IsNullOrEmpty(idQuery) && int.TryParse(idQuery, out int idNoti))
                    {
                        //Crea una instancia de ENNotificacion y asigna la ID obtenida
                        ENNotificacion notiDetalle = new ENNotificacion();
                        notiDetalle.Id = idNoti;

                        //Intenta cargar la notificación desde la base de datos con ReadbyId()
                        //y verifica que pertenezca al usuario actual
                        if (notiDetalle.ReadbyId() && notiDetalle.IdUsuario == idUsuario)
                        {
                            //Si la notificación es válida y pertenece al usuario, mostrar el mensaje
                            ListaNotificaciones.DataSource = new List<string> { notiDetalle.Mensaje };
                            ListaNotificaciones.DataBind();
                        }
                        else
                        {
                            //Si no existe o no pertenece al usuario, mostrar un mensaje de error
                            ListaNotificaciones.DataSource = new List<string> { "No se encontró la notificación o no tienes permiso para verla." };
                            ListaNotificaciones.DataBind();
                        }
                    }
                    else
                    {
                        //No se ha pasado ninguna ID: se cargan todas las notificaciones del usuario
                        ENNotificacion notificacion = new ENNotificacion();
                        notificacion.IdUsuario = idUsuario;

                        //Recupera todas las notificaciones del usuario desde la base de datos
                        List<ENNotificacion> notificaciones = notificacion.ReadAll();

                        //Extrae los mensajes de cada notificación
                        List<string> mensajes = new List<string>();
                        foreach (var n in notificaciones)
                        {
                            mensajes.Add(n.Mensaje);
                        }

                        //Asigna los mensajes al control visual y los muestra
                        ListaNotificaciones.DataSource = mensajes;
                        ListaNotificaciones.DataBind();
                    }
                }
                else
                {
                    //Si no hay sesión activa, redirige al usuario al login
                    Response.Redirect("Login.aspx");
                }
            }
        }

        /// <summary>
        /// Evento que se dispara cuando el usuario hace clic en el botón "Volver".
        /// Redirige al usuario autenticado a su página de perfil.
        /// </summary>
        /// <param name="sender">Objeto que lanzó el evento (el botón).</param>
        /// <param name="e">Argumentos del evento de clic.</param>
        protected void BotonVolver_Click(object sender, EventArgs e)
        {
            Response.Redirect("Perfil.aspx");
        }
    }
}
