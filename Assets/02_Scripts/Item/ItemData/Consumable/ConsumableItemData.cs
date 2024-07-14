public abstract class ConsumableItemData : ItemData
{
    public override ItemType itemType { get { return ItemType.Consumable; } }
    public virtual void SetStatus(UnitBase unitUser)
    {
        this.unitUser = unitUser;
    }
}