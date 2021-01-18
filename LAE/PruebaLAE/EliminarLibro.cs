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
    public partial class EliminarLibro : Form
    {
        /// <summary>
        /// Constructor por defecto de EliminarLibro.
        /// </summary>
        public EliminarLibro()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Cuando se cargue el formulario, se cargarán todos los textBox deshabilitados.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EliminarLibro_Load(object sender, EventArgs e)
        {
            textBox2.Enabled = false;
            textBox3.Enabled = false;
            textBox4.Enabled = false;
            textBox5.Enabled = false;
            textBox6.Enabled = false;
            pictureBox1.Enabled = false;
        }

        private void button4_Click(object sender, EventArgs e) //botón de buscar ID
        {
            /*
            bool encontrado = false; //asigno a false la variable encontrado.
            int contador = 0; //asigno a false la variable contador.
            int indice = 0; //indice para referirme al arrayList.

            if (textBox1.Text.Length != 0) //si en el textBox hay contenido.
            {
                //el usuario ha introducido un dato
                foreach (Libro l in misLibros) //para cada libro dentro del arrayList.
                {
                    if (l.getIdLibro() == Convert.ToInt32(textBox1.Text)) //si el ID coincide.
                    {
                        encontrado = true; //pongo true encontrado.
                        indice = contador; //asigno al indice el numero del contador.
                    }
                    contador++; //incremento contador.
                }
                if (encontrado) //si lo encuentro, habilito los textbox y saco los datos mediante getters
                {
                    Libro l = (Libro)misLibros[indice];
                    textBox2.Text = l.getIdAutor().ToString();
                    textBox2.Enabled = true;
                    textBox3.Text = l.getIdEditorial().ToString();
                    textBox3.Enabled = true;
                    textBox4.Text = l.getTitulo();
                    textBox4.Enabled = true;
                    textBox5.Text = l.getIsbn();
                    textBox5.Enabled = true;
                    textBox6.Text = l.getPaginas().ToString();
                    textBox6.Enabled = true;
                    pictureBox1.Image = l.getImagen();
                    pictureBox1.Enabled = true;
                }
            }
            */

            Base_de_Datos bd = new Base_de_Datos();
            Regex regex = new Regex("^[1-9]+$");
            //si hay caracteres, la variable hasOnlyAlpha se pondrá a true
            bool soloNumeros = regex.IsMatch(textBox1.Text);
            if (soloNumeros == true)
            {
                //utilizo la funcion existeideditorial solo para comprobar si el id introducido es correcto o no antes de hacer el select
                int numero = bd.existeIdLibro(Convert.ToInt32(textBox1.Text));
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
                    case 1: //el id no está en la base de datos y por tanto es correcto
                        {
                            ArrayList provisional = new ArrayList();
                            provisional = bd.obtenerLibrosConId(Convert.ToInt32(textBox1.Text));
                            Libro l;
                            l = (Libro)provisional[0];
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



        private void button1_Click(object sender, EventArgs e) //botón de aceptar.
        {
            //muestro un mensaje de advertencia al que puede darle si o no
            if (MessageBox.Show("Seguro que quieres eliminarlo", "Mensaje de advertencia", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                /*
                int contador = 0; //asigno a false la variable contador.
                int indice = 0; //indice para referirme al arrayList.
                foreach (Libro l in misLibros) //para cada libro dentro del arrayList.
                {
                    if (l.getIdLibro() == Convert.ToInt32(textBox1.Text)) //si el ID coincide.
                    {
                        indice = contador; //asigno al indice el numero del contador.
                    }
                    contador++; //incremento el contador.
                }
                misLibros.RemoveAt(indice); //elimino ese objeto libro.
                MessageBox.Show("Libro eliminado con éxito");
                this.Close();
                */

                Base_de_Datos bd = new Base_de_Datos();
                bd.eliminarLibro(Convert.ToInt32(textBox1.Text));
                this.Close();
            }
        }
    }
}
