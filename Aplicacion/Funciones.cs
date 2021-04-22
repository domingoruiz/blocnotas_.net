using System;
using System.IO;

namespace BlocNotasDomingo
{
    // Colección de funciones que usaremos a lo largo de la aplicación
    class Funciones
    {
        // Función que limpia la pantalla y escribe la cabecera de la aplicación
        public static void escribir_cabecera()
        {
            Console.Clear();
            Console.WriteLine("--------------------------------------------");
            Console.WriteLine("------- Bloc de notas \"Domingo\" v0.2 -------");
            Console.WriteLine("--------------------------------------------");
            Console.WriteLine("");
        }

        //Funcion que pone una pausa y vuelve a la pantalla de inicio
        public static void volver_inicio()
        {
            while (Console.ReadKey().Key != ConsoleKey.Enter) { }
            main.Main();
        }

        // Verifica que exista la carpeta donde vamos a guardar las notas y si no existe la crea
        public static void verificar_carpeta_notas()
        {
            string ruta = @"C:\notas\";

            try
            {
                if (!Directory.Exists(ruta))
                {
                    DirectoryInfo di = Directory.CreateDirectory(ruta);
                }
            } catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        // Muestra en pantalla una lista con todas las notas que le pasamos y devuelve la nota que ha seleccionado el usuario
        public static int selector_array(string[] candidatos)
        {
            //Si no existen notas lo decimos por pantalla
            if (candidatos.Length == 0)
            {
                Funciones.escribir_cabecera();
                Console.WriteLine("No existen notas para visualizar. Pulse intro para volver a inicio.");
                Funciones.volver_inicio();
            }

            //Recorremos todos los archivos de notas para leer el nombre y la fecha para escribirlo en pantalla
            int Contador = 0;
            foreach (String fichero in candidatos)
            {
                // Iniciamos el objeto de la nota actual
                Nota nota_a_ver = new Nota(fichero);

                //Escribimos en pantalla
                Console.WriteLine(Contador.ToString() + ": " + nota_a_ver.get_titulo() + " (" + nota_a_ver.get_fechacreacion() + ")");
                candidatos[Contador] = fichero;
                Contador++;

                // Si hemos leido ya todas las notas salimos del bucle
                if(Contador > candidatos.Length) break;
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

            //Pasamos la cadena a entero y la devolvemos
            return Int32.Parse(lecturaconsola);
        }
    }
}
