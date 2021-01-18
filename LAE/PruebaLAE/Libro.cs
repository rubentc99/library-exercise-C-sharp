using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PruebaLAE
{
    [Serializable]
    class Libro
    {
        private int idLibro;
        private int idAutor;
        private int idEditorial;
        private string titulo;
        private string isbn;
        private int paginas;
        private Image imagen;

        /// <summary>
        /// Constructor vacío de la clase libro
        /// </summary>
        public Libro()
        {
            idLibro = 0;
            idAutor = 0;
            idEditorial = 0;
            titulo = null;
            isbn = null;
            paginas = 0;
            imagen = null;
        }


        /// <summary>
        /// Constructor para la clase Libro
        /// </summary>
        /// <param name="idLibro">ID de referencia para el libro</param>
        /// <param name="idAutor">ID de referencia para el autor</param>
        /// <param name="idEditorial">ID de referencia para el autor</param>
        /// <param name="titulo">titulo del libro</param>
        /// <param name="isbn">isbn (codigo) del libro</param>
        /// <param name="paginas">nº de paginas que tiene el libro</param>
        /// <param name="imagen">imagen del libro</param>
        public Libro(int idLibro, int idAutor, int idEditorial, string titulo, string isbn, int paginas, Image imagen)
        {
            this.idLibro = idLibro;
            this.idAutor = idAutor;
            this.idEditorial = idEditorial;
            this.titulo = titulo;
            this.isbn = isbn;
            this.paginas = paginas;
            this.imagen = imagen;
        }

        /// <summary>
        /// Método que asigna el ID del libro.
        /// </summary>
        /// <param name="idLibro">ID, número de referencia del libro.</param>
        public void setIdLibro(int idLibro)
        {
            this.idLibro = idLibro;
        }

        /// <summary>
        /// Método que asigna el ID del autor.
        /// </summary>
        /// <param name="idAutor">ID, número de referencia del autor.</param>
        public void setIdAutor(int idAutor)
        {
            this.idAutor = idAutor;
        }

        /// <summary>
        /// Método que asigna el ID de la editorial.
        /// </summary>
        /// <param name="idEditorial">ID, número de referencia de la editorial.</param>
        public void setIdEditorial(int idEditorial)
        {
            this.idEditorial = idEditorial;
        }

        /// <summary>
        /// Método que asigna el título del libro.
        /// </summary>
        /// <param name="titulo">Título del libro.</param>
        public void setTitulo(string titulo)
        {
            this.titulo = titulo;
        }

        /// <summary>
        /// Método que asigna el isbn del libro.
        /// </summary>
        /// <param name="isbn">Isbn (código de referencia) del libro.</param>
        public void setIsbn(string isbn)
        {
            this.isbn = isbn;
        }

        /// <summary>
        /// Método que asigna el número de páginas del libro.
        /// </summary>
        /// <param name="paginas">Nº de páginas que tiene el libro.</param>
        public void setPaginas(int paginas)
        {
            this.paginas = paginas;
        }

        /// <summary>
        /// Método que asigna una imagen al libro.
        /// </summary>
        /// <param name="imagen">Imagen del libro.</param>
        public void setImagen(Image imagen)
        {
            this.imagen = imagen;
        }

        /// <summary>
        /// Método para obtener el ID del libro.
        /// </summary>
        /// <returns>ID del libro.</returns>
        public int getIdLibro()
        {
            return this.idLibro;
        }

        /// <summary>
        /// Método para obtener el ID del autor.
        /// </summary>
        /// <returns>ID del autor.</returns>
        public int getIdAutor()
        {
            return this.idAutor;
        }

        /// <summary>
        /// Método para obtener el ID de la editorial.
        /// </summary>
        /// <returns>Id de la editorial.</returns>
        public int getIdEditorial()
        {
            return this.idEditorial;
        }

        /// <summary>
        /// Método para obtener título del libro.
        /// </summary>
        /// <returns>Título del libro.</returns>
        public string getTitulo()
        {
            return this.titulo;
        }

        /// <summary>
        /// Método para obtener el isbn del libro.
        /// </summary>
        /// <returns>ISBN del libro.</returns>
        public string getIsbn()
        {
            return this.isbn;
        }

        /// <summary>
        ///Método para obtener las páginas del libro 
        /// </summary>
        /// <returns>nº paginas del libro.</returns>
        public int getPaginas()
        {
            return this.paginas;
        }
        /// <summary>
        /// Método para obtener la imagen del libro.
        /// </summary>
        /// <returns>Imagen del libro.</returns>
        public Image getImagen()
        {
            return this.imagen;
        }
    }
}
