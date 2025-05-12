using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace library
{
    public class ENFavoritos
    {
        int idFavorito;
        int idEquipo;
        string nombreEquipo;
        string nombreProvincia;

        public int IdFavorito
        {
            get { return idFavorito; }
            set { idFavorito = value; }
        }

        public int IdEquipo
        {
            get { return idEquipo; }
            set { idEquipo = value; }
        }

        public string NombreEquipo
        {
            get { return nombreEquipo; }
            set { nombreEquipo = value; }
        }

        public string NombreProvincia
        {
            get { return nombreProvincia; }
            set { nombreProvincia = value; }
        }

        public ENFavoritos()
        {
            idFavorito = 0;
            idEquipo = 0;
            nombreEquipo = "";
            nombreProvincia = "";
        }

        public ENFavoritos(int idFavorito, int idEquipo)
        {
            this.IdFavorito = idFavorito;
            this.IdEquipo = idEquipo;
            this.NombreEquipo = "";
            this.NombreProvincia = "";
        }

        public bool Create()
        {
            CADFavoritos favoritos = new CADFavoritos();
            return favoritos.Create(this);
        }

        public bool Delete()
        {
            CADFavoritos favoritos = new CADFavoritos();
            return favoritos.Delete(this);
        }

        public List<ENFavoritos> ReadAll()
        {
            CADFavoritos favoritos = new CADFavoritos();
            return favoritos.ReadAll(this);
        }
    }
}
