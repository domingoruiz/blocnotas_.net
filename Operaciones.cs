using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;

namespace BlocNotasDomingo
{
    class Operaciones
    {
        public static string pantallainicio()
        {
            Funciones.escribir_cabecera();

            Console.WriteLine("¿Que quieres hacer?");
            Console.WriteLine("1: Ver las notas existentes");
            Console.WriteLine("2: Crear nueva nota");
            Console.WriteLine("3: Eliminar nota antigua");
            Console.WriteLine("4: Salir de la aplicación");
            Console.WriteLine("");

            Console.WriteLine("Escribe el número de la operación que deseas ejecutar:");
            return Console.ReadLine();
        }
        public static void vernotasexistentes()
        {
            //Recorremos todas las notas que existen en este momento
            string directorio = @"C:\notas\";
            Funciones.verificar_carpeta_notas();
            string[] ficheros = Directory.GetFiles(directorio);

            //Iniciamos el contador
            int Contador = 0;

            //Inicimos el array donde vamos a guardar los candidatos a ver
            string[] candidatos = new string[100];

            //Borramos la pantalla antes de escribir nada
            Funciones.escribir_cabecera();
            Console.WriteLine("Seleccione la nota que deseas ver:");

            //Si no existen notas lo decimos por pantalla
            if (ficheros.Length == 0)
            {
                Funciones.escribir_cabecera();
                Console.WriteLine("No existen notas para visualizar. Pulse intro para volver a inicio.");
                Funciones.volver_inicio();
            }

            //Recorremos todos los archivos de notas para leer el nombre y la fecha para escribirlo en pantalla
            foreach (String fichero in ficheros)
            {
                //Creamos el objeto y cargamos el XML
                XmlDocument doc = new XmlDocument();
                doc.LoadXml(File.ReadAllText(fichero));

                //Sacamos el titulo
                XmlNodeList titulonotaelemList = doc.GetElementsByTagName("titulo");
                String titulonota = titulonotaelemList[0].InnerXml;

                //Sacamos la fecha
                XmlNodeList fechacreacionelemList = doc.GetElementsByTagName("fechacreacion");
                String fechacreacion = fechacreacionelemList[0].InnerXml;

                //Escribimos en pantalla
                Console.WriteLine(Contador.ToString() + ": " + titulonota + " (" + fechacreacion + ")");
                candidatos[Contador] = fichero;
                Contador++;
            }

            //Si existe leemos el numero de nota y lo intentamos ver
            String lecturaconsola = Console.ReadLine();

            //Si el readline es una cadena emitimos un error
            try
            {
                Int32.Parse(lecturaconsola);
            }
            catch
            {
                Funciones.escribir_cabecera();
                Console.WriteLine("Esa nota no existe. Pulse intro para volver a atrás.");
                Console.ReadLine();
                Operaciones.vernotasexistentes();
            }

            //Pasamos la cadena a entero
            int numeronota_aver = Int32.Parse(lecturaconsola);

            //Si esta vacio damos error, si no eliminamos
            if (String.IsNullOrEmpty(candidatos[numeronota_aver]))
            {
                Funciones.escribir_cabecera();
                Console.WriteLine("Esa nota no existe. Pulse intro para volver a atrás.");
                Console.ReadLine();
                Funciones.volver_inicio();
            }
            else
            {
                //Creamos el objeto y cargamos el XML
                XmlDocument doc = new XmlDocument();
                doc.LoadXml(File.ReadAllText(candidatos[numeronota_aver]));

                //Sacamos el titulo
                XmlNodeList titulonotaelemList = doc.GetElementsByTagName("titulo");
                String titulonota = titulonotaelemList[0].InnerXml;

                //Sacamos la fecha
                XmlNodeList fechacreacionelemList = doc.GetElementsByTagName("fechacreacion");
                String fechacreacion = fechacreacionelemList[0].InnerXml;

                //Sacamos la nota
                XmlNodeList notaelemList = doc.GetElementsByTagName("textonota");
                String nota = notaelemList[0].InnerXml;

                Funciones.escribir_cabecera();
                Console.WriteLine("Abajo esta la información de la nota. Pulse intro para ver otra nota.");
                Console.WriteLine("");
                Console.WriteLine("Nombre: "+titulonota);
                Console.WriteLine("Fecha: "+fechacreacion);
                Console.WriteLine("Nota: "+nota);
                Console.ReadLine();
                Funciones.volver_inicio();
            }
        }

        /** Operación que nos permite crear una nota nueva */
        public static void crearnota()
        {
            // Escribimos la cabecera en pantalla
            Funciones.escribir_cabecera();

            // Pedimos el nombre de la nueva nota que vamos a crear
            String nombrenota;
            Console.WriteLine("Escriba el nombre de la nueva nota a crear:");
            nombrenota = Console.ReadLine();

            // Verificamos que no este vacio el titulo
            if(String.IsNullOrEmpty(nombrenota))
            {
                Funciones.escribir_cabecera();
                Console.WriteLine("Título de la nota vacio. Pulse intro para volver a atrás.");
                Console.ReadLine();
                Operaciones.crearnota();
            }

            // Pedimos el contenido de la nota que vamos a crear
            String texto;
            Console.WriteLine("Escriba la nota:");
            texto = Console.ReadLine();

            // Pedimos el Datetime del momento de la creación de la nota
            DateTime dateTime = new DateTime();
            dateTime = DateTime.Now;
            string strDateTime = Convert.ToDateTime(dateTime).ToString("dd-mm-yyyy HH:mm:ss");

            // Generamos el XML con toda la información
            string xmlresultante = "<?xml version=\"1.0\"?> \n" +
                                   "<nota> \n" +
                                        "<titulo>" + nombrenota + "</titulo> \n" +
                                        "<fechacreacion>" + strDateTime + "</fechacreacion> \n" +
                                        "<fechamodificacion>" + "</fechamodificacion> \n" +
                                        "<textonota>" + texto + "</textonota> \n" +
                                   "</nota>";

            // Guardamos la nota en el archivo correspondiente
            string rutaCompleta = @"C:\notas\" + nombrenota + ".xml";
            Funciones.verificar_carpeta_notas();
            using (StreamWriter file = File.AppendText(rutaCompleta))
            {
                file.WriteLine(xmlresultante);
                file.Close();
            }

            // Damos resultado y volvemos a inicio
            Funciones.escribir_cabecera();
            Console.WriteLine("Nota creada correctamente. Pulse intro para volver a inicio.");
            Funciones.volver_inicio();
        }

        public static void eliminarnota()
        {
            //Recorremos todas las notas que existen en este momento
            string directorio =  @"C:\notas\";
            Funciones.verificar_carpeta_notas();
            string[] ficheros = Directory.GetFiles(directorio);

            //Iniciamos el contador
            int Contador = 0;

            //Inicimos el array donde vamos a guardar los candidatos a borrar
            string[] candidatos = new string[100];

            //Borramos la pantalla antes de escribir nada
            Funciones.escribir_cabecera();
            Console.WriteLine("Seleccione la nota que deseas eliminar:");

            //Si no existen notas lo decimos por pantalla
            if (ficheros.Length == 0)
            {
                Funciones.escribir_cabecera();
                Console.WriteLine("No existen notas para eliminar. Pulse intro para volver a inicio.");
                Funciones.volver_inicio();
            }

            //Recorremos todos los archivos de notas para leer el nombre y la fecha para escribirlo en pantalla
            foreach (String fichero in ficheros)
            {
                //Creamos el objeto y cargamos el XML
                XmlDocument doc = new XmlDocument();
                doc.LoadXml(File.ReadAllText(fichero));

                //Sacamos el titulo
                XmlNodeList titulonotaelemList = doc.GetElementsByTagName("titulo");
                String titulonota = titulonotaelemList[0].InnerXml;

                //Sacamos la fecha
                XmlNodeList fechacreacionelemList = doc.GetElementsByTagName("fechacreacion");
                String fechacreacion = fechacreacionelemList[0].InnerXml;

                //Escribimos en pantalla
                Console.WriteLine(Contador.ToString() + ": " + titulonota + " ("+fechacreacion+")");
                candidatos[Contador] = fichero;
                Contador++;
            }

            //Si existe leemos el numero de nota y lo intentamos eliminar
            String lecturaconsola = Console.ReadLine();

            //Si el readline es una cadena emitimos un error
            try {
                Int32.Parse(lecturaconsola);
            } 
            catch {
                Funciones.escribir_cabecera();
                Console.WriteLine("Esa nota no existe. Pulse intro para volver a atrás.");
                Console.ReadLine();
                Operaciones.eliminarnota();
            }

            //Pasamos la cadena a entero
            int numeronota_aborrar = Int32.Parse(lecturaconsola);

            //Si esta vacio damos error, si no eliminamos
            if (String.IsNullOrEmpty(candidatos[numeronota_aborrar]))
            {
                Funciones.escribir_cabecera();
                Console.WriteLine("Esa nota no existe. Pulse intro para volver a atrás.");
                Console.ReadLine();
                Operaciones.eliminarnota();
            }
            else
            {
                File.Delete(candidatos[numeronota_aborrar]);
                Funciones.escribir_cabecera();
                Console.WriteLine("Nota eliminada correctamente. Pulse intro para volver al inicio");
                Funciones.volver_inicio();
            }
        }

        public static void salir()
        {
            //Salimos de la aplicación
            Environment.Exit(1);
        }

        public static void operacionnoexistente()
        {
            //En el caso de que no exista la operación emitimos un error y volvemos a inicio.
            Funciones.escribir_cabecera();
            Console.WriteLine("La operación que has seleccionado no existe. Pulse intro para volver al inicio");
            Funciones.volver_inicio();
        }
    }
}
