using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour
{

    public float moveSpd = 10;


    void Start()
    {
    }

    void Update()
    {
        transform.Translate(0, 0, -moveSpd * Time.deltaTime, Space.World);
    }

    private void OnCollisionEnter(Collision collision)
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "CloudChecker")
        {
            BackGroundManager.instance.SpawnBackObject(gameObject);
        }
    }


    private void OnDestroy()
    {
    }

    static public void InitClouds()
    {
    }

    public void AdjustSpd(float spd, float duration)
    {
        StartCoroutine(LerpSpd(spd, duration));
    }
    IEnumerator LerpSpd(float spd, float duration)
    {
        float timeAdjustingSpd = 0;
        float originalMoveSpd = moveSpd;


        while (true)
        {
            Debug.Log("LerpSpd È£ÃâÁß moveSpd is " + moveSpd + "gameObjectName is: " + gameObject.name);
            timeAdjustingSpd += Time.deltaTime;

            float normalizedTime = timeAdjustingSpd / duration;

            moveSpd = Mathf.Lerp(originalMoveSpd, spd, normalizedTime);

            if (normalizedTime >= 1)
            {
                break;
            }
            yield return new WaitForEndOfFrame();
        }
    }
}
