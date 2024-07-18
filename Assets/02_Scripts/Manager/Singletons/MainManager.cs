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

    public GameObject mainStage { get { return _mainStage; } set { _mainStage = value; } }
    [SerializeField] GameObject _mainStage = null;

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


        /////////////////////////////////////////////////////////////////////////////////////////


        BGMManager.instance.PlayBGM(BGMManager.instance.bgm1);
        BackGroundManager.instance.SetCloudPointsGroup();
        BackGroundManager.instance.CreateClouds();
        backgroundMaterial = backgroundRenderer.material;





        // 스테이지 객체 캐싱
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
        
        // 메인 스테이지 움직임
        _mainStage.transform.Translate(0, 0, 1 * _moveSpd * Time.deltaTime);
    }

    public void AddScore(int score)
    {
        if (currentStageIdx >= stages.Count - 1)
        {
            return;
        }

        this._score += score;
        _moveSpd += score * 0.001f;
        float colorPercent = ((float)_score / 15000) * 0.5f;
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
                if (_score < 1000)
                    return;

                break;
            case 1:
                if (_score < 5000)
                    return;

                break;
            case 2:
                if (_score < 10000)
                    return;
                BackGroundManager.instance.CreateRocks();

                break;
            case 3:
                if (_score < 15000)
                    return;
                BackGroundManager.instance.StopRockMoving();
                BackGroundManager.instance.StopCloudMoving();
                StartCoroutine(AdjustSpeed(0, 2.0f));

                break;
            case 4:
                Debug.Log("보스 스테이지임.");
                return;
            default:
                break;
        }
        ChangeNextStage();

    }
    void ChangeNextStage()
    {
        currentStageIdx++;
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

            // 정규화한 길이.
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

            // 정규화한 길이.
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
