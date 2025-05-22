using System;
using System.Collections.Generic;

namespace library
{
    /// <summary>
    /// Clase de entidad que representa una transacción realizada por un usuario.
    /// Contiene información sobre la cantidad, método de pago y el usuario asociado.
    /// </summary>
    public class ENTransaccion
    {
        private int _id;
        private int _idUsuario;
        private float _cantidad;
        private string _metodoPago;

        /// <summary>
        /// Identificador único de la transacción.
        /// </summary>
        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }

        /// <summary>
        /// Identificador del usuario que realiza la transacción.
        /// </summary>
        public int IdUsuario
        {
            get { return _idUsuario; }
            set { _idUsuario = value; }
        }

        /// <summary>
        /// Cantidad de dinero implicada en la transacción.
        /// </summary>
        public float Cantidad
        {
            get { return _cantidad; }
            set { _cantidad = value; }
        }

        /// <summary>
        /// Método utilizado para realizar la transacción (ej: Ingreso, Retiro).
        /// </summary>
        public string MetodoPago
        {
            get { return _metodoPago; }
            set { _metodoPago = value; }
        }

        /// <summary>
        /// Constructor por defecto.
        /// </summary>
        public ENTransaccion()
        {
            _id = 0;
            _idUsuario = 0;
            _cantidad = 0;
            _metodoPago = string.Empty;
        }

        /// <summary>
        /// Constructor con parámetros para inicializar una transacción.
        /// </summary>
        public ENTransaccion(int id, int idUsuario, float cantidad, string metodoPago)
        {
            _id = id;
            _idUsuario = idUsuario;
            _cantidad = cantidad;
            _metodoPago = metodoPago;
        }

        /// <summary>
        /// Inserta la transacción actual en la base de datos.
        /// </summary>
        /// <returns>True si la operación fue exitosa.</returns>
        public bool Create()
        {
            CADTransaccion cad = new CADTransaccion();
            return cad.Create(this);
        }

        /// <summary>
        /// Elimina esta transacción de la base de datos.
        /// </summary>
        /// <returns>True si la eliminación fue exitosa.</returns>
        public bool Delete()
        {
            CADTransaccion cad = new CADTransaccion();
            return cad.Delete(this);
        }

        /// <summary>
        /// Actualiza los datos de esta transacción en la base de datos.
        /// </summary>
        /// <returns>True si la actualización fue exitosa.</returns>
        public bool Update()
        {
            CADTransaccion cad = new CADTransaccion();
            return cad.Update(this);
        }

        /// <summary>
        /// Carga los datos de la transacción desde la base de datos por su ID.
        /// </summary>
        /// <returns>True si se encontró la transacción.</returns>
        public bool Read()
        {
            CADTransaccion cad = new CADTransaccion();
            return cad.Read(this);
        }

        /// <summary>
        /// Devuelve una lista con todas las transacciones asociadas al usuario.
        /// </summary>
        /// <returns>Lista de transacciones.</returns>
        public List<ENTransaccion> ReadAll()
        {
            CADTransaccion cad = new CADTransaccion();
            return cad.ReadAll(this);
        }
    }
}
