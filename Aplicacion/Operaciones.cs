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
            //Inicimos el array donde vamos a guardar los candidatos a ver
            string[] candidatos = Nota.get_lista_notas();

            //Borramos la pantalla antes de escribir nada
            Funciones.escribir_cabecera();
            Console.WriteLine("Seleccione la nota que deseas ver:");

            //Montamos el selector con todas las notas y recojemos la opción elegida
            int numeronota_aver = Funciones.selector_array(candidatos);

            //Si esta vacio damos error, si no eliminamos
            if (String.IsNullOrEmpty(candidatos[numeronota_aver]))
            {
                Funciones.escribir_cabecera();
                Console.WriteLine("Esa nota no existe. Pulse intro para volver a atrás.");
                Funciones.volver_inicio();
            }
            else
            {
                Nota nota_a_ver = new Nota(candidatos[numeronota_aver]);

                Funciones.escribir_cabecera();
                Console.WriteLine("Abajo esta la información de la nota. Pulse intro para ver otra nota.");
                Console.WriteLine("");
                Console.WriteLine("Nombre: "+ nota_a_ver.get_titulo());
                Console.WriteLine("Fecha: "+ nota_a_ver.get_fechacreacion());
                Console.WriteLine("Nota: "+ nota_a_ver.get_nota());
                Funciones.volver_inicio();
            }
        }

        /** Operación que nos permite crear una nota nueva */
        public static void crearnota()
        {
            // Creamos el objeto de la nueva nota
            Nota nueva_nota = new Nota();

            // Escribimos la cabecera en pantalla
            Funciones.escribir_cabecera();

            // Pedimos el nombre de la nueva nota que vamos a crear
            Console.WriteLine("Escriba el nombre de la nueva nota a crear:");
            nueva_nota.set_titulo(Console.ReadLine());

            // Verificamos que no este vacio el titulo
            if(String.IsNullOrEmpty(nueva_nota.get_titulo()))
            {
                Funciones.escribir_cabecera();
                Console.WriteLine("Título de la nota vacio. Pulse intro para volver a atrás.");
                Console.ReadLine();
                Operaciones.crearnota();
            }

            // Pedimos el contenido de la nota que vamos a crear
            Console.WriteLine("Escriba la nota:");
            nueva_nota.set_nota(Console.ReadLine());

            // Creamos la nota nueva
            nueva_nota.crear_nota();

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

            //Borramos la pantalla antes de escribir nada
            Funciones.escribir_cabecera();
            Console.WriteLine("Seleccione la nota que deseas eliminar:");

            //Montamos el selector con todas las notas y recojemos la opción elegida
            int numeronota_aborrar = Funciones.selector_array(ficheros);

            //Si esta vacio damos error, si no eliminamos
            if (String.IsNullOrEmpty(ficheros[numeronota_aborrar]))
            {
                Funciones.escribir_cabecera();
                Console.WriteLine("Esa nota no existe. Pulse intro para volver a atrás.");
                Funciones.volver_inicio();
            }
            else
            {
                File.Delete(ficheros[numeronota_aborrar]);
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
