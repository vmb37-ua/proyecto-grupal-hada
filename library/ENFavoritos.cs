using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace library
{   
    /// <summary>
     /// Representa una entrada en la lista de favoritos de un usuario.
     /// Cada instancia conecta un usuario con un equipo que ha marcado como favorito.
     /// </summary>
    public class ENFavoritos
    {
        int idFavorito;
        int idEquipo;
        int idUsuario;
    
        string nombreEquipo;
        string escudo;
        string categoria;
        /// <summary>
        /// Identificador de favorito.
        /// </summary>
        public int IdFavorito
        {
            get { return idFavorito; }
            set { idFavorito = value; }
        }
        /// <summary>
        /// Código del equipo que ha sido añadido como favorito.
        /// </summary>
        public int IdEquipo
        {
            get { return idEquipo; }
            set { idEquipo = value; }
        }
        /// <summary>
        /// Código del usuario que ha guardado el equipo en favorito.
        /// </summary>
        public int IdUsuario
        {
            get { return idUsuario; }
            set { idUsuario = value; }
        }
        /// <summary>
        /// Ruta o enlace de la imagen del escudo del equipo.
        /// </summary>
        public string Escudo
        {
            get { return escudo; }
            set { escudo = value; }
        }
        /// <summary>
        /// Categoria a la que pertenece el equipo.
        /// </summary>
        public string Categoria
        {
            get { return categoria; }
            set { categoria = value; }
        }
        /// <summary>
        /// Nombre del equipo asignado como favorito.
        /// </summary>
        public string NombreEquipo
        {
            get { return nombreEquipo; }
            set { nombreEquipo = value; }
        }


        /// <summary>
        /// Constructor por defecto que establece los valores predeterminados.
        /// </summary>
        public ENFavoritos()
        {
            idFavorito = 0;
            idEquipo = 0;
            idUsuario = 0;  
            nombreEquipo = "";
            escudo = "";
            categoria = "";
        }
        /// <summary>
        /// Añade un equipo a favorito con datos pasados por parámetros .
        /// </summary>
        /// <param name="idFavorito">ID del favorito.</param>
        /// <param name="idEquipo">ID del equipo.</param>
        /// <param name="idUsuario">ID del usuario.</param>
        /// <param name="nombreEquipo">Nombre del equipo.</param>
        /// <param name="escudo">Imagen del escudo.</param>
        /// <param name="categoria">Categoría o división.</param>
        public ENFavoritos(int idFavorito, int idEquipo, int idUsuario, string nombreEquipo = "", string escudo = "", string categoria = "")
        {
            this.IdFavorito = idFavorito;
            this.IdEquipo = idEquipo;
            this.IdUsuario = idUsuario;
            this.NombreEquipo = nombreEquipo;
            this.Escudo = escudo;
            this.Categoria = categoria;
        }

        /// <summary>
        /// Añade un equipo en favorito para un usuario en la base de datos.
        /// </summary>
        /// <returns><c>true</c> si se añadió correctamente, <c>false</c> si no se logró.</returns>

        public bool Create()
        {
            CADFavoritos favoritos = new CADFavoritos();
            return favoritos.Create(this);
        }
        /// <summary>
        /// Borra el favorito indicado del sistema.
        /// </summary>
        /// <returns><c>true</c> si se eliminó, <c>false</c> si no se pudo eliminar.</returns>
        public bool Delete()
        {
            CADFavoritos favoritos = new CADFavoritos();
            return favoritos.Delete(this);
        }

        /// <summary>
        /// Recupera todos los equipos favoritos de un usuario en concreto.
        /// </summary>
        /// <returns>Una lista con los equipos favoritos del usuario actual.</returns>
        public List<ENFavoritos> ReadAllUsuario()
        {
            CADFavoritos favoritos = new CADFavoritos();
            return favoritos.ReadAllUsuario(this.IdUsuario); 
        }
    }
}
