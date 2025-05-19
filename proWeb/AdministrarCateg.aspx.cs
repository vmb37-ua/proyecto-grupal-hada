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
        [System.Web.Services.WebMethod]
        public static List<string> ObtenerCategorias(string prefixText, int count)
        {
            ENCategoria categoria = new ENCategoria();
            List<ENCategoria> categorias = categoria.ReadAll();

            var consulta = categorias
                .Where(c => string.IsNullOrEmpty(prefixText) || c.Nombre.StartsWith(prefixText, StringComparison.OrdinalIgnoreCase))
                .Select(c => c.Nombre)
                .Take(count)
                .ToList();

            return consulta;
        }
        protected void Page_Load(object sender, EventArgs e)
        {

        }
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