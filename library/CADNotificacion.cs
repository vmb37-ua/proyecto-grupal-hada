using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using library;

namespace library
{
    //clase de acceso a datos para la entidad Notificacion
    public class CADNotificacion
    {
        //crea una nueva notificación en la base de datos
        public bool CrearNotificacion(Notificacion n)
        {
            throw new NotImplementedException();
        }

        //devuelve una notificación por su id
        public Notificacion LeerNotificacion(int id)
        {
            throw new NotImplementedException();
        }

        //devuelve todas las notificaciones de un usuario
        public List<Notificacion> LeerNotificacionesPorUsuario(int idUsuario)
        {
            throw new NotImplementedException();
        }

        //actualiza una notificación que ya existe
        public bool ActualizarNotificacion(Notificacion n)
        {
            throw new NotImplementedException();
        }

        //borra una notificación por id
        public bool BorrarNotificacion(int id)
        {
            throw new NotImplementedException();
        }

        //devuelve las notificaciones no leídas de un usuario
        public List<Notificacion> BuscarNoLeidas(int idUsuario)
        {
            throw new NotImplementedException();
        }
    }
}