

public interface IPlayer
{
    public float currentLevel { get; set; }
    public float currentExp { get; set; }
    public float maxExp { get; set; }

    public void GivePlayerExp(float exp);


}
