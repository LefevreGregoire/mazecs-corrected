namespace Epsi.MazeCs;

public class Coin : ICollectable
{
    public int Points { get; }
    public bool IsPersistent => false;

    public Coin(int points = 1)
    {
        Points = points;
    }
}
