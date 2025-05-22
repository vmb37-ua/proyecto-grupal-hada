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
    public class EstadioConMunicipio
    {
        public string Nombre { get; set; }
        public int Capacidad { get; set; }
        public string Texto { get; set; }
        public string NombreMunicipio { get; set; }
    }

    public partial class InformacionEstadio : System.Web.UI.Page
    {

        public string GetGoogleMapsEmbedUrl(object nombreObj, object municipioObj)
        {
            string nombre = nombreObj?.ToString() ?? "";
            string municipio = municipioObj?.ToString() ?? "";
            string query = $"{nombre} {municipio}";
            string encodedQuery = HttpUtility.UrlEncode(query);
            return $"https://www.google.com/maps?q={encodedQuery}&output=embed";
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarEstadios();
            }
        }

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