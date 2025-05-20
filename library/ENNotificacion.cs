using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace library
{
    public class ENNotificacion
    {
        private int _id;
        private int _idUsuario;
        private string _mensaje;

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

        public string Mensaje
        {
            get { return _mensaje; }
            set { _mensaje = value; }
        }

        public ENNotificacion()
        {
            _id = 0;
            _idUsuario = 0;
            _mensaje = string.Empty;
        }

        public ENNotificacion(int id, int idUsuario, string mensaje)
        {
            _id = id;
            _idUsuario = idUsuario;
            _mensaje = mensaje;
        }

        public bool Create()
        {
            CADNotificacion cad = new CADNotificacion();
            return cad.Create(this);
        }

        public bool Delete()
        {
            CADNotificacion cad = new CADNotificacion();
            return cad.Delete(this);
        }

        public bool Update()
        {
            CADNotificacion cad = new CADNotificacion();
            return cad.Update(this);
        }

        public bool Read()
        {
            CADNotificacion cad = new CADNotificacion();
            return cad.Read(this);
        }

        public List<ENNotificacion> ReadAll()
        {
            CADNotificacion cad = new CADNotificacion();
            return cad.ReadAll(this);
        }
    }
}
