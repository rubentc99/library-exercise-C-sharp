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
    public partial class EliminarAutor : Form
    {

        /// <summary>
        /// Constructor por defecto de EliminarAutor.
        /// </summary>
        public EliminarAutor()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Cuando se cargue el formulario, se cargarán todos los textBox deshabilitados.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EliminarAutor_Load(object sender, EventArgs e)
        {
            textBox2.Enabled = false;
            textBox3.Enabled = false;
            comboBox1.Enabled = false;
            dateTimePicker1.Enabled = false;
            pictureBox1.Enabled = false;
        }

        private void button1_Click(object sender, EventArgs e) //botón de buscar ID
        {
            /*
            bool encontrado = false; //asigno a false la variable encontrado.
            int contador = 0; //asigno a false la variable contador.
            int indice = 0; //indice para referirme al arrayList.

            if (textBox1.Text.Length != 0) //si en el textBox hay contenido.
            {
                //el usuario ha introducido un dato
                foreach (Autor a in misAutores) //para cada autor dentro del arrayList.
                {
                    if (a.getId() == Convert.ToInt32(textBox1.Text)) //si el ID coincide.
                    {
                        encontrado = true; //pongo true encontrado.
                        indice = contador; //asigno al indice el numero del contador.
                    }
                    contador++; //aumento contador.
                }
                if(encontrado) //si lo encuentro, habilito los textbox y saco los datos mediante getters
                {
                    Autor a = (Autor)misAutores[indice];
                    textBox2.Text = a.getNombre();
                    textBox2.Enabled = true;
                    textBox3.Text = a.getApellidos();
                    textBox3.Enabled = true;
                    textBox4.Text = a.getNacionalidad();
                    textBox4.Enabled = true;
                    dateTimePicker1.Enabled = true;
                    dateTimePicker1.Value = Convert.ToDateTime(a.getFechaNacimiento());
                    pictureBox1.Image = a.getImage();
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
                //utilizo la funcion existe_id_Autor solo para comprobar si el id introducido es correcto o no antes de hacer el select
                int numero = bd.existe_id_Autor(Convert.ToInt32(textBox1.Text));
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
                            provisional = bd.obtener_Autores_con_ID(Convert.ToInt32(textBox1.Text));
                            Autor a;
                            a = (Autor)provisional[0];
                            textBox2.Text = a.getNombre();
                            textBox3.Text = a.getApellidos();
                            comboBox1.Text = a.getNacionalidad();
                            dateTimePicker1.Text = a.getFechaNacimiento();
                            pictureBox1.Image = a.getImage();
                            break;
                        }
                }
            }
            else
            {
                MessageBox.Show("El ID introducido no es correcto. Prueba de nuevo.");
            }
        }

        private void button2_Click(object sender, EventArgs e) //botón de aceptar.
        {
            //muestro un mensaje de advertencia al que puede darle si o no
            if(MessageBox.Show("Seguro que quieres eliminarlo", "Mensaje de advertencia", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                /*
                int contador = 0; //asigno a false la variable contador.
                int indice = 0; //indice para referirme al arrayList.
                foreach (Autor a in misAutores) //para cada autor dentro del arrayList.
                {
                    if (a.getId() == Convert.ToInt32(textBox1.Text)) //si el ID coincide.
                    {
                        indice = contador; //asigno al indice el numero del contador.
                    }
                    contador++; //incremento el contador.
                }
                misAutores.RemoveAt(indice);
                MessageBox.Show("Autor eliminado con éxito"); //elimino ese objeto autor.
                this.Close();
                */

                Base_de_Datos bd = new Base_de_Datos();
                bd.eliminar_autor(Convert.ToInt32(textBox1.Text));
                this.Close(); 
            }
        }

        private void button3_Click(object sender, EventArgs e) //botón de cerrar.
        {
            this.Close(); //cierro el formulario.
        }
    }
}
