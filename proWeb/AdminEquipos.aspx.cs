    using System;
using System.Web.UI;
using System.IO;

using library;

    namespace ProWeb
    {
        public partial class AdminEquipos : System.Web.UI.Page
        {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarCategorias();
            }
        }

        private void CargarCategorias()
        {
            string connStr = System.Configuration.ConfigurationManager.ConnectionStrings["miconex"].ToString();
            using (var conn = new System.Data.SqlClient.SqlConnection(connStr))
            {
                var cmd = new System.Data.SqlClient.SqlCommand("SELECT nombre FROM categoria", conn);
                conn.Open();
                ddlCategoria.DataSource = cmd.ExecuteReader();
                ddlCategoria.DataTextField = "nombre";
                ddlCategoria.DataValueField = "nombre";
                ddlCategoria.DataBind();
            }
        }


      protected void btnCrear_Click(object sender, EventArgs e)
{
    try
    {
        if (!int.TryParse(txtIdEquipo.Text, out int id))
        {
            MostrarMensaje("El ID debe ser un número válido.");
            return;
        }

        if (string.IsNullOrWhiteSpace(txtNombre.Text) || ddlCategoria.SelectedValue == "")
        {
            MostrarMensaje("Todos los campos son obligatorios.");
            return;
        }

        string escudoRuta = "";
        if (fileEscudo.HasFile)
        {
            string nombreArchivo = Path.GetFileName(fileEscudo.FileName);
            string ruta = Server.MapPath("~/Source/Images/") + nombreArchivo;
            fileEscudo.SaveAs(ruta);
            escudoRuta = "Source/Images/" + nombreArchivo;
        }
        else
        {
            MostrarMensaje("Debes subir un archivo para el escudo.");
            return;
        }

        ENEquipo equipo = new ENEquipo
        {
            Id_equipo = id,
            Nombre = txtNombre.Text,
            Escudo = escudoRuta,
            Categoria = ddlCategoria.SelectedValue
        };

        if (equipo.Create())
            MostrarMensaje("Equipo creado correctamente.");
        else
            MostrarMensaje("Error al crear el equipo.");
    }
    catch (Exception ex)
    {
        MostrarMensaje("ERROR: " + ex.Message.Replace("'", ""));
    }
}

               







        protected void btnActualizar_Click(object sender, EventArgs e)
        {
            if (int.TryParse(txtIdEquipo.Text, out int id))
            {
                ENEquipo equipo = new ENEquipo
                {
                    Id_equipo = id,
                    Nombre = txtNombre.Text,
                    Escudo = "", // o mantener el valor anterior si no se cambia
                    Categoria = ddlCategoria.SelectedValue

                };

                if (equipo.Update())
                    MostrarMensaje("Equipo actualizado correctamente.");
                else
                    MostrarMensaje("Error al actualizar el equipo.");
            }
            else
            {
                MostrarMensaje("El ID debe ser un número válido.");
            }
        }


        protected void btnEliminar_Click(object sender, EventArgs e)
            {
                if (int.TryParse(txtIdEquipo.Text, out int id))
                {
                    ENEquipo equipo = new ENEquipo { Id_equipo = id };

                    if (equipo.Delete())
                        MostrarMensaje("Equipo eliminado correctamente.");
                    else
                        MostrarMensaje("Error al eliminar el equipo.");
                }
                else
                {
                    MostrarMensaje("El ID debe ser un número válido.");
                }
            }

        private void MostrarMensaje(string mensaje)
        {
            lblMensaje.Text = mensaje;
        }

    }
}