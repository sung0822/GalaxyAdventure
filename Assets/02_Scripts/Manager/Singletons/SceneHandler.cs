using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneHandler : Singleton<SceneHandler>
{
    List<AsyncOperation> loadingScenes = new List<AsyncOperation>();
    [SerializeField] float waitTime = 0.1f;
    public AsyncOperation LoadMainSceneAsync(LoadSceneMode loadSceneMode)
    {
        AsyncOperation mainScene = SceneManager.LoadSceneAsync("Main_Logic", loadSceneMode);
        mainScene.allowSceneActivation = false;
        LoadingSceneManager.instance.nextSceneProgress = mainScene;
        loadingScenes.Add(mainScene);
        
        AsyncOperation uiScene = SceneManager.LoadSceneAsync("Main_UI", loadSceneMode);
        uiScene.allowSceneActivation = false;
        LoadingSceneManager.instance.nextSceneProgress = uiScene;
        loadingScenes.Add(uiScene);

        return mainScene;
    }

    public void LoadLoadingScene(AsyncOperation nextScene, LoadSceneMode loadSceneMode)
    {

        SceneManager.LoadScene("Loading",  loadSceneMode);
        LoadingSceneManager.instance.nextSceneProgress = nextScene;
        Debug.Log("로딩씬 로드됨");
    }

    public void UnloadLoadingScene(Func<bool> condition)
    {
        StartCoroutine(CoUnloadLoadingScene(condition));
    }
    IEnumerator CoUnloadLoadingScene(Func<bool> condition)
    {
        //yield return new WaitForSeconds(5.0f);
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
        Queue<AsyncOperation> loadingSceneBuffer = new Queue<AsyncOperation>();

        while (loadingScenes.Count > 0)
        {
            for (int i = 0; i < loadingScenes.Count; i++)
            {
                if (loadingScenes[i].isDone)
                {
                    loadingSceneBuffer.Enqueue(loadingScenes[i]);
                    loadingScenes.RemoveAt(i);
                }
            }
            yield return waitTime;
        }
        while (loadingSceneBuffer.Count > 0)
        {
            loadingSceneBuffer.Dequeue().allowSceneActivation = true;
        }
    }


}
