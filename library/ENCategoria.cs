using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace library
{
    /// <summary>
    /// Clase que representa una categoría de deporte (Baloncesto, fútbol femenino, masculino...).
    /// </summary>
    public class ENCategoria
    {
        string _nombre;

        /// <summary>
        /// Get/set del nombre de la categoría.
        /// </summary>
        /// <value>
        /// Cadena que representa el nombre de la categoría.
        /// </value>

        public string Nombre
        {
            get { return _nombre; }
            set { _nombre = value; }
        }

        /// <summary>
        /// Inicializa una instancia vacía de la clase <see cref="ENCategoria"/>.
        /// </summary>
        public ENCategoria()
        {
            _nombre = "";
        }
        /// <summary>
        /// Inicializa una instancia de la clase <see cref="ENCategoria"/> dado un nombre pasado por parámetro.
        /// </summary>
        /// <param name="nombre">Nombre inicial de la categoría.</param>
        public ENCategoria(string nombre)
        {
            _nombre = nombre;
        }
        /// <summary>
        /// Crea una nueva categoría en la base de datos.
        /// </summary>
        /// <returns>
        /// <c>true</c> si la operación fue exitosa, <c>false</c> si no.
        /// </returns>
        public bool Create()
        {
            CADCategoria categoria = new CADCategoria();
            return categoria.Create(this);
        }

        /// <summary>
        /// Elimina la categoría de la base de datos.
        /// </summary>
        /// <returns>
        /// <c>true</c> si la operación fue exitosa, <c>false</c> si no.
        /// </returns>
        public bool Delete()
        {
            CADCategoria categoria = new CADCategoria();
            return categoria.Delete(this);
        }
        /// <summary>
        /// Actualiza la información de la categoría en la base de datos.
        /// </summary>
        /// <returns>
        /// <c>true</c> si la operación fue exitosa, <c>false</c> si no.
        /// </returns>
        public bool Update()
        {
            CADCategoria categoria = new CADCategoria();
            return categoria.Update(this);
        }
        /// <summary>
        /// Lee la información de la categoría desde la base de datos.
        /// </summary>
        /// <returns>
        /// <c>true</c> si la operación fue exitosa, <c>false</c> si no.
        /// </returns>
        public bool Read()
        {
            CADCategoria categoria = new CADCategoria();
            return categoria.Read(this);
        }
        /// <summary>
        /// Consigue todas las categorías existentes en la base de datos.
        /// </summary>
        /// <returns>
        /// Lista de objetos <see cref="ENCategoria"/> que representan todas las categorías.
        /// </returns>
        public List<ENCategoria> ReadAll()
        {
            CADCategoria categoria = new CADCategoria();
            return categoria.ReadAll();

        }
    }
}
