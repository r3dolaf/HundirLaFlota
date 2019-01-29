using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


    static class Output
    {
        //para mostrar un error a la hora de procesaar un input (escuando llamamos a esta funcion)
        public static void errorClear()
        {
            Console.WriteLine("ERROR");
            Console.ReadKey();
            Console.Clear();
        }
    }

