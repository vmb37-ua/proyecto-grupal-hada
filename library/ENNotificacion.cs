using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace library
{
    //esta clas representa una notificación enviada a un usuario
    public class Notificacion
    {
        //el id único de la notificación
        private int id;

        //id del usuario al que se envía la notificación
        private int idUsuario;

        //el texti del mensaje
        private string mensaje;

        //la fecha y hora del envío
        private DateTime fecha;

        //indica si la notificación ha sido leída por el usuario
        private bool leida;

        //las propiedades públicas
        public int Id { get => id; set => id = value; }
        public int IdUsuario { get => idUsuario; set => idUsuario = value; }
        public string Mensaje { get => mensaje; set => mensaje = value; }
        public DateTime Fecha { get => fecha; set => fecha = value; }
        public bool Leida { get => leida; set => leida = value; }

        public Notificacion() { }

        //el constructor completo
        public Notificacion(int id, int idUsuario, string mensaje, DateTime fecha, bool leida)
        {
            this.id = id;
            this.idUsuario = idUsuario;
            this.mensaje = mensaje;
            this.fecha = fecha;
            this.leida = leida;
        }

        //método que llama al CAD para guardar la notificación
        public bool Guardar()
        {
            CADNotificacion cad = new CADNotificacion();
            return cad.CrearNotificacion(this);
        }
    }
}