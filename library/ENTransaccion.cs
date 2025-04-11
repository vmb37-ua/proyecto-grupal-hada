using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace library
{
    //clase que representa una transacción económica hecha por un usuario
    public class Transaccion
    {
        //el identificador único de la transacción
        private int id;

        //identificador del usuario que hace la transacción
        private int idUsuario;

        //la cantidad de dinero implicada en la transacción
        private decimal cantidad;

        //la fecha y hora de la transacción
        private DateTime fecha;

        //el tipo de transacción (ej: ingreso, retiro, ganancia...)
        private string tipo;

        //el método del pago usado (ej: tarjeta, PayPal, transferencia...)
        private string metodoPago;

        //propiedades públicas
        public int Id { get => id; set => id = value; }
        public int IdUsuario { get => idUsuario; set => idUsuario = value; }
        public decimal Cantidad { get => cantidad; set => cantidad = value; }
        public DateTime Fecha { get => fecha; set => fecha = value; }
        public string Tipo { get => tipo; set => tipo = value; }
        public string MetodoPago { get => metodoPago; set => metodoPago = value; }

        public Transaccion() { }

        //el constructor hecho
        public Transaccion(int id, int idUsuario, decimal cantidad, DateTime fecha, string tipo, string metodoPago)
        {
            this.id = id;
            this.idUsuario = idUsuario;
            this.cantidad = cantidad;
            this.fecha = fecha;
            this.tipo = tipo;
            this.metodoPago = metodoPago;
        }

        //el mtodo para guardar esta transacción usando el CAD
        public bool Guardar()
        {
            CADTransaccion cad = new CADTransaccion();
            return cad.CrearTransaccion(this);
        }
    }
}