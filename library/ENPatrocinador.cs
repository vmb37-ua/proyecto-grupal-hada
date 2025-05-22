using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace library
{

    /// <summary>
    /// Clase que representa la entidad de negocio de patrocinador.
    /// Esta asociado a la clase <c>CADPatrocinador</c>>.
    /// </summary>
    public class ENPatrocinador
    {
        int id_patrocinador;
        float dinero;
        string texto;
        string imagen;

        /// <summary>
        /// Identificar del patrocinador
        /// </summary>
        public int Id_patrocinador
        {
            get { return id_patrocinador; }
            set { id_patrocinador = value; }
        }

        /// <summary>
        /// Dinero que paga el patrocinador.
        /// </summary>
        public float Dinero
        {
            get { return dinero; }
            set { dinero = value; }
        }

        /// <summary>
        /// Nombre del patrocinador.
        /// </summary>
        public string Texto
        {
            get { return texto; }
            set { texto = value; }
        }

        /// <summary>
        /// Imagen del patrocinador.
        /// </summary>
        public string Imagen
        {
            get { return imagen; }
            set { imagen = value; }
        }

        /// <summary>
        /// Constructor por defecto.
        /// Inicializa <c>id_patrocinador</c> y <c>dinero</c> a cero, y <c>texto</c> e <c>imagen</c> a cadenas vacias.
        /// </summary>
        public ENPatrocinador()
        {
            id_patrocinador = 0;
            dinero = 0;
            texto = "";
            imagen = "";
        }

        /// <summary>
        /// Constructor sobrecargado.
        /// </summary>
        /// <param name="id_patrocinador">Id del patrocinador a asignar.</param>
        /// <param name="dinero">Dinero del patrocinador a asignar.</param>
        /// /// <param name="texto">Nombre del patrocinador a asiginar.</param>
        /// <param name="imagen">Imagen del patrocinador a asignar.</param>
        public ENPatrocinador(int id_patrocinador, float dinero, string texto, string imagen)
        {
            this.id_patrocinador = id_patrocinador;
            this.dinero = dinero;
            this.texto = texto;
            this.imagen = imagen;
        }

        /// <summary>
        /// Crea un nuevo patrocinador en la BD mediante el CAD.
        /// </summary>
        /// <returns>
        /// <c>true</c> si el patrocinador se ha creado.
        /// <c>false</c> si el patrocinador no se ha podido crear.
        /// </returns>
        public bool Create()
        {
            CADPatrocinador patrocinador = new CADPatrocinador();
            return patrocinador.Create(this);
        }

        /// <summary>
        /// Actualiza un patrocinador existente en la BD mediante el CAD.
        /// </summary>
        /// <returns>
        /// <c>true</c> si el patrocinador se ha actualizado.
        /// <c>false</c> si el patrocinador no se ha podido actualizar.
        /// </returns>
        public bool Update()
        {
            CADPatrocinador patrocinador = new CADPatrocinador();
            return patrocinador.Update(this);
        }

        /// <summary>
        /// Elimina un patrocinador existente en la BD mediante el CAD.
        /// </summary>
        /// <returns>
        /// <c>true</c> si el patrocinador se ha eliminado.
        /// <c>false</c> si el patrocinador no se ha podido eliminar.
        /// </returns>
        public bool Delete()
        {
            CADPatrocinador patrocinador = new CADPatrocinador();
            return patrocinador.Delete(this);
        }

        /// <summary>
        /// Busca un patrocinador existente en la BD a partir de su id mediante el CAD.
        /// </summary>
        /// <returns>
        /// <c>true</c> si el patrocinador se ha encontrado.
        /// <c>false</c> si el patrocinador no se ha podido encontrar.
        /// </returns>
        public bool Read()
        {
            CADPatrocinador patrocinador = new CADPatrocinador();
            return patrocinador.Read(this);
        }

        /// <summary>
        /// Busca el primer patrocinador existente en la BD mediante el CAD.
        /// </summary>
        /// <returns>
        /// <c>true</c> si un patrocinador se ha obtenido.
        /// <c>false</c> si ningun patrocinador se ha podido obtener.
        /// </returns>
        public bool ReadFirst()
        {
            CADPatrocinador patrocinador = new CADPatrocinador();
            return patrocinador.ReadFirst(this);
        }

        /// <summary>
        /// Busca el siguiente patrocinador respecto al actual en la BD mediante el CAD.
        /// </summary>
        /// <returns>
        /// <c>true</c> si el siguiente patrocinador se ha obtenido.
        /// <c>false</c> si el siguiente  patrocinador no se ha podido obtener.
        /// </returns>
        public bool ReadNext()
        {
            CADPatrocinador patrocinador = new CADPatrocinador();
            return patrocinador.ReadNext(this);
        }

        /// <summary>
        /// Busca el anterior patrocinador respecto al actual en la BD mediante el CAD.
        /// </summary>
        /// <returns>
        /// <c>true</c> si el anterior patrocinador se ha obtenido.
        /// <c>false</c> si el anterior  patrocinador no se ha podido obtener.
        /// </returns>
        public bool ReadPrev()
        {
            CADPatrocinador patrocinador = new CADPatrocinador();
            return patrocinador.ReadPrev(this);
        }

        /// <summary>
        /// Obtiene la lista completa de los patrocinadores existentes en la BD.
        /// </summary>
        /// <returns>Lista de objetos ENPatrocinador.</returns>
        public List<ENPatrocinador> ReadAll()
        {
            CADPatrocinador patrocinador = new CADPatrocinador();
            return patrocinador.ReadAll(this);
        }
    }
}
