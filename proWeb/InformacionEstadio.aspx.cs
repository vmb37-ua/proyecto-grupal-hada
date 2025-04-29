using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ProWeb
{
    public partial class WebForm2 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Ejemplo
            /*
            LabelNombre.Text = "Estadio Nacional";
            LabelCiudad.Text = "Ciudad: Madrid";
            LinkDireccion.Text = "Ver en Google Maps";
            LinkDireccion.NavigateUrl = "https://maps.google.com/?q=Estadio+Nacional+Madrid";



            string direccion = "C. Batalla del Salado, 59, Tarifa, Cádiz";
            string q = Server.UrlEncode(direccion);
            string iframe = $@"
              <iframe
                id='Mapa'
                width='100%'
                height='300'
                style='border:0'
                loading='lazy'
                allowfullscreen
                src='https://www.google.com/maps?q={q}&output=embed'>
              </iframe>";

            MapFrame.Text = iframe;
            */
        }
    }
}