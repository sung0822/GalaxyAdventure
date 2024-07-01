using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

// 컴포넌트를 명시해 삭제되는걸 방지한다는데 솔직히 작동원리는 모름
[RequireComponent(typeof(AudioListener))]
public class MainManager : MonoBehaviour
{
    public static MainManager instance = null;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            if (instance == this)
            {
                return;
            }
            Destroy(this.gameObject);
        }
    }

    public static MainManager Get() { return instance; }

    IStage currentStage = null;
    public int score 
    {
        get { return _score; }
        set 
        {
            _score = value;
        } 
    }
    Stage1 stage1;
    [SerializeField] int _score;
    public GameObject cloudManagerPrefab = null;

    public GameObject particleManagerPrefab = null;

    public GameObject inputManagerPrefab = null;

    public GameObject audioManagerPrefab = null;

    bool isPaused = false;
    private void Start()
    {
        ///////////////////////////////////////////////////////////////////////////////////////
        //cloudManagerPrefab = Resources.Load<GameObject>("Managers/CloudManager");
        GameObject cloudManager = Instantiate<GameObject>(cloudManagerPrefab, transform);
        cloudManager.name = cloudManagerPrefab.name;
        
        GameObject particleManager = Instantiate<GameObject>(particleManagerPrefab, transform);
        particleManager.name = particleManagerPrefab.name;

        GameObject inputManager = Instantiate<GameObject>(inputManagerPrefab, transform);
        inputManager.name = inputManagerPrefab.name;

        GameObject audioManager = Instantiate<GameObject>(audioManagerPrefab, transform);
        audioManager.name = audioManagerPrefab.name;

        /////////////////////////////////////////////////////////////////////////////////////////

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

    public void SwitchPauseStat()
    {
        isPaused = !isPaused;
        
        if (isPaused) 
        {
            Time.timeScale = 0f;
        }
        else if (!isPaused)
        {
            Time.timeScale = 1.0f;
        }
        
        UIManager.instance.SwitchPausePanel(isPaused);

    }


}
