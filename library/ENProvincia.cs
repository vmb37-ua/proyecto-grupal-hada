using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace library
{
    public class ENProvincia
    {
        int idProvincia;
        int idPais;
        string nombre;

        public int IdProvincia
        {
            get { return idProvincia; }
            set { idProvincia = value; }
        }

        public int IdPais
        {
            get { return idPais; }
            set { idPais = value; }
        }

        public string Nombre
        {
            get { return nombre; }
            set { nombre = value; }
        }

        public ENProvincia()
        {
            idProvincia = 0;
            idPais = 0;
            nombre = "";
        }


        public ENProvincia(int idProvincia, int idPais, string nombre)
        {
            this.IdProvincia = idProvincia;
            this.IdPais = idPais;
            this.Nombre = nombre;
        }

        public bool Create()
        {
            CADProvincia provincia = new CADProvincia();
            return provincia.Create(this);
        }

        public bool Update()
        {
            CADProvincia provincia = new CADProvincia();
            return provincia.Update(this);
        }
        public bool Delete()
        {
            CADProvincia provincia = new CADProvincia();
            return provincia.Delete(this);
        }
        public List<ENProvincia> ReadAll()
        {
            CADProvincia provincia = new CADProvincia();
            return provincia.ReadAll(this);
        }
        public bool Read()
        {
            CADProvincia provincia = new CADProvincia();
            return provincia.Read(this);
        }

    }
}