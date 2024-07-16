using UnityEngine;

public class BackgroundCtrl : MonoBehaviour
{
    BackgroundCtrl self;
    public Color startColor = new Color(200,255, 255, 255); // 시작 색상
    public Color sunSetColor = new Color(255, 123, 123, 255); // 중간 색상
    public Color endColor = Color.black;     // 끝 색상

    private bool isSunSet = false;
    public float maxScore = 10000f;          // 색상 변화에 걸리는 시간 (초)

    int currentScore = 0;

    private Material material;             // 머터리얼 참조



    void Start()
    {
        // 렌더러에서 머터리얼을 가져옴
        material = GetComponent<Renderer>().material;
        // 머터리얼 초기 색상 설정
        material.color = startColor;
        self = GetComponent<BackgroundCtrl>();
    }

    void Update()
    {
        currentScore++;
    }

    public void CheckState(int score)
    {

        // 0에서 1 사이의 비율 계산

        float lerpFactor = currentScore / maxScore;
        if (isSunSet)
        {
            CheckSunSet(lerpFactor);
        }
        else
        {
            CheckNight(lerpFactor);
        }



        if (lerpFactor >= 1.0f)
        {
            isSunSet = true;
            
            maxScore *= 2;
        }



}

    void CheckSunSet(float normalizedPercent)
    {
        // 세번 째 인자는 정규화된 시간(0~1까지의 진행도를 의미한다고 봐도 무방)을 의미
        material.color = Color.Lerp(startColor, sunSetColor, normalizedPercent);
        return;
    }

    void CheckNight(float normalizedPercent)
    {
        // 세번 째 인자는 정규화된 시간(0~1까지의 진행도를 의미한다고 봐도 무방)을 의미
        material.color = Color.Lerp(sunSetColor, endColor, normalizedPercent);
        return;
    }



}