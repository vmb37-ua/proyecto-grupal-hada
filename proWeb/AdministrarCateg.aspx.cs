using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Reflection.Emit;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using library;

namespace ProWeb
{

    public partial class AdministrarCateg : System.Web.UI.Page
    {

        /// <summary>
        /// Funcion que agrega al AutoCompleteExtender las categorias que coinfiden con lo que el usuario escribe.
        /// </summary>
        /// <param name="prefixText">Texto introducido por el usuario.</param>
        /// <param name="count">Numero de maximos resultados a devolver.</param>
        /// <returns>Lista de nombres que coinfiden con lo buscado.</returns>
        [System.Web.Services.WebMethod]
        public static List<string> ObtenerCategorias(string prefixText, int count)
        {
            ENCategoria categoria = new ENCategoria();
            List<ENCategoria> categorias = categoria.ReadAll();

            List<string> consulta = categorias
                .Where(c => string.IsNullOrEmpty(prefixText) || c.Nombre.StartsWith(prefixText, StringComparison.OrdinalIgnoreCase))
                .Select(c => c.Nombre)
                .Take(count)
                .ToList();

            return consulta;
        }

        /// <summary>
        /// Funcion que se ejecuta al gargar la pagina.
        /// Verifica si el usuario esta logeado y sea un administrador.
        /// </summary>
        /// <param name="sender">Objeto de la pagina.</param>
        /// <param name="e">Argumento del evento de cargar la pagina.</param>
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
        /// Crea una nueva categoria en la pagina web.
        /// Si se crea muestra un panel de confirmacion.
        /// Muestra un mensaje de error de que ya existe la categoria.
        /// </summary>
        /// <param name="sender">Boton Crear.</param>
        /// <param name="e">Argumento del evento de crear categoria.</param>
        protected void Create(object sender, EventArgs e)
        {
            LabelNombre.Visible = false;
            ENCategoria cat = new ENCategoria();
            cat.Nombre = TBNombreCat.Text;
            if (cat.Create())
            {
                LabelPanelOperacion.Text = "Categoria <b>" + TBNombreCat.Text + "</b> creada con exito";
                MPECreacion.Show();
                ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), "cerrarPopup();", true);
            }
            else
            {
                LabelNombre.Text = "Ya existe esta categoria";
                LabelNombre.Visible = true;
                LabelNombre.ForeColor = System.Drawing.Color.Red;
            }
        }

        /// <summary>
        /// Elimina una categoria ya existente en la pagina web.
        /// Antes de elimnarlo, advierte al usuario con una pestaña emergente.
        /// Si se elimina muestra un panel de confirmacion.
        /// Muestra un mensaje de error de que no existe la id.
        /// </summary>
        /// <param name="sender">Boton Eliminar.</param>
        /// <param name="e">Argumento del evento de eliminar categoria.</param>
        protected void Delete(object sender, EventArgs e)
        {
            LabelNombre.Visible = false;
            ENCategoria cat = new ENCategoria();
            cat.Nombre = TBNombreCat.Text;
            if (cat.Read())
            {
                if (cat.Delete())
                {
                    LabelPanelOperacion.Text = "Categoria <b>" + TBNombreCat.Text + "</b> eliminada con exito";
                    MPECreacion.Show();
                    ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), "cerrarPopup();", true);
                    TBNombreCat.Text = "";
                }
                else
                {
                    LabelNombre.Text = "No ha podido eliminar";
                    LabelNombre.Visible = true;
                    LabelNombre.ForeColor = System.Drawing.Color.Red;
                }
            }
            else
            {
                LabelNombre.Text = "No existe la id";
                LabelNombre.Visible = true;
                LabelNombre.ForeColor = System.Drawing.Color.Red;
            }
        }
    }
}