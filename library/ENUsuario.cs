using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace library
{
    public class ENUsuario
    {
        int _id;
        string _nombre;
        float _saldo;
        string _datos;
        int _rol;
        string _direccion;
        int _pais;
        int _provincia;
        int _municipio;
        string _telefono;

        public int ID
        {
            get { return _id; }
            set { _id = value; }
        }
        public string Nombre
        {
            get { return _nombre; }
            set { _nombre = value; }
        }

        public float Saldo
        {
            get { return _saldo; }
            set { _saldo = value; }
        }

        public string Datos
        {
            get { return _datos; }
            set { _datos = value; }
        }

        public int Rol
        {
            get { return _rol; }
            set { _rol = value; }
        }

        public string Direccion
        {
            get { return _direccion; }
            set { _direccion = value; }
        }

        public int Pais
        {
            get { return _pais; }
            set { _pais = value; }
        }

        public int Provincia
        {
            get { return _provincia; }
            set { _provincia = value; }
        }

        public int Municipio
        {
            get { return _municipio; }
            set { _municipio = value; }
        }

        public string Telefono
        {
            get { return _telefono; }
            set { _telefono = value; }
        }

        public ENUsuario() {
            _id = 0;
            _nombre = "";
            _saldo = 0;
            _datos = "";
            _rol = 0;
            _direccion = "";
            _pais = 0;
            _provincia = 0;
            _municipio= 0;
            _telefono = "";
        }

        public ENUsuario(int id, string nombre, float saldo, string datos, int rol, string direccion, int pais, int provincia, int municipio, string telefono)
        {
            ID = id;
            Nombre = nombre;
            Saldo = saldo;
            Datos = datos;
            Rol = rol;
            Direccion = direccion;
            Pais = pais;
            Provincia = provincia;
            Municipio = municipio;
            Telefono = telefono;
        }

        public bool Create() {
            CADUsuario usu = new CADUsuario();
            return usu.Create(this);
        }
        public bool Delete() {
            CADUsuario usu = new CADUsuario();
            return usu.Delete(this);
        }
        public bool Read() {
            CADUsuario usu = new CADUsuario();
            return usu.Read(this);
        }
        public bool Update() {
            CADUsuario usu = new CADUsuario();
            return usu.Update(this);
        }
    }
}
