using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace library
{
    public class ENRol
    {
        int id_rol;
        string nombre;
        string descripcion;

        public int Id_rol
        {
            get { return id_rol; }
            set { id_rol = value; }
        }
        public string Nombre
        {
            get { return nombre; }
            set { nombre = value; }
        }
        public string Descripcion
        {
            get { return descripcion; }
            set { descripcion = value; }
        }
        public ENRol()
        {
            id_rol = 0;
            nombre = "";
            descripcion = "";
        }
        public ENRol(int id_rol, string nombre, string descripcion)
        {
            this.id_rol = id_rol;
            this.nombre = nombre;
            this.descripcion = descripcion;

        }
        public bool Create()
        {
            CADRol rol = new CADRol();
            return rol.Create(this);
        }
        public bool Update()
        {
            CADRol rol = new CADRol();
            return rol.Update(this);
        }
        public bool Delete()
        {
            CADRol rol = new CADRol();
            return rol.Delete(this);
        }
        public bool Read()
        {
            CADRol rol = new CADRol();
            return rol.Read(this);
        }
        public bool ReadFirst()
        {
            CADRol rol = new CADRol();
            return rol.ReadFirst(this);
        }
        public bool ReadNext()
        {
            CADRol rol = new CADRol();
            return rol.ReadNext(this);
        }
        public bool ReadPrev()
        {
            CADRol rol = new CADRol();
            return rol.ReadPrev(this);
        }
        public List<ENRol> ReadAll()
        {
            CADRol rol = new CADRol();
            return rol.ReadAll();
        }
    }
}
