namespace Epsi.MazeCs;

public class Player
{
    private Vec2d position;
    private readonly Maze maze;
    private readonly IGridDisplay screen;
    private int points;
    private readonly List<ICollectable> inventory;

    public Vec2d Position => position;
    public int Points => points;
    public IReadOnlyList<ICollectable> Inventory => inventory.AsReadOnly();
    public const string PlayerSymbol = "🏃";

    // Events
    public event EventHandler<PointsChangedEventArgs>? PointsChanged;
    public event EventHandler<InventoryChangedEventArgs>? InventoryChanged;

    public Player(Maze maze, IGridDisplay screen)
    {
        this.maze = maze;
        this.screen = screen;
        this.position = new Vec2d(0, 0);
        this.points = 0;
        this.inventory = new List<ICollectable>();
        Draw();
    }

    public bool TryMove(Vec2d movement)
    {
        var nextPos = position.Add(movement.X, movement.Y);

        if (!nextPos.IsInBound(maze.Width, maze.Height))
            return false;

        var nextCell = maze.GetCell(nextPos.X, nextPos.Y);
        
        // Check if we can pass through this cell
        if (!CanPass(nextCell))
            return false;

        screen.UpdateCell(position.X, position.Y, new Room());
        position = nextPos;
        Draw();

        return true;
    }

    private bool CanPass(Cell cell)
    {
        // Can't pass through walls
        if (cell.IsWall)
        {
            // But can pass through doors if we have the key
            if (cell is Door door)
            {
                return HasKey(door.DoorId);
            }
            return false;
        }
        return true;
    }

    private bool HasKey(int doorId)
    {
        return inventory.Any(item => item is Key key && key.KeyId == doorId);
    }

    public bool IsAtExit()
    {
        var cell = maze.GetCell(position.X, position.Y);
        return cell is Room room && room.IsExit;
    }

    public void Pickup()
    {
        var cell = maze.GetCell(position.X, position.Y);
        if (cell is not Room room)
            return;

        if (room.Collectables.Count == 0)
            return;

        // Collect all items in the current room
        foreach (var collectable in room.Collectables)
        {
            // Update points
            int oldPoints = points;
            points += collectable.Points;
            PointsChanged?.Invoke(this, new PointsChangedEventArgs(oldPoints, points));

            // Add persistent items to inventory
            if (collectable.IsPersistent)
            {
                inventory.Add(collectable);
                InventoryChanged?.Invoke(this, new InventoryChangedEventArgs(inventory.AsReadOnly()));
            }
        }

        // Clear the room's collectables
        room.Collectables.Clear();
    }

    public void Draw(IGridDisplay display)
    {
        var startRoom = new Room { IsStart = true };
        display.UpdateCell(position.X, position.Y, startRoom);
    }

    private void Draw()
    {
        var startRoom = new Room { IsStart = true };
        screen.UpdateCell(position.X, position.Y, startRoom);
    }
}
