using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace library
{
    /// <summary>
    /// Representa a la apuesta general, no la individual de cada usuario
    /// </summary>

    public class ENApuesta
    {
        int _id_apuesta;
        DateTime _fecha;
        ENEstadio _estadio;
        ENEquipo _equipo1;
        ENEquipo _equipo2;
        string _resultado; // 1=Victoria local, 2=Victoria visitante, X=Empate
        double _cot1;
        double _cot2;
        double _cotX;




        /// <summary>
        /// Getters y setters de los atributos
        /// </summary>
        public int Id_apuesta
        {
            get { return _id_apuesta; }
            set { _id_apuesta = value; }
        }
        public DateTime Fecha
        {
            get { return _fecha; }
            set { _fecha = value; }
        }

        public ENEstadio Estadio
        {
            get { return _estadio; }
            set { _estadio = value; }
        }

        public ENEquipo Equipo1
        {
            get { return _equipo1; }
            set { _equipo1 = value; }
        }

        public ENEquipo Equipo2
        {
            get { return _equipo2; }
            set { _equipo2 = value; }
        }
        public string Resultado   
        {
            get { return _resultado; }
            set { _resultado = value; }
        }
        public double cot1
        {
            get { return _cot1; }
            set { _cot1 = value; }
        }
        public double cot2
        {
            get { return _cot2; }
            set { _cot2 = value; }
        }
        public double cotX
        {
            get { return _cotX; }
            set { _cotX = value; }
        }

        /// <summary>
        /// Constructor que inicializa una apuesta por defecto.
        /// </summary>
        public ENApuesta()
        {
            _id_apuesta = 0;
            _fecha = DateTime.Now;
            _estadio = new ENEstadio();
            _equipo1 = new ENEquipo();
            _equipo2 = new ENEquipo();
            _resultado = string.Empty;
            _cot1 = 1;
            _cot2 = 1;
            _cotX = 1;
        }
        /// <summary>
        /// Constructor que inicializa una apuesta com unos valores determinados.
        /// </summary>
        public ENApuesta(int id_apuesta, DateTime fecha, ENEstadio estadio, ENEquipo equipo1, ENEquipo equipo2, string resultado, double cot1, double cot2, double cotX)
        {
            _id_apuesta = id_apuesta;
            _fecha = fecha;
            _estadio = estadio;
            _equipo1 = equipo1;
            _equipo2 = equipo2;
            _resultado = resultado;
            _cot1 = cot1;
            _cot2 = cot2;
            _cotX = cotX;
        }

        /// <summary>
        /// Crea una nueva apuesta en la base de datos.
        /// </summary>
        /// <returns>True si la operación fue exitosa; si no, false.</returns>
        public bool Create()
        {
            CADApuesta ap = new CADApuesta();
            return ap.Create(this);
        }
        public bool Delete()
        {
            CADApuesta ap = new CADApuesta();
            return ap.Delete(this);
        }
        /// <summary>
        /// Lee los datos de la apuesta desde la base de datos.
        /// </summary>
        /// <returns>True si se encontró; si no, false.</returns>
        public bool Read()
        {
            CADApuesta ap = new CADApuesta();
            return ap.Read(this);
        }
        /// <summary>
        /// Lee todas las apuestas.
        /// </summary>
        /// <returns>Lista de todas las apuestas.</returns>
        public List<ENApuesta> ReadAll()
        {
            CADApuesta equipo = new CADApuesta();
            return equipo.ReadAll();
        }

        /// <summary>
        /// Actualiza la información de la apuesta en la base de datos.
        /// </summary>
        /// <returns>True si se actualizó correctamente; si no, false.</returns
        public bool Update()
        {
            CADApuesta ap = new CADApuesta();
            return ap.Update(this);
        }
    }
}
