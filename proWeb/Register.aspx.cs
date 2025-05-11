using System;
using System.Web.UI;

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
            }
        }

        protected void EventoInicioSesion(object sender, EventArgs e)
        {
            Response.Redirect("Login.aspx");
        }

        private bool CamposIncompletos(out string nombre, out string numero, out string correo, out string direccion, out string pass, out string passRep)
        {
            // Reiniciar clases CSS
            Nameregister.CssClass = "Inputregister";
            Numberregister.CssClass = "Inputregister";
            Emailregister.CssClass = "Inputregister";
            Adressresgister.CssClass = "Inputregister";
            Passregister.CssClass = "Inputregister";
            Passrepregister.CssClass = "Inputregister";

            // Obtener valores
            nombre = Nameregister.Text.Trim();
            numero = Numberregister.Text.Trim();
            correo = Emailregister.Text.Trim();
            direccion = Adressresgister.Text.Trim();
            pass = Passregister.Text.Trim();
            passRep = Passrepregister.Text.Trim();

            bool faltanCampos = false;

            if (string.IsNullOrEmpty(nombre))
            {
                Nameregister.CssClass += " input-error";
                faltanCampos = true;
            }
            if (string.IsNullOrEmpty(numero))
            {
                Numberregister.CssClass += " input-error";
                faltanCampos = true;
            }
            if (string.IsNullOrEmpty(correo))
            {
                Emailregister.CssClass += " input-error";
                faltanCampos = true;
            }
            if (string.IsNullOrEmpty(direccion))
            {
                Adressresgister.CssClass += " input-error";
                faltanCampos = true;
            }
            if (string.IsNullOrEmpty(pass))
            {
                Passregister.CssClass += " input-error";
                faltanCampos = true;
            }
            if (string.IsNullOrEmpty(passRep))
            {
                Passrepregister.CssClass += " input-error";
                faltanCampos = true;
            }

            if (faltanCampos)
            {
                Labelerror.Text = "Por favor, rellena todos los campos.";
                Labelerror.Visible = true;
                return true;
            }

            return false;
        }

        protected void EventoPaginaPrincipal(object sender, EventArgs e)
        {
            string nombre, numero, correo, direccion, pass, passRep;

            if (CamposIncompletos(out nombre, out numero, out correo, out direccion, out pass, out passRep))
                return;

            if (correo.Length < 5 || !correo.Contains("@") || !correo.Contains(".") || correo.IndexOf("@") > correo.LastIndexOf("."))
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

            // Aquí se puede guardar la información del usuario en base de datos, etc.

            Labelerror.Text = "";
            Labelerror.Visible = false;
            Response.Redirect("Juegos.aspx");
        }
    }
}
