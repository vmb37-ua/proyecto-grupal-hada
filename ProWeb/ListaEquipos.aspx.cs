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
            }
        }

        private void CargarEquipos()
        {
            ENEquipo en = new ENEquipo();
            List<ENEquipo> equipos = en.ReadAll();

            foreach (var equipo in equipos)
            {
                equipo.Escudo = "Source/Images/" + equipo.Escudo;
            }

            rptEquipos.DataSource = equipos;
            rptEquipos.DataBind();
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

    }
}