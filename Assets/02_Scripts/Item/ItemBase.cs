using Unity.VisualScripting;



public abstract class ItemBase : IItemAttribute
{
    public ItemBase(UnitBase user)
    {
        this.user = user;
    }
    public abstract UnitBase user { get; set; }
    public abstract void Use();
    public abstract int id { get; }

    public abstract ItemType itemType { get;}

    public abstract ItemUsageType usageType { get; }

}
