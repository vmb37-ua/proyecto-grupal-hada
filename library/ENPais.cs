using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace library
{
    class ENPais
    {
        public int idPais;
        public string nombrePais;

        public ENPais() { }

        public ENPais(int idPais, string nombre)
        {
            this.idPais = idPais;
            this.nombrePais = nombre;
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

        public bool ModificarPais()
        {
            CADPais cad = new CADPais();
            return cad.ModificarPais(this);
        }

        public bool Read()
        {
            CADPais cad = new CADPais();
            return cad.Read(this);
        }

        public List<ENPais> ReadAll()
        {
            CADPais cad = new CADPais();
            return cad.ReadAll(this);
        }


    }
}
