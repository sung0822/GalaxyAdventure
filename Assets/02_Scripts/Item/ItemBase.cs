using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;


public abstract class ItemBase : IItemAttribute
{
    public abstract ItemType itemType { get; }
    public abstract ItemUsageType usageType { get; }
    public abstract UnitBase user { get; set; }
    public abstract int id { get; }

    public ItemBase(UnitBase user)
    {
        this.user = user;
    }
    public ItemBase()
    {
        Debug.Log("�θ� �⺻ ������ ȣ���");
        this.user = null;
    }

    public abstract void Use();
}
