using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterFlow : MonoBehaviour
{
    MeshRenderer meshRenderer;
    [SerializeField] Vector2 offset = Vector3.zero;

    private void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
    }

    private void Update()
    {
        meshRenderer.material.mainTextureOffset += offset * Time.deltaTime;
    }
}
