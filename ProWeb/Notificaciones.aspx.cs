using System;
using System.Collections.Generic;
using System.Web.UI;
using library;

namespace ProWeb
{
    public partial class Notificaciones : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["IdUsuario"] != null)
                {
                    int idUsuario = Convert.ToInt32(Session["IdUsuario"]);

                    string idQuery = Request.QueryString["id"];

                    if (!string.IsNullOrEmpty(idQuery) && int.TryParse(idQuery, out int idNoti))
                    {
                        ENNotificacion notiDetalle = new ENNotificacion();
                        notiDetalle.Id = idNoti;

                        if (notiDetalle.ReadbyId() && notiDetalle.IdUsuario == idUsuario)
                        {
                            ListaNotificaciones.DataSource = new List<string> { notiDetalle.Mensaje };
                            ListaNotificaciones.DataBind();
                        }
                        else
                        {
                            ListaNotificaciones.DataSource = new List<string> { "No se encontró la notificación o no tienes permiso para verla." };
                            ListaNotificaciones.DataBind();
                        }
                    }
                    else
                    {
                        ENNotificacion notificacion = new ENNotificacion();
                        notificacion.IdUsuario = idUsuario;

                        List<ENNotificacion> notificaciones = notificacion.ReadAll();

                        List<string> mensajes = new List<string>();
                        foreach (var n in notificaciones)
                        {
                            mensajes.Add(n.Mensaje);
                        }

                        ListaNotificaciones.DataSource = mensajes;
                        ListaNotificaciones.DataBind();
                    }
                }
                else
                {
                    Response.Redirect("Login.aspx");
                }
            }
        }

        protected void BotonVolver_Click(object sender, EventArgs e)
        {
            Response.Redirect("Perfil.aspx");
        }
    }
}
