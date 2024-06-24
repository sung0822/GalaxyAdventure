using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightCtrl : MonoBehaviour
{
    public Material skyboxMaterial;
    public Color dayTint = new Color(0.5f, 0.5f, 1.0f);
    public Color eveningTint = new Color(1.0f, 0.5f, 0.5f);
    public Color dayGroundColor = new Color(1.0f, 1.0f, 1.0f);
    public Color eveningGroundColor = new Color(0.2f, 0.1f, 0.1f);
    public float duration = 1.0f;

    private float timer = 0.0f;
    private bool isDay = true;

    void Start()
    {
        RenderSettings.skybox = skyboxMaterial;
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= duration)
        {
            timer = 0.0f;
            isDay = !isDay;
        }

        float t = timer / duration;
        skyboxMaterial.SetColor("_SkyTint", Color.Lerp(isDay ? dayTint : eveningTint, isDay ? eveningTint : dayTint, t));
        skyboxMaterial.SetColor("_GroundColor", Color.Lerp(isDay ? dayGroundColor : eveningGroundColor, isDay ? eveningGroundColor : dayGroundColor, t));
    }
}
