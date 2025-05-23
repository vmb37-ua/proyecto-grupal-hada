using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace library
{
    /// <summary>
    /// Clase que representa una entidad de negocio de un equipo deportivo.
    /// </summary>
    public class ENEquipo
    {
        int _id_equipo;
        string _escudo;
        string _nombre;
        string _categoria;

        public int Id_equipo
        {
            get { return _id_equipo; }
            set { _id_equipo = value; }
        }

        public string Escudo
        {
            get { return _escudo; }
            set { _escudo = value; }
        }

        public string Nombre
        {
            get { return _nombre; }
            set { _nombre = value; }
        }

        public string Categoria
        {
            get { return _categoria; }
            set { _categoria = value; }
        }

        /// <summary>
        /// Constructor por defecto. Crea un equipo con valores por defecto.
        /// </summary>
        public ENEquipo()
        {
            _id_equipo = 0;
            _escudo = "";
            _nombre = "";
            _categoria = "";
        }

        /// <summary>
        /// Constructor sobrecargado para inicializar un equipo con valores dados.
        /// </summary>
        /// <param name="id_equipo">ID del equipo</param>
        /// <param name="escudo">Ruta del escudo del equipo</param>
        /// <param name="nombre">Nombre del equipo</param>
        /// <param name="categoria">Categoría del equipo</param>
        public ENEquipo(int id_equipo, string escudo, string nombre, string categoria)
        {
            Id_equipo = id_equipo;
            Escudo = escudo;
            Nombre = nombre;
            Categoria = categoria;
        }

        /// <summary>
        /// Crea este equipo en la base de datos.
        /// </summary>
        /// <returns>True si la operación fue exitosa. False si no.</returns>
        public bool Create() => new CADEquipo().Create(this);

        /// <summary>
        /// Elimina este equipo de la base de datos.
        /// </summary>
        /// <returns>True si la operación fue exitosa. False si no.</returns>
        public bool Delete() => new CADEquipo().Delete(this);

        /// <summary>
        /// Actualiza este equipo en la base de datos.
        /// </summary>
        /// <returns>True si la operación fue exitosa. False si no.</returns>
        public bool Update() => new CADEquipo().Update(this);

        /// <summary>
        /// Lee los datos del equipo desde la base de datos en este objeto.
        /// </summary>
        /// <returns>True si la operación fue exitosa. False si no.</returns>
        public bool Read() => new CADEquipo().Read(this);

        /// <summary>
        /// Lee todos los equipos de la base de datos.
        /// </summary>
        /// <returns>Lista de todos los equipos</returns>
        public List<ENEquipo> ReadAll() => new CADEquipo().ReadAll(this);

        /// <summary>
        /// Lee todos los equipos de una categoría específica desde la base de datos.
        /// </summary>
        /// <returns>Lista de equipos de la categoría</returns>
        public List<ENEquipo> ReadAllbyCategoria() => new CADEquipo().ReadAllbyCategoria(this);
    }
}