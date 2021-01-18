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
    public partial class InsertarEditorial : Form
    {

        /// <summary>
        /// Constructor predeterminado de InsertarAutor.
        /// </summary>
        public InsertarEditorial()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e) //botón cargar imagen
        {
            openFileDialog1.FileName = ""; //hago que el nombre por defecto con el que se va a guardar el archivo sea un campo vacio
            //aplico un filtro para los archivos que el usuario puede seleccionar
            openFileDialog1.Filter = "Archivos JPG (*.JPG)|*.jpg| Archivos PNG (*.png)|*.png| Archivos JPEG(*.jpeg)|.*jpeg";
            openFileDialog1.ShowDialog(); //se abre la ventana de selección de archivo
            if (openFileDialog1.FileName.Length != 0)
            {
                pictureBox1.ImageLocation = openFileDialog1.FileName; // le asigno al picturebox la imagen seleccionada
            }
        }

        private void button2_Click(object sender, EventArgs e) //botón de aceptar
        {
            //expresión regular con la que compruebo que en el campo del id no se han introducido caracteres
            Regex regex = new Regex("^[1-9]+$");
            //si hay caracteres, la variable hasOnlyAlpha se pondrá a true
            bool soloNumeros = regex.IsMatch(textBox1.Text);

            if (soloNumeros == true)
            {
                Base_de_Datos bd = new Base_de_Datos();
                //la funcion existe id autor nos devolverá un int, que se guardará en numero
                int numero = bd.existeIdEditorial(Convert.ToInt32(textBox1.Text));

                switch (numero)
                {
                    case -1:
                        //el id introducido no coincide con ninguno de la base de datos, por lo que 
                        //podemos crear un nuevo autor con ese ID sin problemas
                        {
                            Editorial unaEditoral = new Editorial(); //creo un objeto de la clase Autor y asigno datos al objeto mediante los setters.
                            unaEditoral.setId(Convert.ToInt32(textBox1.Text));
                            unaEditoral.setNombre(textBox2.Text);
                            unaEditoral.setNacionalidad(comboBox1.Text);
                            unaEditoral.setImagen(pictureBox1.Image);

                            //inserto el autor en la BBDD pero primero compruebo si se ha introducido foto o no
                            if (pictureBox1.Image == null) //si no se ha introducido foto
                            {
                                //le mando el objeto autor y lo inserto en la BBDD
                                if (bd.InsertarEditorial(unaEditoral)) //devuelve true
                                {
                                    MessageBox.Show("Editorial insertada con éxito.");
                                    this.Close();
                                }
                                else //devuelve false
                                {
                                    MessageBox.Show("Error al insertar la editorial en la BBDD.");
                                }
                            }
                            else //si hay foto
                            {
                                //le mando el objeto autor y la foto e inserto ambos en la BBDD
                                if (bd.InsertarEditorial(unaEditoral, pictureBox1)) //devuelve true
                                {
                                    MessageBox.Show("Editorial insertada con éxito.");
                                    this.Close();
                                }
                                else //devuelve false
                                {
                                    MessageBox.Show("Error al insertar la editorial en la BBDD.");
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
                            MessageBox.Show("El ID que has introducido ya está siendo utilizado por otra editorial. Prueba con otro.");
                            break;
                        }
                }
            }
            else
            {
                MessageBox.Show("En el campo ID solo se pueden introducir números.");
            }

            /*
            if (editoriales.Count == 0 & soloNumeros==true) //si es el primer campo del arraylist
            {
                //creo un objeto de la clase editorial y le asigno los datos de los textbox mediante los setters
                Editorial unaEditorial = new Editorial();
                unaEditorial.setId(Convert.ToInt32(textBox1.Text));
                unaEditorial.setNombre(textBox2.Text);
                unaEditorial.setNacionalidad(textBox3.Text);
                unaEditorial.setImagen(pictureBox1.Image);

                editoriales.Add(unaEditorial); //añado el objeto al arraylist.
                MessageBox.Show("Editorial añadida correctamente.");
                this.Close();
            }
            else //si no es el primer objeto del array, compruebo que el id introducido no está repetido
            {
                foreach(Editorial ed in editoriales)
                {
                    if (textBox1.Text.ToString() == ed.getId().ToString()) //si el id introducido coincide con alguno de los id del arraylist
                    {
                        idCorrecto = false;
                    }
                }
                if (idCorrecto == true & soloNumeros==true)
                {
                    //creo un objeto de la clase editorial y le asigno los datos de los textbox mediante los setters
                    Editorial unaEditorial = new Editorial();
                    unaEditorial.setId(Convert.ToInt32(textBox1.Text));
                    unaEditorial.setNombre(textBox2.Text);
                    unaEditorial.setNacionalidad(textBox3.Text);
                    unaEditorial.setImagen(pictureBox1.Image);

                    editoriales.Add(unaEditorial); //añado el objeto al arraylist.
                    MessageBox.Show("Editorial añadida correctamente.");
                    this.Close();
                }
                else if(idCorrecto == false)
                {
                    MessageBox.Show("El ID que has introducido coincide con uno introducido previamente. Prueba con otro.");
                    idCorrecto = true; //reseteo la variable para la proxima vez que se compruebe
                }
            }
            */
        }
        private void button3_Click(object sender, EventArgs e) //boton de cancelar
        {
            this.Close();
        }
    }
}
