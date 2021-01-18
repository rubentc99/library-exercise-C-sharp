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
    public partial class ModificarEditorial : Form
    {

        private bool modificado;

        /// <summary>
        /// constructor por defecto.
        /// </summary>
        public ModificarEditorial()
        {
            InitializeComponent();
            modificado = false;
        }
        
        private void ModificarEditorial_Load(object sender, EventArgs e) //al cargar el form
        {
            //solo dejo habilitado el textbox del ID de por tanto deshabilito los demás
            textBox2.Enabled = false;
            comboBox1.Enabled = false;
            button1.Enabled = false;
        }

        private void button1_Click(object sender, EventArgs e) //boton de cargar imagen
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
            if (textBox1.Text.Length != 0) //hay datos escritos en el textbox
            {
                foreach(Editorial ed in listaEditoriales)
                {
                    if (ed.getId() == Convert.ToInt32(textBox1.Text)) 
                    {
                        indice = contador; //asigno a la variable índice la posición del contador
                        encontrado = true; //pongo la variable encontrado a true puesto que este es el que buscaba
                    }
                    else
                    {
                        contador++; //aumento el contador
                    }
                }
                if (encontrado)
                {
                    //quito el botón de buscar
                    button4.Visible = false;

                    //habilito los demás textbox
                    textBox1.Enabled = false;
                    textBox2.Enabled = true;
                    textBox3.Enabled = true;
                    button1.Enabled = true;

                    //muestro los datos con los getters
                    Editorial edi;
                    edi = (Editorial)listaEditoriales[indice];
                    textBox2.Text = edi.getNombre();
                    textBox3.Text = edi.getNacionalidad();
                    pictureBox1.Image = edi.getImagen();
                }
                else //no se ha encontrado el id
                {
                    MessageBox.Show("No se ha encontrado ninguna editorial con ese ID");
                }
            }
            else //no se ha introducido ningun id
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
                    case 1: //el id es el correcto
                        {
                            ArrayList provisional = new ArrayList();
                            provisional = bd.obtenerEditorialesConId(Convert.ToInt32(textBox1.Text));
                            Editorial ed;
                            ed = (Editorial)provisional[0];
                            textBox1.Enabled = false;
                            textBox2.Enabled = true;
                            comboBox1.Enabled = true;
                            button1.Enabled = true;
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

        private void button2_Click(object sender, EventArgs e) //boton de aceptar
        {
            Base_de_Datos bd = new Base_de_Datos();
            bd.modificarEditorial(Convert.ToInt32(textBox1.Text), textBox2.Text, comboBox1.Text, pictureBox1);
            MessageBox.Show("Editorial modificada con éxito.");
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e) //boton de cancelar
        {
            this.Close();
        }
    }
}
