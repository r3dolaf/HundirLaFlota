using System;
using System.Linq;

public class Player
{
    private UserType userType;
    public UserType type { get => userType; set => userType = value; }
    public long id { get; private set; }
    public Board[] boards { get; private set; }
    public int vidasBarcos;
    Random rnd = new Random();

    //constructor
    public Player(long id, UserType userType)
    {
        this.id = id;
        this.userType = userType;
        this.boards = new Board[2] { null, null };
        this.vidasBarcos = 14;
    }

    //asigna un tblero a un jugador
    public void setBoard(Board board)
    {
        if (boards[0] == null)
        {
            boards.SetValue(board, 0);
        }
        else
        {
            boards.SetValue(board, 1);
        }
    }

    //recuperamos lostableros del jugadopr
    public Board[] getBoard()
    {
        return boards;
    }

    //permite recojer coordenadas - deberia moverse a input
    public int[] getCoordinates()
    {
        int[] coordinates = new int[] { 0, 0 };
        int horizontal = 0, vertical = 0;
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
                Console.WriteLine("ERROR");
            }
            coordinates.SetValue(horizontal, 0);
        } while (horizontal > 10 || horizontal < 0); //Repite si mete un valor erróneo.
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
                Console.WriteLine("ERROR");
            }
            coordinates.SetValue(vertical, 1);
        } while (vertical > 10 || vertical < 0); //Repite si mete un valor erróneo.
        return coordinates;
    }

    //recupera el numero de barcos no disparados del jugador
    public int getVidasBarcos()
    {
        return this.vidasBarcos;
    }

    //calcula si donde se ha apuntado, ha alcanzado algún barco.
    public bool attacked(int[] coord)
    {
        int vertical = coord[1];
        int horizontal = coord[0];
        bool hit = false;
        //Comprueba si ha acertado.
        if (boards[0].Panel[vertical, horizontal] != "0" && boards[0].Estado[vertical, horizontal] != "T")
        {
            Console.Write("¡¡TOCADO!! Pulsa una tecla para pasar turno");
            Console.ReadKey();
            boards[0].Estado[vertical, horizontal] = "T";
            vidasBarcos = vidasBarcos - 1;
            hit = true;
        }
        else if (boards[0].Panel[vertical, horizontal] == "0" && boards[0].Estado[vertical, horizontal] == "#")
        {
            Console.Write("¡¡AGUA!! Pulsa una tecla para pasar turno");
            Console.ReadKey();
            boards[0].Estado[vertical, horizontal] = "A";
        }
        else
        {
            Console.Write("Ya has disparado ahí. Pulsa una tecla para pasar turno");
            Console.ReadKey();
            Console.Write("                     ");
        }

        return hit;
    }

    //forma de ataque de la IA
    internal int[] autoAtack()
    {
        int[] coordToAttack = new int[] { 0, 0 };
        int coorX = rnd.Next(0, 9);
        int coorY = rnd.Next(0, 9);
        coordToAttack.SetValue(coorX, 0);
        coordToAttack.SetValue(coorY, 1);
        string a = boards[0].Panel[coorX, coorY];
        return coordToAttack;
    }

    //actualiza el tablero de disparos del jugador con el resultado del mismo
    public void updateHitBoard(int[] coord, bool hit)
    {
        boards[1].Panel[coord[0], coord[1]] = hit ? "T" : "A";
    }
}