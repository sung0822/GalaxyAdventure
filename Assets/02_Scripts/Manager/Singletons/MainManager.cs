using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

// ������Ʈ�� ������ �����Ǵ°� �����Ѵٴµ� ������ �۵������� ��
[RequireComponent(typeof(AudioListener))]
public class MainManager : Singleton<MainManager>
{

    IStage currentStage = null;
    List<IStage> stages = new List<IStage>();
    int currentStageIdx = 0;
    public int currentScore { get { return _currentScore; } }
    Stage1 stage1;
    [SerializeField] int _currentScore;

    public int maxScore { get { return _maxScore; }}
    [SerializeField] int _maxScore;

    public GameObject cloudManagerPrefab = null;

    public GameObject particleManagerPrefab = null;

    public GameObject inputManagerPrefab = null;

    public GameObject bgmManagerPrefab = null;

    public GameObject itemManagerPrefab = null;

    public GameObject soundManagerPrefab = null;

    public GameObject mainCamera { get { return _mainCamera; } set { _mainCamera = value; } }
    [SerializeField] GameObject _mainCamera = null;


    public float moveSpd { get { return _moveSpd; } set { _moveSpd = value; } }
    [SerializeField] float _moveSpd;

    public GameObject background { get { return _background; } set { _background = value; } }
    [SerializeField] GameObject _background;

    [SerializeField] MeshRenderer backgroundRenderer;
    [SerializeField] Material backgroundMaterial;
    [SerializeField] Color changedBackgroundColor;

    bool isPaused = false;

    private void Start()
    {
        Debug.Log("���θŴ��� ��ŸƮ");
        ///////////////////////////////////////////////////////////////////////////////////////
        //cloudManagerPrefab = Resources.Load<GameObject>("Managers/CloudManager");
        GameObject cloudManager = Instantiate<GameObject>(cloudManagerPrefab, transform);
        cloudManager.name = cloudManagerPrefab.name;
        
        GameObject particleManager = Instantiate<GameObject>(particleManagerPrefab, transform);
        particleManager.name = particleManagerPrefab.name;

            GameObject particleManager = Instantiate<GameObject>(particleManagerPrefab, transform);
            particleManager.name = particleManagerPrefab.name;

            GameObject inputManager = Instantiate<GameObject>(inputManagerPrefab, transform);
            inputManager.name = inputManagerPrefab.name;

            GameObject audioManager = Instantiate<GameObject>(bgmManagerPrefab, transform);
            audioManager.name = bgmManagerPrefab.name;

            GameObject itemManager = Instantiate<GameObject>(itemManagerPrefab, transform);
            itemManager.name = itemManagerPrefab.name;


            /////////////////////////////////////////////////////////////////////////////////////////


            BGMManager.instance.StartBGM();
            BackGroundManager.instance.SetCloudPointsGroup();
            BackGroundManager.instance.CreateClouds();
            backgroundMaterial = backgroundRenderer.material;

        }
        // �������� ��ü ĳ��
        stages.Add(new Stage1());
        stages.Add(new Stage2());
        stages.Add(new Stage3());
        stages.Add(new Stage4());
        stages.Add(new StageBoss());
        currentStage = stages[currentStageIdx];
    }

    private void Update()
    {
        currentStage.Execute();
    }

    public void AddScore(int score)
    {
        if (currentStageIdx >= stages.Count - 1)
        {
            return;
        }

        this._currentScore += score;
        _moveSpd += score * 0.001f;
        float colorPercent = ((float)_currentScore / 15000) * 0.5f;
        Debug.Log("colorPercent: " + colorPercent);
        Color color = Color.Lerp(backgroundMaterial.color, changedBackgroundColor, colorPercent);
        StartCoroutine(AdjustBackgroundColor(color, 0.3f));

        CheckStage();
    }

    public void CheckStage()
    {
        switch (currentStageIdx)
        { 
            case 0:
                if (_currentScore < 1000)
                {
                    return;
                }

                break;
            case 1:
                if (_currentScore < 5000)
                    return;

                break;
            case 2:
                if (_currentScore < 10000)
                    return;
                BackGroundManager.instance.CreateRocks();

                break;
            case 3:
                if (_currentScore < _maxScore)
                    return;
                BackGroundManager.instance.StopRockMoving();
                BackGroundManager.instance.StopCloudMoving();
                BGMManager.instance.ChangeBGM(BGMManager.instance.bossBgm);
                StartCoroutine(AdjustSpeed(0, 2.0f));

                break;
            case 4:
                Debug.Log("���� ����������.");
                return;
            default:
                break;
        }
        Debug.Log(currentScore);
        Debug.Log(currentStageIdx);
        ChangeNextStage();

    }
    void ChangeNextStage()
    {
        currentStageIdx++;
        if (currentStage == null)
        {
            return;
        }
        currentStage.StopGenerating();
        currentStage = stages[currentStageIdx];
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
        UIManager.instance.DisplyEndLevelPanel();
    }

    protected virtual IEnumerator AdjustSpeed(float spd, float duration)
    {
        yield return new WaitForSeconds(4.0f);
        float timeAdjustingSpd = 0;
        float originalMoveSpd = _moveSpd;

        while (true)
        {
            timeAdjustingSpd += Time.deltaTime;

            // ����ȭ�� ����.
            float normalizedTime = timeAdjustingSpd / duration;

            if (normalizedTime >= 1)
            {
                break;
            }

            _moveSpd = Mathf.Lerp(originalMoveSpd, spd, normalizedTime);

            yield return new WaitForEndOfFrame();
        }
    }

    protected virtual IEnumerator AdjustBackgroundColor(Color color, float duration)
    {
        float timeAdjustingSpd = 0;
        Color originalColor = backgroundMaterial.color;

        while (true)
        {
            timeAdjustingSpd += Time.deltaTime;

            // ����ȭ�� ����.
            float normalizedTime = timeAdjustingSpd / duration;

            if (normalizedTime >= 1)
            {
                break;
            }
            
            backgroundMaterial.color = Color.Lerp(originalColor, color, normalizedTime);

            yield return new WaitForEndOfFrame();
        }
    }
}
