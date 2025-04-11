using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace library
{
    public class ENApuesta_usuario
    {
        // Identificador único de la apuesta del usuario
        private int idApuesta_usuario;

        // Identificador de la apuesta a la que corresponde
        private int idApuesta;

        // Resultado de la apuesta: Ganada / Perdida
        private string resultadoApuesta;

        // Propiedades públicas
        public int IdApuesta_usuario { get => idApuesta_usuario; set => idApuesta_usuario = value; }

        public string ResultadoApuesta { get => resultadoApuesta; set => resultadoApuesta = value; }

        public int IdApuesta { get => idApuesta; set => idApuesta = value; }

        public ENApuesta_usuario() { }

        public ENApuesta_usuario(int idApuesta_usuario, int idUsuario, int idApuesta, decimal importe, string resultado_predicho, string estadoApuesta, DateTime fechaApuesta, string resultadoApuesta, decimal ganancia, string comentarios)
        {
            IdApuesta_usuario = idApuesta_usuario;
            IdApuesta = idApuesta;
            ResultadoApuesta = resultadoApuesta;
        }

        public bool CrearApuesta()
        {
            CADApuesta_usuario cad = new CADApuesta_usuario();
            return cad.CrearApuesta(this);
        }

        public bool ModificarApuesta()
        {
            CADApuesta_usuario cad = new CADApuesta_usuario();
            return cad.ModificarApuesta(this);
        }

        public bool CancelarApuesta()
        {
            CADApuesta_usuario cad = new CADApuesta_usuario();
            return cad.CancelarApuesta(this);
        }

        public bool MostrarInfoApuesta()
        {
            CADApuesta_usuario cad = new CADApuesta_usuario();
            return cad.MostrarInfoApuesta(this);
        }
    }
}
