namespace Epsi.MazeCs;

public class PointsChangedEventArgs : EventArgs
{
    public int OldPoints { get; }
    public int NewPoints { get; }

    public PointsChangedEventArgs(int oldPoints, int newPoints)
    {
        OldPoints = oldPoints;
        NewPoints = newPoints;
    }
}
