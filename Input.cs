using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

    class Input
    {
        //para recoger la orientacion de un barco
        public static String getOrientacion()
        {
            String tipoBarco = "";
            Console.WriteLine("Selecciona si el barco va a ser Horizontal o Vertical (H/V)");
            try
            {
                tipoBarco = Console.ReadLine();
            }
            catch (Exception e)
            {
                Console.WriteLine("ERROR");
            }
            return tipoBarco;
        }

        //para recoger la coordenada X de un barco
        public static int getX()
        {
            int inicioX = 10;
            Console.WriteLine("indica la X donde queires que comience el barco");
            try
            {
                inicioX = Int32.Parse(Console.ReadLine());
            }
            catch (Exception e)
            {
                Console.WriteLine("ERROR");
            }
            return inicioX;
        }

        //para recoger la coordenada Y de un barco
        public static int getY()
            {
            int inicioY = 10;
            Console.WriteLine("indica la Y donde queires que comience el barco");
            try
            {
                inicioY = Int32.Parse(Console.ReadLine());
            }
            catch (Exception e)
            {
                Console.WriteLine("ERROR");
            }
            return inicioY;
        }
}
