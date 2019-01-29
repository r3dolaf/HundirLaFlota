using System;

public class Board
{
    private string idBoard;
    private string[,] panel;
    private string[,] estado;

    string[] barcoGrande = { "G1", "G2", "G3", "G4" };
    string[] barcoMediano = { "M1", "M2", "M3" };
    string[] barcoPequeno = { "P1", "P2" };
    Random rnd = new Random();

    public string[,] Panel { get => panel; set => panel = value; }
    public string[,] Estado { get => estado; set => estado = value; }
    public Ship ship { get; private set; }

    //constructor
    public Board(Player player, string idBoard)
    {
        createEmptyBoard(idBoard);
    }

    //Declaración de matrices que componen lso tableros de un jugador
    public void createEmptyBoard(string idBoard)
    {
        this.idBoard = idBoard;
        panel = new string[,] {{"0","0","0","0","0","0","0","0","0","0"},
                            {"0","0","0","0","0","0","0","0","0","0"},
                            {"0","0","0","0","0","0","0","0","0","0"},
                            {"0","0","0","0","0","0","0","0","0","0"},
                            {"0","0","0","0","0","0","0","0","0","0"},
                            {"0","0","0","0","0","0","0","0","0","0"},
                            {"0","0","0","0","0","0","0","0","0","0"},
                            {"0","0","0","0","0","0","0","0","0","0"},
                            {"0","0","0","0","0","0","0","0","0","0"},
                            {"0","0","0","0","0","0","0","0","0","0"}};
        estado = new string[,] {{"#","#","#","#","#","#","#","#","#","#"},
                                {"#","#","#","#","#","#","#","#","#","#"},
                                {"#","#","#","#","#","#","#","#","#","#"},
                                {"#","#","#","#","#","#","#","#","#","#"},
                                {"#","#","#","#","#","#","#","#","#","#"},
                                {"#","#","#","#","#","#","#","#","#","#"},
                                {"#","#","#","#","#","#","#","#","#","#"},
                                {"#","#","#","#","#","#","#","#","#","#"},
                                {"#","#","#","#","#","#","#","#","#","#"},
                                {"#","#","#","#","#","#","#","#","#","#"}};

    }

    public void fillBoard(UserType userType)
    {
        if (userType.Equals(UserType.IA))
        {
            // 0-3 Horizontal, 3-9 Vertical
            int tipoBarco = rnd.Next(0, 9);
            int[] arrayV = new int[9] { 0, 1, 2, 3, 4, 5, 6, 7, 8 };
            int[] arrayH = new int[9] { 0, 1, 2, 3, 4, 5, 6, 7, 8 };
            if (tipoBarco < 3)
            {
                int randomV = arrayV[rnd.Next(0, arrayV.Length - 1)];
                int randomH = arrayH[rnd.Next(0, arrayH.Length - 4)];
                for (int j = 0; j < 4; j++)
                {
                    panel[randomH + j, randomV] = barcoGrande[j] + "H";
                }
            }
            else if (tipoBarco >= 3)
            {
                int randomV = arrayV[rnd.Next(0, arrayV.Length - 4)];
                int randomH = arrayH[rnd.Next(0, arrayH.Length - 1)];
                for (int j = 0; j < 4; j++)
                {
                    panel[randomH, randomV + j] = barcoGrande[j] + "V";
                }
            }            
            // Creamos los barcos medianos.
            for (int i = 0; i < 2; i++)
            {
                tipoBarco = rnd.Next(0, 9);
                if (tipoBarco < 3)
                {
                    int randomV = 0;
                    int randomH = 0;
                    string pos = "";
                    bool boatCreated = false;
                    do
                    {
                        randomV = arrayV[rnd.Next(0, arrayV.Length - 1)];
                        randomH = arrayH[rnd.Next(0, arrayH.Length - 3)];
                        pos = panel[randomH, randomV];
                        if (fitMedBoat(panel, randomH, randomV, "H"))
                        {
                            boatCreated = createMedBoat(randomH, randomV, "H");
                        }
                    } while (!boatCreated);
                }
                else if (tipoBarco >= 3)
                {
                    int randomV = 0;
                    int randomH = 0;
                    string pos = "";
                    bool boatCreated = false;
                    do
                    {
                        randomV = arrayV[rnd.Next(0, arrayV.Length - 3)];
                        randomH = arrayH[rnd.Next(0, arrayH.Length - 1)];
                        pos = panel[randomH, randomV];
                        if (fitMedBoat(panel, randomH, randomV, "V"))
                        {                           
                            boatCreated = createMedBoat(randomH, randomV, "V");
                        }
                    } while (!boatCreated);
                }
            }
            // Creamos los barcos pequeños.
            for (int i = 0; i < 2; i++)
            {
                tipoBarco = rnd.Next(0, 9);
                if (tipoBarco < 3)
                {
                    int randomV = 0;
                    int randomH = 0;
                    string pos = "";
                    bool boatCreated = false;
                    do
                    {
                        randomV = arrayV[rnd.Next(0, arrayV.Length - 1)];
                        randomH = arrayH[rnd.Next(0, arrayH.Length - 2)];
                        pos = panel[randomH, randomV];
                        if (fitPeqBoat(panel, randomH, randomV, "H"))
                        {
                            boatCreated = createPeqBoat(randomH, randomV, "H");
                        }
                    } while (!boatCreated);
                }
                else if (tipoBarco >= 3)
                {
                    int randomV = 0;
                    int randomH = 0;
                    string pos = "";
                    bool boatCreated = false;
                    do
                    {
                        randomV = arrayV[rnd.Next(0, arrayV.Length - 2)];
                        randomH = arrayH[rnd.Next(0, arrayH.Length - 1)];
                        pos = panel[randomH, randomV];
                        if (fitPeqBoat(panel, randomH, randomV, "V"))
                        {
                            boatCreated = createPeqBoat(randomH, randomV, "V");
                        }
                    } while (!boatCreated);
                }
            }
        }
        else
        {
            int barcoPq = 0;
            int barcoMed = 0;
            int barcoGd = 1;
            int[] arrayV1 = new int[9] { 0, 1, 2, 3, 4, 5, 6, 7, 8 };
            int[] arrayH1 = new int[9] { 0, 1, 2, 3, 4, 5, 6, 7, 8 };
            for (int i = 0; i < barcoGd; i++)
            {
                String tipoBarco = "";
                do
                {
                    tipoBarco = Input.getOrientacion();
                } while (tipoBarco.CompareTo("H") != 0 && tipoBarco.CompareTo("V") != 0);
                if (tipoBarco.CompareTo("V") == 0)
                {
                    int inicioX = 0;
                    int inicioY = 0;
                    do
                    {
                        inicioX = Input.getX();
                    } while (inicioX > 9 && inicioX < 0);
                    do
                    {
                        inicioY = Input.getY();
                    } while (inicioY > 6 && inicioY < 0);

                    for (int j = inicioX; j < inicioX + 4; j++)
                    {
                        panel[j, inicioY] = "1";
                    }
                }
                else
                {
                    int inicioX = 0;
                    int inicioY = 0;
                    do
                    {
                        inicioX = Input.getX();
                    } while (inicioX > 6 && inicioX < 0);
                    do
                    {
                        inicioY = Input.getY();
                    } while (inicioY > 9 && inicioY < 0);

                    for (int j = inicioY; j < inicioY + 4; j++)
                    {
                        panel[inicioX, j] = "1";
                    }
                }
            }
            for (int i = 0; i < barcoMed; i++)
            {
                String tipoBarco = "";
                int inicioX = 0;
                int inicioY = 0;
                bool boatCreated = false;
                do
                {
                    tipoBarco = Input.getOrientacion();
                } while (tipoBarco.CompareTo("H") != 0 && tipoBarco.CompareTo("V") != 0);
                do
                {
                    inicioX = Input.getX();
                } while (inicioX > 9 && inicioX < 0);
                do
                {
                    inicioY = Input.getY();
                } while (inicioY > 9 && inicioY < 0);
                do
                {
                    boatCreated = false;
                    if (fitMedBoat(panel, inicioX, inicioY, tipoBarco))
                    {
                        boatCreated = createMedBoat(inicioX, inicioY, tipoBarco);
                    }
                    else
                    {
                        do
                        {
                            tipoBarco = Input.getOrientacion();
                        } while (tipoBarco.CompareTo("H") != 0 && tipoBarco.CompareTo("V") != 0);
                        do
                        {
                            inicioX = Input.getX();
                        } while (inicioX > 9 && inicioX < 0);
                        do
                        {
                            inicioY = Input.getY();
                        } while (inicioY > 9 && inicioY < 0);
                    }
                } while (!boatCreated);
            }
            for (int i = 0; i < barcoPq; i++)
            {
                String tipoBarco = "";
                int inicioX = 0;
                int inicioY = 0;
                bool boatCreated = false;
                do
                {
                    tipoBarco = Input.getOrientacion();
                } while (tipoBarco.CompareTo("H") != 0 && tipoBarco.CompareTo("V") != 0);
                do
                {
                    inicioX = Input.getX();
                } while (inicioX > 9 && inicioX < 0);
                do
                {
                    inicioY = Input.getY();
                } while (inicioY > 9 && inicioY < 0);
                do
                {
                    boatCreated = false;
                    if (fitPeqBoat(panel, inicioX, inicioY, tipoBarco))
                    {
                        boatCreated = createPeqBoat(inicioX, inicioY, tipoBarco);
                    }
                    else
                    {
                        do
                        {
                            tipoBarco = Input.getOrientacion();
                        } while (tipoBarco.CompareTo("H") != 0 && tipoBarco.CompareTo("V") != 0);
                        do
                        {
                            inicioX = Input.getX();
                        } while (inicioX > 9 && inicioX < 0);
                        do
                        {
                            inicioY = Input.getY();
                        } while (inicioY > 9 && inicioY < 0);
                    }
                } while (!boatCreated);                
            }
        }

        bool fitMedBoat(string[,] panel, int randomH, int randomV, string whereTo)
        {
            bool boatCreated = true;
            int it = 0;
            if (whereTo.Equals("H"))
            {
                while (boatCreated && it < 3)
                {
                    boatCreated = panel[randomH + it, randomV].Equals("0") ? true : false;
                    it++;
                }
            }
            else if (whereTo.Equals("V"))
            {
                while (boatCreated && it < 3)
                {
                    boatCreated = panel[randomH, randomV + it].Equals("0") ? true : false;
                    it++;
                }
            }
            return boatCreated;
        }

        bool fitPeqBoat(string[,] panel, int randomH, int randomV, string whereTo)
        {
            bool boatCreated = true;
            int it = 0;
            if (whereTo.Equals("H"))
            {
                while (boatCreated && it < 2)
                {
                    boatCreated = panel[randomH + it, randomV].Equals("0") ? true : false;
                    it++;
                }
            }
            else if (whereTo.Equals("V"))
            {
                while (boatCreated && it < 2)
                {
                    boatCreated = panel[randomH, randomV + it].Equals("0") ? true : false;
                    it++;
                }
            }
            return boatCreated;
        }

        bool createMedBoat(int inicioX, int inicioY, string whereTo)
        {
            if (whereTo.Equals("V"))
            {
                panel[inicioX, inicioY] = barcoMediano[0] + "H";
                panel[inicioX, inicioY + 1] = barcoMediano[1] + "H";
                panel[inicioX, inicioY + 2] = barcoMediano[2] + "H";                
                return true;
            }
            else
            {
                panel[inicioX, inicioY] = barcoMediano[0] + "H";
                panel[inicioX, inicioY + 1] = barcoMediano[1] + "H";
                panel[inicioX, inicioY + 2] = barcoMediano[2] + "H";
                return true;
            }
        }

        bool createPeqBoat(int inicioX,int inicioY, string whereTo)
        {
            if (whereTo.Equals("V"))
            {
                panel[inicioX, inicioY] = barcoPequeno[0] + "V";
                panel[inicioX + 1, inicioY] = barcoPequeno[1] + "V";
                return true;
            } else
            {
                panel[inicioX, inicioY] = barcoPequeno[0] + "H";
                panel[inicioX, inicioY + 1] = barcoPequeno[1] + "H";
                return true;
            }
            
        }
    }
}

