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
    public partial class InsertarLibro : Form
    {

        private ArrayList libros;
        private ArrayList autores;
        private ArrayList editoriales;
        private bool autorModificado = false;
        private bool IdlibroCorrecto = true;
        private bool autorEncontrado = false;
        private bool editorialEncontrado = false;

        /// <summary>
        /// constructor predeterminado de InsertarLibro.
        /// </summary>
        public InsertarLibro()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Constructor modificado de InsertarLibro.
        /// </summary>
        /// <param name="a">ArrayList que recibe del form1.</param>
        public InsertarLibro(ArrayList l, ArrayList a, ArrayList e)
        {
            InitializeComponent();
            libros = l;
            autores = a;
            editoriales = e;
        }

        private void button3_Click(object sender, EventArgs e) //botón cargar imagen
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



        private void button1_Click_1(object sender, EventArgs e) //botón aceptar
        {
            //expresión regular con la que compruebo que en el campo del id no se han introducido caracteres
            Regex regex = new Regex("^[1-9]+$");
            //si hay caracteres, la variable hasOnlyAlpha se pondrá a true
            bool soloNumeros1 = regex.IsMatch(textBox1.Text);
            bool soloNumeros2 = regex.IsMatch(textBox2.Text);
            bool soloNumeros3 = regex.IsMatch(textBox3.Text);

            if (!soloNumeros1 | !soloNumeros2 | !soloNumeros3) //si se han introducido letras en alguno de los textbox, muestro mensaje de error.
            {
                MessageBox.Show("El ID debe no puede ser una cadena de caracteres, ni estar vacío, ni ser negativo.");
            }
            else //los textbox tiene solo números
            {
                /*
                //compruebo que el ID autor existe
                foreach (Autor a in autores) //recorro el arrayList autores
                {
                    if (a.getId().ToString() == textBox2.Text.ToString()) //si el id del textbox está en el arrayList, es correcto
                    {
                        autorEncontrado = true;
                    }
                }
                if (!autorEncontrado)
                {
                    //si el id no está en el arraylist, muestro un mensaje de error y abro el formulario de insertar autor
                    MessageBox.Show("No exite nigún autor con ese ID, crea primero el autor.");
                    InsertarAutor ia = new InsertarAutor();
                    ia.ShowDialog();
                }
                
                //compruebo que el ID editorial existe
                foreach (Editorial ed in editoriales) //recorro el arraylist editoriales con el bucle foreach
                {
                    if (ed.getId().ToString() == textBox3.Text.ToString()) // si el id del textbox está en el arrayList, es correcto
                    {
                        editorialEncontrado = true;
                    }
                }
                if(!editorialEncontrado) //si no, muestro un mensaje de error y abro el formulario de insertar editorial 
                {
                    MessageBox.Show("No exite niguna editorial con ese ID, crea primero la editorial.");
                    InsertarEditorial ie = new InsertarEditorial();
                    ie.ShowDialog();

                }

                if (libros.Count == 0 & autorEncontrado & editorialEncontrado) //si es el primer libro del array, no compruebo nada
                {
                    Libro unLibro = new Libro(); //creo un objeto de la clase Autor y asigno datos al objeto mediante los setters.
                    unLibro.setIdLibro(Convert.ToInt32(textBox1.Text));
                    unLibro.setIdAutor(Convert.ToInt32(textBox2.Text));
                    unLibro.setIdEditorial(Convert.ToInt32(textBox3.Text));
                    unLibro.setTitulo(textBox4.Text);
                    unLibro.setIsbn(textBox5.Text);
                    unLibro.setPaginas(Convert.ToInt32(textBox6.Text));
                    unLibro.setImagen(pictureBox1.Image);

                    libros.Add(unLibro); //añado el objeto al arrayList.
                    MessageBox.Show("Libro insertado correctamente.");
                    this.Close();
                }
                else if (autorEncontrado & editorialEncontrado) //si los ids son correctos
                {
                    //ahora debo comprobar que el ID libro introducido no está repetido
                    foreach (Libro l in libros)
                    {
                        //si el id del textbox coincide con alguno de los del arrayList, ese id no sirve
                        if (textBox1.Text.ToString() == l.getIdLibro().ToString())
                        {
                            IdlibroCorrecto = false;
                        }

                    }
                    if (IdlibroCorrecto)
                    {
                        Libro unLibro = new Libro(); //creo un objeto de la clase Autor y asigno datos al objeto mediante los setters.
                        unLibro.setIdLibro(Convert.ToInt32(textBox1.Text));
                        unLibro.setIdAutor(Convert.ToInt32(textBox2.Text));
                        unLibro.setIdEditorial(Convert.ToInt32(textBox3.Text));
                        unLibro.setTitulo(textBox4.Text);
                        unLibro.setIsbn(textBox5.Text);
                        unLibro.setPaginas(Convert.ToInt32(textBox6.Text));
                        unLibro.setImagen(pictureBox1.Image);

                        libros.Add(unLibro); //añado el objeto al arrayList.
                        MessageBox.Show("Libro insertado correctamente.");
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("El ID del libro que has introducido no es válido. Prueba a introducir otro que no esté ya en el sistema.");
                    }
                }
                */

                Base_de_Datos bd = new Base_de_Datos();
                //la funcion existe id autor nos devolverá un int, que se guardará en numero
                int numeroLibro = bd.existeIdLibro(Convert.ToInt32(textBox1.Text));
                int numeroAutor = bd.existe_id_Autor(Convert.ToInt32(textBox2.Text));
                int numeroEditorial = bd.existeIdEditorial(Convert.ToInt32(textBox3.Text));

                // -1 no esta en la base
                // 1 si esta
                // 0 no has puesto na
                if(numeroLibro == -1 & numeroAutor == 1 & numeroEditorial == 1)
                {
                    //todos los id son correctos y podemos insertar el libro
                    Libro unLibro = new Libro(); //creo un objeto de la clase Autor y asigno datos al objeto mediante los setters.
                    unLibro.setIdLibro(Convert.ToInt32(textBox1.Text));
                    unLibro.setIdAutor(Convert.ToInt32(textBox2.Text));
                    unLibro.setIdEditorial(Convert.ToInt32(textBox3.Text));
                    unLibro.setTitulo(textBox4.Text);
                    unLibro.setIsbn(textBox5.Text);
                    unLibro.setPaginas(Convert.ToInt32(textBox6.Text));

                    //inserto el autor en la BBDD pero primero compruebo si se ha introducido foto o no
                    if (pictureBox1.Image == null) //si no se ha introducido foto
                    {
                        //le mando el objeto autor y lo inserto en la BBDD
                        if (bd.InsertarLibro(unLibro)) //devuelve true
                        {
                            MessageBox.Show("Libro insertado con éxito.");
                            this.Close();
                        }
                        else //devuelve false
                        {
                            MessageBox.Show("Error al insertar el libro en la BBDD.");
                        }
                    }
                    else //si hay foto
                    {
                        //le mando el objeto autor y la foto e inserto ambos en la BBDD
                        if (bd.InsertarLibro(unLibro, pictureBox1)) //devuelve true
                        {
                            MessageBox.Show("Libro insertado con éxito.");
                            this.Close();
                        }
                        else //devuelve false
                        {
                            MessageBox.Show("Error al insertar el Libro en la BBDD.");
                        }
                    }
                    this.Close();
                }
                else if(numeroLibro == 1)
                {
                    //ya existe un libro con ese id
                    MessageBox.Show("El ID que has introducido ya está siendo utilizado por otro libro. Prueba con otro.");
                }
                else if(numeroAutor == -1)
                {
                    //no hay ningun autor con ese id y se debe crear primero
                    if(MessageBox.Show("No existe ningún autor con ese ID, ¿quieres crear el autor?", "Mensaje de advertencia", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        //lanzo el formulario de insertar autor
                        InsertarAutor ia = new InsertarAutor();
                        ia.ShowDialog();
                    }
                }
                else if(numeroEditorial == -1)
                {
                    //no hay ninguna editorial con ese id y se debe crear primero
                    if (MessageBox.Show("No existe ninguna editorial con ese ID, ¿quieres crear la editorial?", "Mensaje de advertencia", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        //lanzo el formulario de insertar autor
                        InsertarEditorial ie = new InsertarEditorial();
                        ie.ShowDialog();
                    }
                }
                else if(numeroLibro == 0 | numeroAutor == 0 | numeroEditorial == 0)
                {
                    //alguno de los campos ID esta vacio
                    MessageBox.Show("Primero debes introducir todos los ID.");
                }
                else
                {
                    MessageBox.Show("Error.");
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
