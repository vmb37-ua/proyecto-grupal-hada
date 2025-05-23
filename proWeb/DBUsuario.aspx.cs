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
    /// Página de administración para gestionar usuarios de la base de datos.
    /// Permite eliminar usuarios y modificar ciertos atributos.
    /// </summary>
    public partial class DBUsuario : System.Web.UI.Page
    {
        /// <summary>
        /// Evento que se ejecuta al cargarse la página.
        /// </summary>
        protected void Page_Load(object sender, EventArgs e)
        {
            CargarRoles();

            if (!IsPostBack)
            {
                CargarUsuarios();
                CargarRoles();
                MostrarDatosUsuario();
            }
        }



        private void CargarUsuarios()
        {
            ListaDBUsuarios.Items.Clear();

            //Pillamos todos los usuarios
            List<ENUsuario> usuarios = new CADUsuario().ReadAll();

            //Metemos esto por defecto
            ListaDBUsuarios.Items.Add(new ListItem("Seleccione un usuario", "0"));

            //Agregamos usuarios
            foreach (ENUsuario usuario in usuarios)
            {
                ListaDBUsuarios.Items.Add(new ListItem(usuario.Nombre + " (" + usuario.Correo + ")", usuario.ID.ToString()));
            }
        }

        private void CargarRoles()
        {
            ddlRoles.Items.Clear();
            List<ENRol> roles = new CADRol().ReadAll();

            foreach (ENRol rol in roles)
            {
                ddlRoles.Items.Add(new ListItem(rol.Nombre, rol.Id_rol.ToString()));
            }
        }

        protected void ListaDBUsuarios_SelectedIndexChanged(object sender, EventArgs e)
        {
            MostrarDatosUsuario();
        }

        private void MostrarDatosUsuario()
        {
            if (ListaDBUsuarios.SelectedValue != "0")
            {
                int idUsuario = int.Parse(ListaDBUsuarios.SelectedValue);
                ENUsuario usuario = new ENUsuario();
                usuario.ID = idUsuario;

                if (usuario.Read())
                {
                    TBid.Text = usuario.ID.ToString();
                    TBnombre.Text = usuario.Nombre;
                    TBdireccion.Text = usuario.Direccion;
                    TBtelefono.Text = usuario.Telefono;
                    ddlRoles.SelectedValue = usuario.Rol.ToString(); 
                }
            }
            else
            {
                LimpiarCampos();
            }
        }

        private void LimpiarCampos()
        {
            TBid.Text = "";
            TBnombre.Text = "";
            TBdireccion.Text = "";
            TBtelefono.Text = "";

            if (ddlRoles.Items.Count > 0)
            {
                ddlRoles.SelectedIndex = 0;
            }
        }

        /// <summary>
        /// Evento del botón para actualizar un usuario.
        /// Si se ha seleccionado un usuario se actualiza, no puede crear desde cero
        /// </summary>
        protected void BTNagregar_pat_Click(object sender, EventArgs e)
        {
            try
            {
                ENUsuario usuario = new ENUsuario();

                // Si existe ID es porque estamos actualizando
                if (!string.IsNullOrEmpty(TBid.Text))
                {
                    usuario.ID = int.Parse(TBid.Text);
                    usuario.Read(); 
                }

                
                usuario.Nombre = TBnombre.Text;
                usuario.Direccion = TBdireccion.Text;
                usuario.Telefono = TBtelefono.Text;


                if (ddlRoles.SelectedValue != null)
                {
                    usuario.Rol = int.Parse(ddlRoles.SelectedValue);
                }

                bool resultado;
                if (usuario.ID == 0)
                {
                    //La herramienta no crea usuarios, se debe de seleccionar uno
                    MostrarMensaje("Seleccione un usuario");
                }
                else
                {
                    resultado = usuario.Update();
                }

 
                CargarUsuarios();
  
            }
            catch (Exception ex)
            {
                MostrarMensaje("Error: " + ex.Message);
            }
        }

        /// <summary>
        /// Evento del botón para eliminar el usuario seleccionado.
        /// </summary>
        protected void BTNeliminar_pat_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(TBid.Text))
            {
                ENUsuario usuario = new ENUsuario();
                usuario.ID = int.Parse(TBid.Text);

                if (Session["usuario"] != null && Session["usuario"] is ENUsuario usuarioSesion &&
                     usuarioSesion.ID == usuario.ID)
                {
                    MostrarMensaje("No puedes eliminar tu propio usuario mientras estás conectado.");
                    return;
                }

                if (usuario.Delete())
                {
                    MostrarMensaje("Usuario eliminado correctamente");
                    LimpiarCampos();
                    CargarUsuarios();
                    ListaDBUsuarios.SelectedIndex = 0;
                }
                else
                {
                    MostrarMensaje("Error al eliminar el usuario");
                }
            }
        }

        /// <summary>
        /// Pone un mensaje en pantalla
        /// </summary>
        /// <param name="mensaje">Texto del mensaje </param>
        private void MostrarMensaje(string mensaje)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "showalert",
                $"alert('{mensaje}');", true);
        }
    }
}