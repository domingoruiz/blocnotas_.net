using System;
using System.IO;
using System.Xml;

namespace BlocNotasDomingo
{
    public class Nota
    {
        public string ruta;
        public string titulo;
        public string fechacreacion;
        public string fechamodificacion;
        public string nota;
        public string directorio = @"C:\notas\";

        public Nota(string ruta = null)
        {
            if (!String.IsNullOrEmpty(ruta))
            {
                //Definimos en el objeto el parametro de entrada
                this.set_ruta(ruta);

                //Creamos el objeto y cargamos el XML
                XmlDocument doc = new XmlDocument();
                doc.LoadXml(File.ReadAllText(ruta));

                //Sacamos el titulo
                XmlNodeList titulonotaelemList = doc.GetElementsByTagName("titulo");
                this.set_titulo(titulonotaelemList[0].InnerXml);

                //Sacamos la fecha
                XmlNodeList fechacreacionelemList = doc.GetElementsByTagName("fechacreacion");
                this.set_fechacreacion(fechacreacionelemList[0].InnerXml);

                //Sacamos la nota
                XmlNodeList notaelemList = doc.GetElementsByTagName("textonota");
                this.set_nota(notaelemList[0].InnerXml);
            }
        }
        public bool set_ruta(string ruta)
        {
            this.ruta = ruta;
            return true;
        }

        public string get_ruta()
        {
            return this.ruta;
        }

        public bool set_titulo(string titulo)
        {
            this.titulo = titulo;
            return true;
        }

        public string get_titulo()
        {
            return this.titulo;
        }

        public bool set_fechacreacion(string fechacreacion)
        {
            this.fechacreacion = fechacreacion;
            return true;
        }

        public string get_fechacreacion()
        {
            return this.fechacreacion;
        }

        public bool set_fechamodificacion(string fechamodificacion)
        {
            this.fechamodificacion = fechamodificacion;
            return true;
        }

        public string get_fechamodificacion()
        {
            return this.fechamodificacion;
        }

        public bool set_nota(string nota)
        {
            this.nota = nota;
            return true;
        }

        public string get_nota()
        {
            return this.nota;
        }

        //Creamos una nueva nota
        public bool crear_nota()
        {
            // Pedimos el Datetime del momento de la creación de la nota
            DateTime dateTime = new DateTime();
            dateTime = DateTime.Now;
            string strDateTime = Convert.ToDateTime(dateTime).ToString("dd-mm-yyyy HH:mm:ss");

            // Generamos el XML con toda la información
            string xmlresultante = "<?xml version=\"1.0\"?> \n" +
                                   "<nota> \n" +
                                        "<titulo>" + this.get_titulo() + "</titulo> \n" +
                                        "<fechacreacion>" + strDateTime + "</fechacreacion> \n" +
                                        "<fechamodificacion>" + "</fechamodificacion> \n" +
                                        "<textonota>" + this.get_nota() + "</textonota> \n" +
                                   "</nota>";

            // Guardamos la nota en el archivo correspondiente
            string rutaCompleta = @"C:\notas\" + this.get_titulo() + ".xml";
            Funciones.verificar_carpeta_notas();
            using (StreamWriter file = File.AppendText(rutaCompleta))
            {
                file.WriteLine(xmlresultante);
                file.Close();
            }

            return true;
        }

        //Eliminamos una nueva nota
        public bool borrar_nota()
        {
            File.Delete(this.get_ruta());
            return true;
        }

        // Obtenemos una matriz con todas las notas existentes
        public static string[] get_lista_notas()
        {
            Funciones.verificar_carpeta_notas();

            //Recorremos todas las notas que existen en este momento
            string directorio = @"C:\notas\";
            string[] ficheros = Directory.GetFiles(directorio);

            //Iniciamos el contador
            int Contador = 0;

            //Inicimos el array donde vamos a guardar los candidatos a ver
            string[] candidatos = new string[ficheros.Length];

            //Recorremos todos los archivos de notas para leer el nombre y la fecha para escribirlo en pantalla
            foreach (String fichero in ficheros)
            {
                candidatos[Contador] = fichero;
                Contador++;
            }

            return candidatos;
        }
    }
}
