using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace library
{
    public class ENEquipo
    {
        int _id_equipo;
        int _id_estadio;
        string _nombre;
        string _ciudad;

        public int Id_equipo
        {
            get { return _id_equipo; }
            set { _id_equipo = value; }
        }

        public int Id_estadio
        {
            get { return _id_estadio; }
            set { _id_estadio = value; }
        }

        public string Nombre
        {
            get { return _nombre; }
            set { _nombre = value; }
        }

        public string Ciudad
        {
            get { return _ciudad; }
            set { _ciudad = value; }
        }

        public ENEquipo()
        {
            _id_equipo = 0;
            _id_estadio = 0;
            _nombre = "";
            _ciudad = "";
        }

        public ENEquipo(int id_equipo, int id_estadio, string nombre, string ciudad)
        {
            Id_equipo = id_equipo;
            Id_estadio = id_estadio;
            Nombre = nombre;
            Ciudad = ciudad;
        }

        public bool Create()
        {
            CADEquipo equipo = new CADEquipo();
            return equipo.Create(this);
        }

        public bool Delete()
        {
            CADEquipo equipo = new CADEquipo();
            return equipo.Delete(this);
        }

        public bool Update()
        {
            CADEquipo equipo = new CADEquipo();
            return equipo.Update(this);
        }

        public bool Read()
        {
            CADEquipo equipo = new CADEquipo();
            return equipo.Read(this);
        }

        public List<ENEquipo> ReadAll()
        {
            CADEquipo equipo = new CADEquipo();
            return equipo.ReadAll(this);
        }
    }
}

