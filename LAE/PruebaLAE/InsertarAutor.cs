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
    public partial class InsertarAutor : Form
    {

        private ArrayList losAutores;

        /// <summary>
        /// Constructor predeterminado de InsertarAutor
        /// </summary>
        public InsertarAutor()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Constructor modificado de InsertarAutor
        /// </summary>
        /// <param name="a">ArrayList que recibe del form1</param>
        public InsertarAutor(ArrayList a)
        {
            InitializeComponent();
            losAutores = a;
        }

        private void button1_Click(object sender, EventArgs e) //botón cargar imagen
        {
            
            openFileDialog1.FileName = "";
            openFileDialog1.Filter = "Archivos JPG (*.JPG)|*.jpg| Archivos PNG (*.png)|*.png| Archivos JPEG(*.jpeg)|.*jpeg| Todos los archivos (*.*)|*.*";
            openFileDialog1.ShowDialog(); //se abre la ventana de selección de archivo
            if (openFileDialog1.FileName.Length != 0) //Significa que el usuario ha introducido un nombre de fichero.
            {
                pictureBox1.ImageLocation = openFileDialog1.FileName; //le asigno al picturebox la imagen seleccionada
            }
        }

        private void button3_Click(object sender, EventArgs e) //botón de cancelar
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e) //botón aceptar
        {
            //expresión regular con la que compruebo que en el campo del id no se han introducido caracteres
            Regex regex = new Regex("^[1-9]+$");
            //si hay caracteres, la variable hasOnlyAlpha se pondrá a true
            bool soloNumeros = regex.IsMatch(textBox1.Text);
            
            if (soloNumeros == true)
            {
                Base_de_Datos bd = new Base_de_Datos();
                //la funcion existe id autor nos devolverá un int, que se guardará en numero
                int numero = bd.existe_id_Autor(Convert.ToInt32(textBox1.Text));

                switch (numero)
                {
                    case -1:
                        //el id introducido no coincide con ninguno de la base de datos, por lo que 
                        //podemos crear un nuevo autor con ese ID sin problemas
                        {
                            Autor unAutor = new Autor(); //creo un objeto de la clase Autor y asigno datos al objeto mediante los setters.
                            unAutor.setId(Convert.ToInt32(textBox1.Text));
                            unAutor.setNombre(textBox2.Text);
                            unAutor.setApellidos(textBox3.Text);
                            unAutor.setNacionalidad(comboBox1.Text);
                            unAutor.setFechaNacimiento(dateTimePicker1.Value.ToString());
                            unAutor.setImagen(pictureBox1.Image);

                            //inserto el autor en la BBDD pero primero compruebo si se ha introducido foto o no
                            if (pictureBox1.Image == null) //si no se ha introducido foto
                            {
                                //le mando el objeto autor y lo inserto en la BBDD
                                if (bd.Insertar_Autor(unAutor)) //devuelve true
                                {
                                    MessageBox.Show("Autor insertado con éxito.");
                                    this.Close();
                                }
                                else //devuelve false
                                {
                                    MessageBox.Show("Error al insertar el autor en la BBDD.");
                                }
                            }
                            else //si hay foto
                            {
                                //le mando el objeto autor y la foto e inserto ambos en la BBDD
                                if (bd.Insertar_Autor(unAutor, pictureBox1)) //devuelve true
                                {
                                    MessageBox.Show("Autor insertado con éxito.");
                                    this.Close();
                                }
                                else //devuelve false
                                {
                                    MessageBox.Show("Error al insertar el autor en la BBDD.");
                                }
                            }
                            this.Close();
                            break;
                        }
                    case 0: //el campo está vacío
                        {
                            MessageBox.Show("Primero debes introducir un ID.");
                            break;
                        }
                    case 1: //el id ya está en la BBDD
                        {
                            MessageBox.Show("El ID que has introducido ya está siendo utilizado por otro autor. Prueba con otro.");
                            break;
                        }
                }
            }
            else
            {
                MessageBox.Show("En el campo ID solo se pueden introducir números.");
            }
        }
    }
}
