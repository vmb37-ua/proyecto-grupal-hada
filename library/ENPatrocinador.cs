using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace library
{
    public class ENPatrocinador
    {
        int id_patrocinador;
        string nombre;
        float dinero;
        string descripcion;
        string imagen;

        public int Id_patrocinador
        {
            get { return id_patrocinador; }
            set { id_patrocinador = value; }
        }
        public string Nombre
        {
            get { return nombre; }
            set { nombre = value; }
        }
        public float Dinero
        {
            get { return dinero; }
            set { dinero = value; }
        }
        public string Descripcion
        {
            get { return descripcion; }
            set { descripcion = value; }
        }
        public string Imagen
        {
            get { return imagen; }
            set { imagen = value; }
        }
        public ENPatrocinador()
        {
            id_patrocinador = 0;
            nombre = "";
            dinero = 0;
            descripcion = "";
            imagen = "";
        }
        public ENPatrocinador(int id_patrocinador, string nombre, float dinero, string descripcion, string imagen)
        {
            this.id_patrocinador = id_patrocinador;
            this.nombre = nombre;
            this.dinero = dinero;
            this.descripcion = descripcion;
            this.imagen = imagen;
        }
        public bool Create()
        {
            CADPatrocinador patrocinador = new CADPatrocinador();
            return patrocinador.Create(this);
        }
        public bool Delete()
        {
            CADPatrocinador patrocinador = new CADPatrocinador();
            return patrocinador.Delete(this);
        }
        public bool Update()
        {
            CADPatrocinador patrocinador = new CADPatrocinador();
            return patrocinador.Update(this);
        }
        public bool Read()
        {
            CADPatrocinador patrocinador = new CADPatrocinador();
            return patrocinador.Read(this);
        }
        public List<ENPatrocinador> ReadAll()
        {
            CADPatrocinador patrocinador = new CADPatrocinador();
            return patrocinador.ReadAll(this);
        }
    }
}
