using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingSceneManager : MonoBehaviour
{
    [SerializeField] Image progressBar;
    static public AsyncOperation nextSceneProgress;
    
    [SerializeField] float UpdateCycle;
    
    private void Start() 
    {
        Debug.Log("Start불림");

        //SceneManager.LoadSceneAsync("Main_UI");
        StartCoroutine(UpdateSceneProcess());
    }

    IEnumerator UpdateSceneProcess()
    {
        AsyncOperation op = SceneManager.LoadSceneAsync("Main_Logic");
        AsyncOperation uiOp = SceneManager.LoadSceneAsync("Main_UI", LoadSceneMode.Additive);
        op.allowSceneActivation = false;

        nextSceneProgress = op;
        float timer = 0.0f;
        while (!nextSceneProgress.isDone)
        {
            timer += Time.deltaTime;
            if (nextSceneProgress.progress < 0.9f)
            {
                progressBar.fillAmount = nextSceneProgress.progress;
            }
            else
            {
                progressBar.fillAmount = Mathf.Lerp(progressBar.fillAmount, 1f, timer);
                if (progressBar.fillAmount == 1.0f)
                {
                    op.allowSceneActivation = true;
                    yield break;
                }
            }

            yield return null;
        }
    }


}
