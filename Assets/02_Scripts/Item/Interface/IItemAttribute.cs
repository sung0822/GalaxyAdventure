public enum ItemType
{
    Consumable,
    Weapon,
}

public enum ItemUsageType
{
    ImmediatelyUse,
    NotImmediatelyUse
}

public interface IItemAttribute
{
    public abstract ItemType itemType { get; }
    public abstract ItemUsageType usageType { get; }
}
