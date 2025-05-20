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

        public ENEquipo()
        {
            _id_equipo = 0;
            _escudo = "";
            _nombre = "";
            _categoria = "";
        }

        public ENEquipo(int id_equipo, string escudo, string nombre, string categoria)
        {
            Id_equipo = id_equipo;
            Escudo = escudo;
            Nombre = nombre;
            Categoria = categoria;
        }

        public bool Create()
        {
            return new CADEquipo().Create(this);
        }

        public bool Delete()
        {
            return new CADEquipo().Delete(this);
        }

        public bool Update()
        {
            return new CADEquipo().Update(this);
        }

        public bool Read()
        {
            return new CADEquipo().Read(this);
        }

        public List<ENEquipo> ReadAll()
        {
            return new CADEquipo().ReadAll(this);
        }
    }
}