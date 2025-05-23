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
    /// <summary>
    /// Clase que combina la informacion del estadio mas el nombre del municipio.
    /// </summary>
    public class EstadioConMunicipio
    {

        /// <summary>
        /// Nombre del estadio.
        /// </summary>
        public string Nombre { get; set; }

        /// <summary>
        /// Capacidad del estadio.
        /// </summary>
        public int Capacidad { get; set; }

        /// <summary>
        /// Descripcion del estadio.
        /// </summary>
        public string Texto { get; set; }

        /// <summary>
        /// Nombre del municipio.
        /// </summary>
        public string NombreMunicipio { get; set; }
    }

    public partial class InformacionEstadio : System.Web.UI.Page
    {

        /// <summary>
        /// Funcion que crea la URL de la ubicacion del estadio en Google Maps.
        /// </summary>
        /// <param name="nombreObj">Nombre del estadio.</param>
        /// <param name="municipioObj">Nombre del municipio.</param>
        /// <returns>URL del Google Maps.</returns>
        public string GetGoogleMapsEmbedUrl(object nombreObj, object municipioObj)
        {
            string nombre = nombreObj?.ToString() ?? "";
            string municipio = municipioObj?.ToString() ?? "";
            string query = $"{nombre} {municipio}";
            string encodedQuery = HttpUtility.UrlEncode(query);
            return $"https://www.google.com/maps?q={encodedQuery}&output=embed";
        }

        /// <summary>
        /// Funcion que se ejecuta al gargar la pagina.
        /// </summary>
        /// <param name="sender">Objeto de la pagina.</param>
        /// <param name="e">Argumento del evento de cargar la pagina.</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarEstadios();
            }
        }

        /// <summary>
        /// Carga todos los estadios de la BD y los almacena en el Repeater.
        /// </summary>
        private void CargarEstadios()
        {
            LabelEstadios.Visible = false;
            List<ENEstadio> estadios = new ENEstadio().ReadAll();

            if ((estadios == null) || (!estadios.Any()))
            {
                LabelEstadios.Text = "No hay estadios disponibles.";
                LabelEstadios.Visible = true;
                LabelEstadios.ForeColor = System.Drawing.Color.Red;
            }
            else
            {
                List<EstadioConMunicipio> lista = new List<EstadioConMunicipio>();

                foreach (var estadio in estadios)
                {
                    ENMunicipio municipio = new ENMunicipio();
                    municipio.Id_municipio = estadio.Id_municipio;
                    municipio.Read();

                    lista.Add(new EstadioConMunicipio
                    {
                        Nombre = estadio.Nombre,
                        Capacidad = estadio.Capacidad,
                        Texto = estadio.Texto,
                        NombreMunicipio = municipio.Nombre
                    });
                }

                RepeaterEstadios.DataSource = lista;
                RepeaterEstadios.DataBind();
            }
        }

        /// <summary>
        /// Funcion que trunca la descripcion del estadio para no salirse del Repeater.
        /// </summary>
        /// <param name="texto">Descripcion del estadio.</param>
        /// <returns>La descripcion truncada.</returns>
        public string TruncarTexto(object texto)
        {
            if (texto == null)
            {
                return "";
            }
            string textoaux = texto.ToString();
            if (string.IsNullOrEmpty(textoaux))
            {
                return "";
            }
            if (textoaux.Length > 85)
            {
                return textoaux.Substring(0, 85) + "...";
            }
            else
            {
                return textoaux;
            }
        }
    }
}