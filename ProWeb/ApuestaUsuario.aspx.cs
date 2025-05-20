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
	public partial class ApuestaUsuario : System.Web.UI.Page
	{

        private bool ValidarApuesta()
        {
            // 1. Validar que se haya seleccionado una opción
            if (rblOpcionesApuesta.SelectedValue == null)
            {
                MostrarError("Selecciona una predicción.");
                return false;
            }

            // 2. Validar que la cantidad sea un número positivo
            if (!decimal.TryParse(txtCantidad.Text, out decimal cantidad) || cantidad <= 0)
            {
                MostrarError("Ingresa una cantidad válida (ej: 10.50).");
                return false;
            }

            // 3. Validar que el usuario tenga saldo suficiente
            decimal saldoActual = (decimal)Session["saldo"];
            if (cantidad > saldoActual)
            {
                MostrarError("Saldo insuficiente.");
                return false;
            }

            return true;
        }

        // Método auxiliar para mostrar errores
        private void MostrarError(string mensaje)
        {
            pnlConfirmacion.Visible = true;
            lblMensajeExito.Text = mensaje;
            pnlConfirmacion.CssClass = "alert alert-danger";
        }


        private int ObtenerIdUsuarioActual()
        {
            if (Session["id_usuario"] != null)
                return Convert.ToInt32(Session["id_usuario"]);
            else
                throw new Exception("Usuario no logueado.");
        }

        private int ObtenerIdApuestaActual()
        {
            if (Request.QueryString["id_apuesta"] != null)
                return Convert.ToInt32(Request.QueryString["id_apuesta"]);
            else
                throw new Exception("Apuesta no especificada.");
        }


        protected void Page_Load(object sender, EventArgs e)
		{
            if (!IsPostBack)
            {
                int idUsuario = Convert.ToInt32(Session["Login"]);
                decimal saldoBD = 110;
                Session["saldo"] = saldoBD;
                lblSaldo.Text = saldoBD.ToString();
            }
		}

        protected void txtCantidad_TextChanged(object sender, EventArgs e)
        {
            if (decimal.TryParse(txtCantidad.Text, out decimal cantidad) &&
                decimal.TryParse(lblCuotaActual.Text, NumberStyles.Any, CultureInfo.InvariantCulture, out decimal cuota))
            {
                decimal ganancia = cantidad * cuota;
                lblGananciaPotencial.Text = ganancia.ToString("0.00") + " €";
            }
        }

        protected void btnApostar_Click(object sender, EventArgs e)
        {

            try
            {
                if (ValidarApuesta())
                {
                    int idUsuario = ObtenerIdUsuarioActual();
                    int idApuesta = ObtenerIdApuestaActual();
                    string prediccion = rblOpcionesApuesta.SelectedValue;
                    decimal cantidad = decimal.Parse(txtCantidad.Text);
                    decimal cuota = decimal.Parse(lblCuotaActual.Text, CultureInfo.InvariantCulture);


                    ENApuesta_usuario apuesta = new ENApuesta_usuario(idUsuario, idApuesta, prediccion, cantidad, cuota);
                    bool apuestaCreada = apuesta.Apostar();

                    if (apuestaCreada)
                    {
                        pnlConfirmacion.Visible = true;
                        lblMensajeExito.Text = "¡Apuesta realizada con éxito!";

                        //Actualizar saldo
                    }
                    else
                    {
                        lblMensajeExito.Text = "Error al realizar la apuesta.";
                        pnlConfirmacion.Visible = true;
                    }
                }
                else
                {
                    lblMensajeExito.Text = "Compruebe y rellene todos los campos";
                }
            }
            catch (Exception ex)
            {
                lblMensajeExito.Text = "Error inesperado: " + ex.Message;
                pnlConfirmacion.Visible = true;
            }
        }

        

        protected void btnCancelar_Click(Object sender, EventArgs e) {
            Response.Redirect("Juegos.aspx");
        }

		protected void rblOpcionesApuesta_SelectedIndexChanged(object sender, EventArgs e) { }

		

    }
}