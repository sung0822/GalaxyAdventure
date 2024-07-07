
using Unity.VisualScripting;

public abstract class ItemBase
{
    UnitBase user { get; set; }
    public abstract void Use();
    public int id { get { return _id; } }
    protected abstract int _id { get; set; }

}
