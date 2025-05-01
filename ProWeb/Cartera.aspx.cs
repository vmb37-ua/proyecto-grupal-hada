using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ProWeb
{
    public partial class Cartera : System.Web.UI.Page
    {
        decimal dineroDisponible = 100; //dinero inicial de la persona

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //muestra el saldo cuando carga la página
                DineroDisponible.Text = dineroDisponible.ToString();
            }
        }

        protected void AñadirSaldo(object sender, EventArgs e)
        {
            if (decimal.TryParse(CantidadIngresar.Text, out decimal cantidad) && cantidad > 0)
            {
                dineroDisponible += cantidad;
                DineroDisponible.Text = dineroDisponible.ToString();
                MensajeOperacion.Text = "";
            }
            else
            {
                MensajeOperacion.Text = "Introduce una cantidad válida: ";
            }
        }

        protected void RetirarSaldo(object sender, EventArgs e)
        {
            if (decimal.TryParse(CantidadRetirar.Text, out decimal cantidad) && cantidad > 0)
            {
                if (cantidad <= dineroDisponible)
                {
                    dineroDisponible -= cantidad;
                    DineroDisponible.Text = dineroDisponible.ToString();
                    MensajeOperacion.Text = "";
                }
                else
                {
                    MensajeOperacion.Text = "No hay suficiente dinero";
                }
            }
            else
            {
                MensajeOperacion.Text = "Introduce una cantidad válida: ";
            }
        }
    }
}

