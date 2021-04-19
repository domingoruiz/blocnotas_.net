using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace BlocNotasDomingo
{
    class Funciones
    {
        public static void escribir_cabecera()
        {
            Console.Clear();
            Console.WriteLine("---------------------------------------");
            Console.WriteLine("------- Bloc de notas \"Domingo\" -------");
            Console.WriteLine("---------------------------------------");
            Console.WriteLine("");
        }

        public static void volver_inicio()
        {
            Console.ReadLine();
            main.Main();
        }

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
    }
}
