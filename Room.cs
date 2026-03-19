namespace Epsi.MazeCs;

public class Room : Cell
{
    public bool IsStart { get; set; }
    public bool IsExit { get; set; }

    public Room()
    {
        IsStart = false;
        IsExit = false;
    }

    public override string GetSymbol()
    {
        if (IsStart) return "@";
        if (IsExit) return "★";
        return "·";
    }

    public override ConsoleColor GetColor(ConsoleColor wallColor, ConsoleColor corridorColor, ConsoleColor playerColor, ConsoleColor exitColor)
    {
        if (IsStart) return playerColor;
        if (IsExit) return exitColor;
        return corridorColor;
    }

    public override bool IsWall => false;
}
