using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using Unity.VisualScripting;
using UnityEngine.SceneManagement;
using System.Collections;

public class MenuUIManager : Singleton<MenuUIManager>
{

    public Button startButton;
    public Button ReadMeButton;

    private UnityAction action;

    private void Start()
    {
        action = () => OnStartClick();
        startButton.onClick.AddListener(action);

        ReadMeButton.onClick.AddListener(delegate {OnStartClick();});

    }

    public void OnStartClick()
    {
        Debug.Log("OnStartClick 호출됨");
        AsyncOperation mainScene = SceneHandler.instance.LoadMainSceneAsync(LoadSceneMode.Additive);
        SceneHandler.instance.LoadLoadingScene(mainScene, LoadSceneMode.Additive);
        SceneHandler.instance.UnloadLoadingScene(() => mainScene.allowSceneActivation == false);
        SceneHandler.instance.WaitUntilEverySceneIsOn();
        StartCoroutine(UnloadMenuScene()); // 코루틴으로 호출
    }

    IEnumerator UnloadMenuScene()
    {
        yield return new WaitUntil(() => SceneHandler.instance.loadingScenes.Count == 0);
        SceneManager.UnloadSceneAsync("Menu");
        Debug.Log("Menu 씬 언로드됨");
    }

    public void OnReadMeClick()
    {

    }


}
