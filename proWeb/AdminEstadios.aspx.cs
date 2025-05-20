using System;
using System.Web.UI;
using library;

namespace ProWeb
{
    public partial class AdminEstadios : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
           
        }

        protected void btnCrear_Click(object sender, EventArgs e)
        {
            try
            {
                if (int.TryParse(txtCapacidad.Text, out int capacidad) &&
                    int.TryParse(txtIdMunicipio.Text, out int idMunicipio))
                {
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
                        MostrarMensaje("No se pudo crear el estadio.");
                }
                else
                {
                    MostrarMensaje("Capacidad e ID de municipio deben ser números válidos.");
                }
            }
            catch (Exception ex)
            {
                MostrarMensaje("ERROR SQL: " + ex.Message.Replace("'", ""));
            }
        }


        protected void btnActualizar_Click(object sender, EventArgs e)
        {
            if (int.TryParse(txtCapacidad.Text, out int capacidad) &&
                int.TryParse(txtIdMunicipio.Text, out int idMunicipio))
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

    }
}