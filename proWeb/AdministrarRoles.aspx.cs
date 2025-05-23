using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using library;

namespace ProWeb
{
    public partial class AdministrarRoles : System.Web.UI.Page
    {

        /// <summary>
        /// Funcion que se ejecuta cuando se ha introducido una id.
        /// Maneja la visibilidad de los botones de buscar.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e">Argumento del evento de cambio de texto.</param>
        protected void IdEntrante(object sender, EventArgs e)
        {
            string texto = TBIdRol.Text.Trim();
            if (texto == "")
            {
                BotonIz.Visible = false;
                BotonBuscarRol.Visible = false;
                BotonDe.Visible = false;
            }
            else
            {
                BotonIz.Visible = true;
                BotonBuscarRol.Visible = true;
                BotonDe.Visible = true;
            }
        }

        /// <summary>
        /// Funcion que se ejecuta al gargar la pagina.
        /// Verifica si el usuario esta logeado y sea un administrador.
        /// </summary>
        /// <param name="sender">Objeto de la pagina.</param>
        /// <param name="e">Evento de cargar la pagina.</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Login"] == null)
            {
                Response.Redirect("Juegos.aspx");
            }
            else
            {
                ENUsuario usuario = new ENUsuario();
                usuario.ID = int.Parse(Session["Login"].ToString());
                usuario.Read();
                ENRol rol = new ENRol();
                rol.Id_rol = usuario.Rol;
                rol.Read();
                if (rol.Nombre != "Administrador")
                {
                    Response.Redirect("Juegos.aspx");
                }
            }
        }

        /// <summary>
        /// Crea un nuevo rol en la pagina web.
        /// Si se crea muestra un panel de confirmacion.
        /// Muestra un mensaje de error de que no se ha podido crear el rol.
        /// </summary>
        /// <param name="sender">Boton Crear.</param>
        /// <param name="e">Argumento del evento de crear rol.</param>
        protected void Create(object sender, EventArgs e)
        {
            LabelId.Visible = false;
            LabelNombre.Visible = false;
            ENRol rol = new ENRol();
            rol.Nombre = TBNombreRol.Text;
            rol.Descripcion = TBDescipcionRol.Text;
            if (rol.Create())
            {
                LabelPanelOperacion.Text = "Rol <b>" + TBNombreRol.Text + "</b> creado con exito";
                MPECreacion.Show();
                ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), "cerrarPopup();", true);
            }
            else
            {
                LabelNombre.Text = "No se ha podido crear el rol";
                LabelNombre.Visible = true;
                LabelNombre.ForeColor = System.Drawing.Color.Red;
            }
        }

        /// <summary>
        /// Actualiza un rol ya existente en la pagina web.
        /// Si se elimina muestra un panel de confirmacion.
        /// Muestra un mensaje de error de que no existe la id.
        /// </summary>
        /// <param name="sender">Boton Actualizar.</param>
        /// <param name="e">Argumento del evento de actualizar rol.</param>
        protected void Update(object sender, EventArgs e)
        {
            LabelId.Visible = false;
            LabelNombre.Visible = false;
            ENRol rol = new ENRol();
            rol.Id_rol = int.Parse(TBIdRol.Text);
            rol.Nombre = TBNombreRol.Text;
            rol.Descripcion = TBDescipcionRol.Text;
            if (rol.Update())
            {
                LabelPanelOperacion.Text = "Rol <b>" + TBNombreRol.Text + "</b> actualizado con exito";
                MPECreacion.Show();
                ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), "cerrarPopup();", true);
            }
            else
            {
                LabelId.Text = "No existe la id";
                LabelId.Visible = true;
                LabelId.ForeColor = System.Drawing.Color.Red;
            }
        }

        /// <summary>
        /// Elimina un rol ya existente en la pagina web.
        /// Antes de elimnarlo, advierte al usuario con una pestaña emergente.
        /// Si se elimina muestra un panel de confirmacion.
        /// Muestra un mensaje de error de que no existe la id.
        /// Vacia todos los campos de la pagina web.
        /// </summary>
        /// <param name="sender">Boton Eliminar.</param>
        /// <param name="e">Argumento del evento de eliminar rol.</param>
        protected void Delete(object sender, EventArgs e)
        {
            LabelId.Visible = false;
            LabelNombre.Visible = false;
            ENRol rol = new ENRol();
            rol.Id_rol = int.Parse(TBIdRol.Text);
            if (rol.Read())
            {
                if (rol.Delete())
                {
                    LabelPanelOperacion.Text = "Rol <b>" + TBNombreRol.Text + "</b> eliminado con exito";
                    MPECreacion.Show();
                    ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), "cerrarPopup();", true);
                    TBIdRol.Text = "";
                    TBNombreRol.Text = "";
                    TBDescipcionRol.Text = "";
                    BotonIz.Visible = false;
                    BotonBuscarRol.Visible = false;
                    BotonDe.Visible = false;
                }
                else
                {
                    LabelId.Text = "No ha podido eliminar";
                    LabelId.Visible = true;
                    LabelId.ForeColor = System.Drawing.Color.Red;
                }
            }
            else
            {
                LabelId.Text = "No existe la id";
                LabelId.Visible = true;
                LabelId.ForeColor = System.Drawing.Color.Red;
            }
        }

        /// <summary>
        /// Busca un rol ya existente en la pagina web.
        /// Rellena todos los campos con la informacion del rol.
        /// Muestra un mensaje de error de que no existe la id.
        /// </summary>
        /// <param name="sender">Boton Buscar.</param>
        /// <param name="e">Argumento del evento de buscar rol.</param>
        protected void Read(object sender, EventArgs e)
        {
            LabelId.Visible = false;
            LabelNombre.Visible = false;
            ENRol rol = new ENRol();
            rol.Id_rol = int.Parse(TBIdRol.Text);
            if (rol.Read())
            {
                TBNombreRol.Text=rol.Nombre;
                TBDescipcionRol.Text=rol.Descripcion;
            }
            else
            {
                LabelId.Text = "No existe la id";
                LabelId.Visible = true;
                LabelId.ForeColor = System.Drawing.Color.Red;
            }
        }

        /// <summary>
        /// Busca el primer rol en la pagina web.
        /// Rellena todos los campos con la informacion del rol.
        /// Muestra un mensaje de error de que no existe ningun rol.
        /// </summary>
        /// <param name="sender">Boton First.</param>
        /// <param name="e">Argumento del evento de buscar first rol.</param>
        protected void ReadFirst(object sender, EventArgs e)
        {
            LabelId.Visible = false;
            LabelNombre.Visible = false;
            ENRol rol = new ENRol();
            if (rol.ReadFirst())
            {
                TBIdRol.Text = rol.Id_rol.ToString();
                TBNombreRol.Text = rol.Nombre;
                TBDescipcionRol.Text = rol.Descripcion;
                BotonIz.Visible = true;
                BotonBuscarRol.Visible = true;
                BotonDe.Visible = true;
            }
            else
            {
                LabelId.Text = "No existen roles";
                LabelId.Visible = true;
                LabelId.ForeColor = System.Drawing.Color.Red;
                TBIdRol.Text = "";
                TBNombreRol.Text = "";
                TBDescipcionRol.Text = "";
                BotonIz.Visible = false;
                BotonBuscarRol.Visible = false;
                BotonDe.Visible = false;
            }
        }

        /// <summary>
        /// Busca el siguiente rol respecto al introducido en la pagina web.
        /// Rellena todos los campos con la informacion del rol.
        /// Muestra un mensaje de error de que no existe la id.
        /// Muestra un mensaje de error de que no existe siguiente rol.
        /// </summary>
        /// <param name="sender">Boton Buscar Next.</param>
        /// <param name="e">Argumento del evento de buscar next rol.</param>
        protected void ReadNext(object sender, EventArgs e)
        {
            LabelId.Visible = false;
            LabelNombre.Visible = false;
            ENRol rol = new ENRol();
            rol.Id_rol = int.Parse(TBIdRol.Text);
            if (rol.Read())
                if (rol.ReadNext())
                {
                    TBIdRol.Text = rol.Id_rol.ToString();
                    TBNombreRol.Text = rol.Nombre;
                    TBDescipcionRol.Text = rol.Descripcion;
                }
                else
                {
                    LabelId.Text = "No hay un rol siguiente";
                    LabelId.Visible = true;
                    LabelId.ForeColor = System.Drawing.Color.Red;
                }
            else
            {
                LabelId.Text = "No existe la id";
                LabelId.Visible = true;
                LabelId.ForeColor = System.Drawing.Color.Red;
            }
        }

        /// <summary>
        /// Busca el anterior rol respecto al introducido en la pagina web.
        /// Rellena todos los campos con la informacion del rol.
        /// Muestra un mensaje de error de que no existe la id.
        /// Muestra un mensaje de error de que no existe anterior rol.
        /// </summary>
        /// <param name="sender">Boton Buscar Prev.</param>
        /// <param name="e">Argumento del evento de buscar prev rol.</param>
        protected void ReadPrev(object sender, EventArgs e)
        {
            LabelId.Visible = false;
            LabelNombre.Visible = false;
            ENRol rol = new ENRol();
            rol.Id_rol = int.Parse(TBIdRol.Text);
            if (rol.Read()) {
                if (rol.ReadPrev())
                {
                    TBIdRol.Text = rol.Id_rol.ToString();
                    TBNombreRol.Text = rol.Nombre;
                    TBDescipcionRol.Text = rol.Descripcion;
                }
                else
                {
                    LabelId.Text = "No hay un rol anterior";
                    LabelId.Visible = true;
                    LabelId.ForeColor = System.Drawing.Color.Red;
                }
            }
            else
            {
                LabelId.Text = "No existe la id";
                LabelId.Visible = true;
                LabelId.ForeColor = System.Drawing.Color.Red;
            }
        }
    }
}