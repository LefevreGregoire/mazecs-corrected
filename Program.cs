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

var mazeGen = new MazeGen(width, height);
var maze = new Maze(mazeGen, width, height);

var screen = new ConsoleScreen(maze.Grid, width, height, offsetX, offsetY,
    marginYMessage, messageHeight,
    WallColor, CorridorColor, PlayerColor, ExitColor,
    InfoColor, InstructionColor, SuccessColor, DangerColor,
    sHeader, sInstructions, sPressKey);

maze.Draw(screen);

var player = new Player(maze, screen);
var controller = new KeyboardController();

var mode = State.Playing;

while (mode == State.Playing)
{
    var (movement, quit) = controller.ReadInput();

    if (quit)
    {
        mode = State.Canceled;
        break;
    }

    if (player.TryMove(movement))
    {
        if (player.IsAtExit())
        {
            mode = State.Won;
        }
    }
}

screen.DrawEndScreen(mode == State.Won);
Console.CursorVisible = true;
Console.ReadKey(true);