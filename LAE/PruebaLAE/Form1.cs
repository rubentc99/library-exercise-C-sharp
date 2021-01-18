using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PruebaLAE
{
    public partial class Form1 : Form
    {
        /// <summary>
        /// inicializo los arrayList cuando se lanza el formulario.
        /// </summary>
        public Form1()
        {
            InitializeComponent();
        }

        private void insertarToolStripMenuItem_Click(object sender, EventArgs e) //click en insertar autor
        {
            //Le mando el arraylist al form InsertarAutor
            InsertarAutor ia = new InsertarAutor(); 
            ia.ShowDialog();
        }

        private void consultarToolStripMenuItem_Click(object sender, EventArgs e) //click en consultar autor
        {
            ConsultarAutores ca = new ConsultarAutores(); //le mando al form consultarAutores el arraylist autores
            ca.ShowDialog();
        }

        private void modificarToolStripMenuItem_Click(object sender, EventArgs e) //click en modificar autor
        {
            ModificarAutor ma = new ModificarAutor();
            ma.ShowDialog();
        }

        private void eliminarToolStripMenuItem_Click(object sender, EventArgs e) //click en eliminar autor
        {
            EliminarAutor ea = new EliminarAutor();
            ea.ShowDialog();
        }

        private void insertarToolStripMenuItem1_Click(object sender, EventArgs e) //click en insertar libro
        {
            //Le mando los arraylist
            InsertarLibro il = new InsertarLibro();
            il.ShowDialog();
        }

        private void consultarToolStripMenuItem1_Click(object sender, EventArgs e) //click en consultar libro
        {
            ConsultarLibros cl = new ConsultarLibros(); //le mando al form consultarAutores el arraylist libros
            cl.ShowDialog();
        }

        private void modificarToolStripMenuItem1_Click(object sender, EventArgs e) //click en modificar libro
        {
            ModificarLibro ml = new ModificarLibro();
            ml.ShowDialog();
        }

        private void salirToolStripMenuItem_Click(object sender, EventArgs e) //opción de salir
        {
            /*
            //almacenamos los autores en un fichero binario.
            if (autores.Count != 0)
            {
                //si el arrayList tiene datos, los guardo en un archivo
                IFormatter formato = new BinaryFormatter();
                FileStream fs = new FileStream("Autor", FileMode.Create);
                formato.Serialize(fs, autores);
                fs.Close();
            }
            if(libros.Count != 0){
                //si el arrayList tiene datos, los guardo en un archivo
                IFormatter formato = new BinaryFormatter();
                FileStream fs = new FileStream("Libro", FileMode.Create);
                fs.Close();
            }
            if (editoriales.Count != 0)
            {
                //si el arrayList tiene datos, los guardo en un archivo
                IFormatter formato = new BinaryFormatter();
                FileStream fs = new FileStream("Editorial", FileMode.Create);
                fs.Close();
            }
            Application.Exit();
            */
        }

        private void Form1_Load(object sender, EventArgs e) //cuando se carga el formulario
        {
            /*
            Base_de_Datos bd = new Base_de_Datos();
            if (bd.abrir_Conexion())
            {
                MessageBox.Show("Estoy conectado al servidor de BBDD.");
            }
            else
            {
                MessageBox.Show("No estamos conectados al servidor de BBDD.");
            }

            //cargo los ficheros de autor libro y editorial
            if (File.Exists("Autor")) //compruebo si existe algun fichero llamado autor para cargarlo
            {
                //Si entro aqui es porque exite el fichero autor, vamos a
                //cargar los datos en el arrayList para que el usuario pueda
                //trabajar con ellos
                IFormatter formato = new BinaryFormatter();
                FileStream fs = new FileStream("Autor", FileMode.Open);
                autores = formato.Deserialize(fs) as ArrayList;
                fs.Close();
            }
            if (File.Exists("Libro")) //compruebo si existe algun fichero llamado autor para cargarlo
            {
                //Si entro aqui es porque exite el fichero autor, vamos a
                //cargar los datos en el arrayList para que el usuario pueda
                //trabajar con ellos
                IFormatter formato = new BinaryFormatter();
                FileStream fs = new FileStream("Libro", FileMode.Open);
                libros = formato.Deserialize(fs) as ArrayList;
                fs.Close();
            }
            if (File.Exists("Editorial")) //compruebo si existe algun fichero llamado autor para cargarlo
            {
                //Si entro aqui es porque exite el fichero autor, vamos a
                //cargar los datos en el arrayList para que el usuario pueda
                //trabajar con ellos
                IFormatter formato = new BinaryFormatter();
                FileStream fs = new FileStream("Editorial", FileMode.Open);
                editoriales = formato.Deserialize(fs) as ArrayList;
                fs.Close();
            }*/
        }

        private void insertarToolStripMenuItem2_Click(object sender, EventArgs e) //click en insertar editorial
        {
            //Le mando el arraylist al form InsertarAutor
            InsertarEditorial ie = new InsertarEditorial();
            ie.ShowDialog();
        }

        private void consultarToolStripMenuItem2_Click(object sender, EventArgs e) //click en consultar editorial
        {
            ConsultarEditorial ce = new ConsultarEditorial();
            ce.ShowDialog();
        }

        private void modificarToolStripMenuItem2_Click(object sender, EventArgs e) //click en modificar editorial
        {
            ModificarEditorial me = new ModificarEditorial();
            me.ShowDialog();
        }

        private void eliminarToolStripMenuItem2_Click(object sender, EventArgs e) //click en eliminar editorial
        {
            EliminarEditorial ee = new EliminarEditorial();
            ee.ShowDialog();
        }
    }
}
