using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PruebaLAE
{
    class Base_de_Datos
    {
        private IDbConnection conexion = null;
        private string cadena_de_conexion = "server=(local);" + "Initial Catalog=LAE;" +
            "Integrated Security=true";
        private IDbCommand cmd;
        private IDataReader mis_datos;
        private Boolean conectado = false;

        public Base_de_Datos() //contructor
        {
            //cada vez que se cree un objeto, se establecerá la conexión
            conexion = new SqlConnection(cadena_de_conexion);
        }

        /// <summary>
        /// abre conexion con la base de datos
        /// </summary>
        /// <returns>devuelve true o false depende de la conexion</returns>

        private bool abrir_Conexion()
        {
            Boolean f = false;
            if (!conectado)
            {
                //conectamos
                conexion.Open();
                conectado = true;
                f = true;
            }
            return f; //devuelve si me he conectado o no
        }

        /// <summary>
        /// cierro conexion con la base de datos
        /// </summary>
        private void cerrar_Conexion()
        {
            if (conectado)
            {
                conexion.Close();
                conectado = false;
            }
        }


        public bool Insertar_Autor(Autor a) //autor sin foto
        {
            bool valor = false;
            abrir_Conexion();
            cmd = conexion.CreateCommand();
            cmd.CommandText = "Insert into Autores(idAutor,nombreAutor,apellidosAutor,nacionalidadAutor,fechaNacimientoAutor) values (@id,@nombre,@apellidos,@nacionalidad,@fechaNacimiento)";
            cmd.Parameters.Add(new SqlParameter("@id", a.getId()));
            cmd.Parameters.Add(new SqlParameter("@nombre", a.getNombre()));
            cmd.Parameters.Add(new SqlParameter("@apellidos", a.getApellidos()));
            cmd.Parameters.Add(new SqlParameter("@nacionalidad", a.getNacionalidad()));
            cmd.Parameters.Add(new SqlParameter("@fechaNacimiento", Convert.ToDateTime(a.getFechaNacimiento())));
            int resultado = cmd.ExecuteNonQuery(); //se usa nonquery porque es para hacer un insert

            if (resultado != 0)
            {
                valor = true;
            }
            else
            {
                valor = false;
            }
            cerrar_Conexion();
            return valor;
        }

        public bool Insertar_Autor(Autor a, PictureBox pb) //autor con foto
        {
            bool valor = false;
            abrir_Conexion();
            cmd = conexion.CreateCommand();
            cmd.CommandText = "Insert into Autores(idAutor,nombreAutor,apellidosAutor,nacionalidadAutor" +
                ",fechaNacimientoAutor,imagen) values (@id,@nombre,@apellidos,@nacionalidad,@fecha,@img)";
            cmd.Parameters.Add(new SqlParameter("@id", a.getId()));
            cmd.Parameters.Add(new SqlParameter("@nombre", a.getNombre()));
            cmd.Parameters.Add(new SqlParameter("@apellidos", a.getApellidos()));
            cmd.Parameters.Add(new SqlParameter("@nacionalidad", a.getNacionalidad()));
            cmd.Parameters.Add(new SqlParameter("@fecha", Convert.ToDateTime(a.getFechaNacimiento())));
            cmd.Parameters.Add(new SqlParameter("@img", convertir_Imagen(pb.ImageLocation)));

            int resultado = cmd.ExecuteNonQuery();
            if (resultado != 0)
            {
                valor = true;
            }
            else
            {
                valor = false;
            }
            cerrar_Conexion();
            return valor;
        }

        public byte[] convertir_Imagen(string filefoto)
        {
            MemoryStream ms = new MemoryStream();
            FileStream fs = new FileStream(filefoto, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
            ms.SetLength(fs.Length);
            fs.Read(ms.GetBuffer(), 0, (int)fs.Length);

            byte[] arrIng = ms.GetBuffer();
            ms.Flush();
            fs.Close();

            return arrIng;
        }


        //////////////////////////////////////////////////////////////////
        //Autor
        //////////////////////////////////////////////////////////////////

        public int existe_id_Autor(int id)
        {
            int numero = 0;
            abrir_Conexion();
            cmd = conexion.CreateCommand();
            cmd.CommandText = "Select * from Autores where idAutor=" + id;
            mis_datos = cmd.ExecuteReader();
            if (mis_datos.Read())
            {
                if (Convert.ToInt32(mis_datos[0]) == id)
                {
                    numero = 1;
                }
                else
                {
                    numero = 0;
                }
                mis_datos.Close();
            }
            else
            {
                numero = -1;
            }
            cerrar_Conexion();
            return numero;
        }

        /// <summary>
        /// Funcion a la que le paso un id de un autor, y me devuelve los datos de ese id
        /// </summary>
        /// <param name="id">ID del autor escrito en el textbox por el usuario</param>
        /// <returns>Devuelve los datos del select donde el id de autor es el que manda el usuario</returns>
        public ArrayList obtener_Autores_con_ID(int id)
        {
            ArrayList autores_bd = new ArrayList();
            Autor a;
            Image imagen;
            abrir_Conexion();
            cmd = conexion.CreateCommand();
            cmd.CommandText = "Select * from Autores where idAutor="+id;
            mis_datos = cmd.ExecuteReader();
            while (mis_datos.Read())
            {
                if (mis_datos[5] == DBNull.Value)
                {
                    imagen = null;
                }
                else
                {
                    imagen = extraer_Imagen((byte[])mis_datos[5]);
                }
                a = new Autor(Convert.ToInt32(mis_datos[0]), mis_datos[1].ToString(), mis_datos[2].ToString(), mis_datos[3].ToString(),
                    mis_datos[4].ToString(), imagen);
                autores_bd.Add(a);
            }
            cerrar_Conexion();
            return autores_bd;
        }

        public ArrayList obtener_Autores()
        {
            ArrayList autores_bd = new ArrayList();
            Autor a;
            Image imagen;
            abrir_Conexion();
            cmd = conexion.CreateCommand();
            cmd.CommandText = "Select * from Autores";
            mis_datos = cmd.ExecuteReader();
            while (mis_datos.Read())
            {
                if (mis_datos[5] == DBNull.Value)
                {
                    imagen = null;
                }
                else
                {
                    imagen = extraer_Imagen((byte[])mis_datos[5]);
                }
                a = new Autor(Convert.ToInt32(mis_datos[0]), mis_datos[1].ToString(), mis_datos[2].ToString(), mis_datos[3].ToString(),
                    mis_datos[4].ToString(), imagen);
                autores_bd.Add(a);
            }
            cerrar_Conexion();
            return autores_bd;
        }

        public Image extraer_Imagen(byte[] laimagen)
        {
            Image img = null;
            try
            {
                byte[] arrImg = (byte[])laimagen;
                MemoryStream ms = new MemoryStream(laimagen);
                img = Image.FromStream(ms);
                ms.Close();
            }
            catch (Exception ex)
            {
                ;
            }
            return img;
        }

        public bool modificar_Autor(int id, string nombre, string apellidos, string nacionalidad, string fecha, PictureBox pb)
        {
            bool modificado = false;
            abrir_Conexion();
            cmd = conexion.CreateCommand();
            if (pb.Image != null)
            {
                //Si hay imagen.
                cmd.CommandText = "update Autores set nombreAutor=@nombre, apellidosAutor=@apellidos, nacionalidadAutor=@nacionalidad, fechaNacimientoAutor=@fecha, imagen=@imagen where idAutor=" + id;
                cmd.Parameters.Add(new SqlParameter("@nombre", nombre));
                cmd.Parameters.Add(new SqlParameter("@apellidos", apellidos));
                cmd.Parameters.Add(new SqlParameter("@nacionalidad", nacionalidad));
                cmd.Parameters.Add(new SqlParameter("@fecha", fecha));
                //Referenciamos a la imagen en memoria para poder almacenarla en bd
                cmd.Parameters.Add(new SqlParameter("@imagen", convertir_Imagen(pb.ImageLocation)));
                modificado = true;
                cmd.ExecuteNonQuery();
            }
            else
            {
                //No hay imagen.
                cmd.CommandText = "update Autores set nombreAutor=@nombre, apellidosAutor=@apellidos, nacionalidadAutor=@nacionalidad," +
                    " fechaNacimientoAutor=@fecha where idAutor=" + id;
                cmd.Parameters.Add(new SqlParameter("@nombre", nombre));
                cmd.Parameters.Add(new SqlParameter("@apellidos", apellidos));
                cmd.Parameters.Add(new SqlParameter("@nacionalidad", nacionalidad));
                cmd.Parameters.Add(new SqlParameter("@fecha", fecha));
                modificado = true;
                cmd.ExecuteNonQuery();
            }
            cerrar_Conexion();
            return modificado;
        }

        public bool eliminar_autor(int id)
        {
            bool eliminado = false;
            abrir_Conexion();
            cmd = conexion.CreateCommand();
            cmd.CommandText = "delete from Autores where idAutor=" + id;
            int resultado = cmd.ExecuteNonQuery();
            if (resultado == 0)
            {
                //No se ha eliminado ningun autor.
            }
            else
            {
                // Como minimo se ha eliminado una fila.
            }
            cerrar_Conexion();
            return eliminado;
        }

        //////////////////////////////////////////////////////////////////
        //Editorial
        //////////////////////////////////////////////////////////////////

        public int existeIdEditorial(int id)
        {
            int numero = 0;
            abrir_Conexion();
            cmd = conexion.CreateCommand();
            cmd.CommandText = "Select * from Editoriales where idEditorial=" + id;
            mis_datos = cmd.ExecuteReader();
            if (mis_datos.Read())
            {
                if (Convert.ToInt32(mis_datos[0]) == id)
                {
                    numero = 1;
                }
                else
                {
                    numero = 0;
                }
                mis_datos.Close();
            }
            else
            {
                numero = -1;
            }
            cerrar_Conexion();
            return numero;
        }

        public bool InsertarEditorial(Editorial ed) //Editorial sin foto
        {
            bool valor = false;
            abrir_Conexion();
            cmd = conexion.CreateCommand();
            cmd.CommandText = "Insert into Editoriales(idEditorial,nombreEditorial,nacionalidadEditorial) values (@id,@nombre,@nacionalidad)";
            cmd.Parameters.Add(new SqlParameter("@id", ed.getId()));
            cmd.Parameters.Add(new SqlParameter("@nombre", ed.getNombre()));
            cmd.Parameters.Add(new SqlParameter("@nacionalidad", ed.getNacionalidad()));

            int resultado = cmd.ExecuteNonQuery(); //se usa nonquery porque es para hacer un insert
            if (resultado != 0)
            {
                valor = true;
            }
            else
            {
                valor = false;
            }
            cerrar_Conexion();
            return valor;
        }

        public bool InsertarEditorial(Editorial ed, PictureBox pb) //editorial con foto
        {
            bool valor = false;
            abrir_Conexion();
            cmd = conexion.CreateCommand();
            cmd.CommandText = "Insert into Editoriales(idEditorial,nombreEditorial,nacionalidadEditorial,imagen) values (@id,@nombre,@nacionalidad,@imagen)";
            cmd.Parameters.Add(new SqlParameter("@id", ed.getId()));
            cmd.Parameters.Add(new SqlParameter("@nombre", ed.getNombre()));
            cmd.Parameters.Add(new SqlParameter("@nacionalidad", ed.getNacionalidad()));
            cmd.Parameters.Add(new SqlParameter("@imagen", convertir_Imagen(pb.ImageLocation)));

            int resultado = cmd.ExecuteNonQuery();
            if (resultado != 0)
            {
                valor = true;
            }
            else
            {
                valor = false;
            }
            cerrar_Conexion();
            return valor;
        }

        public ArrayList obtenerEditoriales()
        {
            ArrayList editorialesBD = new ArrayList();
            Editorial ed;
            Image imagen;
            abrir_Conexion();
            cmd = conexion.CreateCommand();
            cmd.CommandText = "Select * from Editoriales";
            mis_datos = cmd.ExecuteReader();
            while (mis_datos.Read())
            {
                if (mis_datos[3] == DBNull.Value)
                {
                    imagen = null;
                }
                else
                {
                    imagen = extraer_Imagen((byte[])mis_datos[3]);
                }
                ed = new Editorial(Convert.ToInt32(mis_datos[0]), mis_datos[1].ToString(), mis_datos[2].ToString(), imagen);
                editorialesBD.Add(ed);
            }
            cerrar_Conexion();
            return editorialesBD;
        }

        public ArrayList obtenerEditorialesConId(int id)
        {
            ArrayList editorialesBD = new ArrayList();
            Editorial ed;
            Image imagen;
            abrir_Conexion();
            cmd = conexion.CreateCommand();
            cmd.CommandText = "Select * from Editoriales where idEditorial=" + id;
            mis_datos = cmd.ExecuteReader();
            while (mis_datos.Read())
            {
                if (mis_datos[3] == DBNull.Value)
                {
                    imagen = null;
                }
                else
                {
                    imagen = extraer_Imagen((byte[])mis_datos[3]);
                }
                ed = new Editorial(Convert.ToInt32(mis_datos[0]), mis_datos[1].ToString(), mis_datos[2].ToString(), imagen);
                editorialesBD.Add(ed);
            }
            cerrar_Conexion();
            return editorialesBD;
        }

        public bool modificarEditorial(int id, string nombre, string nacionalidad, PictureBox pb)
        {
            bool modificado = false;
            abrir_Conexion();
            cmd = conexion.CreateCommand();
            if (pb.Image != null)
            {
                //Si hay imagen.
                cmd.CommandText = "update Editoriales set nombreEditorial=@nombre, nacionalidadEditorial=@nacionalidad, imagen=@imagen where idEditorial=" + id;
                cmd.Parameters.Add(new SqlParameter("@nombre", nombre));
                cmd.Parameters.Add(new SqlParameter("@nacionalidad", nacionalidad));
                //Referenciamos a la imagen en memoria para poder almacenarla en bd
                cmd.Parameters.Add(new SqlParameter("@imagen", convertir_Imagen(pb.ImageLocation)));
                modificado = true;
                cmd.ExecuteNonQuery();
            }
            else
            {
                //No hay imagen.
                cmd.CommandText = "update Editoriales set nombreEditorial=@nombre, nacionalidadEditorial=@nacionalidad where idEditorial=" + id;
                cmd.Parameters.Add(new SqlParameter("@nombre", nombre));
                cmd.Parameters.Add(new SqlParameter("@nacionalidad", nacionalidad));
                //Referenciamos a la imagen en memoria para poder almacenarla en bd
                modificado = true;
                cmd.ExecuteNonQuery();
            }
            cerrar_Conexion();
            return modificado;
        }

        public bool eliminarEditorial(int id)
        {
            bool eliminado = false;
            abrir_Conexion();
            cmd = conexion.CreateCommand();
            cmd.CommandText = "delete from Editoriales where idEditorial=" + id;
            int resultado = cmd.ExecuteNonQuery();
            if (resultado == 0)
            {
                //No se ha eliminado ninguna editorial.
            }
            else
            {
                // Como minimo se ha eliminado una fila.
            }
            cerrar_Conexion();
            return eliminado;
        }

        //////////////////////////////////////////////////////////////////
        //Libro
        //////////////////////////////////////////////////////////////////

        public int existeIdLibro(int id)
        {
            int numero = 0;
            abrir_Conexion();
            cmd = conexion.CreateCommand();
            cmd.CommandText = "Select * from Libros where idEditorial=" + id;
            mis_datos = cmd.ExecuteReader();
            if (mis_datos.Read())
            {
                if (Convert.ToInt32(mis_datos[0]) == id)
                {
                    numero = 1;
                }
                else
                {
                    numero = 0;
                }
                mis_datos.Close();
            }
            else
            {
                numero = -1;
            }
            cerrar_Conexion();
            return numero;
        }

        public bool InsertarLibro(Libro l) //libro sin foto
        {
            bool valor = false;
            abrir_Conexion();
            cmd = conexion.CreateCommand();
            cmd.CommandText = "Insert into Libros(idLibro,idAutor,idEditorial,tituloLibro,isbnLibro,paginasLibro) values (@idLibro,@idAutor,@idEditorial,@tituloLibro,@isbnLibro,@paginasLibro)";
            cmd.Parameters.Add(new SqlParameter("@idLibro", l.getIdLibro()));
            cmd.Parameters.Add(new SqlParameter("@idAutor", l.getIdAutor()));
            cmd.Parameters.Add(new SqlParameter("@idEditorial", l.getIdEditorial()));
            cmd.Parameters.Add(new SqlParameter("@tituloLibro", l.getTitulo()));
            cmd.Parameters.Add(new SqlParameter("@isbnLibro", l.getIsbn()));
            cmd.Parameters.Add(new SqlParameter("@paginasLibro", l.getPaginas()));

            int resultado = cmd.ExecuteNonQuery(); //se usa nonquery porque es para hacer un insert
            if (resultado != 0)
            {
                valor = true;
            }
            else
            {
                valor = false;
            }
            cerrar_Conexion();
            return valor;
        }

        public bool InsertarLibro(Libro l, PictureBox pb) //libro con foto
        {
            bool valor = false;
            abrir_Conexion();
            cmd = conexion.CreateCommand();
            cmd.CommandText = "Insert into Libros(idLibro,idAutor,idEditorial,tituloLibro,isbnLibro,paginasLibro,imagen) values (@idLibro,@idAutor,@idEditorial,@tituloLibro,@isbnLibro,@paginasLibro,@imagen)";
            cmd.Parameters.Add(new SqlParameter("@idLibro", l.getIdLibro()));
            cmd.Parameters.Add(new SqlParameter("@idAutor", l.getIdAutor()));
            cmd.Parameters.Add(new SqlParameter("@idEditorial", l.getIdEditorial()));
            cmd.Parameters.Add(new SqlParameter("@tituloLibro", l.getTitulo()));
            cmd.Parameters.Add(new SqlParameter("@isbnLibro", l.getIsbn()));
            cmd.Parameters.Add(new SqlParameter("@paginasLibro", l.getPaginas()));
            cmd.Parameters.Add(new SqlParameter("@imagen", convertir_Imagen(pb.ImageLocation)));

            int resultado = cmd.ExecuteNonQuery();
            if (resultado != 0)
            {
                valor = true;
            }
            else
            {
                valor = false;
            }
            cerrar_Conexion();
            return valor;
        }

        public ArrayList obtenerLibros()
        {
            ArrayList libroBD = new ArrayList();
            Libro l;
            Image imagen;
            abrir_Conexion();
            cmd = conexion.CreateCommand();
            cmd.CommandText = "Select * from Libros";
            mis_datos = cmd.ExecuteReader();
            while (mis_datos.Read())
            {
                if (mis_datos[6] == DBNull.Value)
                {
                    imagen = null;
                }
                else
                {
                    imagen = extraer_Imagen((byte[])mis_datos[6]);
                }
                l = new Libro(Convert.ToInt32(mis_datos[0]), Convert.ToInt32(mis_datos[1]), Convert.ToInt32(mis_datos[2]), mis_datos[3].ToString(), mis_datos[4].ToString(), Convert.ToInt32(mis_datos[5]), imagen); ;
                libroBD.Add(l);
            }
            cerrar_Conexion();
            return libroBD;
        }

        public ArrayList obtenerLibrosConId(int id)
        {
            ArrayList librosBD = new ArrayList();
            Libro l;
            Image imagen;
            abrir_Conexion();
            cmd = conexion.CreateCommand();
            cmd.CommandText = "Select * from Libros where idLibro=" + id;
            mis_datos = cmd.ExecuteReader();
            while (mis_datos.Read())
            {
                if (mis_datos[6] == DBNull.Value)
                {
                    imagen = null;
                }
                else
                {
                    imagen = extraer_Imagen((byte[])mis_datos[6]);
                }
                l = new Libro(Convert.ToInt32(mis_datos[0]), Convert.ToInt32(mis_datos[1]), Convert.ToInt32(mis_datos[2]), mis_datos[3].ToString(), mis_datos[4].ToString(), Convert.ToInt32(mis_datos[5]), imagen); ;
                librosBD.Add(l);
            }
            cerrar_Conexion();
            return librosBD;
        }

        public bool modificarLibro(int id, int idAutor, int idEditorial, string titulo, string isbn, int paginas, PictureBox pb)
        {
            bool modificado = false;
            abrir_Conexion();
            cmd = conexion.CreateCommand();
            if (pb.Image != null)
            {
                //Si hay imagen.
                cmd.CommandText = "update Libros set idAutor=@idAutor, idEditorial=@idEditorial, tituloLibro=@titulo, isbnLibro=@isbn, paginasLibro=@paginas, imagen=@imagen where idLibro=" + id;
                cmd.Parameters.Add(new SqlParameter("@idAutor", idAutor));
                cmd.Parameters.Add(new SqlParameter("@idEditorial", idEditorial));
                cmd.Parameters.Add(new SqlParameter("@titulo", titulo));
                cmd.Parameters.Add(new SqlParameter("@isbn", isbn));
                cmd.Parameters.Add(new SqlParameter("@paginas", paginas));
                //Referenciamos a la imagen en memoria para poder almacenarla en bd
                cmd.Parameters.Add(new SqlParameter("@imagen", convertir_Imagen(pb.ImageLocation)));
                modificado = true;
                cmd.ExecuteNonQuery();
            }
            else
            {
                //No hay imagen.
                cmd.CommandText = "update Libros set idAutor=@idAutor, idEditorial=@idEditorial, tituloLibro=@titulo, isbnLibro=@isbn, paginasLibro=@paginas, imagen=@imagen where idLibro=" + id;
                cmd.Parameters.Add(new SqlParameter("@idAutor", idAutor));
                cmd.Parameters.Add(new SqlParameter("@idEditorial", idEditorial));
                cmd.Parameters.Add(new SqlParameter("@titulo", titulo));
                cmd.Parameters.Add(new SqlParameter("@isbn", isbn));
                cmd.Parameters.Add(new SqlParameter("@paginas", paginas));
                //Referenciamos a la imagen en memoria para poder almacenarla en bd
                modificado = true;
                cmd.ExecuteNonQuery();
            }
            cerrar_Conexion();
            return modificado;
        }

        public bool eliminarLibro(int id)
        {
            bool eliminado = false;
            abrir_Conexion();
            cmd = conexion.CreateCommand();
            cmd.CommandText = "delete from Libros where idLibro=" + id;
            int resultado = cmd.ExecuteNonQuery();
            if (resultado == 0)
            {
                //No se ha eliminado ninguna editorial.
            }
            else
            {
                // Como minimo se ha eliminado una fila.
            }
            cerrar_Conexion();
            return eliminado;
        }
    }
}
