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
                    usuario.ID = int.Parse(Session["login"].ToString());
                    usuario.Read();

                    CajaNombre.Text = usuario.Nombre;
                    CajaCvv.Text = usuario.Cvv;
                    CajaCad.Text = usuario.Caducidad.ToString();
                    CajaDir.Text = usuario.Direccion;
                    CajaNumTar.Text = usuario.NumTar;
                    CajaTelef.Text = usuario.Telefono;
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