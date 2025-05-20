using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using library;

namespace ProWeb
{
    public partial class AdministrarRoles : System.Web.UI.Page
    {
        
        protected void IdEntrante(object sender, EventArgs e)
        {
            string texto = TBIdRol.Text.Trim();
            if (texto == "")
            {
                BotonIz.Visible = false;
                BotonBuscarRol.Visible = false;
                BotonDe.Visible = false;
            }
            else
            {
                BotonIz.Visible = true;
                BotonBuscarRol.Visible = true;
                BotonDe.Visible = true;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
    
        }
        protected void Create(object sender, EventArgs e)
        {
            LabelId.Visible = false;
            string nombre= TBNombreRol.Text;
            string descripcion= TBDescipcionRol.Text;
            ENRol rol = new ENRol();
            rol.Nombre = nombre;
            rol.Descripcion = descripcion;
            if (rol.Create())
            {
                MPECreacion.Show();
                ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), "cerrarPopup();", true);
            }
            else
            {
                LabelNombre.Text = "Todo mal";
                LabelNombre.Visible = true;
                LabelNombre.ForeColor = System.Drawing.Color.Red;
            }
        }
        protected void Update(object sender, EventArgs e)
        {
            LabelId.Visible = false;
            string nombre = TBNombreRol.Text;
        }
        protected void Delete(object sender, EventArgs e)
        {
            string id = TBIdRol.Text;
            ENRol rol = new ENRol();
            rol.Id_rol = int.Parse(id);
        }
        protected void Read(object sender, EventArgs e)
        {
            LabelId.Visible = false;
            int id;
            if(!int.TryParse(TBIdRol.Text, out id))
            {
                
            }
            else
            {
                ENRol rol = new ENRol();
                rol.Id_rol = id;
                if (rol.Read())
                {
                    TBNombreRol.Text=rol.Nombre;
                    TBDescipcionRol.Text=rol.Descripcion;
                }
                else
                {
                    LabelId.Text = "No existe la id";
                    LabelId.Visible = true;
                    LabelId.ForeColor = System.Drawing.Color.Red;
                }
            }
        }
        protected void ReadNext(object sender, EventArgs e)
        {

        }
        protected void ReadPrev(object sender, EventArgs e)
        {

        }
    }
}