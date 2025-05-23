using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace library
{
    /// <summary>
    /// Clase que representa una entidad de negocio de un municipio en una provincia.
    /// </summary>
    public class ENMunicipio
    {
        int _id_municipio;
        int _id_provincia;
        string _nombre;
        /// <summary>
        /// Identificador único del municipio.
        /// </summary>
        public int Id_municipio {
            get { return _id_municipio; }
            set { _id_municipio = value; }
        }
        /// <summary>
        /// Identificador de la provincia a la que pertenece el municipio.
        /// </summary>
        public int Id_provincia {
            get { return _id_provincia; }
            set { _id_provincia = value; }
        }
        /// <summary>
        /// Nombre del municipio.
        /// </summary>
        public string Nombre { 
            get { return _nombre; }
            set { _nombre = value; }
        }
        /// <summary>
        /// Constructor por defecto. Crea un municipio con valores por defecto.
        /// </summary>
        public ENMunicipio(){
            _id_municipio=0;
            _id_provincia=0;
            _nombre = "";
        }
        /// <summary>
        /// Constructor sobrecargado.
        /// </summary>
        /// <param name="id_municipio">Id del municipio a crear</param>
        /// <param name="id_provincia">Id de la provincia</param>
        /// <param name="nombre">Nombre del municipio</param>
        public ENMunicipio(int id_municipio, int id_provincia, string nombre)
        {
            Id_municipio = id_municipio;
            Id_provincia = id_provincia;
            Nombre = nombre;
        }
        /// <summary>
        /// Crea este (this) municipio en la base de datos.
        /// </summary>
        /// <returns>True si la opercación ha sido existosa. False si no se pudo realizar la transacción o excepción</returns>
        public bool Create() {
            CADMunicipio municipio = new CADMunicipio();
            return municipio.Create(this);
        }
        /// <summary>
        /// Elimina este (this) municipio en la base de datos.
        /// </summary>
        /// <returns>True si la opercación ha sido existosa. False si no se pudo realizar la transacción o excepción</returns>
        public bool Delete() { 
            CADMunicipio municipio =new CADMunicipio();
            return municipio.Delete(this);
        }
        /// <summary>
        /// Actualiza el municipio con la misma id que este (this) con su información.
        /// </summary>
        /// <returns>True si la opercación ha sido existosa. False si no se pudo realizar la transacción o excepción</returns>
        public bool Update() {
            CADMunicipio municipio = new CADMunicipio();
            return municipio.Update(this);
        }
        /// <summary>
        /// Lee un municipio con la id de este (this) municipio de la base de datos y lo escribe en este.
        /// </summary>
        /// <returns>True si la opercación ha sido existosa. False si no se pudo realizar la transacción o excepción</returns>
        public bool Read() { 
            CADMunicipio municipio=new CADMunicipio();
            return municipio.Read(this);
        }
        /// <summary>
        /// Lee todos los municipios de la provincia que tiene id este municipio (this).
        /// </summary>
        /// <returns>Lista de entidades municipio de la provincia</returns>
        public List<ENMunicipio> ReadAll() {
            CADMunicipio municipio = new CADMunicipio();
            return municipio.ReadAll(this);
        }
    }
}
