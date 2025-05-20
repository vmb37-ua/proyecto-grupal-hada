using System;
using System.Data.SqlClient;
using System.Web.UI;
using library;

namespace ProWeb
{
    public partial class AdminEstadios : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarMunicipios();
            }
        }


        protected void btnCrear_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtNombre.Text))
                {
                    MostrarMensaje("El nombre del estadio no puede estar vacío.");
                    return;
                }

                if (!int.TryParse(txtCapacidad.Text, out int capacidad))
                {
                    MostrarMensaje("La capacidad debe ser un número válido.");
                    return;
                }

                if (!int.TryParse(ddlMunicipios.SelectedValue, out int idMunicipio))
                {
                    MostrarMensaje("Debes seleccionar un municipio válido.");
                    return;
                }

                ENEstadio estadio = new ENEstadio
                {
                    Nombre = txtNombre.Text,
                    Capacidad = capacidad,
                    Texto = txtTexto.Text,
                    Id_municipio = idMunicipio
                };

                if (estadio.Create())
                    MostrarMensaje("Estadio creado correctamente.");
                else
                    MostrarMensaje("Error al crear el estadio.");
            }
            catch (Exception ex)
            {
                MostrarMensaje("ERROR SQL: " + ex.Message.Replace("'", ""));
            }
        }




        protected void btnActualizar_Click(object sender, EventArgs e)
        {
            if (int.TryParse(txtCapacidad.Text, out int capacidad) &&
                int.TryParse(ddlMunicipios.SelectedValue, out int idMunicipio))
            {
                ENEstadio estadio = new ENEstadio
                {
                    Nombre = txtNombre.Text,
                    Capacidad = capacidad,
                    Texto = txtTexto.Text,
                    Id_municipio = idMunicipio
                };

                if (estadio.Update())
                    MostrarMensaje("Estadio actualizado correctamente.");
                else
                    MostrarMensaje("Error al actualizar el estadio.");
            }
            else
            {
                MostrarMensaje("Capacidad e ID de municipio deben ser números válidos.");
            }
        }

        protected void btnEliminar_Click(object sender, EventArgs e)
        {
            ENEstadio estadio = new ENEstadio
            {
                Nombre = txtNombre.Text
            };

            if (estadio.Delete())
                MostrarMensaje("Estadio eliminado correctamente.");
            else
                MostrarMensaje("Error al eliminar el estadio.");
        }

        private void MostrarMensaje(string mensaje)
        {
            lblMensaje.Text = mensaje;
        }
        private void CargarMunicipios()
        {
            string connStr = System.Configuration.ConfigurationManager.ConnectionStrings["miconex"].ToString();
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                string query = "SELECT id_municipio, nombre FROM municipio";
                SqlCommand cmd = new SqlCommand(query, conn);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                ddlMunicipios.DataSource = reader;
                ddlMunicipios.DataTextField = "nombre";
                ddlMunicipios.DataValueField = "id_municipio";
                ddlMunicipios.DataBind();
            }
        }

    }
}