using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using Unity.VisualScripting;
using UnityEngine.SceneManagement;

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
        SceneManager.UnloadScene("Menu");
    }

    public void OnReadMeClick()
    {

    }


}
