using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;
using library;

namespace ProWeb
{
    public partial class Login : System.Web.UI.Page
    {
        /// <summary>
        /// Evento que se ejecuta al cargar la página.
        /// Coloca los placeholders de los campos de texto.
        /// </summary>
        /// <param name="sender">Página objeto que lanza el evento</param>
        /// <param name="e">Argumentos del evento</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Emaillogin.Attributes["placeholder"] = "Correo electrónico";
                Passlogin.Attributes["placeholder"] = "Contraseña";
            }
        }
        /// <summary>
        /// Evento que se ejecuta al pulsar el botón de registrar.
        /// Redirije a la página de registrar.
        /// </summary>
        /// <param name="sender">Página objeto que lanza el evento</param>
        /// <param name="e">Argumentos del evento</param>
        protected void EventoRegistrar(object sender, EventArgs e)
        {
            Response.Redirect("Register.aspx");
        }
        /// <summary>
        /// Evento que se ejecuta al pulsar el botón de iniciar sesión.
        /// Verifica el reCaptcha y los campos de incio de sesión para validar e identificar al usuario.
        /// </summary>
        /// <param name="sender">Página objeto que lanza el evento</param>
        /// <param name="e">Argumentos del evento</param>
        protected void EventoMainPage(object sender, EventArgs e)
        {
            // Obtener el token de reCAPTCHA enviado desde el formulario
            string token = Request.Form["g-recaptcha-response"];
            if (string.IsNullOrEmpty(token))
            {
                Response.Write("<script>alert('Por favor, verifica que no eres un robot.');</script>");
                return;
            }

            string secretKey = "6Lcq4S8rAAAAAPjRbFB4ge9YHw7CZVj5nPKiImwf";
            var client = new WebClient();
            var result = client.DownloadString(
                $"https://www.google.com/recaptcha/api/siteverify?secret={secretKey}&response={token}");

            var js = new JavaScriptSerializer();
            var captchaResponse = js.Deserialize<RecaptchaResponse>(result);

            if (captchaResponse.success)
            {
                // Validación reCAPTCHA exitosa
                if (Session["Login"] != null)
                {
                    // Ya está loggeado
                    Response.Redirect("Juegos.aspx");
                }
                else
                {
                    ENUsuario usuario = new ENUsuario();
                    usuario.Correo = Emaillogin.Text;
                    usuario.Password = HashPassword(Passlogin.Text);
                    if (Page.IsValid)
                    {
                        if (usuario.LoginUsu())
                        {
                            // El usuario existe (contraseña correcta)
                            Session["Login"] = usuario.ID;
                            HttpCookie userCookie = new HttpCookie("UsuarioID");
                            userCookie.Value = usuario.ID.ToString();
                            userCookie.Expires = DateTime.Now.AddDays(7);
                            Response.Cookies.Add(userCookie);
                            Response.Redirect("Juegos.aspx");
                        }
                        else
                        {
                            ErrMsg.Text = "Correo o contraseña incorrectos";
                        }
                    }
                }
            }
            else
            {
                Response.Write("<script>alert('Acceso denegado. reCAPTCHA fallido.');</script>");
            }
        }
        /// <summary>
        /// Funcìón auxiliar que cifra la contraseña con hashing.
        /// </summary>
        /// <param name="password">Cadena con la contraseña a cifrar</param>
        /// <returns>Cadena de 64 caracteres con la cadena original cifrada</returns>
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
        /// <summary>
        /// Clase con valores necesarios para utilizar reCaptcha.
        /// Los atributos de la clase corresponden a la cadena JSON que devuelve la función captcha.
        /// </summary>
        public class RecaptchaResponse
        {
            public bool success { get; set; }
            public float score { get; set; }
            public string action { get; set; }
            public DateTime challenge_ts { get; set; }
            public string hostname { get; set; }
            public List<string> error_codes { get; set; }
        }
    }
}
