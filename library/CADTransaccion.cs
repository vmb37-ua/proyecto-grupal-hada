using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using library;

namespace library
{
    //clase de acceso a datos para la entidad transaccion
    public class CADTransaccion
    {
        //crea una nueva transacción en la base de datos
        public bool CrearTransaccion(Transaccion t)
        {
            throw new NotImplementedException();
        }

        //lee una transacción por su id
        public Transaccion LeerTransaccion(int id)
        {
            throw new NotImplementedException();
        }

        //devuelve todas las transacciones de un usuario
        public List<Transaccion> LeerTransaccionesPorUsuario(int idUsuario)
        {
            throw new NotImplementedException();
        }

        //actualiza una transacción que ya existe
        public bool ActualizarTransaccion(Transaccion t)
        {
            throw new NotImplementedException();
        }

        //elimina una transacción por su id
        public bool BorrarTransaccion(int id)
        {
            throw new NotImplementedException();
        }

        //busca transacciones por tipo (ingreso, retiro...)
        public List<Transaccion> BuscarPorTipo(string tipo)
        {
            throw new NotImplementedException();
        }
    }
}