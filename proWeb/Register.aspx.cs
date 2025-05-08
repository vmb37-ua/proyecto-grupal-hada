using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using static System.Net.Mime.MediaTypeNames;

namespace ProWeb
{
    public partial class Register : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                Nameregister.Attributes["placeholder"] = "Nombre Completo";
                Numberregister.Attributes["placeholder"] = "Número de telefono";
                Emailregister.Attributes["placeholder"] = "Correo electrónico";
                Passregister.Attributes["placeholder"] = "Contraseña";
                Passrepregister.Attributes["placeholder"] = "Confirmar Contraseña";

            }
        }
        protected void EventoInicioSesion(object sender, EventArgs e)
        {
            Response.Redirect("Login.aspx");
        }
        private bool todosLosCampos(out string nombre,out string numero,out string correo,out string pass,out string passRep)
        {
            Nameregister.CssClass = "Inputregister";
            Numberregister.CssClass = "Inputregister";
            Emailregister.CssClass = "Inputregister";
            Passregister.CssClass = "Inputregister";
            Passrepregister.CssClass = "Inputregister";

            nombre = Nameregister.Text.Trim();
             numero = Numberregister.Text.Trim();
             correo = Emailregister.Text.Trim();
             pass = Passregister.Text.Trim();
             passRep = Passrepregister.Text.Trim();
            if (string.IsNullOrEmpty(nombre)) {
                Nameregister.CssClass += " input-error";
               
            }
            if (string.IsNullOrEmpty(numero))
            {
                Numberregister.CssClass += " input-error";

            }
            if (string.IsNullOrEmpty(correo))
            {
                Emailregister.CssClass += " input-error";

            }
            if (string.IsNullOrEmpty(pass))
            {
                Passregister.CssClass += " input-error";

            }

            if (string.IsNullOrEmpty(passRep))
            {
                Passrepregister.CssClass += " input-error";

            }

            if (string.IsNullOrEmpty(nombre) || string.IsNullOrEmpty(numero) ||
                string.IsNullOrEmpty(correo) || string.IsNullOrEmpty(pass) || string.IsNullOrEmpty(passRep))
            {
                Labelerror.Text = "Por favor, rellena todos los campos.";
                Labelerror.Visible = true;
                return true;
            }
            return false;
        }
        protected void EventoPaginaPrincipal(object sender, EventArgs e)
        {
            string nombre, numero, correo, pass, passRep;
            bool camposCompletos = todosLosCampos(out nombre, out numero, out correo, out pass, out passRep);
            if (camposCompletos) {
                return;
            }

            if (correo.Length < 5 || !correo.Contains("@") || !correo.Contains("."))
            {
                Labelerror.Text = "El correo electrónico no es válido.";
                Labelerror.Visible = true;
                Emailregister.CssClass += " input-error";
                return;
            }

            if (pass != passRep)
            {
                Labelerror.Text = "Las contraseñas no coinciden.";
                Labelerror.Visible = true;

                Passregister.CssClass += " input-error";
                Passrepregister.CssClass += " input-error";

                return;
            }
            if (pass.Length < 6)
            {
                Labelerror.Text = "La contraseña debe tener al menos 6 caracteres.";
                Labelerror.Visible = true;
                Passregister.CssClass += " input-error";
                Passrepregister.CssClass += " input-error";
                return;
            }
            
            else
            {
                Labelerror.Text = "";
                Labelerror.Visible = false;
                Response.Redirect("Juegos.aspx");
            }
        }
    }
}