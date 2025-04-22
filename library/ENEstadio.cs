using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace library
{
    public class ENEstadio
    {
        int _id_estadio;
        string _nombre;
        string _ciudad;
        string _direccion;

        public int Id_estadio
        {
            get { return _id_estadio; }
            set { _id_estadio = value; }
        }

        public string Nombre
        {
            get { return _nombre; }
            set { _nombre = value; }
        }

        public string Ciudad
        {
            get { return _ciudad; }
            set { _ciudad = value; }
        }

        public string Direccion
        {
            get { return _direccion; }
            set { _direccion = value; }
        }

        public ENEstadio()
        {
            _id_estadio = 0;
            _nombre = "";
            _ciudad = "";
            _direccion = "";
        }

        public ENEstadio(int id_estadio, string nombre, string ciudad, string direccion)
        {
            Id_estadio = id_estadio;
            Nombre = nombre;
            Ciudad = ciudad;
            Direccion = direccion;
        }

        public bool Create()
        {
            CADEstadio estadio = new CADEstadio();
            return estadio.Create(this);
        }

        public bool Delete()
        {
            CADEstadio estadio = new CADEstadio();
            return estadio.Delete(this);
        }

        public bool Update()
        {
            CADEstadio estadio = new CADEstadio();
            return estadio.Update(this);
        }

        public bool Read()
        {
            CADEstadio estadio = new CADEstadio();
            return estadio.Read(this);
        }

        public List<ENEstadio> ReadAll()
        {
            CADEstadio estadio = new CADEstadio();
            return estadio.ReadAll(this);
        }
    }
}

