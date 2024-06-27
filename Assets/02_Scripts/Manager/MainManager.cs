using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MainManager : MonoBehaviour
{
    public static MainManager instance = null;

    GameObject cloudManagerPrefab;

    IStage currentStage = null;

    Stage1 stage1 = new Stage1();

    CloudManager cloudManager = null;
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
    public int score { get { return _score; } set { _score = value; } }
    [SerializeField] int _score;    

    private void Start()
    {
        Debug.Log("MainManager Start");
        cloudManagerPrefab = Resources.Load<GameObject>("Managers/CloudManager");
        GameObject cloudManager = Instantiate<GameObject>(cloudManagerPrefab, transform);
        cloudManager.name = cloudManagerPrefab.name;
        currentStage = stage1;
    }

    private void Update()
    {
        currentStage.Execute();
    }

    public static MainManager Get() { return instance; }

    public void CheckStage()
    {
        if (_score <= 1000)
        {
            currentStage = stage1;
        }
        else if (_score >= 1000)
        {

        }
        else if (_score >= 3000)
        {

        }
        else if (_score >= 7000)
        {
        }
        else if(_score >= 10000)
        {

        }

    }

}
