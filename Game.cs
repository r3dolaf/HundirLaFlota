using System;

public class Game
{
    public Player playerIA { get; private set; }
    public Player playerOne { get; private set; }
    public Player playerTwo { get; private set; }
    private int[] coordToAttack;

    //constructor que recibe el numero de jugadores
    public Game(int numHumanPlayers)
    {
        switch (numHumanPlayers)
        {
            case 1:
                initNewGameVsIA();
                break;
            case 2:
                initNewGame();
                break;
            default:
                break;
        }
    }

    //creamos un nuevo juego contra la máquina
    private void initNewGameVsIA()
    {
        playerOne = new Player(1, UserType.IA);
        Console.Clear();
        Console.WriteLine("PIA");
        createBoard(playerOne);
        createTargetBoard(playerOne);
        playerTwo = new Player(2, UserType.PLAYER);
        Console.Clear();
        Console.WriteLine("P2");
        createBoard(playerTwo);
        createTargetBoard(playerTwo);
        playing();
    }

    //creamos un nuevo juego contra una persona
    private void initNewGame()
    {
        playerOne = new Player(1, UserType.PLAYER);
        Console.Clear();
        Console.WriteLine("P1");
        createBoard(playerOne);
        createTargetBoard(playerOne);       
        Console.Clear();
        Console.WriteLine("P2");
        playerTwo = new Player(2, UserType.PLAYER);
        createBoard(playerTwo);
        createTargetBoard(playerTwo);
        playing();
    }    

    //creacion del tablero de barcos del jugador
    private void createBoard(Player player)
    {
        Board boardPlayer = new Board(player, ("myBoard" + player.id));
        boardPlayer.fillBoard(player.type);
        player.setBoard(boardPlayer);
    }

    //creción del tablero de disparos del jugador
    private void createTargetBoard(Player playerHuman)
    {
        Board boardPlayer = new Board(playerHuman, ("targetBoard" + playerHuman.id));
        playerHuman.setBoard(boardPlayer);
    }

    //bucle principal del juego
    private void playing()
    {
        coordToAttack = new int[] { 0, 0 };
        bool hit;
        do
        {
            for (int i = 0; i < 2; i++)
            {
                // Comprobamos si los jugadores siguen con vidas disponibles
                // Comprobamos el turno. Si el turno es 0, juega y ataca el playerTwo o playerOnevsIA
                if (i == 0)
                {
                    showBoard(playerOne.getBoard()[0]);
                    Console.WriteLine("P2_TURN");
                    int[] coordToAttack = playerTwo.getCoordinates();
                    hit = playerOne.attacked(coordToAttack);
                    playerTwo.updateHitBoard(coordToAttack, hit);
                    Console.WriteLine("P2: " + coordToAttack[0] + "-" + coordToAttack[1] + "->" + playerTwo.vidasBarcos);
                    //Console.ReadKey();
                    if (playerOne.getVidasBarcos() == 0)
                    {
                        break;
                    }
                }
                else
                {
                    showBoard(playerTwo.getBoard()[0]);
                    if (playerOne.type.Equals(UserType.IA))
                    {
                        Console.WriteLine("PIA_TURN");
                        coordToAttack = playerOne.autoAtack();
                    }
                    else
                    {
                        Console.WriteLine("P1_TURN");
                        coordToAttack = playerOne.getCoordinates();
                    }
                    hit = playerTwo.attacked(coordToAttack);
                    playerOne.updateHitBoard(coordToAttack, hit);
                    Console.WriteLine("P1: " + coordToAttack[0] + "-" + coordToAttack[1] + "->" + playerOne.vidasBarcos);
                    //Console.ReadKey();
                    if (playerTwo.getVidasBarcos() == 0)
                    {
                        break;
                    }
                }
            }
        } while (playerOne.getVidasBarcos() > 0 && playerTwo.getVidasBarcos() > 0);
        if (playerOne.getVidasBarcos() > 0)
        {
            Console.Clear();
            Console.WriteLine("Player One ha ganado");
            Console.ReadKey();
            Environment.Exit(0);
        }
        else
        {
            Console.Clear();
            Console.WriteLine("Player Two ha ganado");
            Console.ReadKey();
            Environment.Exit(0);
        }
    }
   
    //impresión de tablero
    private void showBoard(Board board)
    {
        Console.Clear();
        //Imprime el panel de aciertos y fallos
        Console.SetCursorPosition(0, 0); //Coloca el cursor en la posición indicada.
        Console.ForegroundColor = ConsoleColor.Yellow; //Pone las coords. en amarillo.
        Console.WriteLine("0123456789"); //Imprime la linea de coordenadas horizontales.
        Console.ForegroundColor = ConsoleColor.Gray;//Vuelve a poner las letras en gris.
        for (int i = 0; i < 10; i++)
        {
            for (int j = 0; j < 10; j++)
            {
                if (board.Estado[i, j] == "#")
                {
                    Console.Write("{0}", board.Estado[i, j]);//Imprime # en gris.
                }
                else if (board.Estado[i, j] == "T")
                {
                    Console.ForegroundColor = ConsoleColor.Red;//Pone las letras en ROJO.
                    Console.Write("{0}", board.Estado[i, j]);//Imprime "T".
                    Console.ForegroundColor = ConsoleColor.Gray;//Pone las letras otra vez en gris. 
                }
                else if (board.Estado[i, j] == "A")
                {
                    Console.ForegroundColor = ConsoleColor.Cyan;//Pone las letras en AZUL.
                    Console.Write("{0}", board.Estado[i, j]);//Imprime "A".
                    Console.ForegroundColor = ConsoleColor.Gray;//Pone las letras otra vez en gris.
                }
            }
            Console.ForegroundColor = ConsoleColor.Yellow;//Pone las letras en amarillo.
            Console.WriteLine("{0}", i); //Cada vez que llega al final de una linea imprime el número de linea.
            Console.ForegroundColor = ConsoleColor.Gray;//Vuelve a poner las letras en gris.
        }
    }
}


