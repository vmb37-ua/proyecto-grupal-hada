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

        protected void Page_Load(object sender, EventArgs e)
		{
            if (!IsPostBack)
            {
                try
                {
                    int idUsuario = ObtenerIdUsuarioActual();
                    int idApuesta = ObtenerIdApuestaActual();

                    Console.WriteLine("ID de apuesta recibido: " + idApuesta);//DEBUG

                    ENUsuario usuario = new ENUsuario { ID = idUsuario };

                    if (usuario.Read())
                    {
                        Session["Login"] = usuario.Saldo;
                        lblSaldo.Text = usuario.Saldo.ToString("F2") + " €";
                        Console.WriteLine("Saldo del usuario: " + usuario.Saldo);
                    }
                    else
                    {
                        lblSaldo.Text = "No se pudo cargar el saldo(PageLoad).";
                    }
                }
                catch (Exception ex)
                {
                    lblSaldo.Text = "Error al cargar saldo: " + ex.Message;
                }
            }
		}

        protected void txtCantidad_TextChanged(object sender, EventArgs e)
        {
            if (float.TryParse(txtCantidad.Text, NumberStyles.Any, CultureInfo.InvariantCulture, out float cantidad) &&
                float.TryParse(lblCuotaActual.Text, NumberStyles.Any, CultureInfo.InvariantCulture, out float cuota))
            {
                float ganancia = cantidad * cuota;
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
                        // Actualiza el saldo en la base de datos
                        float saldoActual = 0;

                        ENUsuario us = new ENUsuario();
                        if (us.Read())
                        {
                            saldoActual = us.Saldo;
                            float nuevoSaldo = saldoActual - cantidad;
                            ActualizarSaldo(idUsuario, nuevoSaldo);
                        }

                        // Mensaje de confirmacion
                        lblMensajeExito.Text = "¡Apuesta realizada con éxito!";
                        pnlConfirmacion.Visible = true;
                        pnlConfirmacion.CssClass = "alert alert-success";
                    }
                    else
                    {
                        lblMensajeExito.Text = "Error al realizar la apuesta.";
                    }
                }
            }
            catch (Exception ex)
            {
                lblMensajeExito.Text = "Error inesperado: " + ex.Message;
            }
        }

        protected void btnCancelar_Click(Object sender, EventArgs e) {
            Response.Redirect("Juegos.aspx");
        }

		protected void rblOpcionesApuesta_SelectedIndexChanged(object sender, EventArgs e) { }


        // Metodos auxiliares

        private bool ValidarApuesta()
        {
            // Validar que se haya seleccionado una opción
            if (rblOpcionesApuesta.SelectedValue == null)
            {
                MostrarError("Selecciona una predicción.");
                return false;
            }

            // Validar que la cantidad sea un número positivo
            if (!float.TryParse(txtCantidad.Text, out float cantidad) || cantidad <= 0)
            {
                MostrarError("Ingresa una cantidad válida (ej: 10.50).");
                return false;
            }

            // Validar que el usuario tenga saldo suficiente
            float saldoActual = 0;

            ENUsuario us = new ENUsuario();
            if(us.Read())
            {
                saldoActual = us.Saldo;

                if (cantidad > saldoActual)
                {
                    MostrarError("Saldo insuficiente.");
                    return false;
                }
            }
            else
            {
                MostrarError("Error al leer el usuaro para obtener el saldo");
            }



                return true;
        }

        private void MostrarError(string mensaje)
        {
            pnlConfirmacion.Visible = true;
            lblMensajeExito.Text = mensaje;
            pnlConfirmacion.CssClass = "alert alert-danger";
        }

        private int ObtenerIdUsuarioActual()
        {
            return Convert.ToInt32(Session["Login"]);
        }

        private int ObtenerIdApuestaActual()
        {
            return Convert.ToInt32(Request.QueryString["id_apuesta"]); //Cambiar esto por el correcto
        }

        private void ActualizarSaldo(int idUsuario, float cantidad)
        {
            var usuario = new ENUsuario();
            usuario.ID = idUsuario;
            if (usuario.Read())
            {
                usuario.Saldo -= cantidad;
                usuario.Update();
                Session["Login"] = usuario.Saldo;
            }
        }

    }
}