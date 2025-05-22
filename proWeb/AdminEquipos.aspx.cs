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
            if (Session["Login"] == null)
            {
                Response.Redirect("Login.aspx");
                return;
            }
            // Solo cargamos los datos la primera vez
            if (!IsPostBack)
            {
                CargarCategorias();
                CargarEquipos();
            }
        }

        private void CargarEquipos()
        {
            // Llenamos el dropdown con todos los equipos existentes
            ENEquipo equipo = new ENEquipo();
            var lista = equipo.ReadAll();

            ddlEquipos.DataSource = lista;
            ddlEquipos.DataTextField = "Nombre";
            ddlEquipos.DataValueField = "Id_equipo";
            ddlEquipos.DataBind();
        }

        private void CargarCategorias()
        {
            // Cargamos las categorías de la base de datos
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

        protected void ddlEquipos_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Al seleccionar un equipo, cargamos sus datos
            int id = int.Parse(ddlEquipos.SelectedValue);
            ENEquipo equipo = new ENEquipo { Id_equipo = id };

            if (equipo.Read())
            {
                txtNombre.Text = equipo.Nombre;
                ddlCategoria.SelectedValue = equipo.Categoria;
            }
            else
            {
                MostrarMensaje("No se encontró el equipo.");
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
            // Actualizamos el equipo seleccionado
            if (!int.TryParse(ddlEquipos.SelectedValue, out int id))
            {
                MostrarMensaje("Selecciona un equipo válido.");
                return;
            }

            ENEquipo equipo = new ENEquipo { Id_equipo = id };
            if (!equipo.Read())
            {
                MostrarMensaje("No se encontró un equipo con ese ID.");
                return;
            }

            string escudoRuta = equipo.Escudo;

            if (fileEscudo.HasFile)
            {
                string nombreArchivo = Path.GetFileName(fileEscudo.FileName);
                string ruta = Server.MapPath("~/Source/Images/") + nombreArchivo;
                fileEscudo.SaveAs(ruta);
                escudoRuta = "Source/Images/" + nombreArchivo;
            }

            equipo.Nombre = txtNombre.Text.Trim();
            equipo.Escudo = escudoRuta;
            equipo.Categoria = ddlCategoria.SelectedValue;

            if (equipo.Update())
                MostrarMensaje("Equipo actualizado correctamente.");
            else
                MostrarMensaje("Error al actualizar el equipo.");
        }

        protected void btnEliminar_Click(object sender, EventArgs e)
        {
            // Eliminamos el equipo seleccionado
            if (!int.TryParse(ddlEquipos.SelectedValue, out int id))
            {
                MostrarMensaje("Selecciona un equipo válido.");
                return;
            }

            ENEquipo equipo = new ENEquipo { Id_equipo = id };

            if (!equipo.Read())
            {
                MostrarMensaje("No se encontró un equipo con ese ID.");
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