namespace Epsi.MazeCs;

public interface IGridDisplay
{
    void Draw();
    void UpdateCell(int cx, int cy, CellType type);
    void DrawMazeCell(int cx, int cy, CellType type);
}
