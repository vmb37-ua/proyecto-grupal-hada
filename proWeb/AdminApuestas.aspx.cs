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
                CargarApuestas(); // nuevo
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

        private void CargarApuestas()
        {
            ENApuesta ap = new ENApuesta();
            var lista = ap.ReadAll(); // asegúrate de que este método existe

            ddlApuestas.DataSource = lista;
            ddlApuestas.DataTextField = "Id_apuesta";
            ddlApuestas.DataValueField = "Id_apuesta";
            ddlApuestas.DataBind();
        }

        protected void btnCrear_Click(object sender, EventArgs e)
        {
            try
            {
                ENApuesta ap = new ENApuesta
                {
                    
                    Fecha = DateTime.Parse(txtFecha.Text),
                    Estadio = new ENEstadio { Nombre = ddlEstadios.SelectedValue },
                    Equipo1 = new ENEquipo { Id_equipo = int.Parse(ddlEquipo1.SelectedValue) },
                    Equipo2 = new ENEquipo { Id_equipo = int.Parse(ddlEquipo2.SelectedValue) },
                    cot1 = double.Parse(txtCot1.Text),
                    cot2 = double.Parse(txtCot2.Text),
                    cotX = double.Parse(txtCotX.Text)
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
            try
            {
                ENApuesta ap = new ENApuesta
                {
                    Id_apuesta = int.Parse(ddlApuestas.SelectedValue), // <-- Añadido aquí
                    Fecha = DateTime.Parse(txtFecha.Text),
                    Estadio = new ENEstadio { Nombre = ddlEstadios.SelectedValue },
                    Equipo1 = new ENEquipo { Id_equipo = int.Parse(ddlEquipo1.SelectedValue) },
                    Equipo2 = new ENEquipo { Id_equipo = int.Parse(ddlEquipo2.SelectedValue) },
                    cot1 = double.Parse(txtCot1.Text),
                    cot2 = double.Parse(txtCot2.Text),
                    cotX = double.Parse(txtCotX.Text)
                };

                if (ap.Update())
                    MostrarMensaje("Apuesta actualizada correctamente.");
                else
                    MostrarMensaje("Error al actualizar la apuesta.");
            }
            catch (Exception ex)
            {
                MostrarMensaje("ERROR SQL: " + ex.Message.Replace("'", ""));
            }
        }



        protected void btnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                ENApuesta ap = new ENApuesta
                {
                    Id_apuesta = int.Parse(ddlApuestas.SelectedValue)
                };

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


        protected void ddlApuestas_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                int id = int.Parse(ddlApuestas.SelectedValue);
                ENApuesta ap = new ENApuesta { Id_apuesta = id };

                if (ap.Read())
                {
                    txtFecha.Text = ap.Fecha.ToString("yyyy-MM-dd");
                    ddlEstadios.SelectedValue = ap.Estadio.Nombre;
                    ddlEquipo1.SelectedValue = ap.Equipo1.Id_equipo.ToString();
                    ddlEquipo2.SelectedValue = ap.Equipo2.Id_equipo.ToString();
                    txtCot1.Text = ap.cot1.ToString();
                    txtCot2.Text = ap.cot2.ToString();
                    txtCotX.Text = ap.cotX.ToString();
                }
            }
            catch (Exception ex)
            {
                MostrarMensaje("Error al cargar datos de la apuesta: " + ex.Message);
            }
        }

        private void MostrarMensaje(string mensaje)
        {
            lblMensaje.Text = mensaje;
        }
    }
}