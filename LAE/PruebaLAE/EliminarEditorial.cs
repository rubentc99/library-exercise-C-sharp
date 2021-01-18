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
    public partial class EliminarEditorial : Form
    {
        /// <summary>
        /// Constructor por defecto de EliminarEditorial
        /// </summary>
        public EliminarEditorial()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Cuando se cargue el formulario, se cargarán todos los textBox deshabilitados.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EliminarEditorial_Load(object sender, EventArgs e) //cuando se cargue el formulario
        {
            //solo dejo habilitado el textbox del id 
            textBox2.Enabled = false;
            comboBox1.Enabled = false;
            button1.Enabled = false;
        }

        private void button4_Click(object sender, EventArgs e) //botón buscar ID
        {
            /*
            bool encontrado = false; //asigno a false la variable encontrado.
            int contador = 0; //asigno a false la variable contador.
            int indice = 0; //indice para referirme al arrayList.

            if (textBox1.Text.Length != 0) //si en el textBox hay contenido.
            {
                //el usuario ha introducido un dato
                foreach (Editorial ed in misEditoriales) //para cada libro dentro del arrayList.
                {
                    if (ed.getId() == Convert.ToInt32(textBox1.Text)) //si el ID coincide.
                    {
                        encontrado = true; //pongo true encontrado.
                        indice = contador; //asigno al indice el numero del contador.
                    }
                    contador++; //incremento contador.
                }
                if (encontrado) //si lo encuentro, habilito los textbox y saco los datos mediante getters
                {
                    Editorial ed = (Editorial)misEditoriales[indice];
                    textBox1.Enabled = false;
                    textBox2.Text = ed.getNombre();
                    textBox2.Enabled = true;
                    textBox3.Text = ed.getNacionalidad(); ;
                    textBox3.Enabled = true;
                    pictureBox1.Image = ed.getImagen();
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
                    case 1: //el id no está en la base de datos y por tanto es correcto
                        {
                            ArrayList provisional = new ArrayList();
                            provisional = bd.obtenerEditorialesConId(Convert.ToInt32(textBox1.Text));
                            Editorial ed;
                            ed = (Editorial)provisional[0];
                            textBox2.Text = ed.getNombre();
                            comboBox1.Text = ed.getNacionalidad();
                            pictureBox1.Image = ed.getImagen();
                            break;
                        }
                }
            }
            else
            {
                MessageBox.Show("El ID introducido no es correcto. Prueba de nuevo.");
            }
        }

        private void button2_Click(object sender, EventArgs e) //botón de aceptar
        {
            //muestro un mensaje de advertencia al que puede darle si o no
            if (MessageBox.Show("Seguro que quieres eliminarlo", "Mensaje de advertencia", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                /*
                int contador = 0; //asigno a false la variable contador.
                int indice = 0; //indice para referirme al arrayList.
                foreach (Editorial ed in misEditoriales) //para cada libro dentro del arrayList.
                {
                    if (ed.getId() == Convert.ToInt32(textBox1.Text)) //si el ID coincide.
                    {
                        indice = contador; //asigno al indice el numero del contador.
                    }
                    contador++; //incremento el contador.
                }
                misEditoriales.RemoveAt(indice); //elimino ese objeto editorial.
                MessageBox.Show("Editorial eliminada con éxito");
                this.Close();
                */

                Base_de_Datos bd = new Base_de_Datos();
                bd.eliminarEditorial(Convert.ToInt32(textBox1.Text));
                this.Close();
            }
        }
    }
}
