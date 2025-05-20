using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using library;

namespace ProWeb
{
    public partial class AdminApuestas : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarEquipos();
                CargarEstadios();
            }
        }

        private void CargarEquipos()
        {
            ENEquipo eq = new ENEquipo();
            var lista = eq.ReadAll();

            ddlEquipo1.DataSource = lista;
            ddlEquipo1.DataTextField = "Nombre";
            ddlEquipo1.DataValueField = "Id_equipo";
            ddlEquipo1.DataBind();

            ddlEquipo2.DataSource = lista;
            ddlEquipo2.DataTextField = "Nombre";
            ddlEquipo2.DataValueField = "Id_equipo";
            ddlEquipo2.DataBind();
        }

        private void CargarEstadios()
        {
            ENEstadio es = new ENEstadio();
            var lista = es.ReadAll();

            ddlEstadios.DataSource = lista;
            ddlEstadios.DataTextField = "Nombre";
            ddlEstadios.DataValueField = "Nombre"; // PK = nombre en base de datos
            ddlEstadios.DataBind();
        }

        protected void btnCrear_Click(object sender, EventArgs e)
        {
            try
            {
                ENApuesta ap = new ENApuesta
                {
                    Id_apuesta = int.Parse(txtIdApuesta.Text),
                    Fecha = DateTime.Parse(txtFecha.Text),
                    Estadio = new ENEstadio { Nombre = ddlEstadios.SelectedValue },
                    Equipo1 = new ENEquipo { Id_equipo = int.Parse(ddlEquipo1.SelectedValue) },
                    Equipo2 = new ENEquipo { Id_equipo = int.Parse(ddlEquipo2.SelectedValue) },
                    Resultado = int.Parse(txtResultado.Text)
                };

                if (ap.Create())
                    MostrarMensaje("Apuesta creada correctamente.");
                else
                    MostrarMensaje("Error al crear la apuesta.");
            }
            catch (Exception ex)
            {
                MostrarMensaje("ERROR SQL: " + ex.Message.Replace("'", ""));
            }
        }

        protected void btnActualizar_Click(object sender, EventArgs e)
        {
            // Similar a crear, solo llama ap.Update() en vez de Create()
        }

        protected void btnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                ENApuesta ap = new ENApuesta { Id_apuesta = int.Parse(txtIdApuesta.Text) };

                if (ap.Delete())
                    MostrarMensaje("Apuesta eliminada correctamente.");
                else
                    MostrarMensaje("Error al eliminar la apuesta.");
            }
            catch (Exception ex)
            {
                MostrarMensaje("ERROR: " + ex.Message.Replace("'", ""));
            }
        }

        private void MostrarMensaje(string mensaje)
        {
            lblMensaje.Text = mensaje;
        }
    }
}