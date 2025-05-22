using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using library;

namespace ProWeb
{
    public partial class AdminNotificaciones : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Login"] == null)
            {
                Response.Redirect("Juegos.aspx");
                return;
            }

            ENUsuario usuario = new ENUsuario();
            usuario.ID = int.Parse(Session["Login"].ToString());

            if (!usuario.Read())
            {
                Response.Redirect("Juegos.aspx");
                return;
            }

            ENRol rol = new ENRol();
            rol.Id_rol = usuario.Rol;

            if (!rol.Read() || rol.Nombre != "Administrador")
            {
                Response.Redirect("Juegos.aspx");
                return;
            }

            if (!IsPostBack)
            {
                IdGestion.Attributes["placeholder"] = "Escriba el id aquí";
                CargarUsuarios();
                LimpiarMensajes();
            }
        }


        private void LimpiarMensajes()
        {
            LblErrorEnviar.Text = "";
            LblErrorEliminar.Text = "";
        }

        private void CargarUsuarios()
        {
            ListaUsuarios.Items.Clear();

            ListaUsuarios.Items.Add(new ListItem("Todos", "Todo"));
            ListaUsuarios.Items.Add(new ListItem("enviar a rol específico", "rol"));

            ENUsuario en = new ENUsuario();
            var usuarios = en.ReadAll();

            foreach (var u in usuarios)
            {
                ListaUsuarios.Items.Add(new ListItem(u.Correo, u.ID.ToString()));
            }
        }

        protected void ListaUsuarios_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ListaUsuarios.SelectedValue == "rol")
            {
                CargarRoles();
                ListaRoles.Visible = true;
            }
            else
            {
                ListaRoles.Visible = false;
            }
        }

        protected void ListaRoles_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void CargarRoles()
        {
            ENRol rol = new ENRol();
            var roles = rol.ReadAll();

            ListaRoles.DataSource = roles;
            ListaRoles.DataTextField = "Nombre";
            ListaRoles.DataValueField = "Id_rol";
            ListaRoles.DataBind();

            ListaRoles.Items.Insert(0, new ListItem("-- Selecciona un rol --", ""));
        }

        protected void EnviarNotificacion_Click(object sender, EventArgs e)
        {

            LblErrorEliminar.Text = "";
            LblErrorEliminar.ForeColor = System.Drawing.Color.Black;

            LblErrorEnviar.Text = "";
            LblErrorEnviar.ForeColor = System.Drawing.Color.Black;

            string mensaje = TextoNotificacion.Text.Trim();

            if (string.IsNullOrWhiteSpace(mensaje))
            {
                MostrarMensaje(LblErrorEnviar, "La notificación está vacía", false);
                return;
            }

            string seleccion = ListaUsuarios.SelectedValue;

            if (seleccion == "Todo")
            {
                ENUsuario enUsuario = new ENUsuario();
                var usuarios = enUsuario.ReadAll();

                foreach (var usuario in usuarios)
                {
                    ENNotificacion not = new ENNotificacion
                    {
                        Mensaje = mensaje,
                        IdUsuario = usuario.ID,
                    };

                    not.Create();
                }

                MostrarMensaje(LblErrorEnviar, "Notificación enviada a todos los usuarios", true);
                TextoNotificacion.Text = "";
            }
            else if (seleccion == "rol")
            {
                string idRolSeleccionado = ListaRoles.SelectedValue;

                if (!string.IsNullOrEmpty(idRolSeleccionado))
                {
                    List<ENUsuario> usuarios = ObtenerUsuariosPorRol(Convert.ToInt32(idRolSeleccionado));

                    if (usuarios.Count > 0)
                    {
                        foreach (var usuario in usuarios)
                        {
                            EnviarNotificacionAUsuario(usuario.ID.ToString(), mensaje);
                        }

                        MostrarMensaje(LblErrorEnviar, "Notificación enviada a todos los usuarios del rol seleccionado", true);
                        TextoNotificacion.Text = "";
                    }
                    else
                    {
                        MostrarMensaje(LblErrorEnviar, "No hay usuarios con ese rol", false);
                    }
                }
                else
                {
                    MostrarMensaje(LblErrorEnviar, "Selecciona un rol válido", false);
                }
            }
            else
            {
                if (!string.IsNullOrEmpty(seleccion))
                {
                    bool enviado = EnviarNotificacionAUsuario(seleccion, mensaje);

                    if (enviado)
                    {
                        MostrarMensaje(LblErrorEnviar, "Notificación enviada correctamente", true);
                        TextoNotificacion.Text = "";
                    }
                    else
                    {
                        MostrarMensaje(LblErrorEnviar, "Error al enviar la notificación", false);
                    }
                }
                else
                {
                    MostrarMensaje(LblErrorEnviar, "Selecciona un usuario válido", false);
                }
            }
        }

        private List<ENUsuario> ObtenerUsuariosPorRol(int idRol)
        {
            ENUsuario en = new ENUsuario();
            return en.ReadByRol(idRol);
        }

        private bool EnviarNotificacionAUsuario(string idUsuario, string mensaje)
        {
            try
            {
                ENNotificacion notif = new ENNotificacion();
                notif.IdUsuario = Convert.ToInt32(idUsuario);
                notif.Mensaje = mensaje;

                return notif.Create();
            }
            catch (Exception)
            {
                return false;
            }
        }

        protected void BtnEliminarNotificacion_Click(object sender, EventArgs e)
        {

            LblErrorEnviar.Text = "";
            LblErrorEnviar.ForeColor = System.Drawing.Color.Black;

            LblErrorEliminar.Text = "";
            LblErrorEliminar.ForeColor = System.Drawing.Color.Black;

            string idTexto = IdGestion.Text.Trim();

            if (int.TryParse(idTexto, out int id))
            {
                ENNotificacion not = new ENNotificacion();
                not.Id = id;

                if (not.Delete())
                {
                    MostrarMensaje(LblErrorEliminar, "Notificación eliminada correctamente", true);
                    IdGestion.Text = "";
                }
                else
                {
                    MostrarMensaje(LblErrorEliminar, "No se encontró la notificación", false);
                }
            }
            else
            {
                MostrarMensaje(LblErrorEliminar, "Introduce un ID válido", false);
            }
        }

        private void MostrarMensaje(Label label, string mensaje, bool esExito)
        {
            label.Text = mensaje;
            label.ForeColor = esExito ? System.Drawing.Color.Green : System.Drawing.Color.Red;
        }
        protected void BtnLeerNotificacion_Click(object sender, EventArgs e)
        {
            LimpiarMensajes(); 

            string idTexto = IdGestion.Text.Trim();

            if (int.TryParse(idTexto, out int id))
            {
                ENNotificacion not = new ENNotificacion();
                not.Id = id;

                if (not.ReadbyId()) 
                {
                    TextoNotificacion.Text = not.Mensaje; 
                    MostrarMensaje(LblErrorEliminar, "Notificación cargada correctamente", true);
                }
                else
                {
                    MostrarMensaje(LblErrorEliminar, "No se encontró la notificación", false);
                    TextoNotificacion.Text = "";
                }
            }
            else
            {
                MostrarMensaje(LblErrorEliminar, "Introduce un ID válido", false);
            }
        }

    }
}
