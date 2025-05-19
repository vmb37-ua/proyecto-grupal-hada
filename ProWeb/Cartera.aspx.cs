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
        private const decimal SALDO_MAXIMO = 10000m;
        private const decimal SALDO_MINIMO = 0m;

        private ENUsuario usuarioActual;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["id_usuario"] == null)
            {
                Response.Redirect("Login.aspx"); 
                return;
            }

            int idUsuario = (int)Session["id_usuario"];
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
            if (!decimal.TryParse(CantidadIngresar.Text, out decimal cantidad) || cantidad <= 0)
            {
                MensajeOperacion.Text = "Introduce una cantidad válida.";
                MensajeOperacion.ForeColor = System.Drawing.Color.Red;
                return;
            }

            usuarioActual.ID = (int)Session["id_usuario"];
            if (usuarioActual.Read())
            {
                decimal nuevoSaldo = (decimal)usuarioActual.Saldo + cantidad;

                if (nuevoSaldo > SALDO_MAXIMO)
                {
                    MensajeOperacion.Text = "No puedes superar el saldo máximo de 10,000.";
                    MensajeOperacion.ForeColor = System.Drawing.Color.Red;
                }
                else
                {
                    usuarioActual.Saldo = (float)nuevoSaldo;
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
            if (!decimal.TryParse(CantidadRetirar.Text, out decimal cantidad) || cantidad <= 0)
            {
                MensajeOperacion.Text = "Introduce una cantidad válida.";
                MensajeOperacion.ForeColor = System.Drawing.Color.Red;
                return;
            }

            usuarioActual.ID = (int)Session["id_usuario"];
            if (usuarioActual.Read())
            {
                decimal saldoActual = (decimal)usuarioActual.Saldo;

                if (cantidad > saldoActual)
                {
                    MensajeOperacion.Text = "No tienes suficiente saldo.";
                    MensajeOperacion.ForeColor = System.Drawing.Color.Red;
                }
                else
                {
                    decimal nuevoSaldo = saldoActual - cantidad;
                    usuarioActual.Saldo = (float)nuevoSaldo;
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
