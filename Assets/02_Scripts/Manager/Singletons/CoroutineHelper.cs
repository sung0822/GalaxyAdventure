using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoroutineHelper : Singleton<CoroutineHelper>
{

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
