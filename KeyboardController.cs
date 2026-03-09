namespace Epsi.MazeCs;

public class KeyboardController
{
    public (Vec2d movement, bool quit) ReadInput()
    {
        var key = Console.ReadKey(true).Key;
        var movement = Vec2d.Zero;
        var quit = false;

        switch (key)
        {
            case ConsoleKey.Z or ConsoleKey.UpArrow:
                movement = new Vec2d(0, -1);
                break;
            case ConsoleKey.S or ConsoleKey.DownArrow:
                movement = new Vec2d(0, 1);
                break;
            case ConsoleKey.Q or ConsoleKey.LeftArrow:
                movement = new Vec2d(-1, 0);
                break;
            case ConsoleKey.D or ConsoleKey.RightArrow:
                movement = new Vec2d(1, 0);
                break;
            case ConsoleKey.Escape:
                quit = true;
                break;
        }

        return (movement, quit);
    }
}
