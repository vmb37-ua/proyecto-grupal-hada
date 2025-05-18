using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
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
            LabelNombre.Visible = false;
            ENRol rol = new ENRol();
            rol.Nombre = TBNombreRol.Text;
            rol.Descripcion = TBDescipcionRol.Text;
            if (rol.Create())
            {
                LabelPanelOperacion.Text = "Rol <b>" + TBNombreRol.Text + "</b> creado con exito";
                MPECreacion.Show();
                ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), "cerrarPopup();", true);
            }
            else
            {
                LabelNombre.Text = "No se ha podido crear el rol";
                LabelNombre.Visible = true;
                LabelNombre.ForeColor = System.Drawing.Color.Red;
            }
        }
        protected void Update(object sender, EventArgs e)
        {
            LabelId.Visible = false;
            LabelNombre.Visible = false;
            ENRol rol = new ENRol();
            rol.Id_rol = int.Parse(TBIdRol.Text);
            rol.Nombre = TBNombreRol.Text;
            rol.Descripcion = TBDescipcionRol.Text;
            if (rol.Update())
            {
                LabelPanelOperacion.Text = "Rol <b>" + TBNombreRol.Text + "</b> actualizado con exito";
                MPECreacion.Show();
                ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), "cerrarPopup();", true);
            }
            else
            {
                LabelId.Text = "No existe la id";
                LabelId.Visible = true;
                LabelId.ForeColor = System.Drawing.Color.Red;
            }
        }
        protected void Delete(object sender, EventArgs e)
        {
            LabelId.Visible = false;
            LabelNombre.Visible = false;
            ENRol rol = new ENRol();
            rol.Id_rol = int.Parse(TBIdRol.Text);
            if (rol.Delete())
            {
                LabelPanelOperacion.Text = "Rol <b>" + TBNombreRol.Text + "</b> eliminado con exito";
                MPECreacion.Show();
                ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), "cerrarPopup();", true);
                TBIdRol.Text = "";
                TBNombreRol.Text = "";
                TBDescipcionRol.Text = "";
                BotonIz.Visible = false;
                BotonBuscarRol.Visible = false;
                BotonDe.Visible = false;
            }
            else
            {
                LabelId.Text = "No existe la id";
                LabelId.Visible = true;
                LabelId.ForeColor = System.Drawing.Color.Red;
            }
        }
        protected void Read(object sender, EventArgs e)
        {
            LabelId.Visible = false;
            LabelNombre.Visible = false;
            ENRol rol = new ENRol();
            rol.Id_rol = int.Parse(TBIdRol.Text);
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
        protected void ReadFirst(object sender, EventArgs e)
        {
            LabelId.Visible = false;
            LabelNombre.Visible = false;
            ENRol rol = new ENRol();
            if (rol.ReadFirst())
            {
                TBIdRol.Text = rol.Id_rol.ToString();
                TBNombreRol.Text = rol.Nombre;
                TBDescipcionRol.Text = rol.Descripcion;
                BotonIz.Visible = true;
                BotonBuscarRol.Visible = true;
                BotonDe.Visible = true;
            }
            else
            {
                LabelId.Text = "No existen roles";
                LabelId.Visible = true;
                LabelId.ForeColor = System.Drawing.Color.Red;
                TBIdRol.Text = "";
                TBNombreRol.Text = "";
                TBDescipcionRol.Text = "";
                BotonIz.Visible = false;
                BotonBuscarRol.Visible = false;
                BotonDe.Visible = false;
            }
        }
        protected void ReadNext(object sender, EventArgs e)
        {
            LabelId.Visible = false;
            LabelNombre.Visible = false;
            ENRol rol = new ENRol();
            rol.Id_rol = int.Parse(TBIdRol.Text);
            if (rol.Read())
                if (rol.ReadNext())
                {
                    TBIdRol.Text = rol.Id_rol.ToString();
                    TBNombreRol.Text = rol.Nombre;
                    TBDescipcionRol.Text = rol.Descripcion;
                }
                else
                {
                    LabelId.Text = "No hay un rol siguiente";
                    LabelId.Visible = true;
                    LabelId.ForeColor = System.Drawing.Color.Red;
                }
            else
            {
                LabelId.Text = "No existe la id";
                LabelId.Visible = true;
                LabelId.ForeColor = System.Drawing.Color.Red;
            }
        }
        protected void ReadPrev(object sender, EventArgs e)
        {
            LabelId.Visible = false;
            LabelNombre.Visible = false;
            ENRol rol = new ENRol();
            rol.Id_rol = int.Parse(TBIdRol.Text);
            if (rol.Read()) {
                if (rol.ReadPrev())
                {
                    TBIdRol.Text = rol.Id_rol.ToString();
                    TBNombreRol.Text = rol.Nombre;
                    TBDescipcionRol.Text = rol.Descripcion;
                }
                else
                {
                    LabelId.Text = "No hay un rol anterior";
                    LabelId.Visible = true;
                    LabelId.ForeColor = System.Drawing.Color.Red;
                }
            }
            else
            {
                LabelId.Text = "No existe la id";
                LabelId.Visible = true;
                LabelId.ForeColor = System.Drawing.Color.Red;
            }
        }
    }
}