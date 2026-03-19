namespace Epsi.MazeCs;

public class Wall : Cell
{
    public override string GetSymbol() => "█";
    
    public override ConsoleColor GetColor(ConsoleColor wallColor, ConsoleColor corridorColor, ConsoleColor playerColor, ConsoleColor exitColor) 
        => wallColor;
    
    public override bool IsWall => true;
}
