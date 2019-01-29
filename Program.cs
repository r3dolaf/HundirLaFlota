using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace Hundir_la_flota
{
    class Program
    {
        //creamos el menú inicial
        static void Main(string[] args)
        {        
            int opc = 4;
            do
            {
                Console.WriteLine("Welcome to BattleShip, please select game mode:\n" +
                "1 for single player\n" +
                "2 for dual player\n" +
                "3 for the static version\n" +
                "0 to exit");
                try
                {
                    opc = Int32.Parse(Console.ReadLine());
                }
                catch (Exception e)
                {
                    Output.errorClear();
                }                
            } while (opc != 0 && opc != 1 && opc != 2 && opc != 3);
            switch (opc)
            {
                case 0:
                    Environment.Exit(0);
                    break;
                case 1:
                    new Game(1);
                    break;
                case 2:
                    new Game(2);
                    break;
                case 3:
                    staticGame();
                    break;
                default:
                    break;
            }
        }

        //modo de juego estático
        //creado como prototipo
        static void staticGame()
        {
            int horizontal = 0;
            int vertical = 0;
            int barcos = 14;
            //Declaración de matrices y arrays.
            int[,] panel = {{1,1,1,0,0,0,0,0,0,0},
                            {0,0,0,0,0,0,0,0,0,0},
                            {0,0,0,1,1,0,0,1,0,0},
                            {0,0,0,0,0,0,0,1,0,0},
                            {0,0,0,0,0,0,0,0,0,0},
                            {0,0,0,0,0,0,0,0,0,0},
                            {1,0,0,0,0,0,0,0,0,0},
                            {1,0,0,1,1,1,1,0,0,0},
                            {1,0,0,0,0,0,0,0,0,0},
                            {0,0,0,0,0,0,0,0,0,0}};

            string[,] estado = {{"#","#","#","#","#","#","#","#","#","#"},
                                {"#","#","#","#","#","#","#","#","#","#"},
                                {"#","#","#","#","#","#","#","#","#","#"},
                                {"#","#","#","#","#","#","#","#","#","#"},
                                {"#","#","#","#","#","#","#","#","#","#"},
                                {"#","#","#","#","#","#","#","#","#","#"},
                                {"#","#","#","#","#","#","#","#","#","#"},
                                {"#","#","#","#","#","#","#","#","#","#"},
                                {"#","#","#","#","#","#","#","#","#","#"},
                                {"#","#","#","#","#","#","#","#","#","#"}};
            Console.Clear();
            do
            {
                //Imprime el panel de aciertos y fallos
                Console.SetCursorPosition(0, 0); //Coloca el cursor en la posición indicada.
                Console.ForegroundColor = ConsoleColor.Yellow; //Pone las coords. en amarillo.
                Console.WriteLine("0123456789"); //Imprime la linea de coordenadas horizontales.
                Console.ForegroundColor = ConsoleColor.Gray;//Vuelve a poner las letras en gris.
                for (int i = 0; i < 10; i++)
                {
                    for (int j = 0; j < 10; j++)
                    {
                        if (estado[i, j] == "#")
                        {
                            Console.Write("{0}", estado[i, j]);//Imprime # en gris.
                        }
                        else if (estado[i, j] == "T")
                        {
                            Console.ForegroundColor = ConsoleColor.Red;//Pone las letras en ROJO.
                            Console.Write("{0}", estado[i, j]);//Imprime "T".
                            Console.ForegroundColor = ConsoleColor.Gray;//Pone las letras otra vez en gris. 
                        }
                        else if (estado[i, j] == "A")
                        {
                            Console.ForegroundColor = ConsoleColor.Cyan;//Pone las letras en AZUL.
                            Console.Write("{0}", estado[i, j]);//Imprime "A".
                            Console.ForegroundColor = ConsoleColor.Gray;//Pone las letras otra vez en gris.
                        }
                    }
                    Console.ForegroundColor = ConsoleColor.Yellow;//Pone las letras en amarillo.
                    Console.WriteLine("{0}", i); //Cada vez que llega al final de una linea imprime el número de linea.
                    Console.ForegroundColor = ConsoleColor.Gray;//Vuelve a poner las letras en gris.

                }
                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.Yellow;//Pone las letras en amarillo.
                Console.WriteLine("¡¡DISPARA!!");
                Console.ForegroundColor = ConsoleColor.Gray;//Vuelve a poner las letras en gris.

                //Limpia las coordenadas introducidas previamente.
                Console.SetCursorPosition(0, 16); Console.Write("             ");
                Console.SetCursorPosition(0, 18); Console.Write("             ");

                //Recoje las coordenadas horizontales.

                do
                {
                    Console.SetCursorPosition(0, 15);
                    Console.WriteLine("Coordenadas horizontales (0-9):");
                    Console.Write("             "); //Borra la linea escrita anteriormente.           
                    Console.SetCursorPosition(0, 16);
                    try
                    {
                        horizontal = int.Parse(Console.ReadLine());
                    }
                    catch (Exception e)
                    {
                        Output.errorClear();

                    }
                } while (horizontal > 10 || horizontal < 0); //Repite si mete un valor erróneo.

                //Recoje las coordenadas verticales.
                do
                {
                    Console.SetCursorPosition(0, 17);
                    Console.WriteLine("Coordenadas verticales (0-9):");
                    Console.WriteLine("             "); //Borra la linea escrita anteriormente.
                    Console.SetCursorPosition(0, 18);
                    try
                    {
                        vertical = int.Parse(Console.ReadLine());
                    }
                    catch (Exception e)
                    {
                        Output.errorClear();

                    }
                } while (vertical > 10 || vertical < 0); //Repite si mete un valor erróneo.


                //Comprueba si ha acertado.
                if (panel[vertical, horizontal] == 1 && estado[vertical, horizontal] != "T")//Si aciertas y no está "tocado"...
                {
                    Console.Write("¡¡TOCADO!! Pulsa una tecla para tirar otra vez...");    //Muestra "tocado!"...
                    Console.ReadKey(); //Espera una pulsación.
                    Console.Write("                                                     "); //Borra lo escrito.                
                    estado[vertical, horizontal] = "T"; //cambia el valor de la casilla. 
                    barcos--;                           //...y le resta 1 a "barcos" (al llegar a 0 se acaba).
                }
                else if (panel[vertical, horizontal] == 0 && estado[vertical, horizontal] != "T") //si no...y además el lugar está sin barco...
                {
                    Console.Write("¡¡AGUA!! Pulsa una tecla para tirar otra vez...");//...muestra "agua!".
                    Console.ReadKey(); //Espera una pulsación.
                    Console.Write("                                                     "); //Borra lo escrito.
                    estado[vertical, horizontal] = "A"; //cambia el valor de la casilla.
                }
                else
                {
                    Console.Write("Ya has disparado ahí.");
                    Console.ReadKey();
                    Console.Write("                     ");
                }

            } while (barcos > 0);

            //Borra la pantalla y muestra el mensaje de que el jugador ha ganado.
            Console.Clear();
            Console.WriteLine("¡¡HAS GANADO!!");

        }
    }
}