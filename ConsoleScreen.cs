namespace Epsi.MazeCs;

public class ConsoleScreen
{
    private readonly CellType[,] grid;
    private readonly int width;
    private readonly int height;
    private readonly int offsetX;
    private readonly int offsetY;
    private readonly int marginYMessage;
    private readonly int messageHeight;

    private readonly ConsoleColor wallColor;
    private readonly ConsoleColor corridorColor;
    private readonly ConsoleColor playerColor;
    private readonly ConsoleColor exitColor;
    private readonly ConsoleColor infoColor;
    private readonly ConsoleColor instructionColor;
    private readonly ConsoleColor successColor;
    private readonly ConsoleColor dangerColor;

    private readonly string header;
    private readonly string instructions;
    private readonly string pressKeyMessage;

    public ConsoleScreen(CellType[,] grid, int width, int height, int offsetX, int offsetY,
        int marginYMessage, int messageHeight,
        ConsoleColor wallColor, ConsoleColor corridorColor, ConsoleColor playerColor, ConsoleColor exitColor,
        ConsoleColor infoColor, ConsoleColor instructionColor, ConsoleColor successColor, ConsoleColor dangerColor,
        string header, string instructions, string pressKeyMessage)
    {
        this.grid = grid;
        this.width = width;
        this.height = height;
        this.offsetX = offsetX;
        this.offsetY = offsetY;
        this.marginYMessage = marginYMessage;
        this.messageHeight = messageHeight;
        this.wallColor = wallColor;
        this.corridorColor = corridorColor;
        this.playerColor = playerColor;
        this.exitColor = exitColor;
        this.infoColor = infoColor;
        this.instructionColor = instructionColor;
        this.successColor = successColor;
        this.dangerColor = dangerColor;
        this.header = header;
        this.instructions = instructions;
        this.pressKeyMessage = pressKeyMessage;
    }

    public void Draw()
    {
        Console.Clear();
        Console.CursorVisible = false;

        DrawTextXY(0, 0, header, infoColor);
        for (var y = 0; y < height; y++)
        {
            for (var x = 0; x < width; x++)
            {
                DrawCell(x, y);
            }
        }
        DrawTextXY(0, offsetY + height, instructions, instructionColor);
    }

    public void UpdateCell(int cx, int cy, CellType type)
    {
        grid[cx, cy] = type;
        if (type == CellType.Start)
        {
            DrawTextXY(offsetX + cx, offsetY + cy, "@", playerColor);
        }
        else
        {
            DrawCell(cx, cy);
        }
    }

    public void DrawMazeCell(int cx, int cy, CellType type)
    {
        DrawCell(cx, cy);
    }

    public void DrawFramedText(string text, int x, int y, ConsoleColor color)
    {
        var lines = text.Split('\n');
        foreach (var line in lines)
        {
            DrawTextXY(x, y, line, color);
            y++;
        }
    }

    public void DrawEndScreen(bool won)
    {
        if (won)
        {
            DrawFramedText(
                "╔════════════════════════════════╗\n" +
                "║   🎉  FÉLICITATIONS !  🎉      ║\n" +
                "║   Vous avez trouvé la sortie ! ║\n" +
                "╚════════════════════════════════╝",
                0, offsetY + height + marginYMessage, successColor);
        }
        else
        {
            DrawTextXY(0, offsetY + height + marginYMessage, "  Partie abandonnée. À bientôt !", dangerColor);
        }

        DrawTextXY(0, offsetY + height + marginYMessage + messageHeight, pressKeyMessage);
    }

    private void DrawCell(int cx, int cy)
    {
        var (symbol, color) = grid[cx, cy] switch
        {
            CellType.Wall => ("█", wallColor),
            CellType.Start => ("@", playerColor),
            CellType.Exit => ("★", exitColor),
            _ => ("·", corridorColor)
        };
        DrawTextXY(offsetX + cx, offsetY + cy, symbol, color);
    }

    private void DrawTextXY(int x, int y, string text, ConsoleColor? color = null)
    {
        Console.SetCursorPosition(x, y);
        if (color.HasValue)
        {
            Console.ForegroundColor = color.Value;
        }
        Console.Write(text);
        Console.ResetColor();
    }
}
