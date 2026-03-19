namespace Epsi.MazeCs;

public class Key : ICollectable
{
    public int KeyId { get; }
    public int Points => 0;
    public bool IsPersistent => true;

    public Key(int keyId = 0)
    {
        KeyId = keyId;
    }
}
