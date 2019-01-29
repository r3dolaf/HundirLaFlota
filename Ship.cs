using System;

public class Ship
{
    public int id { get; private set; }
    public int puntos { get; private set; }

    public Ship(int id, int longitudBarco)
	{
        this.id = id;
        this.puntos = longitudBarco;
	}

    public void removeLifeShip()
    {
        if (puntos > 0)
        {
            puntos--;
        }
    }

}
