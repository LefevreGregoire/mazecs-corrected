using Epsi.MazeCs;

const int width = 50;
const int height = 20;

const int offsetY = 3;
const int offsetX = 0;

const int marginYMessage = 3;
const int messageHeight = 5;

const string sHeader = """
    ╔══════════════════════════════════════════════════╗
    ║          🏃 LABYRINTHE ASCII  C#  🏃             ║
    ╚══════════════════════════════════════════════════╝
    """;
const string sInstructions = "  [Z/↑] Haut   [S/↓] Bas   [Q/←] Gauche   [D/→] Droite   [Échap] Quitter";
const string sPressKey = "  Appuyez sur une key pour quitter...";

const ConsoleColor SuccessColor     = ConsoleColor.Green;
const ConsoleColor DangerColor      = ConsoleColor.Red;
const ConsoleColor InfoColor        = ConsoleColor.Cyan;
const ConsoleColor InstructionColor = ConsoleColor.DarkCyan;
const ConsoleColor WallColor        = ConsoleColor.DarkGray;
const ConsoleColor CorridorColor    = ConsoleColor.DarkBlue;
const ConsoleColor PlayerColor      = ConsoleColor.Yellow;
const ConsoleColor ExitColor        = ConsoleColor.Green;

var grid = new CellType[width, height];

var player = new Vec2d(0, 0);
var mode = State.Playing;

GenerateMaze(grid, player.X, player.Y);

var screen = new ConsoleScreen(grid, width, height, offsetX, offsetY,
    marginYMessage, messageHeight,
    WallColor, CorridorColor, PlayerColor, ExitColor,
    InfoColor, InstructionColor, SuccessColor, DangerColor,
    sHeader, sInstructions, sPressKey);

var controller = new KeyboardController();

screen.Draw();

while (mode == State.Playing)
{
    var (movement, quit) = controller.ReadInput();

    if (quit)
    {
        mode = State.Canceled;
        break;
    }

    var nextPos = player.Add(movement.X, movement.Y);

    if (nextPos.IsInBound(width, height) && grid[nextPos.X, nextPos.Y] != CellType.Wall)
    {
        if (grid[nextPos.X, nextPos.Y] == CellType.Exit) mode = State.Won;

        screen.UpdateCell(player.X, player.Y, CellType.Corridor);
        screen.UpdateCell(nextPos.X, nextPos.Y, CellType.Player);
        player = nextPos;
    }
}

screen.DrawEndScreen(mode == State.Won);
Console.CursorVisible = true;
Console.ReadKey(true);

bool InBound(int val, int max) => val >= 0 && val < max;

void GenerateMaze(CellType[,] grid, int playerStartX, int playerStartY)
{
    for (var y = 0; y < height; y++)
        for (var x = 0; x < width; x++)
            grid[x, y] = CellType.Wall;

    int[] dx = [ 0, 1, 0, -1 ];
    int[] dy = [ -1, 0, 1, 0 ];
    int[][] orders = [
        [ 0, 1, 2, 3 ], [ 0, 1, 3, 2 ], [ 0, 2, 1, 3 ], [ 0, 2, 3, 1 ], [ 0, 3, 1, 2 ], [ 0, 3, 2, 1 ],
        [ 1, 0, 2, 3 ], [ 1, 0, 3, 2 ], [ 1, 2, 0, 3 ], [ 1, 2, 3, 0 ], [ 1, 3, 0, 2 ], [ 1, 3, 2, 0 ],
        [ 2, 0, 1, 3 ], [ 2, 0, 3, 1 ], [ 2, 1, 0, 3 ], [ 2, 1, 3, 0 ], [ 2, 3, 0, 1 ], [ 2, 3, 1, 0 ],
        [ 3, 0, 1, 2 ], [ 3, 0, 2, 1 ], [ 3, 1, 0, 2 ], [ 3, 1, 2, 0 ], [ 3, 2, 0, 1 ], [ 3, 2, 1, 0 ]
    ];
    var rng = new Random();

    GenerateMazeRec(playerStartX, playerStartY);

    var outX = (width  - 1) & ~1;
    var outY = (height - 1) & ~1;

    grid[playerStartX, playerStartY] = CellType.Player;
    grid[outX, outY] = CellType.Exit;
    
    void GenerateMazeRec(int x, int y)
    {
        grid[x, y] = CellType.Corridor;
        foreach (var dir in orders[rng.Next(orders.Length)])
        {
            if( InMaze(x, dx[dir], width , out var nx) && 
                InMaze(y, dy[dir], height, out var ny) && 
                grid[nx, ny] == CellType.Wall)
            {
                grid[(x + nx) / 2, (y + ny) / 2] = CellType.Corridor;
                GenerateMazeRec(nx, ny);
            }
        }
        bool InMaze(int val, int delta, int max, out int next) => 
            InBound(next = val + delta * 2, max);
    }
}