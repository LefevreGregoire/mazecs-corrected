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

        screen.UpdateCell(position.X, position.Y, new Room());
        position = nextPos;
        Draw();

        return true;
    }

    public bool IsAtExit()
    {
        var cell = maze.GetCell(position.X, position.Y);
        return cell is Room room && room.IsExit;
    }

    private void Draw()
    {
        var startRoom = new Room { IsStart = true };
        screen.UpdateCell(position.X, position.Y, startRoom);
    }
}
