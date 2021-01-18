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
    public partial class ModificarLibro : Form
    {
        //creo el ArrayList con el que estoy trabajando y una booleana que usaré luego.
        private ArrayList listaLibros;
        private bool modificado;

        /// <summary>
        /// Constructor por defecto de ModificarLibro.
        /// </summary>
        public ModificarLibro()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Constructor modificado de ModificarAutor al que le pasaré un ArrayList
        /// </summary>
        /// <param name="l">ArrayList que proviene del form principal</param>
        public ModificarLibro(ArrayList l)
        {
            InitializeComponent();
            listaLibros = l;
            modificado = false;
        }

        private void ModificarLibro_Load(object sender, EventArgs e)
        {
            textBox2.Enabled = false;
            textBox3.Enabled = false;
            textBox4.Enabled = false;
            textBox5.Enabled = false;
            textBox6.Enabled = false;
            button3.Enabled = false;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            openFileDialog1.FileName = "";
            openFileDialog1.Filter = "Archivos JPG (*.jpg)|.*jpg| Archivos PNG (*.png)|*.png| Archivos JPEG(*.jpeg)|.*jpeg| Todos los archivos(*.*)|*.*";
            openFileDialog1.ShowDialog(); //se abre la ventana de selección de archivo
            if (openFileDialog1.FileName.Length != 0) //Significa que el usuario ha introducido un nombre de fichero.
            {
                pictureBox1.ImageLocation = openFileDialog1.FileName; //le asigno al picturebox la imagen seleccionada
            }
        }

        private void button4_Click(object sender, EventArgs e) //botón de buscar ID
        {
            /*
            bool encontrado = false;
            int contador = 0;
            int indice = 0;
            if (textBox1.Text.Length != 0) //Hay datos, ahora que sea valido el id o no es otra cosa.
            {
                foreach (Libro l in listaLibros)
                {
                    if (l.getIdLibro() == Convert.ToInt32(textBox1.Text))
                    {
                        indice = contador;
                        encontrado = true;
                    }
                    else
                    {
                        contador++;
                    }
                }
                if (encontrado)
                {
                    //elimino el boton de buscar ID
                    button4.Visible = false;

                    //habilito los demás textbox y deshabilito el del ID
                    textBox1.Enabled = false;
                    textBox2.Enabled = true;
                    textBox3.Enabled = true;
                    textBox4.Enabled = true;
                    textBox5.Enabled = true;
                    textBox6.Enabled = true;
                    button3.Enabled = true;

                    Libro l;
                    l = (Libro)listaLibros[indice];

                    textBox2.Text = l.getIdAutor().ToString();
                    textBox3.Text = l.getIdEditorial().ToString();
                    textBox4.Text = l.getTitulo();
                    textBox5.Text = l.getIsbn();
                    textBox6.Text = l.getPaginas().ToString();
                    pictureBox1.Image = l.getImagen();
                }
                else
                {
                    MessageBox.Show("No se ha encontrado ningún autor con ese ID.");
                }
            }
            else
            {
                MessageBox.Show("Debes introducir un ID en su campo correspondiente.");
            }
            */

            Base_de_Datos bd = new Base_de_Datos();
            Regex regex = new Regex("^[1-9]+$");
            //si hay caracteres, la variable hasOnlyAlpha se pondrá a true
            bool soloNumeros = regex.IsMatch(textBox1.Text);
            if (soloNumeros == true)
            {
                int numero = bd.existeIdEditorial(Convert.ToInt32(textBox1.Text));
                switch (numero) //depende del numero que devuelva la funcion hago una cosa u otra
                {
                    case -1: //el id introducido no coincide con ninguno de la base de datos
                        {
                            MessageBox.Show("El ID que has introducido no figura en la base de datos.");
                            break;
                        }
                    case 0:
                        {
                            MessageBox.Show("Primero debes introducir un ID.");
                            break;
                        }
                    case 1: //el id esta en la base de datos
                        {
                            ArrayList provisional = new ArrayList();
                            provisional = bd.obtenerEditorialesConId(Convert.ToInt32(textBox1.Text));
                            Libro l;
                            l = (Libro)listaLibros[0];
                            textBox1.Text = l.getIdLibro().ToString();
                            textBox2.Text = l.getIdAutor().ToString();
                            textBox3.Text = l.getIdEditorial().ToString();
                            textBox4.Text = l.getTitulo();
                            textBox5.Text = l.getIsbn();
                            textBox6.Text = l.getPaginas().ToString();
                            pictureBox1.Image = l.getImagen();
                            break;
                        }
                }
            }
            else
            {
                MessageBox.Show("El ID introducido no es correcto. Prueba de nuevo.");
            }
        }
        private void button2_Click(object sender, EventArgs e) //boton de cancelar
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Base_de_Datos bd = new Base_de_Datos();
            bd.modificarLibro(Convert.ToInt32(textBox1.Text), Convert.ToInt32(textBox2.Text), Convert.ToInt32(textBox3.Text), textBox4.Text, textBox5.Text, Convert.ToInt32(textBox6.Text), pictureBox1);
            MessageBox.Show("Libro modificado con éxito.");
            this.Close();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
    }
}
