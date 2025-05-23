using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using library;

namespace ProWeb
{
    /// <summary>
    /// Página para que un usuario realice apuestas en un evento deportivo.
    /// Permite seleccionar el resultado (local, empate o visitante), introducir la cantidad a apostar,
    /// mostrar saldo actual, cuotas y calcular ganancia potencial.
    /// </summary>
    public partial class ApuestaUsuario : System.Web.UI.Page
	{
        /// <summary>
        /// Evento que se ejecuta al cargar la página. Inicializa datos del usuario, apuesta
        /// y actualiza la interfaz con el saldo, cuotas y nombres de equipos.
        /// </summary>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    int idUsuario = ObtenerIdUsuarioActual();
                    int idApuesta = ObtenerIdApuestaActual();

                    ENUsuario usuario = new ENUsuario { ID = idUsuario };
                    if (usuario.Read())
                    {
                        lblSaldo.Text = usuario.Saldo.ToString("F2") + " €";
                    }
                    else
                    {
                        lblSaldo.Text = "No se pudo cargar el saldo.";
                        return;
                    }

                    ENApuesta apuesta = new ENApuesta { Id_apuesta = idApuesta };
                    if (apuesta.Read())
                    {

                        ENEquipo local = new ENEquipo { Id_equipo = apuesta.Equipo1.Id_equipo };
                        ENEquipo visitante = new ENEquipo { Id_equipo = apuesta.Equipo2.Id_equipo };

                        if (local.Read() && visitante.Read())
                        {
                            lblEquipoLocal.Text = local.Nombre;
                            lblEquipoVisitante.Text = visitante.Nombre;

                            // Guardar cuotas en ViewState para usarlas más adelante
                            ViewState["Cuota1"] = apuesta.cot1;
                            ViewState["CuotaX"] = apuesta.cotX;
                            ViewState["Cuota2"] = apuesta.cot2;

                            // Modificar texto de los items para que muestren nombre y cuota
                            rblOpcionesApuesta.Items.FindByValue("1").Text = $"{local.Nombre}";
                            rblOpcionesApuesta.Items.FindByValue("X").Text = $"Empate";
                            rblOpcionesApuesta.Items.FindByValue("2").Text = $"{visitante.Nombre}";

                            lblCuotaActual.Text = apuesta.cot1.ToString("F2");
                        }
                        else
                        {
                            lblEquipoLocal.Text = "Equipo local no encontrado";
                            lblEquipoVisitante.Text = "Equipo visitante no encontrado";
                        }
                    }
                }
                catch (Exception ex)
                {
                    lblSaldo.Text = "Error al cargar datos: " + ex.Message;
                }
            }
        }

        /// <summary>
        /// Actualiza la ganancia potencial cuando el usuario cambia la cantidad apostada.
        /// </summary>
        protected void txtCantidad_TextChanged(object sender, EventArgs e)
        {
            if (float.TryParse(txtCantidad.Text, NumberStyles.Any, CultureInfo.InvariantCulture, out float cantidad) &&
                float.TryParse(lblCuotaActual.Text, NumberStyles.Any, CultureInfo.InvariantCulture, out float cuota))
            {
                float ganancia = cantidad * cuota;
                lblGananciaPotencial.Text = ganancia.ToString("0.00") + " €";
            }
        }

        /// <summary>
        /// Evento que se ejecuta al pulsar el botón de apostar.
        /// Valida la apuesta, crea la apuesta, actualiza el saldo y muestra mensajes de confirmación o error.
        /// </summary>
        protected void btnApostar_Click(object sender, EventArgs e)
        {
            try
            {
                if (ValidarApuesta())
                {
                    int idUsuario = ObtenerIdUsuarioActual();
                    int idApuesta = ObtenerIdApuestaActual();
                    string prediccion = rblOpcionesApuesta.SelectedValue;
                    float cantidad = float.Parse(txtCantidad.Text);
                    float cuota = float.Parse(lblCuotaActual.Text);

                    // Leer saldo actualizado de BD
                    ENUsuario usuario = new ENUsuario { ID = idUsuario };
                    if (!usuario.Read())
                    {
                        mostrarMensaje("Error al leer saldo de usuario", "error");
                        return;
                    }

                    if (cantidad > usuario.Saldo)
                    {
                        mostrarMensaje("Saldo insuficiente.", "error");
                        return;
                    }

                    ENApuesta_usuario apuesta = new ENApuesta_usuario(idUsuario, idApuesta, prediccion, cantidad, cuota);
                    bool apuestaCreada = apuesta.Apostar();

                    if (apuestaCreada)
                    {
                        // Actualiza saldo en base de datos
                        usuario.Saldo -= cantidad;
                        usuario.Update();

                        lblSaldo.Text = usuario.Saldo.ToString("F2") + " €";

                        // Mensaje de confirmacion
                        mostrarMensaje("¡Apuesta realizada con éxito!", "exito");
                    }
                    else
                    {
                        mostrarMensaje("Ya ha jugado esta apuesta.", "error");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        /// <summary>
        /// Redirige a la página de juegos al cancelar la apuesta.
        /// </summary>
        protected void btnCancelar_Click(Object sender, EventArgs e) {
            Response.Redirect("Juegos.aspx");
        }

        /// <summary>
        /// Actualiza la cuota actual y la ganancia potencial cuando se cambia la opción de apuesta seleccionada.
        /// </summary>
        protected void rblOpcionesApuesta_SelectedIndexChanged(object sender, EventArgs e)
        {
            string seleccion = rblOpcionesApuesta.SelectedValue;
            float cuota = 0.0f;

            switch (seleccion)
            {
                case "1":
                    cuota = Convert.ToSingle(ViewState["Cuota1"]);
                    break;
                case "X":
                    cuota = Convert.ToSingle(ViewState["CuotaX"]);
                    break;
                case "2":
                    cuota = Convert.ToSingle(ViewState["Cuota2"]);
                    break;
            }

            lblCuotaActual.Text = cuota.ToString("F2");

            
            if (float.TryParse(txtCantidad.Text, NumberStyles.Any, CultureInfo.InvariantCulture, out float cantidad))
            {
                float ganancia = cantidad * cuota;
                lblGananciaPotencial.Text = ganancia.ToString("0.00") + " €";
            }
        }



        // Metodos auxiliares

        /// <summary>
        /// Valida los datos de la apuesta antes de procesarla:
        /// comprueba selección, cantidad válida y saldo suficiente.
        /// </summary>
        /// <returns>true si la apuesta es válida; false en caso contrario.</returns>
        private bool ValidarApuesta()
        {
            // Validar que se haya seleccionado una opción
            if (rblOpcionesApuesta.SelectedValue == null)
            {
                mostrarMensaje("Selecciona una predicción.", "error");
                return false;
            }

            // Validar que la cantidad sea un número positivo
            if (!float.TryParse(txtCantidad.Text, NumberStyles.Float, CultureInfo.InvariantCulture, out float cantidad) || cantidad <= 0)
            {
                mostrarMensaje("Cantidad inválida", "error");
                return false;
            }

            // Validar que el usuario tenga saldo suficiente
            ENUsuario usuario = new ENUsuario();
            usuario.ID = ObtenerIdUsuarioActual();
            if (!usuario.Read())
            {
                mostrarMensaje("Error al leer usuario para obtener saldo", "error");
                return false;
            }

            if (cantidad > usuario.Saldo)
            {
                mostrarMensaje("Saldo insuficiente.", "error");
                return false;
            }

            return true;
        }

        /// <summary>
        /// Muestra un mensaje de error en el panel de confirmación con estilo de alerta de error.
        /// </summary>
        /// <param name="mensaje">Texto del mensaje a mostrar.</param>
        private void mostrarMensaje(string mensaje, string tipo)
        {
            pnlConfirmacion.Visible = true;
            lblMensaje.Text = mensaje;

            if (tipo == "exito")
            {
                lblMensaje.CssClass = "MensajeExito";
            }
            else
            {
                lblMensaje.CssClass = "MensajeError";
            }
        }

        /// <summary>
        /// Obtiene el identificador del usuario actual de la sesión.
        /// </summary>
        /// <returns>ID del usuario.</returns>
        private int ObtenerIdUsuarioActual()
        {
            return Convert.ToInt32(Session["Login"]);
        }

        /// <summary>
        /// Obtiene el identificador de la apuesta actual desde la query string.
        /// </summary>
        /// <returns>ID de la apuesta.</returns>
        private int ObtenerIdApuestaActual()
        {
            return Convert.ToInt32(Request.QueryString["idApuesta"]);
        }

    }
}