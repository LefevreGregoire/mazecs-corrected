namespace Epsi.MazeCs;

public class MazeGen : IMazeGenerator
{
    private readonly int width;
    private readonly int height;

    public MazeGen(int width, int height)
    {
        this.width = width;
        this.height = height;
    }

    public CellType[,] Generate()
    {
        var grid = new CellType[width, height];

        for (var y = 0; y < height; y++)
            for (var x = 0; x < width; x++)
                grid[x, y] = CellType.Wall;

        int[] dx = [0, 1, 0, -1];
        int[] dy = [-1, 0, 1, 0];
        int[][] orders = [
            [0, 1, 2, 3], [0, 1, 3, 2], [0, 2, 1, 3], [0, 2, 3, 1], [0, 3, 1, 2], [0, 3, 2, 1],
            [1, 0, 2, 3], [1, 0, 3, 2], [1, 2, 0, 3], [1, 2, 3, 0], [1, 3, 0, 2], [1, 3, 2, 0],
            [2, 0, 1, 3], [2, 0, 3, 1], [2, 1, 0, 3], [2, 1, 3, 0], [2, 3, 0, 1], [2, 3, 1, 0],
            [3, 0, 1, 2], [3, 0, 2, 1], [3, 1, 0, 2], [3, 1, 2, 0], [3, 2, 0, 1], [3, 2, 1, 0]
        ];
        var rng = new Random();

        GenerateMazeRec(grid, 0, 0);

        var outX = (width - 1) & ~1;
        var outY = (height - 1) & ~1;

        grid[0, 0] = CellType.Start;
        grid[outX, outY] = CellType.Exit;

        return grid;

        void GenerateMazeRec(CellType[,] g, int x, int y)
        {
            g[x, y] = CellType.Corridor;
            foreach (var dir in orders[rng.Next(orders.Length)])
            {
                if (InMaze(x, dx[dir], width, out var nx) &&
                    InMaze(y, dy[dir], height, out var ny) &&
                    g[nx, ny] == CellType.Wall)
                {
                    g[(x + nx) / 2, (y + ny) / 2] = CellType.Corridor;
                    GenerateMazeRec(g, nx, ny);
                }
            }

            static bool InMaze(int val, int delta, int max, out int next) =>
                InBound(next = val + delta * 2, max);

            static bool InBound(int val, int max) => val >= 0 && val < max;
        }
    }
}
