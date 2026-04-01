namespace Epsi.MazeCs;

public interface IGridDisplay
{
    void Draw();
    void UpdateCell(int cx, int cy, Cell cell);
    void DrawMazeCell(int cx, int cy, Cell cell);
}
