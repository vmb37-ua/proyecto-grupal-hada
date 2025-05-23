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
        /// <summary>
        /// Evento que ocurre al cargar la página.
        /// Se asegura de que haya sesión iniciada, si no redirige a Juegos.aspx.
        /// Si es la primera vez, carga los datos del usuario y rellena los controles.
        /// </summary>
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
                    CajaCad.Text = usuario.Caducidad.ToShortDateString();
                    CajaDir.Text = usuario.Direccion;
                    CajaNumTar.Text = usuario.NumTar;
                    CajaTelef.Text = usuario.Telefono;
                }
            }
        }
        /// <summary>
        /// Evento que se ocurre al cambiar la foto de perfil.
        /// Verifica la extensión y guarda la imagen.
        /// Actualiza la ruta de la imagen en la base de datos.
        /// </summary>
        protected void EventoCambioFoto (object sender, EventArgs e)
        {
            if (selecFoto.HasFile)
            {
                string extension = System.IO.Path.GetExtension(selecFoto.FileName).ToLower();
                string[] permitidas = { ".jpg", ".jpeg", ".png", ".gif" };

                if (permitidas.Contains(extension))
                {
                    string ruta = Server.MapPath("~/Source/Images/") + "pfp_"+Session["Login"].ToString()+extension;
                    selecFoto.SaveAs(ruta);
                    MensajeFoto.Text = "Imagen subida correctamente.";
                    MensajeFoto.Text = "";

                    ENUsuario usuario = new ENUsuario();
                    usuario.ID = int.Parse(Session["Login"].ToString());
                    usuario.Read();
                    usuario.Imagen = "pfp_"+Session["Login"].ToString()+extension;
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
        /// <summary>
        /// Guarda los cambios realizados por el usuario en su perfil.
        /// Mira que los datos sean correctos y actualiza el perfil.
        /// </summary>
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
    }
}