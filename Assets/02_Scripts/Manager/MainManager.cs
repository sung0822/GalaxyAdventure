using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MainManager : MonoBehaviour
{
    public static MainManager instance = null;

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
    public static MainManager Get() { return instance; }

    IStage currentStage = null;
    public int score { get { return _score; } set { _score = value; } }
    Stage1 stage1;
    [SerializeField] int _score;    
    GameObject cloudManagerPrefab;
    CloudManager cloudManager = null;
    private void Start()
    {
        Debug.Log("MainManager Start");
        cloudManagerPrefab = Resources.Load<GameObject>("Managers/CloudManager");
        GameObject cloudManager = Instantiate<GameObject>(cloudManagerPrefab, transform);
        cloudManager.name = cloudManagerPrefab.name;
        
        GameObject stage1Object = new GameObject("Stage1Object");
        stage1Object.transform.SetParent(transform);

        stage1 = stage1Object.AddComponent<Stage1>();
        currentStage = stage1;
    }

    private void Update()
    {
        currentStage.Execute();
    }


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
