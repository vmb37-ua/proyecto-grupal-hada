using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using library;

namespace ProWeb
{
    public partial class DBUsuario : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
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
            // Cogemos los roles
            List<ENRol> roles = new CADRol().ReadAll();

            
            TBrol.Visible = false;

            DropDownList ddlRoles = new DropDownList();
            ddlRoles.ID = "ddlRoles";
            ddlRoles.CssClass = "ListaDesplegable";

            foreach (ENRol rol in roles)
            {
                ddlRoles.Items.Add(new ListItem(rol.Nombre, rol.Id_rol.ToString()));
            }

           
            TBrol.Parent.Controls.Add(ddlRoles);
            TBrol.Parent.Controls.Remove(TBrol);
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
                    TBsaldo.Text = usuario.Saldo.ToString("0.00");
                    TBdatos_pago.Text = usuario.NumTar;
                    TBdireccion.Text = usuario.Direccion;
                    TBtelefono.Text = usuario.Telefono;
                    TBimagen.Text = usuario.Imagen;

                    //Metemos el rol quehemos escogido
                    DropDownList ddlRoles = (DropDownList)FindControl("ddlRoles");
                    if (ddlRoles != null)
                    {
                        ddlRoles.SelectedValue = usuario.Rol.ToString();
                    }
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
            TBsaldo.Text = "";
            TBdatos_pago.Text = "";
            TBdireccion.Text = "";
            TBtelefono.Text = "";
            TBimagen.Text = "";

            DropDownList ddlRoles = (DropDownList)FindControl("ddlRoles");
            if (ddlRoles != null)
            {
                ddlRoles.SelectedIndex = 0;
            }
        }

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
                usuario.Saldo = float.Parse(TBsaldo.Text);
                usuario.NumTar = TBdatos_pago.Text;
                usuario.Direccion = TBdireccion.Text;
                usuario.Telefono = TBtelefono.Text;
                usuario.Imagen = TBimagen.Text;

              
                DropDownList ddlRoles = (DropDownList)FindControl("ddlRoles");
                if (ddlRoles != null)
                {
                    usuario.Rol = int.Parse(ddlRoles.SelectedValue);
                }

                bool resultado;
                if (usuario.ID == 0)
                {

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

        protected void BTNeliminar_pat_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(TBid.Text))
            {
                ENUsuario usuario = new ENUsuario();
                usuario.ID = int.Parse(TBid.Text);

                if (usuario.Delete())
                {
                    MostrarMensaje("Usuario eliminado correctamente");
                    LimpiarCampos();
                    CargarUsuarios();
                }
                else
                {
                    MostrarMensaje("Error al eliminar el usuario");
                }
            }
        }

        private void MostrarMensaje(string mensaje)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "showalert",
                $"alert('{mensaje}');", true);
        }
    }
}