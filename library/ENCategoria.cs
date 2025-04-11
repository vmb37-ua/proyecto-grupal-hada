using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace library
{
    public class ENCategoria
    {
        string _nombre;

        public string Nombre
        {
            get { return _nombre; }
            set { _nombre = value; }
        }

        public ENCategoria() 
        {
            _nombre = "";
        }
        public ENCategoria(string nombre)
        {
            _nombre = nombre;
        }

        public bool Create()
        {
            CADCategoria categoria = new CADCategoria();
            return categoria.Create(this);
        }

        public bool Delete()
        {
            CADCategoria categoria = new CADCategoria();
            return categoria.Delete(this);
        }

        public bool Update()
        {
            CADCategoria categoria = new CADCategoria();
            return categoria.Update(this);
        }

        public bool Read()
        {
            CADCategoria categoria = new CADCategoria();
            return categoria.Read(this);
        }

        public List<ENCategoria> ReadAll()
        {
            CADCategoria categoria = new CADCategoria();
            return categoria.ReadAll(this);

        }
    }
