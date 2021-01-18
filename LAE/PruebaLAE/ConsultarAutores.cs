using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PruebaLAE
{
    public partial class ConsultarAutores : Form
    {

        //private ArrayList losAutores; //defino como variable privada el arraylist que estaré utilizando todo el rato.
        private int contador; //defino como variable privada un contador que usaré más tarde.

        /// <summary>
        /// Constructor por defecto del form consultarAutores
        /// </summary>
        public ConsultarAutores()
        {
            InitializeComponent();
            contador = 0; //inicializo el contador
        }

        private void consultarAutores_Load(object sender, EventArgs e)
        {
            /*
            Autor a;
            a = (Autor)losAutores[contador]; //con la variable a me referiré al objeto autor dentro del arrayList de autores en la posicion del contador
            //utilizo los getters para poner la información en su campo correspondiente
            textBox1.Text = a.getId().ToString();
            textBox2.Text = a.getNombre();
            textBox3.Text = a.getApellidos();
            comboBox1.Text = a.getNacionalidad();
            dateTimePicker1.Value = Convert.ToDateTime(a.getFechaNacimiento());
            pictureBox1.Image = a.getImage();
            */

            //muestro los campos deshabilitados puesto que es solo para consultar
            textBox1.Enabled = false;
            textBox2.Enabled = false;
            textBox3.Enabled = false;
            comboBox1.Enabled = false;
            dateTimePicker1.Enabled = false;
            pictureBox1.Enabled = false;

            Base_de_Datos bd = new Base_de_Datos();
             ArrayList listaAutores = new ArrayList(); //utilizo este arraylist para recibir el que devuelve la funcion obtener_Autores
            listaAutores = bd.obtener_Autores(); //el arraylist solo sirve de apoyo para referirnos al objeto que queremos
            Autor a;
            a = (Autor)listaAutores[contador];
            textBox1.Text = a.getId().ToString();
            textBox2.Text = a.getNombre();
            textBox3.Text = a.getApellidos();
            comboBox1.Text = a.getNacionalidad();
            dateTimePicker1.Text = a.getFechaNacimiento();
            pictureBox1.Image = a.getImage();
        }

        private void button1_Click(object sender, EventArgs e) //botón de siguiente
        {
            contador++;
            Base_de_Datos bd = new Base_de_Datos();
            ArrayList listaAutores = new ArrayList();
            listaAutores = bd.obtener_Autores();
            if (contador < listaAutores.Count) //si el contador de autores es menor a la longitud del arraylist
            {
                Autor a;
                a = (Autor)listaAutores[contador];
                textBox1.Text = a.getId().ToString();
                textBox2.Text = a.getNombre();
                textBox3.Text = a.getApellidos();
                comboBox1.Text = a.getNacionalidad();
                dateTimePicker1.Text = a.getFechaNacimiento();
                pictureBox1.Image = a.getImage();
            }
            else //quiere decir que el contador es más grande que la longitud del propio arraylist, por lo que no hay nada que mostrar
            {
                MessageBox.Show("No hay más autores para mostrar.");
                contador--;
            }
        }

        private void button2_Click(object sender, EventArgs e) //botón de anterior
        {
            if (contador == 0) //si el contador es 0, quiere decir que no quedan autores para mostrar
            {
                MessageBox.Show("No hay más autores para mostrar.");
            }
            else //si no, diminuyo la variable contador
            {
                contador--;
                if (contador >= 0) //si el contador es mayor o igual a 0, significa que puedo mostrar los datos del autor
                {
                    Base_de_Datos bd = new Base_de_Datos();
                    //utilizo la funcion existe_id_Autor solo para comprobar si el id introducido es correcto o no antes de hacer el select
                    ArrayList listaAutores = new ArrayList();
                    listaAutores = bd.obtener_Autores();
                    Autor a;
                    a = (Autor)listaAutores[contador];
                    textBox1.Text = a.getId().ToString();
                    textBox2.Text = a.getNombre();
                    textBox3.Text = a.getApellidos();
                    comboBox1.Text = a.getNacionalidad();
                    dateTimePicker1.Text = a.getFechaNacimiento();
                    pictureBox1.Image = a.getImage();
                }
            }
        }

        private void button3_Click(object sender, EventArgs e) //botón de cancelar
        {
            this.Close(); //cierro el formulario
        }
    }
}
