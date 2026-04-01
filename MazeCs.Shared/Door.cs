namespace Epsi.MazeCs;

public class Door : Cell
{
    public Key Key { get; }
    public int DoorId { get; }

    public Door(int doorId)
    {
        DoorId = doorId;
        Key = new Key(doorId);
    }

    public override string GetSymbol() => "🚪";

    public override ConsoleColor GetColor(ConsoleColor wallColor, ConsoleColor corridorColor, ConsoleColor playerColor, ConsoleColor exitColor)
        => ConsoleColor.Magenta;

    public override bool IsWall => true;
}
