using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing; //Añado esto para poder trabajar con la imagen

namespace PruebaLAE
{
    [Serializable]
    class Autor
    {
        private int id;
        private string nombre;
        private string apellidos;
        private string nacionalidad;
        private string fechaNacimiento;
        private Image imagenAutor;

        /// <summary>
        /// Constructor por defecto para la clase Autor
        /// </summary>
        public Autor()
        {
            id = 0;
            nombre = null;
            apellidos = null;
            nacionalidad = null;
            fechaNacimiento = null;
            imagenAutor = null;

        }


        /// <summary>
        /// Constructor de la clase Autor con todos los parámetros.
        /// </summary>
        /// <param name="id">Establece el id para el autor.</param>
        /// <param name="nombre">Establece el nombre para el autor.</param>
        /// <param name="apellidos">Establece los apellidos para el autor.</param>
        /// <param name="nacionalidad">Establece la nacionalidad del autor.</param>
        /// <param name="fechaNacimiento">Establece la fecha de nacimiento del autor.</param>
        /// <param name="imagenAutor">Establece la imagen que el autor decida cargar.</param>
        public Autor(int id, string nombre, string apellidos, string nacionalidad, string fechaNacimiento, Image imagenAutor)
        {
            this.id = id;
            this.nombre = nombre;
            this.apellidos = apellidos;
            this.nacionalidad = nacionalidad;
            this.fechaNacimiento = fechaNacimiento;
            this.imagenAutor = imagenAutor;
        }


        /// <summary>
        /// Método para cambiar el id del autor.
        /// </summary>
        /// <param name="id">Id a asignar para el autor.</param>
        public void setId(int id)
        {
            this.id = id;
        }

        /// <summary>
        /// Método que nos devuelve el id del autor.
        /// </summary>
        /// <returns>Devuelve el id del autor.</returns>
        public int getId()
        {
            return this.id;
        }

        /// <summary>
        /// Método para establecer el nombre del autor.
        /// </summary>
        /// <param name="nombre">Nombre a asignar para el autor.</param>
        public void setNombre(string nombre)
        {
            this.nombre = nombre;
        }

        /// <summary>
        /// Método para obtener el nombre del autor.
        /// </summary>
        /// <returns>Devuelve el nombre del autor.</returns>
        public string getNombre()
        {
            return this.nombre;
        }

        /// <summary>
        /// Método para establecer el nombre del autor.
        /// </summary>
        /// <param name="apellidos">Apellidos a asignar para el autor.</param>
        public void setApellidos(string apellidos)
        {
            this.apellidos = apellidos;
        }

        /// <summary>
        /// Método para obtener los apellidos del autor.
        /// </summary>
        /// <returns>Devuelve los apellidos del autor.</returns>
        public string getApellidos()
        {
            return this.apellidos;
        }

        /// <summary>
        /// Método para establecer la nacionalidad del autor.
        /// </summary>
        /// <param name="nacionalidad">Nacionalidad a asignar para el autor.</param>
        public void setNacionalidad(string nacionalidad)
        {
            this.nacionalidad = nacionalidad;
        }

        /// <summary>
        /// Método para obtener la nacionalidad del autor.
        /// </summary>
        /// <returns>Devuelve la nacionalidad del autor.</returns>
        public string getNacionalidad()
        {
            return this.nacionalidad;
        }

        /// <summary>
        /// Método para establecer la fecha de nacimiento del autor.
        /// </summary>
        /// <param name="fechaNacimiento">Fecha de nacimiento a asignar para el autor.</param>
        public void setFechaNacimiento(string fechaNacimiento)
        {
            this.fechaNacimiento = fechaNacimiento;
        }

        /// <summary>
        /// Método para obtener la fecha de nacimiento del autor.
        /// </summary>
        /// <returns>Devuelve la fecha de nacimiento del autor.</returns>
        public string getFechaNacimiento()
        {
            return this.fechaNacimiento;
        }

        /// <summary>
        /// Método para establecer la imagen que el autor seleccione.
        /// </summary>
        /// <param name="imagenAutor">Imagen a asignar.</param>
        public void setImagen(Image imagenAutor)
        {
            this.imagenAutor = imagenAutor;
        }

        /// <summary>
        /// Método para obtener la imagen del autor.
        /// </summary>
        /// <returns>Devuelve la imagen del autor.</returns>
        public Image getImage()
        {
            return this.imagenAutor;
        }

        /// <summary>
        /// Método que devuelve toda la información del autor.
        /// </summary>
        /// <returns>Devuelve todos los datos del autor.</returns>
        public string obtenerInfoAutor()
        {
            string infoAutor;
            infoAutor = this.id + " " + this.nombre + " " + this.apellidos + " " + this.nacionalidad + " " + this.fechaNacimiento;
            return infoAutor;
        }
    }
}
