using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using library;

namespace ProWeb
{
    public partial class AdminUsuario : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Login"] == null)
            {
                Response.Redirect("Juegos.aspx");
            }
            else
            {
                if (!IsPostBack)
                {
                    ENUsuario usuario = new ENUsuario();
                    ENMunicipio municipio = new ENMunicipio();
                    ENProvincia provincia = new ENProvincia();
                    usuario.ID = int.Parse(Session["login"].ToString());
                    usuario.Read();

                    CajaNombre.Text = usuario.Nombre;
                    CajaCvv.Text = usuario.Cvv;
                    CajaCad.Text = usuario.Caducidad.ToString();
                    CajaDir.Text = usuario.Direccion;
                    CajaNumTar.Text = usuario.NumTar;
                    CajaTelef.Text = usuario.Telefono;
                    CargarMunicipios(usuario.Municipio);
                    municipio.Id_municipio = usuario.Municipio;
                    municipio.Read();
                    CargarProvincia(municipio.Id_provincia);
                    provincia.IdProvincia = municipio.Id_provincia;
                    provincia.Read();
                    CargarPaises();
                }
            }
        }
        
        protected void EventoCambioFoto (object sender, EventArgs e)
        {
            if (selecFoto.HasFile)
            {
                string extension = System.IO.Path.GetExtension(selecFoto.FileName).ToLower();
                string[] permitidas = { ".jpg", ".jpeg", ".png", ".gif" };

                if (permitidas.Contains(extension))
                {
                    string ruta = Server.MapPath("~/Source/Images/") + Session["Login"].ToString();
                    selecFoto.SaveAs(ruta);
                    MensajeFoto.Text = "Imagen subida correctamente.";
                    MensajeFoto.Text = "";

                    ENUsuario usuario = new ENUsuario();
                    usuario.ID = int.Parse(Session["Login"].ToString());
                    usuario.Read();
                    usuario.Imagen = selecFoto.FileName;
                    usuario.UpdateFoto();

                    Response.Redirect(Request.RawUrl);
                }
                else
                {
                    MensajeFoto.Text = "Solo se permiten archivos de imagen (.jpg, .jpeg, .png, .gif).";
                }
            }
            else
            {
                MensajeFoto.Text = "Por favor selecciona una imagen.";
            }
        }

        protected void EventoCambiar(object sender, EventArgs e) {
            ENUsuario usuario = new ENUsuario();
            usuario.ID = int.Parse(Session["Login"].ToString()) ;
            usuario.Read();
            if (Page.IsValid)
            {
                DateTime fecha;
                usuario.Nombre = CajaNombre.Text;
                usuario.Cvv = CajaCvv.Text;
                usuario.Direccion = CajaDir.Text;
                usuario.NumTar = CajaNumTar.Text;
                usuario.Telefono = CajaTelef.Text;
                usuario.Municipio = int.Parse(Municipioregister.SelectedValue);

                if(DateTime.TryParse(CajaCad.Text, out fecha))
                {
                    usuario.Caducidad = fecha.Date;
                    if (usuario.Update()) Response.Redirect("Perfil.aspx");
                }
                else
                {
                    //Mensaje error validacion
                }
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
            Paisregister.SelectedIndex = 0;
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
        }

        private void CargarMunicipios(int idProvincia)
        {
            var cadMunicipio = new CADMunicipio();
            var municipios = new List<ENMunicipio>();

            ENMunicipio filtroMunicipio = new ENMunicipio();
            filtroMunicipio.Id_provincia = idProvincia;

            municipios = cadMunicipio.ReadAllByProvincia(filtroMunicipio);

            Municipioregister.DataSource = municipios;
            Municipioregister.DataTextField = "Nombre";
            Municipioregister.DataValueField = "Id_municipio";
            Municipioregister.DataBind();
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
    }
}