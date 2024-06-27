using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage1 : MonoBehaviour, IStage
{
    GameObject BasicEnemyPrefab;

    public IPlayer player { get ; set ; }
    public float timeElapsed { get { return _timeElapsed; } set { _timeElapsed = value; } }

    protected float _timeElapsed = 0;
    public void Execute()
    {
        _timeElapsed += Time.deltaTime;

        if (_timeElapsed >= 1) 
        {
            Debug.Log("tlqkf");
            var hello = Instantiate<GameObject>(BasicEnemyPrefab, transform);
            _timeElapsed = 0;
        }
    }

    void Start()
    {
        BasicEnemyPrefab = Resources.Load<GameObject>("Enemies/BasicEnemy");
        if ( BasicEnemyPrefab == null )
        {
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
    
    void Instantiate1()
    {
        Instantiate(BasicEnemyPrefab);
    }
}
