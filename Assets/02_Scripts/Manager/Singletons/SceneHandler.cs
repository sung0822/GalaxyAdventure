using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneHandler : Singleton<SceneHandler>
{
    public List<AsyncOperation> loadingScenes {get { return _loadingScenes;}}
    private List<AsyncOperation> _loadingScenes = new List<AsyncOperation>();
    [SerializeField] float waitTime = 0.1f;
    public AsyncOperation LoadMainSceneAsync(LoadSceneMode loadSceneMode)
    {
        AsyncOperation mainScene = SceneManager.LoadSceneAsync("Main_Logic", loadSceneMode);
        mainScene.allowSceneActivation = false;

        loadingScenes.Add(mainScene);
        
        AsyncOperation uiScene = SceneManager.LoadSceneAsync("Main_UI", loadSceneMode);
        uiScene.allowSceneActivation = false;
        
        loadingScenes.Add(uiScene);

        return mainScene;
    }

    public void LoadLoadingScene(AsyncOperation nextScene, LoadSceneMode loadSceneMode)
    {
        SceneManager.LoadScene("Loading",  loadSceneMode);
        LoadingSceneManager.nextSceneProgress = nextScene;
        Debug.Log("로딩씬 로드됨");
    }

    public void UnloadLoadingScene(Func<bool> condition)
    {
        StartCoroutine(CoUnloadLoadingScene(condition));
    }
    IEnumerator CoUnloadLoadingScene(Func<bool> condition)
    {
        yield return new WaitUntil(condition);
        SceneManager.UnloadSceneAsync("Loading");
        Debug.Log("로딩씬 언로드됨");
    }
    public void WaitUntilEverySceneIsOn()
    {
        StartCoroutine(CoWaitUntilEverySceneIsOn());
    }

    IEnumerator CoWaitUntilEverySceneIsOn()
    {
        Debug.Log("CoWaitUntilEverySceneIsOn 불려짐");
        Queue<AsyncOperation> sceneBuffer = new Queue<AsyncOperation>();

        while (loadingScenes.Count > 0)
        {
            for (int i = 0; i < loadingScenes.Count; i++)
            {
                if (loadingScenes[i].isDone)
                {
                    sceneBuffer.Enqueue(loadingScenes[i]);
                    loadingScenes.RemoveAt(i);
                }
            }
            yield return waitTime;
        }
        while (sceneBuffer.Count > 0)
        {
            Debug.Log("로딩 전부 완료");
            sceneBuffer.Dequeue().allowSceneActivation = true;
        }
    }
    public void LoadMenu()
    {
        Destroy(MainManager.instance);
        Destroy(BGMManager.instance);
        Destroy(InputManager.instance);
        Destroy(ItemManager.instance);
        Destroy(ObjectPoolManager.instance);
        Destroy(ParticleManager.instance);
        Destroy(EnemyFactory.instance);
        Destroy(BackGroundManager.instance);
        
        SceneManager.LoadScene("Menu");
    }


}
