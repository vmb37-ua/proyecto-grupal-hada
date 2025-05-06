using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ProWeb
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Emaillogin.Attributes["placeholder"] = "Correo electrónico";
                Passlogin.Attributes["placeholder"] = "Contraseña";
            }
        }

        protected void EventoRegistrar(object sender, EventArgs e)
        {
            Response.Redirect("Register.aspx");
        }

        protected void EventoMainPage(object sender, EventArgs e)
        {
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

                Response.Redirect("Juegos.aspx");
            }
            else
            {

                Response.Write("<script>alert('Acceso denegado. reCAPTCHA fallido.');</script>");
            }
        }
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