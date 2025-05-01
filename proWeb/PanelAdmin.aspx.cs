using System;
using System.Web.UI;

namespace ProWeb
{
    public partial class PanelAdmin : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void EventoAdminRoles(object sender, EventArgs e)
        {
            Response.Redirect("AdministrarRoles.aspx");
        }

        protected void EventoAdminUsuarios(object sender, EventArgs e)
        {
            Response.Redirect("DBUsuario.aspx");
        }

        protected void EventoAdminUbicaciones(object sender, EventArgs e)
        {
            Response.Redirect("Ubicacion.aspx");
        }

        protected void EventoAdminEstadios(object sender, EventArgs e)
        {
            Response.Redirect("AdminEstadios.aspx");
        }

        protected void EventoAdminEquipos(object sender, EventArgs e)
        {
            Response.Redirect("AdminEquipos.aspx");
        }

        protected void EventoAdminNotificaciones(object sender, EventArgs e)
        {
            Response.Redirect("AdminNotificaciones.aspx");
        }

        protected void EventoAdminApuestas(object sender, EventArgs e)
        {
            Response.Redirect("AdminApuestas.aspx");
        }

        protected void EventoAdminPatrocinadores(object sender, EventArgs e)
        {
            Response.Redirect("DBPatrocinadores.aspx");
        }

        protected void EventoAdminCategorias(object sender, EventArgs e)
        {
            Response.Redirect("AdministrarCateg.aspx");
        }
    }
}
