using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace library
{

    /// <summary>
    /// Clase que representa la entidad de negocio de rol.
    /// Esta asociado a la clase <c>CADRol</c>>.
    /// </summary>
    public class ENRol
    {
        int id_rol;
        string nombre;
        string descripcion;

        /// <summary>
        /// Identificador del rol.
        /// </summary>
        public int Id_rol
        {
            get { return id_rol; }
            set { id_rol = value; }
        }

        /// <summary>
        /// Nombre del rol.
        /// </summary>
        public string Nombre
        {
            get { return nombre; }
            set { nombre = value; }
        }

        /// <summary>
        /// Descripcion del rol.
        /// </summary>
        public string Descripcion
        {
            get { return descripcion; }
            set { descripcion = value; }
        }

        /// <summary>
        /// Constructor por defecto.
        /// Inicializa <c>id_rol</c> a cero, y <c>nombre</c> y <c>descripcion</c> a cadenas vacias.
        /// </summary>
        public ENRol()
        {
            id_rol = 0;
            nombre = "";
            descripcion = "";
        }

        /// <summary>
        /// Constructor sobrecargado.
        /// </summary>
        /// <param name="id_rol">Id del rol a asignar.</param>
        /// <param name="nombre">Nombre del rol a asignar.</param>
        /// <param name="descripcion">Descripcion del rol a asignar.</param>
        public ENRol(int id_rol, string nombre, string descripcion)
        {
            this.id_rol = id_rol;
            this.nombre = nombre;
            this.descripcion = descripcion;

        }

        /// <summary>
        /// Crea un nuevo rol en la BD mediante el CAD.
        /// </summary>
        /// <returns>
        /// <c>true</c> si el rol se ha creado.
        /// <c>false</c> si el rol no se ha podido crear.
        /// </returns>
        public bool Create()
        {
            CADRol rol = new CADRol();
            return rol.Create(this);
        }

        /// <summary>
        /// Actualiza un rol existente en la BD mediante el CAD.
        /// </summary>
        /// <returns>
        /// <c>true</c> si el rol se ha actualizado.
        /// <c>false</c> si el rol no se ha podido actualizar.
        /// </returns>
        public bool Update()
        {
            CADRol rol = new CADRol();
            return rol.Update(this);
        }

        /// <summary>
        /// Elimina un rol existente en la BD mediante el CAD.
        /// </summary>
        /// <returns>
        /// <c>true</c> si el rol se ha eliminado.
        /// <c>false</c> si el rol no se ha podido eliminar.
        /// </returns>
        public bool Delete()
        {
            CADRol rol = new CADRol();
            return rol.Delete(this);
        }

        /// <summary>
        /// Busca un rol existente en la BD a partir de su id mediante el CAD.
        /// </summary>
        /// <returns>
        /// <c>true</c> si el rol se ha encontrado.
        /// <c>false</c> si el rol no se ha podido encontrar.
        /// </returns>
        public bool Read()
        {
            CADRol rol = new CADRol();
            return rol.Read(this);
        }

        /// <summary>
        /// Busca el primer rol existente en la BD mediante el CAD.
        /// </summary>
        /// <returns>
        /// <c>true</c> si un rol se ha obtenido.
        /// <c>false</c> si ningun rol se ha podido obtener.
        /// </returns>
        public bool ReadFirst()
        {
            CADRol rol = new CADRol();
            return rol.ReadFirst(this);
        }

        /// <summary>
        /// Busca el siguiente rol respecto al actual en la BD mediante el CAD.
        /// </summary>
        /// <returns>
        /// <c>true</c> si el siguiente rol se ha obtenido.
        /// <c>false</c> si el siguiente  rol no se ha podido obtener.
        /// </returns>
        public bool ReadNext()
        {
            CADRol rol = new CADRol();
            return rol.ReadNext(this);
        }

        /// <summary>
        /// Busca el anterior rol respecto al actual en la BD mediante el CAD.
        /// </summary>
        /// <returns>
        /// <c>true</c> si el anterior rol se ha obtenido.
        /// <c>false</c> si el anterior  rol no se ha podido obtener.
        /// </returns>
        public bool ReadPrev()
        {
            CADRol rol = new CADRol();
            return rol.ReadPrev(this);
        }

        /// <summary>
        /// Obtiene la lista completa de los roles existentes en la BD.
        /// </summary>
        /// <returns>Lista de objetos ENRol.</returns>
        public List<ENRol> ReadAll()
        {
            CADRol rol = new CADRol();
            return rol.ReadAll();
        }
    }
}
