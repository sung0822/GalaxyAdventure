using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class RenderTexture : MonoBehaviour
{
    [SerializeField] float rotSpd = 100;
    [SerializeField] float moveDistance = 10;
    [SerializeField] float moveSpd = 0.15f;
    [SerializeField] float risePos = 0.3f;
    private float previousRot = 0;

    bool isRising = true;
    void Update()
    {
        transform.Rotate(0, rotSpd * Time.deltaTime, 0);
        
        if (transform.localPosition.y >= risePos)
            isRising = false;
        else if (transform.localPosition.y <= 0f)
            isRising = true;

        if (isRising)
            transform.localPosition = new Vector3(0, transform.localPosition.y + 1 * moveSpd * Time.deltaTime, 5);
        else
            transform.localPosition = new Vector3(0, transform.localPosition.y - 1 * moveSpd * Time.deltaTime, 5);
        
        
    }

}
