using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadingSceneManager : MonoBehaviour
{
    [SerializeField] Image progressBar;
    static public AsyncOperation nextSceneProgress;
    
    [SerializeField] float UpdateCycle;
    
    private void Start() 
    {
        Debug.Log("Start불림");
        StartCoroutine(UpdateSceneProcess());
    }

    IEnumerator UpdateSceneProcess()
    {
        while (!nextSceneProgress.isDone)
        {
            if (nextSceneProgress != null)
            {
                Debug.Log("로딩씬 프로세스");
                progressBar.fillAmount = nextSceneProgress.progress;    
            }
            else
            {
                Debug.Log("다음 씬이 없습니다.");
            }

            yield return new WaitForSeconds(UpdateCycle);
        }
    }


}
