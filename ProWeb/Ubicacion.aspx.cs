using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
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

                ddlEliminarPais.Items.Insert(0, new ListItem("-- Seleccione un país --", "0"));


                ddlPaisProvincia.DataSource = paises;
                ddlPaisProvincia.DataTextField = "NombrePais";
                ddlPaisProvincia.DataValueField = "IdPais";
                ddlPaisProvincia.DataBind();

                ddlPaisProvincia.Items.Insert(0, new ListItem("-- Seleccione un país --", "0"));

            }
            catch (Exception ex)
            {
                MostrarMensaje($"Error cargando países: {ex.Message}", "error");
            }
        }

        private void CargarProvincias(int idPais)
        {
            try
            {
                lblMensaje.Visible = false;

                CADProvincia cadProvincia = new CADProvincia();
                ENProvincia en = new ENProvincia();
                en.IdPais = idPais;
                List<ENProvincia> provincias = cadProvincia.ReadAllByPais(en);

                ddlEliminarProvincia.DataSource = provincias;
                ddlEliminarProvincia.DataTextField = "Nombre";
                ddlEliminarProvincia.DataValueField = "IdProvincia";
                ddlEliminarProvincia.DataBind();

                ddlEliminarProvincia.Items.Insert(0, new ListItem("-- Seleccione una provincia --", "0"));


                ddlProvinciaMunicipio.DataSource = provincias;
                ddlProvinciaMunicipio.DataTextField = "Nombre";
                ddlProvinciaMunicipio.DataValueField = "IdProvincia";
                ddlProvinciaMunicipio.DataBind();

                ddlProvinciaMunicipio.Items.Insert(0, new ListItem("-- Seleccione una provincia --", "0"));

            }
            catch (Exception ex)
            {
                MostrarMensaje($"Error cargando provincias: {ex.Message}", "error");
            }
        }

        private void CargarMunicipios(int idProvincia)
        {
            try
            {
                lblMensaje.Visible = false;

                CADMunicipio cadMunicipio = new CADMunicipio();
                ENMunicipio en = new ENMunicipio();
                en.Id_provincia = idProvincia;
                List<ENMunicipio> provincias = cadMunicipio.ReadAll(en);

                ddlEliminarMunicipio.DataSource = provincias;
                ddlEliminarMunicipio.DataTextField = "Nombre";
                ddlEliminarMunicipio.DataValueField = "Id_municipio";
                ddlEliminarMunicipio.DataBind();

                ddlEliminarMunicipio.Items.Insert(0, new ListItem("-- Seleccione un municipio --", "0"));

            }
            catch (Exception ex)
            {
                MostrarMensaje($"Error cargando provincias: {ex.Message}", "error");
            }
        }

        private void MostrarMensaje(string texto, string tipo = "informativo")
        {
            lblMensaje.Text = texto;
            lblMensaje.Visible = true;

            
            lblMensaje.CssClass = "mensaje-estilo";

            
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

                    if (ddlPaisProvincia.Items.Count > 0)
                    {
                        int idPais = Convert.ToInt32(ddlPaisProvincia.SelectedValue);
                        CargarProvincias(idPais);

                        if (ddlProvinciaMunicipio.Items.Count > 0)
                        {
                            int idProvincia = Convert.ToInt32(ddlProvinciaMunicipio.SelectedValue);
                            CargarMunicipios(idProvincia);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error cargando datos iniciales: {ex.Message}");
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

                bool ok = pais.Create();
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
            try
            {
                
                if (string.IsNullOrWhiteSpace(txtNuevoProvincia.Text))
                {
                    MostrarMensaje("Debe ingresar un nombre para la provincia", tipo: "error");
                    return;
                }

                
                if (ddlPaisProvincia.SelectedValue == "0" || string.IsNullOrEmpty(ddlPaisProvincia.SelectedValue))
                {
                    MostrarMensaje("Debe seleccionar un país", tipo: "error");
                    return;
                }

                int idPais = Convert.ToInt32(ddlPaisProvincia.SelectedValue);
                int idProvincia = Convert.ToInt32(ddlProvinciaMunicipio.SelectedValue);

                ENProvincia provincia = new ENProvincia
                {
                    Nombre = txtNuevoProvincia.Text.Trim(),
                    IdPais = idPais  
                };

                CADProvincia cadProvincia = new CADProvincia();
                bool ok = cadProvincia.Create(provincia);  

                if (ok)
                {
                    MostrarMensaje("Provincia creada correctamente", tipo: "exito");
                    txtNuevoProvincia.Text = "";

                    CargarProvincias(idPais);
                    CargarMunicipios(idProvincia);
                }
            }
            catch (Exception ex)
            {
                MostrarMensaje("Error al crear la provincia", tipo: "error");
                Console.WriteLine("Errol al crear la provincia" + ex.Message);
            }
        }

        protected void btnCrearMunicipio_Click(object sender, EventArgs e)
        {
            try
            {
                
                if (string.IsNullOrWhiteSpace(txtNuevoMunicipio.Text))
                {
                    MostrarMensaje("Debe ingresar un nombre para el municipio", tipo: "error");
                    return;
                }

                
                if (ddlProvinciaMunicipio.SelectedValue == "0" || string.IsNullOrEmpty(ddlProvinciaMunicipio.SelectedValue))
                {
                    MostrarMensaje("Debe seleccionar una provincia", tipo: "error");
                    return;
                }

                int idProvincia = Convert.ToInt32(ddlProvinciaMunicipio.SelectedValue);

                ENMunicipio municipio = new ENMunicipio
                {
                    Nombre = txtNuevoMunicipio.Text.Trim(),
                    Id_provincia = idProvincia  
                };

                CADMunicipio cadMunicipio = new CADMunicipio();
                bool ok = cadMunicipio.Create(municipio);

                if (ok)
                {
                    MostrarMensaje("Municipio creado correctamente", tipo: "exito");
                    txtNuevoMunicipio.Text = "";
                    CargarMunicipios(idProvincia);
                }
            }
            catch (Exception ex)
            {
                MostrarMensaje("Error al crear la provincia", tipo: "error");
            }
        }

        protected void btnEliminarPais_Click(object sender, EventArgs e)
        {
            if (ddlEliminarPais.SelectedValue == "0")
            {
                MostrarMensaje("Seleccione un país válido", "error");
                return;
            }

            try
            {
                int idPais = Convert.ToInt32(ddlEliminarPais.SelectedValue);
                ENPais pais = new ENPais { IdPais = idPais };

                if (pais.Delete())
                {
                    MostrarMensaje("País eliminado correctamente", "exito");
                    RecargarListaPaises();
                }
                else
                {
                    MostrarMensaje("No se pudo eliminar el país", "error");
                }
            }
            catch (Exception ex)
            {
                MostrarMensaje($"Error al eliminar el país: {ex.Message}", "error");
            }
        }

        protected void btnEliminarProvincia_Click(object sender, EventArgs e)
        {
            if (ddlEliminarProvincia.SelectedValue == "0")
            {
                MostrarMensaje("Seleccione una provincia válida", "error");
                return;
            }

            try
            {
                int idProvincia = Convert.ToInt32(ddlEliminarProvincia.SelectedValue);
                ENProvincia en = new ENProvincia();
                en.IdProvincia = idProvincia;
                CADProvincia cadProvincia = new CADProvincia();

                if (cadProvincia.Delete(en))
                {
                    MostrarMensaje("Provincia eliminada correctamente", "exito");
                    // Recargar dropdowns
                    CargarProvincias(Convert.ToInt32(ddlPaisProvincia.SelectedValue));
                    CargarMunicipios(Convert.ToInt32(ddlProvinciaMunicipio.SelectedValue));
                }
                else
                {
                    MostrarMensaje("No se pudo eliminar la provincia", "error");
                }
            }
            catch (SqlException sqlEx)
            {
                MostrarMensaje($"Error de base de datos: {sqlEx.Message}", "error");
            }
            catch (Exception ex)
            {
                MostrarMensaje($"Error inesperado: {ex.Message}", "error");
            }
        }

        protected void btnEliminarMunicipio_Click(object sender, EventArgs e)
        {
            if (ddlEliminarMunicipio.SelectedValue == "0" || string.IsNullOrEmpty(ddlEliminarMunicipio.SelectedValue))
            {
                lblMensaje.Text = "Seleccione un municipio válido";
                return;
            }

            try
            {
                int idMunicipio = Convert.ToInt32(ddlEliminarMunicipio.SelectedValue);
                int idProvincia = Convert.ToInt32(ddlProvinciaMunicipio.SelectedValue);
                ENMunicipio en = new ENMunicipio();
                en.Id_municipio = idMunicipio;
                CADMunicipio cadMunicipio = new CADMunicipio();

                if (cadMunicipio.Delete(en))
                {
                    lblMensaje.Text = "Municipio eliminado correctamente";
                    lblMensaje.CssClass = "text-success";
                    CargarMunicipios(idProvincia);
                }
                else
                {
                    lblMensaje.Text = "No se pudo eliminar el municipio";
                }
            }
            catch (SqlException sqlEx)
            {
                lblMensaje.Text = $"Error de base de datos: {sqlEx.Message}";
            }
            catch (Exception ex)
            {
                lblMensaje.Text = $"Error inesperado: {ex.Message}";
            }
        }

        // Evento cuando se selecciona un país
        protected void ddlPaises_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlPaisProvincia.SelectedValue != "0")
            {
                int idPais = Convert.ToInt32(ddlPaisProvincia.SelectedValue);
                CargarProvincias(idPais); // Carga las provincias del país seleccionado

                // Solo si el dropdown de provincias tiene items y el SelectedValue es válido, cargo municipios
                if (ddlProvinciaMunicipio.Items.Count > 0 && ddlProvinciaMunicipio.SelectedValue != "0")
                {
                    int idProvincia = Convert.ToInt32(ddlProvinciaMunicipio.SelectedValue);
                    CargarMunicipios(idProvincia);
                }
                else
                {
                    // Si no hay provincias, limpiar dropdown de municipios para que no quede con datos inválidos
                    ddlEliminarMunicipio.Items.Clear();
                    ddlEliminarMunicipio.Items.Add(new ListItem("--Seleccione municipio--", "0"));
                }
            }
        }

        // Evento cundo se selecciona una provincia
        protected void ddlProvincias_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlProvinciaMunicipio.SelectedValue != "0")
            {
                int idProvincia = Convert.ToInt32(ddlProvinciaMunicipio.SelectedValue);
                CargarMunicipios(idProvincia); // Carga solo los municipios de la provincia seleccionado

            }
        }


    }
}