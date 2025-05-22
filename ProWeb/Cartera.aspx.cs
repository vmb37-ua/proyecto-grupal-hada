using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using library;

namespace ProWeb
{
    /// <summary>
    /// Página que permite a un usuario autenticado consultar su saldo,
    /// ingresar dinero o retirar dinero de su cartera virtual.
    /// También se registran las transacciones realizadas y se aplican límites definidos.
    /// </summary>
    public partial class Cartera : Page
    {
        //Constante que define el saldo máximo permitido en la cartera
        private const float SALDO_MAXIMO = 10000f;

        //Constante que define el saldo mínimo permitido (no se usa directamente)
        private const float SALDO_MINIMO = 0f;

        //Objeto que representa al usuario actualmente autenticado
        private ENUsuario usuarioActual;

        /// <summary>
        /// Carga la información del usuario autenticado cuando se abre la página.
        /// Si el usuario no está autenticado, lo redirige a la página de login.
        /// Si es la primera vez que se carga la página, muestra el saldo disponible.
        /// </summary>
        protected void Page_Load(object sender, EventArgs e)
        {
            //Si no hay sesión iniciada, redirige al login
            if (Session["Login"] == null)
            {
                Response.Redirect("Login.aspx");
                return;
            }

            //Obtiene el ID de usuario almacenado en la sesión
            int idUsuario = (int)Session["Login"];

            //Crea una instancia del usuario con ese ID
            usuarioActual = new ENUsuario { ID = idUsuario };

            //Intenta leer los datos del usuario desde la base de datos
            if (usuarioActual.Read())
            {
                //Solo muestra el saldo si es la primera carga de la página
                if (!IsPostBack)
                {
                    //Formatea el saldo con 2 decimales y lo muestra en pantalla
                    DineroDisponible.Text = usuarioActual.Saldo.ToString("F2") + " €";
                }
            }
            else
            {
                //Si ocurre un error al cargar el usuario, muestra un mensaje de error
                MensajeOperacion.Text = "No se pudo cargar la información del usuario.";
                MensajeOperacion.ForeColor = System.Drawing.Color.Red;
            }
        }

        /// <summary>
        /// Método que se ejecuta cuando el usuario hace clic en el botón "Ingresar".
        /// Añade la cantidad indicada al saldo, si es válida y no excede el máximo.
        /// Registra la transacción de ingreso.
        /// </summary>
        protected void Ingresar_Click(object sender, EventArgs e)
        {
            //Intenta convertir el texto ingresado a número (float)
            if (!float.TryParse(CantidadIngresar.Text, out float cantidad) || cantidad <= 0)
            {
                //Si no es una cantidad válida, muestra un error
                MensajeOperacion.Text = "Introduce una cantidad válida.";
                MensajeOperacion.ForeColor = System.Drawing.Color.Red;
                return;
            }

            //Obtiene el ID del usuario desde la sesión
            int idUsuario = (int)Session["Login"];
            usuarioActual.ID = idUsuario;

            //Lee los datos actuales del usuario desde la base de datos
            if (usuarioActual.Read())
            {
                //Calcula el nuevo saldo sumando la cantidad ingresada
                float nuevoSaldo = usuarioActual.Saldo + cantidad;

                //Verifica que no se supere el saldo máximo
                if (nuevoSaldo > SALDO_MAXIMO)
                {
                    MensajeOperacion.Text = "No puedes superar el saldo máximo de 10,000.";
                    MensajeOperacion.ForeColor = System.Drawing.Color.Red;
                }
                else
                {
                    //Actualiza el saldo del usuario
                    usuarioActual.Saldo = nuevoSaldo;

                    //Guarda el nuevo saldo en la base de datos
                    if (usuarioActual.Update())
                    {
                        //Muestra el nuevo saldo actualizado
                        DineroDisponible.Text = nuevoSaldo.ToString("F2") + " €";

                        //Muestra mensaje de éxito
                        MensajeOperacion.Text = "Saldo ingresado correctamente.";
                        MensajeOperacion.ForeColor = System.Drawing.Color.Green;

                        //Crea una nueva transacción de tipo "Ingreso"
                        ENTransaccion transaccion = new ENTransaccion(0, usuarioActual.ID, cantidad, "Ingreso");

                        //Guarda la transacción en la base de datos
                        transaccion.Create();
                    }
                    else
                    {
                        //Si falla la actualización del saldo, muestra error
                        MensajeOperacion.Text = "Error al actualizar el saldo.";
                        MensajeOperacion.ForeColor = System.Drawing.Color.Red;
                    }
                }
            }
        }

        /// <summary>
        /// Método que se ejecuta cuando el usuario hace clic en "Retirar".
        /// Sustrae la cantidad indicada del saldo, si es válida y suficiente.
        /// Registra la transacción de retiro.
        /// </summary>
        protected void Retirar_Click(object sender, EventArgs e)
        {
            //Intenta convertir el texto ingresado a número (float)
            if (!float.TryParse(CantidadRetirar.Text, out float cantidad) || cantidad <= 0)
            {
                //Si la cantidad es inválida o menor que 0, muestra error
                MensajeOperacion.Text = "Introduce una cantidad válida.";
                MensajeOperacion.ForeColor = System.Drawing.Color.Red;
                return;
            }

            //Obtiene el ID del usuario desde la sesión
            int idUsuario = (int)Session["Login"];
            usuarioActual.ID = idUsuario;

            //Carga los datos actuales del usuario
            if (usuarioActual.Read())
            {
                //Guarda el saldo actual
                float saldoActual = usuarioActual.Saldo;

                //Verifica si el usuario tiene suficiente saldo para retirar
                if (cantidad > saldoActual)
                {
                    MensajeOperacion.Text = "No tienes suficiente saldo.";
                    MensajeOperacion.ForeColor = System.Drawing.Color.Red;
                }
                else
                {
                    //Calcula el nuevo saldo tras retirar el dinero
                    float nuevoSaldo = saldoActual - cantidad;
                    usuarioActual.Saldo = nuevoSaldo;

                    //Actualiza el saldo en la base de datos
                    if (usuarioActual.Update())
                    {
                        //Muestra el saldo actualizado
                        DineroDisponible.Text = nuevoSaldo.ToString("F2") + " €";

                        //Mensaje de éxito
                        MensajeOperacion.Text = "Has retirado dinero correctamente.";
                        MensajeOperacion.ForeColor = System.Drawing.Color.Green;

                        //Crea y guarda la transacción de tipo "Retiro"
                        ENTransaccion transaccion = new ENTransaccion(0, usuarioActual.ID, cantidad, "Retiro");
                        transaccion.Create();
                    }
                    else
                    {
                        //Error al guardar los cambios
                        MensajeOperacion.Text = "Error al actualizar el saldo.";
                        MensajeOperacion.ForeColor = System.Drawing.Color.Red;
                    }
                }
            }
        }

        /// <summary>
        /// Método que se ejecuta al hacer clic en el botón "Volver".
        /// Redirige al usuario a su página de perfil.
        /// </summary>
        protected void Volver_Click(object sender, EventArgs e)
        {
            Response.Redirect("Perfil.aspx");
        }
    }
}
