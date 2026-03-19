namespace Epsi.MazeCs;

public abstract class Cell
{
    public abstract string GetSymbol();
    public abstract ConsoleColor GetColor(ConsoleColor wallColor, ConsoleColor corridorColor, ConsoleColor playerColor, ConsoleColor exitColor);
    public abstract bool IsWall { get; }
}
