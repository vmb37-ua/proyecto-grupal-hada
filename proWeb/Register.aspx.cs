using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;
using library;
using System.Security.Cryptography;
using System.Text;
using AjaxControlToolkit.HtmlEditor.ToolbarButtons;
namespace ProWeb
{

    public partial class Register : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Nameregister.Attributes["placeholder"] = "Nombre Completo";
                Numberregister.Attributes["placeholder"] = "Número de teléfono";
                Emailregister.Attributes["placeholder"] = "Correo electrónico";
                Adressresgister.Attributes["placeholder"] = "Dirección";
                Passregister.Attributes["placeholder"] = "Contraseña";
                Passrepregister.Attributes["placeholder"] = "Confirmar Contraseña";
                CargarPaises();

            }
        }

        private void CargarPaises()
        {
            var cadPais = new CADPais();
            var paises = cadPais.ReadAll();
            Paisregister.DataSource = paises;
            Paisregister.DataTextField = "NombrePais";
            Paisregister.DataValueField = "IdPais";
            Paisregister.DataBind();

            Paisregister.Items.Insert(0, new ListItem("Selecciona un país", ""));
        }


        private void CargarProvincia(int idPais)
        {
            var cadProvincia = new CADProvincia();
            var provincias = new List<ENProvincia>();

            ENProvincia filtroProvincia = new ENProvincia();
            filtroProvincia.IdPais = idPais;

            provincias = cadProvincia.ReadAllByPais(filtroProvincia);

            Provinciaregister.DataSource = provincias;
            Provinciaregister.DataTextField = "Nombre";
            Provinciaregister.DataValueField = "IdProvincia";
            Provinciaregister.DataBind();

            Provinciaregister.Items.Insert(0, new ListItem("Selecciona una provincia", ""));
        }

        private void CargarMunicipios(int idProvincia)
        {
            var cadMunicipio = new CADMunicipio();
            var municipios = new List<ENMunicipio>();

            ENMunicipio filtroMunicipio = new ENMunicipio();
            filtroMunicipio.Id_provincia = idProvincia;

            municipios = cadMunicipio.ReadAll(filtroMunicipio);

            Municipioregister.DataSource = municipios;
            Municipioregister.DataTextField = "Nombre";
            Municipioregister.DataValueField = "Id_municipio";
            Municipioregister.DataBind();

            Municipioregister.Items.Insert(0, new ListItem("Selecciona un municipio", ""));
        }


        protected void Paisregister_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (int.TryParse(Paisregister.SelectedValue, out int idPais))
            {
                Provinciaregister.Items.Clear();
                Municipioregister.Items.Clear();
                CargarProvincia(idPais);
            }
            else
            {
                Provinciaregister.Items.Clear();
                Municipioregister.Items.Clear();
            }
        }

        protected void Provinciaregister_SelectedIndexChanged(object sender, EventArgs e)
        {
            Municipioregister.Items.Clear();
            if (int.TryParse(Provinciaregister.SelectedValue, out int idProvincia))
            {
                CargarMunicipios(idProvincia);
            }
        }



        protected void EventoInicioSesion(object sender, EventArgs e)
        {
            Response.Redirect("Login.aspx");
        }



        protected void EventoPaginaPrincipal(object sender, EventArgs e)
        {
            string correo = Emailregister.Text.Trim();

            CADUsuario cadUsuario = new CADUsuario();
            if (cadUsuario.ExisteCorreo(correo))
            {
                Labelerror.Text = "Este correo ya está registrado.";
                Labelerror.Visible = true;
                return;
            }

            ENUsuario nuevoUsuario = new ENUsuario
            {
                Nombre = Nameregister.Text.Trim(),
                Telefono = Numberregister.Text.Trim(),
                Correo = correo,
                Password = HashPassword(Passregister.Text.Trim()),
                Direccion = Adressresgister.Text.Trim(),
                Saldo = 0,
                NumTar = "",
                Caducidad = DateTime.Now,
                Cvv = "",
                Rol = 1,
                Municipio = int.Parse(Municipioregister.SelectedValue),
                Imagen = "default.jpg"
            };



            try
            {
                if (nuevoUsuario.Create())
                {

                    nuevoUsuario.ReadByCorreo();

                    Session["Login"] = nuevoUsuario.ID;
                    Response.Redirect("Juegos.aspx");
                }
                else
                {
                    Labelerror.Text = "Error al crear el Usuario";
                    Labelerror.Visible = true;
                }
            }
            catch (Exception ex)
            {
                Labelerror.Text = "Excepción: " + ex.Message;
                Labelerror.Visible = true;
            }
        }

        public static string HashPassword(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = Encoding.UTF8.GetBytes(password);
                byte[] hashBytes = sha256.ComputeHash(bytes);

                // Convertir a string hexadecimal
                StringBuilder builder = new StringBuilder();
                foreach (byte b in hashBytes)
                    builder.Append(b.ToString("x2"));

                return builder.ToString();
            }
        }

    }
}