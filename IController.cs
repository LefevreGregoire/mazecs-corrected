namespace Epsi.MazeCs;

public interface IController
{
    (Vec2d movement, bool quit) ReadInput();
}
