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
            if (!float.TryParse(txtCantidad.Text, out float cantidad) || cantidad <= 0)
            {
                MostrarError("Ingresa una cantidad válida (ej: 10.50).");
                return false;
            }

            // 3. Validar que el usuario tenga saldo suficiente
            float saldoActual = (float)Session["saldo"];
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
            return Convert.ToInt32(Session["id_usuario"]);   
        }

        private int ObtenerIdApuestaActual()
        { 
            return Convert.ToInt32(Request.QueryString["id_apuesta"]);
        }

        private void ActualizarSaldo(int idUsuario, float nuevoSaldo)
        {
            
        }


        protected void Page_Load(object sender, EventArgs e)
		{
            if (!IsPostBack)
            {
                int idUsuario = Convert.ToInt32(Session["Login"]);
                float saldoBD = 110;
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
                    float cantidad = float.Parse(txtCantidad.Text);
                    float cuota = float.Parse(lblCuotaActual.Text);


                    ENApuesta_usuario apuesta = new ENApuesta_usuario(idUsuario, idApuesta, prediccion, cantidad, cuota);
                    bool apuestaCreada = apuesta.Apostar();

                    if (apuestaCreada)
                    {
                        pnlConfirmacion.Visible = true;
                        

                        //Actualizar saldo
                        ENUsuario usuario = new ENUsuario();
                        usuario.ID = idUsuario;

                        if (usuario.Read())
                        {
                            usuario.Saldo -= cantidad;
                            usuario.Update();

                            lblSaldo.Text = usuario.Saldo.ToString("F2") + " €";
                            lblMensajeExito.Text = "¡Apuesta realizada con éxito!";
                        }
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