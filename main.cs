/**
 * Bloc de Notas Domingo v0.2
 * 
 * @copyright Domingo Ruiz Arroyo 2021
 * @url https://github.com/domingoruiz/blocnotas_.net
 */
using System;

namespace BlocNotasDomingo
{
    class main
    {
        // Función principal de la aplicación la cuál es donde comienza todo
        public static void Main()
        {
            // Mostramos en la pantalla de inicio todas las operaciones disponibles
            String modo = Operaciones.pantallainicio();

            // Según lo que se elija cargamos una operación u otra
            if (modo == "1")
            {
                Operaciones.vernotasexistentes();
            }
            else if (modo == "2")
            {
                Operaciones.crearnota();
            }
            else if (modo == "3")
            {
                Operaciones.eliminarnota();
            }
            else if (modo == "4")
            {
                Operaciones.salir(); 
            }
            else
            {
                Operaciones.operacionnoexistente();
            }
        }
    }
}
