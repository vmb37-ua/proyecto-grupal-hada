using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace library
{
    /// <summary>
    /// Clase que representa una provincia dentro del sistema.
    /// Relacionada con <c>CADProvincia</c>, que se encarga del acceso a datos.
    /// </summary>
    public class ENProvincia
    {
        int idProvincia;
        int idPais;
        string nombre;

        /// <summary>
        /// Identificador de  provincia.
        /// </summary>
        public int IdProvincia
        {
            get { return idProvincia; }
            set { idProvincia = value; }
        }

        /// <summary>
        /// Identificador del país al que pertenece esta provincia.
        /// </summary>
        public int IdPais
        {
            get { return idPais; }
            set { idPais = value; }
        }

        /// <summary>
        /// Nombre de la provincia.
        /// </summary>
        public string Nombre
        {
            get { return nombre; }
            set { nombre = value; }
        }

        /// <summary>
        /// Constructor por defecto. Inicializacion de los campos.
        /// </summary>
        public ENProvincia()
        {
            idProvincia = 0;
            idPais = 0;
            nombre = "";
        }

        /// <summary>
        /// Constructor con parámetros. Permite asignar valores al crear una nueva instancia.
        /// </summary>
        /// <param name="idProvincia">ID de la provincia.</param>
        /// <param name="idPais">ID del país.</param>
        /// <param name="nombre">Nombre de la provincia.</param>
        public ENProvincia(int idProvincia, int idPais, string nombre)
        {
            this.IdProvincia = idProvincia;
            this.IdPais = idPais;
            this.Nombre = nombre;
        }

        /// <summary>
        /// Inserta esta provincia en la base de datos.
        /// </summary>
        /// <returns>Devuelve <c>true</c> si la operación fue exitosa, <c>false</c> si falló.</returns>
        public bool Create()
        {
            CADProvincia provincia = new CADProvincia();
            return provincia.Create(this);
        }

        /// <summary>
        /// Actualiza la información de esta provincia en la base de datos.
        /// </summary>
        /// <returns><c>true</c> si los datos se modificaron correctamente, <c>false</c> si hubo un error.</returns>
        public bool Update()
        {
            CADProvincia provincia = new CADProvincia();
            return provincia.Update(this);
        }

        /// <summary>
        /// Elimina esta provincia de la base de datos.
        /// </summary>
        /// <returns><c>true</c> si se borró con éxito, <c>false</c> en caso contrario.</returns>
        public bool Delete()
        {
            CADProvincia provincia = new CADProvincia();
            return provincia.Delete(this);
        }

        /// <summary>
        /// Recupera todas las provincias registradas en el sistema.
        /// </summary>
        /// <returns>Lista con todas las instancias de <c>ENProvincia</c> encontradas.</returns>
        public List<ENProvincia> ReadAll()
        {
            CADProvincia provincia = new CADProvincia();
            return provincia.ReadAll();
        }

        /// <summary>
        /// Busca una provincia específica en la base de datos usando sus datos actuales.
        /// </summary>
        /// <returns><c>true</c> si se encuentra la provincia, <c>false</c> si no.</returns>
        public bool Read()
        {
            CADProvincia provincia = new CADProvincia();
            return provincia.Read(this);
        }

        /// <summary>
        /// Obtiene todas las provincias que pertenecen al país indicado.
        /// </summary>
        /// <returns>Lista de provincias asociadas al país de esta instancia.</returns>
        public List<ENProvincia> ReadAllByPais()
        {
            CADProvincia cad = new CADProvincia();
            return cad.ReadAllByPais(this);
        }
    }
}
