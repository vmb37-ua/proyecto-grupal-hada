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
        int idUsuario;
    
        string nombreEquipo;
        string escudo;
        string categoria;

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
        public int IdUsuario
        {
            get { return idUsuario; }
            set { idUsuario = value; }
        }
        public string Escudo
        {
            get { return escudo; }
            set { escudo = value; }
        }
        public string Categoria
        {
            get { return categoria; }
            set { categoria = value; }
        }
        public string NombreEquipo
        {
            get { return nombreEquipo; }
            set { nombreEquipo = value; }
        }


        public ENFavoritos()
        {
            idFavorito = 0;
            idEquipo = 0;
            idUsuario = 0;  
            nombreEquipo = "";
            escudo = "";
            categoria = "";
        }

        public ENFavoritos(int idFavorito, int idEquipo, int idUsuario, string nombreEquipo = "", string escudo = "", string categoria = "")
        {
            this.IdFavorito = idFavorito;
            this.IdEquipo = idEquipo;
            this.IdUsuario = idUsuario;
            this.NombreEquipo = nombreEquipo;
            this.Escudo = escudo;
            this.Categoria = categoria;
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


        public List<ENFavoritos> ReadAllUsuario()
        {
            CADFavoritos favoritos = new CADFavoritos();
            return favoritos.ReadAllUsuario(this.IdUsuario); 
        }
    }
}
