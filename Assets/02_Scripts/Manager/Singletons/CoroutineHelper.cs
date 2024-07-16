using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoroutineHelper : MonoBehaviour
{
    public static CoroutineHelper instance = null;

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

    public Coroutine RunCoroutine(IEnumerator enumerator)
    {
        return StartCoroutine(enumerator);
    }

    public void StopRunningCoroutine(IEnumerator enumerator)
    {
        StopCoroutine(enumerator);
    }

    public void StopRunningCoroutine(Coroutine enumerator)
    {
        StopCoroutine(enumerator);
    }

}
