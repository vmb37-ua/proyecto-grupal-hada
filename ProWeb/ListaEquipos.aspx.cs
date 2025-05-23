using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using library;

namespace ProWeb
{
    /// <summary>
    /// Página ASP.NET que permite visualizar un listado de equipos deportivos.
    /// Ofrece funcionalidades de filtrado por categoría, búsqueda por nombre y gestión de favoritos por usuario.
    /// </summary>
    public partial class ListaEquipos : System.Web.UI.Page
	{
        /// <summary>
        /// Evento que se ejecuta al cargar la página.
        /// Si no es un postback, carga la lista completa de equipos y las categorías disponibles.
        /// </summary>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarEquipos();
                CargarCategorias();
            }
        }

        /// <summary>
        /// Carga todos los equipos desde la base de datos y los muestra en el Repeater.
        /// También obtiene los favoritos del usuario actual desde la sesión y los guarda en ViewState.
        /// </summary>
        private void CargarEquipos()
        {
            ENEquipo en = new ENEquipo();
            List<ENEquipo> equipos = en.ReadAll();

            int idUsuario = Convert.ToInt32(Session["Login"]);

            List<ENFavoritos> favoritos = new ENFavoritos { IdUsuario = idUsuario }.ReadAllUsuario();
            ViewState["favoritosIdEquipos"] = favoritos.Select(f => f.IdEquipo).ToList();

            rptEquipos.DataSource = equipos;
            rptEquipos.DataBind();
        }

        /// <summary>
        /// Carga los equipos de una categoría específica seleccionada por el usuario.
        /// </summary>
        /// <param name="categoria">Nombre de la categoría seleccionada.</param>
        private void CargarEquiposPorCategoria(string categoria)
        {
            ENEquipo en = new ENEquipo();
            en.Categoria = categoria;
            List<ENEquipo> equipos = en.ReadAllbyCategoria();

            

            rptEquipos.DataSource = equipos;
            rptEquipos.DataBind();

        }

        /// <summary>
        /// Carga todas las categorías de equipos disponibles desde la base de datos
        /// y las muestra en un desplegable para permitir filtrado.
        /// </summary>
        private void CargarCategorias()
        {
            ENCategoria en = new ENCategoria();
            List<ENCategoria> categorias = en.ReadAll();

            ddlCategorias.DataSource = categorias;
            ddlCategorias.DataValueField = "Nombre";
            ddlCategorias.DataTextField = "Nombre";
            ddlCategorias.DataBind();

            // Opcion por defecto
            ddlCategorias.Items.Insert(0, new ListItem("Categorias", "0"));
        }

        /// <summary>
        /// Evento que se lanza cuando el usuario cambia el texto en la caja de búsqueda.
        /// Filtra los equipos cuyo nombre contenga el texto buscado.
        /// </summary>
        protected void txtBusqueda_TextChanged(object sender, EventArgs e)
        {
            string nombre = txtBusqueda.Text.ToLower();
            ENEquipo en = new ENEquipo();
            List<ENEquipo> equipos = en.ReadAll();

            // Filtro simple por nombre
            var filtrados = equipos
                .Where(eq => eq.Nombre.ToLower().Contains(nombre))
                .ToList();

            rptEquipos.DataSource = filtrados;
            rptEquipos.DataBind();
        }

        /// <summary>
        /// Evento que se lanza cuando el usuario selecciona una categoría del desplegable.
        /// Filtra los equipos según la categoría seleccionada o muestra todos si no se selecciona ninguna.
        /// </summary>
        protected void ddlCategorias_SelectedIndexChanged(object sender, EventArgs e)
        {
            string categoria = ddlCategorias.SelectedValue;

            if (categoria == "0")
            {
                // Mostrar todos los equipos
                CargarEquipos();
            }
            else
            {
                
                CargarEquiposPorCategoria(categoria); 
            }
        }

        /// <summary>
        /// Evento que se lanza cuando se hace clic en un botón del Repeater de equipos.
        /// Permite añadir o quitar un equipo de los favoritos del usuario actual.
        /// </summary>
        protected void rptEquipos_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "ToggleFavorito")
            {
                int idEquipo = int.Parse(e.CommandArgument.ToString());
                int idUsuario = Convert.ToInt32(Session["Login"]);

                ENFavoritos fav = new ENFavoritos
                {
                    IdEquipo = idEquipo,
                    IdUsuario = idUsuario
                };

                // Ver si ya es favorito
                List<ENFavoritos> favoritos = fav.ReadAllUsuario();
                ENFavoritos yaFavorito = favoritos.Find(f => f.IdEquipo == idEquipo);

                if (yaFavorito != null)
                {
                    fav.IdFavorito = yaFavorito.IdFavorito;
                    fav.Delete();
                }
                else
                {
                    fav.Create();
                }

                // Recargar equipos o favoritos
                CargarEquipos();
            }
        }
    }
}