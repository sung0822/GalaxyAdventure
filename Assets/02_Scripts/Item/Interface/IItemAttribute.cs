public enum ItemType
{
    Consumable,
    Weapon,
    Projectile
}

public enum ItemUsageType
{
    ImmediatelyUse,
    NotImmediatelyUse
}

public interface IItemAttribute
{
    public ItemType itemType { get; }
    public ItemUsageType usageType { get; }
    public UnitBase user { get; set; }
    public void Use();
    public int id { get; }

}
