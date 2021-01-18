using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PruebaLAE
{
    public partial class ConsultarLibros : Form
    {

        //private ArrayList losLibros; //defino como variable privada el arraylist que estaré utilizando todo el rato.
        private int contador; //defino como variable privada un contador que usaré más tarde.

        /// <summary>
        /// Constructor por defecto del form consultarLibros
        /// </summary>
        public ConsultarLibros()
        {
            InitializeComponent();
            contador = 0;
        }

        private void ConsultarLibros_Load(object sender, EventArgs e) //cuando se carga el formulario
        {
            /*
            Libro l;
            l = (Libro)losLibros[contador]; //con la variable l me referiré al objeto libro dentro del arrayList de autores en la posicion del contador

            //utilizo los getters para poner la información en su campo correspondiente
            textBox1.Text = l.getIdAutor().ToString();
            textBox2.Text = l.getIdLibro().ToString();
            textBox3.Text = l.getIdEditorial().ToString();
            textBox4.Text = l.getTitulo();
            textBox5.Text = l.getIsbn();
            textBox6.Text = l.getPaginas().ToString();
            pictureBox1.Image = l.getImagen();
            */

            //dejo todo deshabilitado porque es solo para consultar
            textBox1.Enabled = false;
            textBox2.Enabled = false;
            textBox3.Enabled = false;
            textBox4.Enabled = false;
            textBox5.Enabled = false;
            textBox6.Enabled = false;
            button3.Enabled = false;

            Base_de_Datos bd = new Base_de_Datos();
            ArrayList listaLibros = new ArrayList(); //utilizo este arraylist para recibir el que devuelve la funcion obtenerLibros
            listaLibros = bd.obtenerLibros(); //el arraylist solo sirve de apoyo para referirnos al objeto que queremos
            Libro l;
            l = (Libro)listaLibros[contador];
            textBox1.Text = l.getIdLibro().ToString();
            textBox2.Text = l.getIdAutor().ToString();
            textBox3.Text = l.getIdEditorial().ToString();
            textBox4.Text = l.getTitulo();
            textBox5.Text = l.getIsbn();
            textBox6.Text = l.getPaginas().ToString();
            pictureBox1.Image = l.getImagen();
        }

        private void button2_Click(object sender, EventArgs e) //botón de anterior
        {
            if (contador == 0) //si el contador es 0, quiere decir que no quedan editoriales para mostrar
            {
                MessageBox.Show("No hay más libros para mostrar.");
            }
            else //si no, diminuyo la variable contador
            {
                contador--;
                if (contador >= 0) //si el contador es mayor o igual a 0, significa que puedo mostrar los datos del autor
                {
                    Base_de_Datos bd = new Base_de_Datos();
                    ArrayList listaLibros = new ArrayList();
                    listaLibros = bd.obtenerEditoriales();
                    Libro l;
                    l = (Libro)listaLibros[contador];
                    textBox1.Text = l.getIdLibro().ToString();
                    textBox2.Text = l.getIdAutor().ToString();
                    textBox3.Text = l.getIdEditorial().ToString();
                    textBox4.Text = l.getTitulo();
                    textBox5.Text = l.getIsbn();
                    textBox6.Text = l.getPaginas().ToString();
                    pictureBox1.Image = l.getImagen();
                }
            }
        }

        private void button1_Click(object sender, EventArgs e) //botón de siguiente
        {
            contador++;
            Base_de_Datos bd = new Base_de_Datos();
            ArrayList listaLibros = new ArrayList();
            listaLibros = bd.obtenerLibros();
            if (contador < listaLibros.Count) //si el contador es menor que el tamaño del array
            {
                //muestro los datos con los getter
                Libro l;
                l = (Libro)listaLibros[contador];
                textBox1.Text = l.getIdLibro().ToString();
                textBox2.Text = l.getIdAutor().ToString();
                textBox3.Text = l.getIdEditorial().ToString();
                textBox4.Text = l.getTitulo();
                textBox5.Text = l.getIsbn();
                textBox6.Text = l.getPaginas().ToString();
                pictureBox1.Image = l.getImagen();
            }
            else //quiere decir que el contador es más grande que la longitud del propio arraylist, por lo que no hay nada que mostrar.
            {
                MessageBox.Show("No hay más libros para mostrar.");
                contador--;
            }
        }

        private void button4_Click(object sender, EventArgs e) //botón de cancelar.
        {
            this.Close(); //cierro el formulario.
        }
    }
}
