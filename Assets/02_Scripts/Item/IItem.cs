
using Unity.VisualScripting;

public interface IItem
{
    Unit user { get; set; }
    public void Use();
    public int id { get;}
}
