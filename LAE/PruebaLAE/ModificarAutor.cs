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
    public partial class ModificarAutor : Form
    {

        /// <summary>
        /// constructor por defecto de ModificarAutor
        /// </summary>
        public ModificarAutor()
        {
            InitializeComponent();
        }
        
        private void ModificarAutor_Load(object sender, EventArgs e) //hago que por defecto, los campos aparezcan deshabilitados
        {
            textBox2.Enabled = false;
            textBox3.Enabled = false;
            comboBox1.Enabled = false;
            dateTimePicker1.Enabled = false;
            button1.Enabled = false;
        }

        private void button1_Click(object sender, EventArgs e) //boton de añadir imagen
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
                foreach(Autor a in listaAutores)
                {
                    if(a.getId()==Convert.ToInt32(textBox1.Text))
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
                    dateTimePicker1.Enabled = true;

                    Autor a;
                    a = (Autor)listaAutores[indice];

                    textBox2.Text = a.getNombre();
                    textBox3.Text = a.getApellidos();
                    textBox4.Text = a.getNacionalidad();
                    dateTimePicker1.Value = Convert.ToDateTime(a.getFechaNacimiento());
                    pictureBox1.Image = a.getImage();
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
                    case 1: //el id es el correcto
                        {
                            ArrayList provisional = new ArrayList();
                            provisional = bd.obtener_Autores_con_ID(Convert.ToInt32(textBox1.Text));
                            Autor a;
                            a = (Autor)provisional[0];
                            textBox1.Enabled = false;
                            textBox2.Enabled = true;
                            textBox3.Enabled = true;
                            comboBox1.Enabled = true;
                            dateTimePicker1.Enabled = true;
                            button1.Enabled = true;
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

        private void button2_Click(object sender, EventArgs e) //botón de aceptar
        {
            /*
            if (!modificado) //si no se ha modificado nada, cierro el formulario.
            {
                this.Close();
            }
            else //si se ha modificado algo:
            {
                //busco el elemento y lo elimino.
                int contador = 0;
                int indice = 0;
                foreach (Autor a in listaAutores)
                {
                    if (a.getId() == Convert.ToInt32(textBox1.Text)) //si el id del objeto a coincide con lo introducido en el textBox1.
                    {
                        indice = contador; //asigno el valor al índice.
                    }
                    else
                    {
                        contador++; //incremento el contador
                    }
                }
                listaAutores.RemoveAt(indice); //elimino ese autor

                //creo un autor nuevo y mediante el constructor de la clase autor le paso los datos de los campos correspondientes
                Autor unAutorModificado = new Autor(Convert.ToInt32(textBox1.Text), textBox2.Text, textBox3.Text, textBox4.Text, dateTimePicker1.Value.ToString(), pictureBox1.Image);
                
                //introduzco ese objeto autor en el arrayList, concretamente en el mismo lugar donde estaba el que he borrado.
                listaAutores.Insert(indice, unAutorModificado);
                
                MessageBox.Show("Autor modificado correctamente!");
                this.Close();
            }
            */

            Base_de_Datos bd = new Base_de_Datos();
            bd.modificar_Autor(Convert.ToInt32(textBox1.Text), textBox2.Text, textBox3.Text, comboBox1.Text, dateTimePicker1.Value.ToString(), pictureBox1);
            MessageBox.Show("Autor modificado con éxito.");
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e) //boton de cancelar
        {
            this.Close();
        }
    }
}
