using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using library;

namespace ProWeb
{
	public partial class DBPatrocinadores : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
            if (!IsPostBack) 
            {
                CargarPatrocinadores();
                MostrarPatrocinadorActual();
            }

        }

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

        protected void ListaPatrocinadores_SelectedIndexChanged(object sender, EventArgs e)
        {
            MostrarPatrocinadorActual();
        }

        private void LimpiarCampos()
        {
            TBid.Text = "0";
            TBdinero.Text = "";
            TBtexto.Text = "";
            TBimagen.Text = "";
        }

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
        private void MostrarError(string mensaje)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "showerror",
                $"alert('ERROR: {mensaje.Replace("'", "\\'")}');", true);
        }
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

        private void MostrarMensaje(string mensaje)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "showalert",
                $"alert('{mensaje.Replace("'", "\\'")}');", true);
        }
        private int ObtenerNuevoId()
        {//Creamos una función para asignar automáticamente el ID al patrocinador
            var patrocinadores = new ENPatrocinador().ReadAll();
            return patrocinadores.Count > 0 ? patrocinadores.Max(p => p.Id_patrocinador) + 1 : 1;
        }

    }
}