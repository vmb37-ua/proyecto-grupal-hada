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
        float dinero;
        string texto;
        string imagen;

        public int Id_patrocinador
        {
            get { return id_patrocinador; }
            set { id_patrocinador = value; }
        }
        public float Dinero
        {
            get { return dinero; }
            set { dinero = value; }
        }
        public string Texto
        {
            get { return texto; }
            set { texto = value; }
        }
        public string Imagen
        {
            get { return imagen; }
            set { imagen = value; }
        }
        public ENPatrocinador()
        {
            id_patrocinador = 0;
            dinero = 0;
            texto = "";
            imagen = "";
        }
        public ENPatrocinador(int id_patrocinador, string nombre, float dinero, string descripcion, string imagen)
        {
            this.id_patrocinador = id_patrocinador;
            this.dinero = dinero;
            this.texto = descripcion;
            this.imagen = imagen;
        }
        public bool Create()
        {
            CADPatrocinador patrocinador = new CADPatrocinador();
            return patrocinador.Create(this);
        }
        public bool Update()
        {
            CADPatrocinador patrocinador = new CADPatrocinador();
            return patrocinador.Update(this);
        }
        public bool Delete()
        {
            CADPatrocinador patrocinador = new CADPatrocinador();
            return patrocinador.Delete(this);
        }
        public bool Read()
        {
            CADPatrocinador patrocinador = new CADPatrocinador();
            return patrocinador.Read(this);
        }
        public bool ReadFirst()
        {
            CADPatrocinador patrocinador = new CADPatrocinador();
            return patrocinador.ReadFirst(this);
        }
        public bool ReadNext()
        {
            CADPatrocinador patrocinador = new CADPatrocinador();
            return patrocinador.ReadNext(this);
        }
        public bool ReadPrev()
        {
            CADPatrocinador patrocinador = new CADPatrocinador();
            return patrocinador.ReadPrev(this);
        }
        public List<ENPatrocinador> ReadAll()
        {
            CADPatrocinador patrocinador = new CADPatrocinador();
            return patrocinador.ReadAll(this);
        }
    }
}
