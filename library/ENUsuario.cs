using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace library
{
    /// <summary>
    /// Clase que representa una entidad usuario con todos sus datos.
    /// </summary>
    public class ENUsuario
    {
        int _id;
        string _imagen;
        string _nombre;
        string _correo;
        string _password;
        float _saldo;
        string _numtar;
        DateTime _caducidad;
        string _cvv;
        int _rol;
        string _direccion;
        int _municipio;
        string _telefono;

        public int ID
        {
            get { return _id; }
            set { _id = value; }
        }

        public string Imagen
        {
            get { return _imagen; }
            set { _imagen = value; }
        }

        public string Nombre
        {
            get { return _nombre; }
            set { _nombre = value; }
        }

        public string Correo
        {
            get { return _correo; }
            set { _correo = value; }
        }

        public string Password
        {
            get { return _password; }
            set { _password = value; }
        }

        public float Saldo
        {
            get { return _saldo; }
            set { _saldo = value; }
        }

        public string NumTar
        {
            get { return _numtar; }
            set { _numtar = value; }
        }

        public DateTime Caducidad
        {
            get { return _caducidad; }
            set { _caducidad = value; }
        }

        public string Cvv
        {
            get { return _cvv; }
            set { _cvv = value; }
        }

        public int Rol
        {
            get { return _rol; }
            set { _rol = value; }
        }

        public string Direccion
        {
            get { return _direccion; }
            set { _direccion = value; }
        }

        public int Municipio
        {
            get { return _municipio; }
            set { _municipio = value; }
        }

        public string Telefono
        {
            get { return _telefono; }
            set { _telefono = value; }
        }
        /// <summary>
        /// Constructor por defecto. Inicializa los atributos de la entidad por defecto.
        /// </summary>
         public ENUsuario()
        {
            _id = 0;
            _imagen = "";
            _nombre = "";
            _correo = "";
            _password = "";
            _saldo = 0;
            _numtar = "";
            _caducidad = DateTime.Now;
            _cvv = "";
            _rol = 0;
            _direccion = "";
            _municipio = 0;
            _telefono = "";
        }
        /// <summary>
        /// Constructor sobrecargado. 
        /// </summary>
        /// <param name="id">ID del usuario</param>
        /// <param name="imagen">Nombre de la imagen del usuario</param>
        /// <param name="nombre">Nombre del usuario</param>
        /// <param name="correo">Correo electrónico</param>
        /// <param name="password">Contraseña</param>
        /// <param name="saldo">Saldo actual</param>
        /// <param name="numtar">Número de la tarjeta de crédito</param>
        /// <param name="caducidad">Fecha de caducidad de la tarjeta</param>
        /// <param name="cvv">Cvv de la tarjeta</param>
        /// <param name="rol">ID del rol del usuario</param>
        /// <param name="direccion">Dirección física del usuario</param>
        /// <param name="municipio">ID del municipio</param>
        /// <param name="telefono">Número de teléfono</param>
        public ENUsuario(int id, string imagen, string nombre, string correo, string password, float saldo, string numtar, DateTime caducidad, string cvv, int rol, string direccion, int municipio, string telefono)
        {
            _id = id;
            _imagen = imagen;
            _nombre = nombre;
            _correo = correo;
            _password = password;
            _saldo = saldo;
            _numtar = numtar;
            _caducidad = caducidad;
            _cvv = cvv;
            _rol = rol;
            _direccion = direccion;
            _municipio = municipio;
            _telefono = telefono;
        }
        /// <summary>
        /// Crea un usuario en la base de datos.
        /// </summary>
        /// <returns>True si la operación resulta existosa, false si no</returns>
        public bool Create() {
            CADUsuario usu = new CADUsuario();
            return usu.Create(this);
        }
        /// <summary>
        /// Elimina el usuario en la base de datos.
        /// </summary>
        /// <returns>True si la operación resulta existosa, false si no</returns>
        public bool Delete() {
            CADUsuario usu = new CADUsuario();
            return usu.Delete(this);
        }
        /// <summary>
        /// Lee un usuario (con esta ID) en la base de datos y lo almacena en este objeto.
        /// </summary>
        /// <returns>True si la operación resulta existosa, false si no</returns>
        public bool Read() {
            CADUsuario usu = new CADUsuario();
            return usu.Read(this);
        }
        /// <summary>
        /// Actualiza un usuario en la base de datos con esta ID.
        /// </summary>
        /// <returns>True si la operación resulta existosa, false si no</returns>
        public bool Update() {
            CADUsuario usu = new CADUsuario();
            return usu.Update(this);
        }
        /// <summary>
        /// Comprueba que haya un usuario en la BD con cierto correo y contraseña.
        /// </summary>
        /// <returns>True si existe, false si no</returns>
        public bool LoginUsu() {
            CADUsuario usu = new CADUsuario();
            return usu.LoginUsu(this);
        }
        /// <summary>
        /// Lee todos los usuarios de la BD.
        /// </summary>
        /// <returns>Lista con todas las entidades usuario</returns>
        public List<ENUsuario> ReadAll() {
            CADUsuario usu = new CADUsuario();
            return usu.ReadAll();
        }
        /// <summary>
        /// Lee todos los usuarios con un rol en la BD.
        /// </summary>
        /// <param name="idRol">ID del rol a leer</param>
        /// <returns>Lista de todos los usuarios con dicho rol</returns>
        public List<ENUsuario> ReadByRol(int idRol)
        {
            CADUsuario cad = new CADUsuario();
            return cad.ReadByRol(idRol);
        }
        /// <summary>
        /// Comprueba si existe algún usuario con cierto correo.
        /// </summary>
        /// <param name="correo">Correo a buscar</param>
        /// <returns>True si existe, false si no</returns>
        public bool ExisteCorreo(string correo)
        {
            CADUsuario usu = new CADUsuario();
            return usu.ExisteCorreo(correo);
        }
        /// <summary>
        /// Actualiza la foto de un usuario en la base de datos con la que hay en este objeto.
        /// </summary>
        /// <returns>True si la operación resulta existosa, false si no</returns>
        public bool UpdateFoto()
        {
            CADUsuario usu = new CADUsuario();
            return usu.UpdateFoto(this);
        }
        /// <summary>
        /// Lee un usuario de la base de datos con el correo indicado en este objeto.
        /// </summary>
        /// <returns>True si existe, false si no</returns>
        public bool ReadByCorreo()
        {
            CADUsuario usu = new CADUsuario();
            return usu.ReadByCorreo(this);
        }
    }
}
