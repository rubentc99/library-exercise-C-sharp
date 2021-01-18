using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PruebaLAE
{
    [Serializable]
    class Editorial
    {
        private int id;
        private string nombre;
        private string nacionalidad;
        private Image imagen;

        /// <summary>
        /// Constructor por defecto de la clase Autor.
        /// </summary>
        public Editorial()
        {
            id = 0;
            nombre = null;
            nacionalidad = null;
            imagen = null;
        }

        /// <summary>
        /// Constructor de la clase Editorial con todos sus parámetros.
        /// </summary>
        /// <param name="id">ID de referencia para la editorial.</param>
        /// <param name="nombre">Nombre de la editorial.</param>
        /// <param name="nacionalidad">Nacionalidad de la editorial.</param>
        /// <param name="imagen">Imagen de la editorial.</param>
        public Editorial(int id, string nombre, string nacionalidad, Image imagen)
        {
            this.id = id;
            this.nombre = nombre;
            this.nacionalidad = nacionalidad;
            this.imagen = imagen;
        }

        /// <summary>
        /// Método para obtener el ID de la editorial.
        /// </summary>
        /// <returns>ID de la editorial.</returns>
        public int getId()
        {
            return this.id;
        }

        /// <summary>
        /// Método para obtener el nombre de la editorial.
        /// </summary>
        /// <returns>Nombre de la editorial.</returns>
        public string getNombre()
        {
            return this.nombre;
        }

        /// <summary>
        /// Método para obtener la nacionalidad de la editorial.
        /// </summary>
        /// <returns>Nacionalidad de la editorial.</returns>
        public string getNacionalidad()
        {
            return this.nacionalidad;
        }

        /// <summary>
        /// Método para obtener la imagen de la editorial.
        /// </summary>
        /// <returns>Imagen de la editorial.</returns>
        public Image getImagen()
        {
            return this.imagen;
        }

        /// <summary>
        /// Método para asignar el ID de la editorial.
        /// </summary>
        /// <param name="id">ID (numero de identificación) de la editorial.</param>
        public void setId(int id)
        {
            this.id = id;
        }

        /// <summary>
        /// Método para asignar el nombre de la editorial.
        /// </summary>
        /// <param name="nombre">Nombre de la editorial.</param>
        public void setNombre(string nombre)
        {
            this.nombre = nombre;
        }

        /// <summary>
        /// Método para asignar la nacionalidad de la editorial.
        /// </summary>
        /// <param name="nacionalidad">Nacionalidad de la editorial.</param>
        public void setNacionalidad(string nacionalidad)
        {
            this.nacionalidad = nacionalidad;
        }

        /// <summary>
        /// Método para asignar la imagen de la editorial.
        /// </summary>
        /// <param name="imagen">Imagen de la editorial.</param>
        public void setImagen(Image imagen)
        {
            this.imagen = imagen;
        }
    }
}
