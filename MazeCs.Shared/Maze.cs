namespace Epsi.MazeCs;

public class Maze
{
    private readonly Cell[,] grid;
    public int Width { get; }
    public int Height { get; }
    public Cell[,] Grid => grid;

    public Maze(IMazeGenerator mazeGenerator, int width, int height)
    {
        this.grid = mazeGenerator.Generate();
        this.Width = width;
        this.Height = height;
    }

    public Cell GetCell(int x, int y)
    {
        if (!new Vec2d(x, y).IsInBound(Width, Height))
            throw new ArgumentOutOfRangeException($"Position ({x}, {y}) est hors des limites ({Width}, {Height})");
        return grid[x, y];
    }

    public void SetCell(int x, int y, Cell cell)
    {
        if (!new Vec2d(x, y).IsInBound(Width, Height))
            throw new ArgumentOutOfRangeException($"Position ({x}, {y}) est hors des limites ({Width}, {Height})");
        grid[x, y] = cell;
    }

    public bool IsWall(int x, int y)
    {
        if (!new Vec2d(x, y).IsInBound(Width, Height))
            return true;
        return grid[x, y].IsWall;
    }

    public bool IsWall(Vec2d pos) => IsWall(pos.X, pos.Y);

    public void Draw(IGridDisplay screen)
    {
        screen.Draw();
        for (var y = 0; y < Height; y++)
        {
            for (var x = 0; x < Width; x++)
            {
                var cell = grid[x, y];
                if (cell is not Room room || !room.IsStart)
                {
                    screen.DrawMazeCell(x, y, cell);
                }
            }
        }
    }
}
