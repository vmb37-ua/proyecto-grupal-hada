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
    /// Página para administrar los patrocinadores
    /// </summary>
    public partial class DBPatrocinadores : System.Web.UI.Page
	{
        /// <summary>
        /// Evento que se ejecuta al cargar la página.
        /// </summary>
        protected void Page_Load(object sender, EventArgs e)
		{
            if (!IsPostBack) 
            {
                CargarPatrocinadores();
                MostrarPatrocinadorActual();
            }

        }

        /// <summary>
        /// Carga la lista de patrocinadores 
        /// </summary>
        private void CargarPatrocinadores()
        {   
            ListaPatrocinadores.Items.Clear();
            //Nuevo patrocinador tiene reservado el valor 0 y no representa un patrocinador, sino la opción de crear uno nuevo
            ListaPatrocinadores.Items.Add(new ListItem("Nuevo patrocinador", "0"));

            var patrocinadores = new ENPatrocinador().ReadAll();

            foreach (var patrocinador in patrocinadores)
            {
                ListaPatrocinadores.Items.Add(new ListItem($"Patrocinador {patrocinador.Id_patrocinador}",patrocinador.Id_patrocinador.ToString()));
            }
            
        }
        /// <summary>
        /// Muestra los datos del patrocinador seleccionado 
        /// Si es la opción Nuevo patrocinador limpia los campos.
        /// </summary>
        private void MostrarPatrocinadorActual()
        {
            //Sacas el ID del patrocinador
            int idPatrocinador = int.Parse(ListaPatrocinadores.SelectedValue);

            //Si es el nuevo patrocinador se limpia todo
            if (idPatrocinador == 0) 
            {
                LimpiarCampos();
                TBid.Text = "0";
                return;
            }

            //Creas y cargas el patrocinador
            var patrocinador = new ENPatrocinador { Id_patrocinador = idPatrocinador };
            patrocinador.Read();

            //Ponemos los datos
            TBid.Text = patrocinador.Id_patrocinador.ToString();
            TBdinero.Text = patrocinador.Dinero.ToString();
            TBtexto.Text = patrocinador.Texto;
            TBimagen.Text = patrocinador.Imagen;
        }
        /// <summary>
        /// Evento que se ejecuta al pulsar el botón para agregar o actualizar un patrocinador.
        /// Valida los campos y decide si crear uno nuevo o actualizar el que hay
        /// </summary>
        protected void BTNagregar_pat_Click(object sender, EventArgs e)
        {
            if (!ValidarCampos()) 
            {
                return;
            }

            int id = int.Parse(TBid.Text);
            float dinero = float.Parse(TBdinero.Text);

            //Si el value es 0 se ha seleccionado nuevo patrocinador
            if (ListaPatrocinadores.SelectedValue == "0")
            {
                ENPatrocinador patrocinador = new ENPatrocinador
                {
                    Id_patrocinador = ObtenerNuevoId(),
                    Dinero = dinero,
                    Texto = TBtexto.Text,
                    Imagen = TBimagen.Text
                };
                patrocinador.Create();
                CargarPatrocinadores();
                SeleccionarPatrocinador(patrocinador.Id_patrocinador);
                MostrarMensaje("Patrocinador creado exitosamente");
            }
            else
            {   //Si no es que ya existe y se edita
                ENPatrocinador patrocinador = new ENPatrocinador
                {
                    Id_patrocinador = int.Parse(ListaPatrocinadores.SelectedValue),
                    Dinero = dinero,
                    Texto = TBtexto.Text,
                    Imagen = TBimagen.Text
                };

                patrocinador.Update();
                CargarPatrocinadores();
                SeleccionarPatrocinador(id);
                MostrarMensaje("Patrocinador actualizado exitosamente");
            }


        }
        /// <summary>
        /// Evento que se ocurre al pulsar el botón para eliminar un patrocinador.
        /// Elimina el patrocinador seleccionado, recarga la lista y limpia los campos.
        /// </summary>
        protected void BTNeliminar_pat_Click(object sender, EventArgs e)
        {
            int id = int.Parse(TBid.Text);

            if (ListaPatrocinadores.SelectedValue == "0") 
            {
                return;
            }

            ENPatrocinador patrocinador = new ENPatrocinador { Id_patrocinador = id };
            patrocinador.Delete();
            CargarPatrocinadores();
            LimpiarCampos();
            MostrarMensaje("Patrocinador eliminado exitosamente");

            ListaPatrocinadores.SelectedIndex = 0;
            MostrarPatrocinadorActual();
        }

        /// <summary>
        /// Evento que se ejecuta al cambiar la selección en el DropDownList.
        /// Muestra el patrocinador seleccionado.
        /// </summary>
        protected void ListaPatrocinadores_SelectedIndexChanged(object sender, EventArgs e)
        {
            MostrarPatrocinadorActual();
        }
        /// <summary>
        /// Limpia los campos del formulario 
        /// </summary>
        private void LimpiarCampos()
        {
            TBid.Text = "0";
            TBdinero.Text = "";
            TBtexto.Text = "";
            TBimagen.Text = "";
        }

        /// <summary>
        /// Valida que los campos obligatorios tengan datos correctos
        /// y muestra mensajes de error si no cumplen.
        /// </summary>
        /// <returns>True si los campos son válidos, false si no lo son.</returns>
        private bool ValidarCampos()
        {   //Comprobamos si cumple las normas
            if (string.IsNullOrWhiteSpace(TBdinero.Text) || !float.TryParse(TBdinero.Text, out _))
            {
                MostrarError("Ingrese un valor válido para el pago");
                return false;
            }

            if (string.IsNullOrWhiteSpace(TBtexto.Text))
            {
                MostrarError("Ingrese un texto descriptivo");
                return false;
            }

            if (string.IsNullOrWhiteSpace(TBimagen.Text))
            {
                MostrarError("Ingrese una ruta de imagen");
                return false;
            }

            return true;
        }
        /// <summary>
        /// Muestra una mensaje de error en pantalla 
        /// </summary>
        /// <param name="mensaje">Mensaje de error que se va a mostrar.</param>
        private void MostrarError(string mensaje)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "showerror",
                $"alert('ERROR: {mensaje.Replace("'", "\\'")}');", true);
        }

        /// <summary>
        /// Selecciona en el DropDownList el patrocinador con el Id especificado.
        /// Si no existe, selecciona la opción "Nuevo patrocinador".
        /// </summary>
        /// <param name="id">Id del patrocinador.</param>
        private void SeleccionarPatrocinador(int id)
        {
            var item = ListaPatrocinadores.Items.FindByValue(id.ToString());
            if (item != null)
            {
                ListaPatrocinadores.SelectedValue = id.ToString();
            }
            else
            {
                ListaPatrocinadores.SelectedIndex = 0;
            }
        }

        /// <summary>
        /// Muestra un mensaje con el texto recibido.
        /// </summary>
        /// <param name="mensaje">Mensaje que se ha de mostrar.</param>
        private void MostrarMensaje(string mensaje)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "showalert",
                $"alert('{mensaje.Replace("'", "\\'")}');", true);
        }

        /// <summary>
        /// Obtiene un nuevo Id para un patrocinador, que es el máximo Id que haya + 1,
        /// si no hay patrocinadores, devuelve 1.
        /// </summary>
        /// <returns>El nuevo Id.</returns>
        private int ObtenerNuevoId()
        {//Creamos una función para asignar automáticamente el ID al patrocinador
            var patrocinadores = new ENPatrocinador().ReadAll();
            return patrocinadores.Count > 0 ? patrocinadores.Max(p => p.Id_patrocinador) + 1 : 1;
        }

    }
}