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
            if (Session["Login"] == null)
            {
                Response.Redirect("Login.aspx");
                return;
            }
            // Solo cargamos los datos si no es un postback
            if (!IsPostBack)
            {
                CargarEquipos();
                CargarEstadios();
                CargarApuestas();
                CargarResultados(); // ← ¡esto debe existir!
            }

        }

        private void CargarResultados()
        {
            ddlResultado.Items.Clear();
            ddlResultado.Items.Add(new ListItem("Sin Resultado", ""));
            ddlResultado.Items.Add(new ListItem("Victoria Equipo 1", "1"));
            ddlResultado.Items.Add(new ListItem("Empate", "X"));
            ddlResultado.Items.Add(new ListItem("Victoria Equipo 2", "2"));

        }

        private void CargarEquipos()
        {
            // Leemos todos los equipos y los asignamos a los dos dropdowns
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
            // Cargamos todos los estadios en el dropdown
            ENEstadio es = new ENEstadio();
            var lista = es.ReadAll();

            ddlEstadios.DataSource = lista;
            ddlEstadios.DataTextField = "Nombre";
            ddlEstadios.DataValueField = "Nombre"; // Usamos nombre porque es la PK
            ddlEstadios.DataBind();
        }

        private void CargarApuestas()
        {
            ENApuesta ap = new ENApuesta();
            var lista = ap.ReadAll();

            ddlApuestas.Items.Clear();

            foreach (var apuesta in lista)
            {
                // Formato: 14/04/2020 (ID: 9)
                string textoVisible = $"{apuesta.Fecha:dd/MM/yyyy} (ID: {apuesta.Id_apuesta})";
                ddlApuestas.Items.Add(new ListItem(textoVisible, apuesta.Id_apuesta.ToString()));
            }
        }


        protected void btnCrear_Click(object sender, EventArgs e)
        {
            try
            {
                string res = ddlResultado.SelectedValue;
                // No validar si es vacío, ya que "Sin Resultado" es válido
                if (res.Length > 1)
                {
                    MostrarMensaje("Error: Resultado demasiado largo ('" + res + "')");
                    return;
                }


                // Creamos una nueva apuesta a partir de los datos del formulario
                ENApuesta ap = new ENApuesta
                {
                    Fecha = DateTime.Parse(txtFecha.Text),
                    Estadio = new ENEstadio { Nombre = ddlEstadios.SelectedValue },
                    Equipo1 = new ENEquipo { Id_equipo = int.Parse(ddlEquipo1.SelectedValue) },
                    Equipo2 = new ENEquipo { Id_equipo = int.Parse(ddlEquipo2.SelectedValue) },
                    cot1 = double.Parse(txtCot1.Text),
                    cot2 = double.Parse(txtCot2.Text),
                    cotX = double.Parse(txtCotX.Text),
                    Resultado = ddlResultado.SelectedValue


                };

                if (ap.Create())
                {
                    MostrarMensaje("Apuesta creada correctamente.");
                    CargarApuestas();
                }
                else
                {
                    MostrarMensaje("Error al crear la apuesta.");
                }

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
                string res = ddlResultado.SelectedValue;
                // No validar si es vacío, ya que "Sin Resultado" es válido
                if (res.Length > 1)
                {
                    MostrarMensaje("Error: Resultado demasiado largo ('" + res + "')");
                    return;
                }

                // Actualizamos la apuesta seleccionada con los nuevos datos
                ENApuesta ap = new ENApuesta
                {
                    Id_apuesta = int.Parse(ddlApuestas.SelectedValue),
                    Fecha = DateTime.Parse(txtFecha.Text),
                    Estadio = new ENEstadio { Nombre = ddlEstadios.SelectedValue },
                    Equipo1 = new ENEquipo { Id_equipo = int.Parse(ddlEquipo1.SelectedValue) },
                    Equipo2 = new ENEquipo { Id_equipo = int.Parse(ddlEquipo2.SelectedValue) },
                    cot1 = double.Parse(txtCot1.Text),
                    cot2 = double.Parse(txtCot2.Text),
                    cotX = double.Parse(txtCotX.Text),
                    Resultado = ddlResultado.SelectedValue
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
                // Eliminamos la apuesta seleccionada
                ENApuesta ap = new ENApuesta
                {
                    Id_apuesta = int.Parse(ddlApuestas.SelectedValue)
                };

                if (ap.Delete())
                {
                    MostrarMensaje("Apuesta eliminada correctamente.");
                    CargarApuestas();
                }
                else
                {
                    MostrarMensaje("Error al eliminar la apuesta.");
                }

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
                // Cargamos los datos de la apuesta seleccionada para mostrarlos en el formulario
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
                    ddlResultado.SelectedValue = ap.Resultado;
                }
            }
            catch (Exception ex)
            {
                MostrarMensaje("Error al cargar datos de la apuesta: " + ex.Message);
            }
        }

        // Método auxiliar para mostrar mensajes en el formulario
        private void MostrarMensaje(string mensaje)
        {
            lblMensaje.Text = mensaje;
        }
    }
}