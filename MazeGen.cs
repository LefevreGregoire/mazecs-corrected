namespace Epsi.MazeCs;

public class MazeGen : IMazeGenerator
{
    private readonly int width;
    private readonly int height;
    private readonly double coinProbability;

    public MazeGen(int width, int height, double coinProbability = 0.3)
    {
        this.width = width;
        this.height = height;
        this.coinProbability = Math.Clamp(coinProbability, 0.0, 1.0);
    }

    public Cell[,] Generate()
    {
        var grid = new Cell[width, height];

        for (var y = 0; y < height; y++)
            for (var x = 0; x < width; x++)
                grid[x, y] = new Wall();

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

        var startRoom = (Room)grid[0, 0];
        startRoom.IsStart = true;
        
        var exitRoom = (Room)grid[outX, outY];
        exitRoom.IsExit = true;

        // Add coins to rooms based on probability
        for (var y = 0; y < height; y++)
            for (var x = 0; x < width; x++)
                if (grid[x, y] is Room room && !room.IsStart && !room.IsExit)
                    if (rng.NextDouble() < coinProbability)
                        room.Collectables.Add(new Coin());

        return grid;

        void GenerateMazeRec(Cell[,] g, int x, int y)
        {
            var room = new Room();
            g[x, y] = room;
            
            foreach (var dir in orders[rng.Next(orders.Length)])
            {
                if (InMaze(x, dx[dir], width, out var nx) &&
                    InMaze(y, dy[dir], height, out var ny) &&
                    g[nx, ny] is Wall)
                {
                    g[(x + nx) / 2, (y + ny) / 2] = new Room();
                    GenerateMazeRec(g, nx, ny);
                }
            }

            static bool InMaze(int val, int delta, int max, out int next) =>
                InBound(next = val + delta * 2, max);

            static bool InBound(int val, int max) => val >= 0 && val < max;
        }
    }
}
