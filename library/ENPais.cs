using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace library
{
    public class ENPais
    {
        int _idPais;
        string _nombrePais;

        public int IdPais
        {
            get { return _idPais; }
            set { _idPais = value; }
        }

        public string NombrePais
        {
            get { return _nombrePais; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("El nombre del país no puede estar vacío.");
                _nombrePais = value.Trim();
            }
        }

        public ENPais() { }

        public ENPais(int idPais, string nombre)
        {
            IdPais = idPais;
            NombrePais = nombre;
        }

        public bool CrearPais()
        {
            CADPais cad = new CADPais();
            return cad.CrearPais(this);
        }

        public bool EliminarPais()
        {
            CADPais cad = new CADPais();
            return cad.EliminarPais(this);
        }
    }
}
