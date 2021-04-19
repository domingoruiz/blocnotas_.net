using System;
using System.Threading;

namespace BlocNotasDomingo
{
    class main
    {
        public static void Main()
        {
            String modo = Operaciones.pantallainicio();

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
