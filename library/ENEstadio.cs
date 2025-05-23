using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace library
{
    /// <summary>
    /// Clase que representa una entidad de negocio de un estadio.
    /// </summary>
    public class ENEstadio
    {
        string _nombre;
        int _capacidad;
        string _texto;
        int _id_municipio;

        public string Nombre
        {
            get { return _nombre; }
            set { _nombre = value; }
        }

        public int Capacidad
        {
            get { return _capacidad; }
            set { _capacidad = value; }
        }

        public string Texto
        {
            get { return _texto; }
            set { _texto = value; }
        }

        public int Id_municipio
        {
            get { return _id_municipio; }
            set { _id_municipio = value; }
        }

        /// <summary>
        /// Constructor por defecto. Crea un estadio con valores por defecto.
        /// </summary>
        public ENEstadio()
        {
            _nombre = "";
            _capacidad = 0;
            _texto = "";
            _id_municipio = 0;
        }

        /// <summary>
        /// Crea este estadio en la base de datos.
        /// </summary>
        /// <returns>True si la operación fue exitosa. False si no.</returns>
        public bool Create() => new CADEstadio().Create(this);

        /// <summary>
        /// Actualiza este estadio en la base de datos.
        /// </summary>
        /// <returns>True si la operación fue exitosa. False si no.</returns>
        public bool Update() => new CADEstadio().Update(this);

        /// <summary>
        /// Elimina este estadio de la base de datos.
        /// </summary>
        /// <returns>True si la operación fue exitosa. False si no.</returns>
        public bool Delete() => new CADEstadio().Delete(this);

        /// <summary>
        /// Lee los datos del estadio desde la base de datos en este objeto.
        /// </summary>
        /// <returns>True si la operación fue exitosa. False si no.</returns>
        public bool Read() => new CADEstadio().Read(this);

        /// <summary>
        /// Lee todos los estadios desde la base de datos.
        /// </summary>
        /// <returns>Lista de todos los estadios</returns>
        public List<ENEstadio> ReadAll() => new CADEstadio().ReadAll(this);
    }
}

