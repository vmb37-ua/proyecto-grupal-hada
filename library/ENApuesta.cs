using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace library
{
    public class ENApuesta
    {
        int _id_apuesta;
        DateTime _fecha;
        ENEstadio _estadio;
        ENEquipo _equipo1;
        ENEquipo _equipo2;
        int _resultado;
        double _cot1;
        double _cot2;
        double _cotX;
        public int Resultado
        {
            get { return _resultado; }
            set { _resultado = value; }
        }


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

        public ENApuesta()
        {
            _id_apuesta = 0;
            _fecha = DateTime.Now;
            _estadio = new ENEstadio();
            _equipo1 = new ENEquipo();
            _equipo2 = new ENEquipo();
            _cot1 = 1;
            _cot2 = 1;
            _cotX = 1;
        }
        public ENApuesta(int id_apuesta, int importe, int resultado_predicho, DateTime fecha, ENEstadio estadio, ENEquipo equipo1, ENEquipo equipo2, double cot1, double cot2, double cotX)
        {
            _id_apuesta = id_apuesta;
            _fecha = fecha;
            _estadio = estadio;
            _equipo1 = equipo1;
            _equipo2 = equipo2;
            _cot1 = cot1;
            _cot2 = cot2;
            _cotX = cotX;
        }

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
        public bool Read()
        {
            CADApuesta ap = new CADApuesta();
            return ap.Read(this);
        }
        public List<ENApuesta> ReadAll()
        {
            CADApuesta equipo = new CADApuesta();
            return equipo.ReadAll(this);
        }
        public bool Update()
        {
            CADApuesta ap = new CADApuesta();
            return ap.Update(this);
        }
    }
}