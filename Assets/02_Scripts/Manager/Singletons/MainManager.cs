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
    List<IStage> stages = new List<IStage>();
    int currentStageIdx = 0;
    public int score 
    {
        get { return _score; }
    }
    Stage1 stage1;
    [SerializeField] int _score;
    public GameObject cloudManagerPrefab = null;

    public GameObject particleManagerPrefab = null;

    public GameObject inputManagerPrefab = null;

    public GameObject bgmManagerPrefab = null;

    public GameObject itemManagerPrefab = null;

    public GameObject soundManagerPrefab = null;

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

        GameObject audioManager = Instantiate<GameObject>(bgmManagerPrefab, transform);
        audioManager.name = bgmManagerPrefab.name;

        GameObject itemManager = Instantiate<GameObject>(itemManagerPrefab, transform);
        itemManager.name = itemManagerPrefab.name;


        GameObject soundManager = Instantiate<GameObject>(soundManagerPrefab, transform);
        itemManager.name = itemManagerPrefab.name;
        /////////////////////////////////////////////////////////////////////////////////////////


        stages.Add(new Stage1());
        stages.Add(new Stage2());
        stages.Add(new Stage3());
        stages.Add(new Stage4());
        stages.Add(new StageBoss());


        BGMManager.instance.PlayBGM(BGMManager.instance.bgm1);
        currentStage = stages[currentStageIdx];

    }

    private void Update()
    {
        currentStage.Execute();
    }

    public void AddScore(int score)
    {
        this._score += score;
        CheckStage();
    }

    public void CheckStage()
    {
        switch (currentStageIdx)
        { 
            case 0:
                if (_score < 1000)
                {
                    break;
                }
                currentStageIdx++;
                currentStage.StopGenerating();
                currentStage = stages[currentStageIdx];


                break;
            case 1:
                if (_score < 5000)
                {
                    break;
                }
                currentStageIdx++;
                currentStage.StopGenerating();
                currentStage = stages[currentStageIdx];

                break;
            case 2:
                if (_score < 10000)
                {
                    break;
                }
                currentStageIdx++;
                currentStage.StopGenerating();
                currentStage = stages[currentStageIdx];

                break;
            case 3:
                if (_score < 15000)
                {
                    break;
                }
                currentStageIdx++;
                currentStage.StopGenerating();
                currentStage = stages[currentStageIdx];

                break;

            case 4:
                Debug.Log("보스 스테이지임.");
                break;
            default:
                break;
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

    public void EndLevel()
    {
        Time.timeScale = 0f;
        UIManager.instance.ShowEndLevelPanel();
    }


}
