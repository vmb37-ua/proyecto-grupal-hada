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
        public ENFavoritos()
        {
            idFavorito = 0;
            idEquipo = 0;
        }
        public ENFavoritos(int idFavorito, int idEquipo)
        {
            this.IdFavorito = idFavorito;
            this.IdEquipo = idEquipo;
        }
        public bool Create()
        {
            CADFavoritos favoritos = new CADFavoritos();
            return favoritos.Create(this);
        }
        public bool Update()
        {
            CADFavoritos favoritos = new CADFavoritos();
            return favoritos.Update(this);
        }
        public bool Delete()
        {
            CADFavoritos favoritos = new CADFavoritos();
            return favoritos.Delete(this);
        }

        public bool Read()
        {
            CADFavoritos favoritos = new CADFavoritos();
            return favoritos.Read(this);
        }
        public List<ENFavoritos> ReadAll()
        {
            CADFavoritos favoritos = new CADFavoritos();
            return favoritos.ReadAll(this);
        }
    }
}