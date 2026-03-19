namespace Epsi.MazeCs;

public class InventoryChangedEventArgs : EventArgs
{
    public IReadOnlyList<ICollectable> Inventory { get; }

    public InventoryChangedEventArgs(IReadOnlyList<ICollectable> inventory)
    {
        Inventory = inventory;
    }
}
