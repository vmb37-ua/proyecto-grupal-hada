using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace library
{
    public class ENApuesta_usuario
    {
        private int _idUsuario;
        private int _idApuesta;
        private string _prediccion;  // "1", "X" o "2"
        private float _cantidad;
        private float _cuota;

        public int IdUsuario { get => _idUsuario; set => _idUsuario = value; }
        public int IdApuesta { get => _idApuesta; set => _idApuesta = value; }
        public string Prediccion { get => _prediccion; set => _prediccion = value; }
        public float Cantidad { get => _cantidad; set => _cantidad = value; }
        public float Cuota { get => _cuota; set => _cuota = value; }

        
        public ENApuesta_usuario() { }

        public ENApuesta_usuario(int idUsuario, int idApuesta, string prediccion, float cantidad, float cuota)
        {
            _idUsuario = idUsuario;
            _idApuesta = idApuesta;
            _prediccion = prediccion;
            _cantidad = cantidad;
            _cuota = cuota;
        }

        
        public bool Apostar()
        {
            CADApuesta_usuario cad = new CADApuesta_usuario();
            return cad.CrearApuesta(this);
        }

        public List<ENApuesta_usuario> ReadAll()
        {
            CADApuesta_usuario cad = new CADApuesta_usuario();
            return cad.ReadAll();
        }
    }
}
