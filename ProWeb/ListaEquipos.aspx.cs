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
	public partial class ListaEquipos : System.Web.UI.Page
	{
        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarEquipos();
                CargarCategorias();
            }
        }

        private void CargarEquipos()
        {
            ENEquipo en = new ENEquipo();
            List<ENEquipo> equipos = en.ReadAll();

            int idUsuario = Convert.ToInt32(Session["Login"]);

            List<ENFavoritos> favoritos = new ENFavoritos { IdUsuario = idUsuario }.ReadAllUsuario();
            ViewState["favoritosIdEquipos"] = favoritos.Select(f => f.IdEquipo).ToList();


            foreach (var equipo in equipos)
            {
                equipo.Escudo = "Source/Images/" + equipo.Escudo;
            }

            rptEquipos.DataSource = equipos;
            rptEquipos.DataBind();
        }

        private void CargarEquiposPorCategoria(string categoria)
        {
            ENEquipo en = new ENEquipo();
            en.Categoria = categoria;
            List<ENEquipo> equipos = en.ReadAllbyCategoria();

            foreach (var equipo in equipos)
            {
                equipo.Escudo = "Source/Images/" + equipo.Escudo;
            }

            rptEquipos.DataSource = equipos;
            rptEquipos.DataBind();

        }

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


        protected void txtBusqueda_TextChanged(object sender, EventArgs e)
        {
            string nombre = txtBusqueda.Text.ToLower();
            ENEquipo en = new ENEquipo();
            List<ENEquipo> equipos = en.ReadAll();

            // Filtro simple por nombre
            var filtrados = equipos
                .Where(eq => eq.Nombre.ToLower().Contains(nombre))
                .ToList();

            foreach (var equipo in filtrados)
            {
                equipo.Escudo = "Source/Images/" + equipo.Escudo;
            }

            rptEquipos.DataSource = filtrados;
            rptEquipos.DataBind();
        }

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