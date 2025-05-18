using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;
using library;
using static System.Runtime.CompilerServices.RuntimeHelpers;


namespace ProWeb
{
    public partial class Ubicacion : System.Web.UI.Page
    {
        private void RecargarListaPaises()
        {
            try
            {
                lblMensaje.Visible = false;

                CADPais cadPais = new CADPais();
                List<ENPais> paises = cadPais.ReadAll();

                ddlEliminarPais.DataSource = paises;
                ddlEliminarPais.DataTextField = "NombrePais"; 
                ddlEliminarPais.DataValueField = "IdPais";    
                ddlEliminarPais.DataBind();

                ddlPaisProvincia.DataSource = paises;
                ddlPaisProvincia.DataTextField = "NombrePais";
                ddlPaisProvincia.DataValueField = "IdPais";
                ddlPaisProvincia.DataBind();
            }
            catch (Exception ex)
            {
                MostrarMensaje($"Error cargando países: {ex.Message}", "error");
            }
        }

        private void MostrarMensaje(string texto, string tipo = "informativo")
        {
            lblMensaje.Text = texto;
            lblMensaje.Visible = true;

            // Limpia estilos previos
            lblMensaje.CssClass = "mensaje-estilo";

            // Añade clase según el tipo
            switch (tipo.ToLower())
            {
                case "error":
                    lblMensaje.CssClass += " mensaje-error";
                    break;
                case "exito":
                    lblMensaje.CssClass += " mensaje-exito";
                    break;
                default:
                    lblMensaje.CssClass += " mensaje-informativo";
                    break;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    RecargarListaPaises();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error cargando países: {ex.Message}");
                }
            }
        }

        protected void btnCrearPais_Click(object sender, EventArgs e)
        {
            try
            {
                ENPais pais = new ENPais
                {
                    NombrePais = txtNuevoPais.Text.Trim()
                };

                bool ok = pais.CrearPais();
                if (ok)
                {
                    MostrarMensaje("País creado correctamente", tipo: "exito");
                    txtNuevoPais.Text = "";
                    RecargarListaPaises();
                }
            }
            catch (Exception ex)
            {
                MostrarMensaje("Error: El país ya existe o no es válido", tipo: "error");
            }
        }

        protected void btnCrearProvincia_Click(object sender, EventArgs e)
        {

        }

        protected void btnCrearMunicipio_Click(object sender, EventArgs e)
        {
        }

        protected void btnEliminarPais_Click(object sender, EventArgs e)
        {
            try
            {
                int idPais = Convert.ToInt32(ddlEliminarPais.SelectedValue); // Obtener ID
                string nombre = ddlEliminarPais.SelectedItem.Text;

                ENPais pais = new ENPais(idPais, nombre); // Usar constructor con ID
                bool resultado = pais.EliminarPais();

                if (resultado)
                {
                    MostrarMensaje("País eliminado correctamente", tipo: "exito");
                    RecargarListaPaises();
                }
                else
                {
                    MostrarMensaje("No se pudo eliminar el país (puede tener provincias asociadas)", tipo: "error");
                }
            }
            catch (Exception ex)
            {
                MostrarMensaje("Seleccione un Pais", tipo: "error");
            }
        }

        protected void btnEliminarProvincia_Click(object sender, EventArgs e)
        {
        }

        protected void btnEliminarMunicipio_Click(object sender, EventArgs e)
        {
        }

    }
}