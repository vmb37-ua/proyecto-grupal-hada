using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace library
{
    public class ENTransaccion
    {
        private int _id;
        private int _idUsuario;
        private float _cantidad;
        private string _metodoPago;

        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }

        public int IdUsuario
        {
            get { return _idUsuario; }
            set { _idUsuario = value; }
        }

        public float Cantidad
        {
            get { return _cantidad; }
            set { _cantidad = value; }
        }

        public string MetodoPago
        {
            get { return _metodoPago; }
            set { _metodoPago = value; }
        }

        public ENTransaccion()
        {
            _id = 0;
            _idUsuario = 0;
            _cantidad = 0;
            _metodoPago = string.Empty;
        }

        public ENTransaccion(int id, int idUsuario, float cantidad, string metodoPago)
        {
            _id = id;
            _idUsuario = idUsuario;
            _cantidad = cantidad;
            _metodoPago = metodoPago;
        }

        public bool Create()
        {
            CADTransaccion cad = new CADTransaccion();
            return cad.Create(this);
        }

        public bool Delete()
        {
            CADTransaccion cad = new CADTransaccion();
            return cad.Delete(this);
        }

        public bool Update()
        {
            CADTransaccion cad = new CADTransaccion();
            return cad.Update(this);
        }

        public bool Read()
        {
            CADTransaccion cad = new CADTransaccion();
            return cad.Read(this);
        }

        public List<ENTransaccion> ReadAll()
        {
            CADTransaccion cad = new CADTransaccion();
            return cad.ReadAll(this);
        }
    }
}