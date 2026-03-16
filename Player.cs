namespace Epsi.MazeCs;

public class Player
{
    private Vec2d position;
    private readonly Maze maze;
    private readonly IGridDisplay screen;

    public Vec2d Position => position;

    public Player(Maze maze, IGridDisplay screen)
    {
        this.maze = maze;
        this.screen = screen;
        this.position = new Vec2d(0, 0);
        Draw();
    }

    public bool TryMove(Vec2d movement)
    {
        var nextPos = position.Add(movement.X, movement.Y);

        if (!nextPos.IsInBound(maze.Width, maze.Height))
            return false;

        if (maze.IsWall(nextPos))
            return false;

        screen.UpdateCell(position.X, position.Y, CellType.Corridor);
        position = nextPos;
        Draw();

        return true;
    }

    public bool IsAtExit()
    {
        return maze.GetCell(position.X, position.Y) == CellType.Exit;
    }

    private void Draw()
    {
        screen.UpdateCell(position.X, position.Y, CellType.Start);
    }
}
