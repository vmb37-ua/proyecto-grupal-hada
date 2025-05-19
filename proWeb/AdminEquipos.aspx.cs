    using System;
    using System.Web.UI;
    using library;

    namespace ProWeb
    {
        public partial class AdminEquipos : System.Web.UI.Page
        {
            protected void Page_Load(object sender, EventArgs e)
            {
            }

        protected void btnCrear_Click(object sender, EventArgs e)
        {
            try
            {
                MostrarMensaje("Entró al botón crear"); // Esto se debe ver
                if (int.TryParse(txtIdEquipo.Text, out int id))
                {
                    ENEquipo equipo = new ENEquipo
                    {
                        Id_equipo = id,
                        Nombre = txtNombre.Text,
                        Escudo = txtEscudo.Text,
                        Categoria = txtCategoria.Text
                    };

                    if (equipo.Create())
                        MostrarMensaje("Equipo creado correctamente.");
                    else
                        MostrarMensaje("Error al crear el equipo (no se insertó).");
                }
                else
                {
                    MostrarMensaje("El ID debe ser un número válido.");
                }
            }
            catch (Exception ex)
            {
                MostrarMensaje("ERROR SQL: " + ex.Message.Replace("'", ""));
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
                    Escudo = txtEscudo.Text,
                    Categoria = txtCategoria.Text
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