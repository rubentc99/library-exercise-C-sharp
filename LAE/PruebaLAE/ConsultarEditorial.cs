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
    public partial class ConsultarEditorial : Form
    {

        private int contador;

        public ConsultarEditorial()
        {
            InitializeComponent();
            contador = 0;
        }

        private void ConsultarEditorial_Load(object sender, EventArgs e) //se carga cuando se abre el form
        {
            /*
            //con la variable ed me referiré al objeto libro dentro del arrayList de autores en la posicion del contador
            Editorial ed;
            ed = (Editorial)lasEditoriales[contador];
            //muestro la información mediante getters
            textBox1.Text = ed.getId().ToString();
            textBox2.Text = ed.getNombre();
            textBox3.Text = ed.getNacionalidad();
            pictureBox1.Image = ed.getImagen();
            */

            //dejo todo deshabilitado porque es solo para consultar
            textBox1.Enabled = false;
            textBox2.Enabled = false;
            comboBox1.Enabled = false;
            button1.Enabled = false;

            Base_de_Datos bd = new Base_de_Datos();
            ArrayList listaEditoriales = new ArrayList(); //utilizo este arraylist para recibir el que devuelve la funcion obtenerEditoriales
            listaEditoriales = bd.obtenerEditoriales(); //el arraylist solo sirve de apoyo para referirnos al objeto que queremos
            Editorial ed;
            ed = (Editorial)listaEditoriales[contador];
            textBox1.Text = ed.getId().ToString();
            textBox2.Text = ed.getNombre();
            comboBox1.Text = ed.getNacionalidad();
            pictureBox1.Image = ed.getImagen();
        }

        private void button2_Click(object sender, EventArgs e) //botón siguiente
        {
            contador++;
            Base_de_Datos bd = new Base_de_Datos();
            ArrayList listaEditoriales = new ArrayList();
            listaEditoriales = bd.obtenerEditoriales();
            if (contador < listaEditoriales.Count) //si el contador de autores es menor a la longitud del arraylist
            {
                //muestro los datos con los getter
                Editorial ed;
                ed = (Editorial)listaEditoriales[contador];
                textBox1.Text = ed.getId().ToString();
                textBox2.Text = ed.getNombre();
                comboBox1.Text = ed.getNacionalidad();
                pictureBox1.Image = ed.getImagen();
            }
            else //quiere decir que el contador es más grande que la longitud del propio arraylist, por lo que no hay nada que mostrar
            {
                MessageBox.Show("No hay más editoriales para mostrar.");
                contador--;
            }
        }

        private void button4_Click(object sender, EventArgs e) //botón anterior
        {
            if (contador == 0) //si el contador es 0, quiere decir que no quedan editoriales para mostrar
            {
                MessageBox.Show("No hay más editoriales para mostrar.");
            }
            else //si no, diminuyo la variable contador
            {
                contador--;
                if (contador >= 0) //si el contador es mayor o igual a 0, significa que puedo mostrar los datos de la editorial
                {
                    Base_de_Datos bd = new Base_de_Datos();
                    ArrayList listaEditoriales = new ArrayList();
                    listaEditoriales = bd.obtenerEditoriales();
                    Editorial ed;
                    ed = (Editorial)listaEditoriales[contador];
                    textBox1.Text = ed.getId().ToString();
                    textBox2.Text = ed.getNombre();
                    comboBox1.Text = ed.getNacionalidad();
                    pictureBox1.Image = ed.getImagen();
                }
            }
        }

        private void button3_Click(object sender, EventArgs e) //botón de cancelar.
        {
            this.Close();
        }
    }
}
