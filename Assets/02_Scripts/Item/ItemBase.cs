using Unity.VisualScripting;


enum ItemType
{
    Consumable,
    Weapon,
}

public abstract class ItemBase
{
    public abstract UnitBase user { get; set; }
    public abstract void Use();
    public abstract int id { get; }

    //public abstract ItemType itemType { get; set; }

    //public ItemType itemType { get; set; }
}