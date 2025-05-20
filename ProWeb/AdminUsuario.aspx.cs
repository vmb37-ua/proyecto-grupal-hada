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
        }
        
        protected void EventoCambioFoto (object sender, EventArgs e)
        {
            if (selecFoto.HasFile)
            {
                string extension = System.IO.Path.GetExtension(selecFoto.FileName).ToLower();
                string[] permitidas = { ".jpg", ".jpeg", ".png", ".gif" };

                if (permitidas.Contains(extension))
                {
                    string ruta = Server.MapPath("~/Source/Images/") + selecFoto.FileName;
                    selecFoto.SaveAs(ruta);
                    MensajeFoto.Text = "Imagen subida correctamente.";
                    MensajeFoto.Text = "";

                    ENUsuario usuario = new ENUsuario();
                    usuario.ID = int.Parse(Session["Login"].ToString());
                    usuario.Read();
                    usuario.Imagen = selecFoto.FileName;
                    usuario.Update();

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
            usuario.Nombre = CajaNombre.Text;
            usuario.NumTar = CajaNumTar.Text;
            Response.Redirect("Juegos.aspx");
        }
    }
}