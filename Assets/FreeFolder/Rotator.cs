using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    public float spd = 10;

    MeshRenderer meshRenderer;
    bool isInvincibilityBlinking;

    float time_elapsed = 0;
    void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        Color color = meshRenderer.material.color;
        color.a = 1;
        meshRenderer.material.color = color;
        isInvincibilityBlinking = true;
        StartCoroutine(InvincibilityBlink());
    }

    void Update()
    {
        time_elapsed += Time.deltaTime;
        transform.RotateAround(new Vector3(10, 0, 0), Vector3.up, 10 * spd * Time.deltaTime);
    }
    IEnumerator InvincibilityBlink()
    {
        meshRenderer.material.SetFloat("_Mode", 3);

        while (isInvincibilityBlinking)
        {
            //반투명으로 변경
            if (meshRenderer.material.color.a == 1.0f)
            {
                Color color = meshRenderer.material.color;
                color.a = 0.6f;
                meshRenderer.material.color = color;
                meshRenderer.material.renderQueue = 3000;
            }// 불투명으로 변경
            else
            {
                Color color = meshRenderer.material.color;
                color.a = 1.0f;
                meshRenderer.material.color = color;
                meshRenderer.material.renderQueue = 2000;
                if (time_elapsed >= 10)
                {
                    isInvincibilityBlinking = false;
                }
            }

            yield return new WaitForSeconds(0.4f);
        }

        meshRenderer.material.SetFloat("_Mode", 0);

    }

}
