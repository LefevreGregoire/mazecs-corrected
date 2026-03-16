namespace Epsi.MazeCs;

public class Maze
{
    private readonly CellType[,] grid;
    public int Width { get; }
    public int Height { get; }
    public CellType[,] Grid => grid;

    public Maze(IMazeGenerator mazeGenerator, int width, int height)
    {
        this.grid = mazeGenerator.Generate();
        this.Width = width;
        this.Height = height;
    }

    public CellType GetCell(int x, int y)
    {
        if (!new Vec2d(x, y).IsInBound(Width, Height))
            throw new ArgumentOutOfRangeException($"Position ({x}, {y}) est hors des limites ({Width}, {Height})");
        return grid[x, y];
    }

    public void SetCell(int x, int y, CellType type)
    {
        if (!new Vec2d(x, y).IsInBound(Width, Height))
            throw new ArgumentOutOfRangeException($"Position ({x}, {y}) est hors des limites ({Width}, {Height})");
        grid[x, y] = type;
    }

    public bool IsWall(int x, int y)
    {
        if (!new Vec2d(x, y).IsInBound(Width, Height))
            return true;
        return grid[x, y] == CellType.Wall;
    }

    public bool IsWall(Vec2d pos) => IsWall(pos.X, pos.Y);

    public void Draw(ConsoleScreen screen)
    {
        screen.Draw();
        for (var y = 0; y < Height; y++)
        {
            for (var x = 0; x < Width; x++)
            {
                if (grid[x, y] != CellType.Start)
                {
                    screen.DrawMazeCell(x, y, grid[x, y]);
                }
            }
        }
    }
}
