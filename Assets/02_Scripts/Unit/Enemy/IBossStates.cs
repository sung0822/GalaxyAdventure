public interface IBossPageState
{
    Boss boss { get; set; }
    void Attack();
}
public class BossPageOneState : IBossPageState
{
    public Boss boss { get; set; }
    public void Attack()
    {
        boss.sprayGun.Use();
    }

}
public class BossPageTwoState : IBossPageState
{
    public Boss boss { get; set; }
    public void Attack()
    {
        boss.sprayGun.Use();
        boss.machineGunWeaponSpace.transform.LookAt(boss.targetPlayer);
        boss.machineGun.Use();
    }
}
public class BossPageThreeState : IBossPageState
{
    public Boss boss { get; set; }

    public void Attack()
    {

    }
}