using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Puppet : UnitBase
{
    public override TeamType teamType { get { return _currentPuppetData.teamType; } set { _currentPuppetData.teamType = value; } }
    public Image hpBar { get { return _hpBar; } set { _hpBar = value; } }
    [SerializeField] Image _hpBar;
    public TextMeshProUGUI hpText { get { return _hpText; } set { _hpText = value; } }
    [SerializeField] TextMeshProUGUI _hpText;

    public UnitBaseData currentPuppetData { get { return _currentPuppetData; } set { _currentPuppetData = value; } }
    UnitBaseData _currentPuppetData;

    protected override void Awake()
    {
        base.Awake();
    }

    protected override void SetFirstStatus()
    {
        base.SetFirstStatus();
        _currentPuppetData = (UnitBaseData)currentUnitBaseData;
        CheckHpBarFill();
    }

    public override void Hit(int damage)
    {
        if (_currentPuppetData.isImmortal)
            return;

        Debug.Log("기본 Hit 호출");
        _currentPuppetData.currentHp -= damage;
        CheckHpBarFill();

        GameObject particle = ParticleManager.instance.CreateParticle(ParticleManager.instance.basicParticle, this.transform.position, this.transform.rotation);
        Destroy(particle, 0.7f);

        CheckDead();
    }

    public override void Hit(int damage, Vector3 position)
    {
        if (_currentPuppetData.isImmortal)
            return;

        Debug.Log("position Hit 호출");
        _currentPuppetData.currentHp -= damage;
        CheckHpBarFill();

        GameObject particle = ParticleManager.instance.CreateParticle(ParticleManager.instance.basicParticle, position, Quaternion.Euler(0, 0, 0));
        Destroy(particle, 0.7f);

        CheckDead();
    }

    public override void Hit(int damage, Transform hitTransform)
    {
        if (_currentPuppetData.isImmortal)
            return;


        Debug.Log("transform Hit 호출");
        _currentPuppetData.currentHp -= damage;
        CheckHpBarFill();

        GameObject particle = ParticleManager.instance.CreateParticle(ParticleManager.instance.basicParticle, hitTransform.position, Quaternion.Euler(0, 0, 0));
        Destroy(particle, 0.7f);

        CheckDead();
    }

    void CheckHpBarFill()
    {
        hpBar.fillAmount = (float)_currentPuppetData.currentHp / (float)_currentPuppetData.currentMaxHp;
        hpText.text = _currentPuppetData.currentHp.ToString() + "/" + _currentPuppetData.currentMaxHp.ToString();
    }

}
