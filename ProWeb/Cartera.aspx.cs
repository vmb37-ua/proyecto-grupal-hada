using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using library;

namespace ProWeb
{
    public partial class Cartera : Page
    {
        private const float SALDO_MAXIMO = 10000f;
        private const float SALDO_MINIMO = 0f;

        private ENUsuario usuarioActual;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Login"] == null)
            {
                Response.Redirect("Login.aspx");
                return;
            }

            int idUsuario = (int)Session["Login"];
            usuarioActual = new ENUsuario { ID = idUsuario };
            if (usuarioActual.Read())
            {
                if (!IsPostBack)
                {
                    DineroDisponible.Text = usuarioActual.Saldo.ToString("F2") + " €";
                }
            }
            else
            {
                MensajeOperacion.Text = "No se pudo cargar la información del usuario.";
                MensajeOperacion.ForeColor = System.Drawing.Color.Red;
            }
        }

        protected void Ingresar_Click(object sender, EventArgs e)
        {
            if (!float.TryParse(CantidadIngresar.Text, out float cantidad) || cantidad <= 0)
            {
                MensajeOperacion.Text = "Introduce una cantidad válida.";
                MensajeOperacion.ForeColor = System.Drawing.Color.Red;
                return;
            }

            int idUsuario = (int)Session["Login"];
            usuarioActual.ID = idUsuario;

            if (usuarioActual.Read())
            {
                float nuevoSaldo = usuarioActual.Saldo + cantidad;

                if (nuevoSaldo > SALDO_MAXIMO)
                {
                    MensajeOperacion.Text = "No puedes superar el saldo máximo de 10,000.";
                    MensajeOperacion.ForeColor = System.Drawing.Color.Red;
                }
                else
                {
                    usuarioActual.Saldo = nuevoSaldo;
                    if (usuarioActual.Update())
                    {
                        DineroDisponible.Text = nuevoSaldo.ToString("F2") + " €";
                        MensajeOperacion.Text = "Saldo ingresado correctamente.";
                        MensajeOperacion.ForeColor = System.Drawing.Color.Green;

                        ENTransaccion transaccion = new ENTransaccion(0, usuarioActual.ID, cantidad, "Ingreso");
                        transaccion.Create();
                    }
                    else
                    {
                        MensajeOperacion.Text = "Error al actualizar el saldo.";
                        MensajeOperacion.ForeColor = System.Drawing.Color.Red;
                    }
                }
            }
        }

        protected void Retirar_Click(object sender, EventArgs e)
        {
            if (!float.TryParse(CantidadRetirar.Text, out float cantidad) || cantidad <= 0)
            {
                MensajeOperacion.Text = "Introduce una cantidad válida.";
                MensajeOperacion.ForeColor = System.Drawing.Color.Red;
                return;
            }

            int idUsuario = (int)Session["Login"];
            usuarioActual.ID = idUsuario;

            if (usuarioActual.Read())
            {
                float saldoActual = usuarioActual.Saldo;

                if (cantidad > saldoActual)
                {
                    MensajeOperacion.Text = "No tienes suficiente saldo.";
                    MensajeOperacion.ForeColor = System.Drawing.Color.Red;
                }
                else
                {
                    float nuevoSaldo = saldoActual - cantidad;
                    usuarioActual.Saldo = nuevoSaldo;
                    if (usuarioActual.Update())
                    {
                        DineroDisponible.Text = nuevoSaldo.ToString("F2") + " €";
                        MensajeOperacion.Text = "Has retirado dinero correctamente.";
                        MensajeOperacion.ForeColor = System.Drawing.Color.Green;

                        ENTransaccion transaccion = new ENTransaccion(0, usuarioActual.ID, cantidad, "Retiro");
                        transaccion.Create();
                    }
                    else
                    {
                        MensajeOperacion.Text = "Error al actualizar el saldo.";
                        MensajeOperacion.ForeColor = System.Drawing.Color.Red;
                    }
                }
            }
        }

        protected void Volver_Click(object sender, EventArgs e)
        {
            Response.Redirect("Perfil.aspx");
        }
    }
}
