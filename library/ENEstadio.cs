using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace library
{
    public class ENEstadio
    {
        string _nombre;
        int _capacidad;
        string _texto;
        int _id_municipio;

        public string Nombre
        {
            get { return _nombre; }
            set { _nombre = value; }
        }

        public int Capacidad
        {
            get { return _capacidad; }
            set { _capacidad = value; }
        }

        public string Texto
        {
            get { return _texto; }
            set { _texto = value; }
        }

        public int Id_municipio
        {
            get { return _id_municipio; }
            set { _id_municipio = value; }
        }

        public ENEstadio()
        {
            _nombre = "";
            _capacidad = 0;
            _texto = "";
            _id_municipio = 0;
        }

        public bool Create() => new CADEstadio().Create(this);
        public bool Update() => new CADEstadio().Update(this);
        public bool Delete() => new CADEstadio().Delete(this);
        public bool Read() => new CADEstadio().Read(this);
        public List<ENEstadio> ReadAll() => new CADEstadio().ReadAll(this);
    }
}
