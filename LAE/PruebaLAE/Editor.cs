using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PruebaLAE
{
    class Editor : Autor
    {
        private string seudonimo;

        public Editor(int idAutor, string nombre, string apellidos, string nacionalidad, string fecha, Image imagen, string seudonimo):base(idAutor, nombre, apellidos, nacionalidad, fecha, imagen)
        {

        }
    }
}
