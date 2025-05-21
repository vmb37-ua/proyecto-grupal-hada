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
            string nombreEquipo = txtNombre.Text.Trim();

            if (string.IsNullOrEmpty(nombreEquipo))
            {
                MostrarMensaje("Debes escribir el nombre del equipo a actualizar.");
                return;
            }

            // Buscar el equipo por nombre
            ENEquipo equipo = new ENEquipo { Nombre = nombreEquipo };
            if (!equipo.Read())
            {
                MostrarMensaje("No se encontró un equipo con ese nombre.");
                return;
            }

            string escudoRuta = equipo.Escudo; // valor actual, por si no sube uno nuevo

            if (fileEscudo.HasFile)
            {
                string nombreArchivo = Path.GetFileName(fileEscudo.FileName);
                string ruta = Server.MapPath("~/Source/Images/") + nombreArchivo;
                fileEscudo.SaveAs(ruta);
                escudoRuta = "Source/Images/" + nombreArchivo;
            }

            equipo.Escudo = escudoRuta;
            equipo.Categoria = ddlCategoria.SelectedValue;

            if (equipo.Update())
                MostrarMensaje("Equipo actualizado correctamente.");
            else
                MostrarMensaje("Error al actualizar el equipo.");
        }


        protected void btnEliminar_Click(object sender, EventArgs e)
        {
            string nombreEquipo = txtNombre.Text.Trim();

            if (string.IsNullOrEmpty(nombreEquipo))
            {
                MostrarMensaje("Debes escribir el nombre del equipo a eliminar.");
                return;
            }

            ENEquipo equipo = new ENEquipo { Nombre = nombreEquipo };

            if (!equipo.Read())
            {
                MostrarMensaje("No se encontró un equipo con ese nombre.");
                return;
            }

            if (equipo.Delete())
                MostrarMensaje("Equipo eliminado correctamente.");
            else
                MostrarMensaje("Error al eliminar el equipo.");
        }


        private void MostrarMensaje(string mensaje)
        {
            lblMensaje.Text = mensaje;
        }

    }
}