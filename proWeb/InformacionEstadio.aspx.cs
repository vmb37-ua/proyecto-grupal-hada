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

            // Prueba, luego Borrar, lo inserto a mano
            estadios.Add(new ENEstadio
            {
                Nombre = "Santiago Bernabéu",
                Capacidad = 81044,
                Texto = "Estadio del Real Madrid C.F., ubicado en Madrid.",
                Id_municipio = 1
            });

            estadios.Add(new ENEstadio
            {
                Nombre = "Camp Nou",
                Capacidad = 99354,
                Texto = "Antiguo estadio del F.C. Barcelona, situado en Barcelona.",
                Id_municipio = 2
            });

            estadios.Add(new ENEstadio
            {
                Nombre = "Wanda Metropolitano",
                Capacidad = 68456,
                Texto = "Estadio del Atlético de Madrid, en Madrid.",
                Id_municipio = 1
            });

            estadios.Add(new ENEstadio
            {
                Nombre = "La Cartuja",
                Capacidad = 60000,
                Texto = "Estadio de La Cartuja en Sevilla, usado para eventos deportivos y conciertos.",
                Id_municipio = 3
            });

            estadios.Add(new ENEstadio
            {
                Nombre = "San Mamés",
                Capacidad = 53289,
                Texto = "Estadio del Athletic Club de Bilbao, en el País Vasco.",
                Id_municipio = 4
            });

            // hasta aqui

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
                bgDiv.Attributes["style"] = $"background-image: url('Source/Images/{estadio.Nombre}.jpg');";
                cardDiv.Controls.Add(bgDiv);

                HtmlGenericControl contenidoDiv = new HtmlGenericControl("div");
                contenidoDiv.Attributes["class"] = "contenido-estadio";

                HtmlGenericControl h3 = new HtmlGenericControl("h3");
                HtmlAnchor link = new HtmlAnchor
                {
                    HRef = $"DetalleEstadio.aspx?nombre={Server.UrlEncode(estadio.Nombre)}",
                    InnerText = estadio.Nombre
                };
                h3.Controls.Add(link);
                contenidoDiv.Controls.Add(h3);

                HtmlGenericControl capacidadP = new HtmlGenericControl("p");
                capacidadP.InnerText = "Capacidad: " + estadio.Capacidad;
                contenidoDiv.Controls.Add(capacidadP);

                if (!string.IsNullOrEmpty(estadio.Texto))
                {
                    HtmlGenericControl textoP = new HtmlGenericControl("p");
                    textoP.InnerText = estadio.Texto.Length > 100 ? estadio.Texto.Substring(0, 100) + "..." : estadio.Texto;
                    contenidoDiv.Controls.Add(textoP);
                }

                HtmlGenericControl municipioP = new HtmlGenericControl("p");
                municipioP.InnerText = "ID Municipio: " + estadio.Id_municipio;
                contenidoDiv.Controls.Add(municipioP);

                cardDiv.Controls.Add(contenidoDiv);
                gridEstadios.Controls.Add(cardDiv);
            }
        }
    }
}