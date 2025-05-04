using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ProWeb
{
    public partial class Cartera : Page
    {
        //los límites permitidos para el saldo
        private const decimal SALDO_MAXIMO = 10000m;
        private const decimal SALDO_MINIMO = 0m;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DineroDisponible.Text = saldo.ToString();
            }
        }

        //propiedad para leer y guardar el saldo en la sesion del usuario
        private decimal saldo
        {
            get
            {
                if (Session["saldo"] != null)
                {
                    return (decimal)Session["saldo"];
                }
                else
                {
                    //si no hay saldo se inicia en 100 por defecto
                    return 100;
                }
            }
            set
            {
                Session["saldo"] = value;
            }
        }

        protected void Ingresar_Click(object sender, EventArgs e)
        {
            //para ver que se ha ingresado una cantidad válida y positiva
            if (decimal.TryParse(CantidadIngresar.Text, out decimal cantidad) && cantidad > 0)
            {
                //el nuevo saldo no supere el límite máximo
                if (saldo + cantidad > SALDO_MAXIMO)
                {
                    MensajeOperacion.Text = "No puedes superar el saldo máximo de 10,000.";
                    MensajeOperacion.ForeColor = System.Drawing.Color.Red;
                }
                else
                {
                    saldo += cantidad;
                    DineroDisponible.Text = saldo.ToString(); //se muestra el nuevo saldo
                    MensajeOperacion.Text = "Saldo ingresado correctamente.";
                    MensajeOperacion.ForeColor = System.Drawing.Color.Green;
                }
            }
            else
            {
                MensajeOperacion.Text = "Introduce una cantidad válida.";
                MensajeOperacion.ForeColor = System.Drawing.Color.Red;
            }
        }

        protected void Retirar_Click(object sender, EventArgs e)
        {
            //verificamos que se haya ingresado una cantidad válida y positiva
            if (decimal.TryParse(CantidadRetirar.Text, out decimal cantidad) && cantidad > 0)
            {
                //solo se permite retirar si hay saldo suficiente
                if (cantidad <= saldo)
                {
                    saldo -= cantidad; //se resta la cantidad del saldo
                    DineroDisponible.Text = saldo.ToString(); //muestra el nuevo saldo
                    MensajeOperacion.Text = "Has retirado dinero correctamente.";
                    MensajeOperacion.ForeColor = System.Drawing.Color.Green;
                }
                else
                {
                    //si se intenta retirar más de lo que hay salta error
                    MensajeOperacion.Text = "No tienes suficiente saldo.";
                    MensajeOperacion.ForeColor = System.Drawing.Color.Red;
                }
            }
            else
            {
                MensajeOperacion.Text = "Introduce una cantidad válida.";
                MensajeOperacion.ForeColor = System.Drawing.Color.Red;
            }
        }

        protected void Volver_Click(object sender, EventArgs e)
        {
            //manda al usuario a la página de perfil
            Response.Redirect("Perfil.aspx");
        }
    }
}
