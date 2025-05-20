using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace library
{
    public class ENMunicipio
    {
        int _id_municipio;
        int _id_provincia;
        string _nombre;

        public int Id_municipio {
            get { return _id_municipio; }
            set { _id_municipio = value; }
        }
        public int Id_provincia {
            get { return _id_provincia; }
            set { _id_provincia = value; }
        }
        public string Nombre { 
            get { return _nombre; }
            set { _nombre = value; }
        }
        public ENMunicipio(){
            _id_municipio=0;
            _id_provincia=0;
            _nombre = "";
        }
        public ENMunicipio(int id_municipio, int id_provincia, int id_pais, string nombre)
        {
            Id_municipio = id_municipio;
            Id_provincia = id_provincia;
            Nombre = nombre;
        }
        public bool Create() {
            CADMunicipio municipio = new CADMunicipio();
            return municipio.Create(this);
        }
        public bool Delete() { 
            CADMunicipio municipio =new CADMunicipio();
            return municipio.Delete(this);
        }
        public bool Update() {
            CADMunicipio municipio = new CADMunicipio();
            return municipio.Update(this);
        }
        public bool Read() { 
            CADMunicipio municipio=new CADMunicipio();
            return municipio.Read(this);
        }
        public List<ENMunicipio> ReadAll() {
            CADMunicipio municipio = new CADMunicipio();
            return municipio.ReadAll(this);
        }
        public List<ENMunicipio> ReadAllByProvincia()
        {
            CADMunicipio municipio = new CADMunicipio();
            return municipio.ReadAllByProvincia(this);
        }
    }
}
