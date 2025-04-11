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

        // Identificador del usuario que realiza la apuesta
        private int idUsuario;

        // Identificador de la apuesta a la que corresponde
        private int idApuesta;

        // Cantidad que el usuario apuesta
        private decimal importe;

        // Resultado predicho por el usuario
        private string resultado_predicho;

        // Estado de la apuesta: Pendiente / Ganada / Perdida / Cancelada
        private string estadoApuesta;

        // Fecha y hora en la que se realiza la apuesta
        private DateTime fechaApuesta;

        // Resultado de la apuesta: Ganada / Perdida
        private string resultadoApuesta;

        // Ganancias
        private decimal ganancia;

        // Comentarios del usuario sobre la apuesta
        private string comentarios;

        // Propiedades públicas
        public int IdApuesta_usuario { get => idApuesta_usuario; set => idApuesta_usuario = value; }
        public int IdUsuario { get => idUsuario; set => idUsuario = value; }
        public int IdApuesta { get => idApuesta; set => idApuesta = value; }
        public decimal Importe { get => importe; set => importe = value; }
        public string Resultado_predicho { get => resultado_predicho; set => resultado_predicho = value; }
        public string EstadoApuesta { get => estadoApuesta; set => estadoApuesta = value; } 
        public DateTime FechaApuesta { get => fechaApuesta; set => fechaApuesta = value; }
        public string ResultadoApuesta { get => resultadoApuesta; set => resultadoApuesta = value; }
        public decimal Ganancia { get => ganancia; set => ganancia = value; }
        public string Comentarios { get => comentarios; set => comentarios = value; }

        public ENApuesta_usuario() { }

        public ENApuesta_usuario(int idApuesta_usuario, int idUsuario, int idApuesta, decimal importe, string resultado_predicho, string estadoApuesta, DateTime fechaApuesta, string resultadoApuesta, decimal ganancia, string comentarios)
        {
            IdApuesta_usuario = idApuesta_usuario;
            IdUsuario = idUsuario;
            IdApuesta = idApuesta;
            Importe = importe;
            Resultado_predicho = resultado_predicho;
            EstadoApuesta = estadoApuesta;
            FechaApuesta = fechaApuesta;
            ResultadoApuesta = resultadoApuesta;
            Ganancia = ganancia;
            Comentarios = comentarios;
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
