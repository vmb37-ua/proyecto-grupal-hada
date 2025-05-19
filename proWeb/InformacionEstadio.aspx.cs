using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using library;

namespace ProWeb
{
    public partial class InformacionEstadio : System.Web.UI.Page
    {

        /*protected void Page_Load(object sender, EventArgs e)
        {
            // Ejemplo
            
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
            
        }
        */
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarEstadios();
            }
        }

        private void CargarEstadios()
        {
            var estadios = new ENEstadio().ReadAll();
            estadios.Add(new ENEstadio { Nombre = "Santiago Bernabéu", Ciudad = "Madrid", Direccion = "Av. de Concha Espina, 1" });
            estadios.Add(new ENEstadio { Nombre = "Camp Nou", Ciudad = "Barcelona", Direccion = "C. d'Arístides Maillol, 12" });
            estadios.Add(new ENEstadio { Nombre = "Wanda Metropolitano", Ciudad = "Madrid", Direccion = "Av. de Luis Aragonés, 4" });
            if (estadios == null || estadios.Count == 0)
            {
                HtmlGenericControl msg = new HtmlGenericControl("p");
                msg.InnerText = "No hay estadios disponibles.";
                gridEstadios.Controls.Add(msg);
                return;
            }

            foreach (var estadio in estadios)
            {
                HtmlGenericControl cardDiv = new HtmlGenericControl("div");
                cardDiv.Attributes["class"] = "card-estadio";
                HtmlGenericControl bgDiv = new HtmlGenericControl("div");
                bgDiv.Attributes["class"] = "fondo-estadio";
                bgDiv.Attributes["style"] = $"background-image: url('Source/Images/estadios/{estadio.Nombre}.jpg');";
                cardDiv.Controls.Add(bgDiv);
                HtmlGenericControl contenidoDiv = new HtmlGenericControl("div");
                contenidoDiv.Attributes["class"] = "contenido-estadio";
                HtmlGenericControl h3 = new HtmlGenericControl("h3");
                HtmlAnchor link = new HtmlAnchor();
                link.HRef = $"DetalleEstadio.aspx?nombre={Server.UrlEncode(estadio.Nombre)}";
                link.InnerText = estadio.Nombre;
                h3.Controls.Add(link);
                contenidoDiv.Controls.Add(h3);

                if (!string.IsNullOrEmpty(estadio.Ciudad))
                {
                    HtmlGenericControl ciudadP = new HtmlGenericControl("p");
                    ciudadP.InnerText = estadio.Ciudad.Length > 100 ? estadio.Ciudad.Substring(0, 100) + "..." : estadio.Ciudad;
                    contenidoDiv.Controls.Add(ciudadP);
                }

                if (!string.IsNullOrEmpty(estadio.Direccion))
                {
                    HtmlGenericControl direccionP = new HtmlGenericControl("p");
                    direccionP.InnerText = "Dirección: " + (estadio.Direccion.Length > 100 ? estadio.Direccion.Substring(0, 100) + "..." : estadio.Direccion);
                    contenidoDiv.Controls.Add(direccionP);
                }

                cardDiv.Controls.Add(contenidoDiv);
                gridEstadios.Controls.Add(cardDiv);
            }
        }
    }
}