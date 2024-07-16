using UnityEngine;

public class BackgroundCtrl : MonoBehaviour
{
    BackgroundCtrl self;
    public Color startColor = new Color(200,255, 255, 255); // ���� ����
    public Color sunSetColor = new Color(255, 123, 123, 255); // �߰� ����
    public Color endColor = Color.black;     // �� ����

    private bool isSunSet = false;
    public float maxScore = 10000f;          // ���� ��ȭ�� �ɸ��� �ð� (��)

    int currentScore = 0;

    private Material material;             // ���͸��� ����



    void Start()
    {
        // ���������� ���͸����� ������
        material = GetComponent<Renderer>().material;
        // ���͸��� �ʱ� ���� ����
        material.color = startColor;
        self = GetComponent<BackgroundCtrl>();
    }

    void Update()
    {
        currentScore++;
    }

    public void CheckState(int score)
    {

        // 0���� 1 ������ ���� ���

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
        // ���� ° ���ڴ� ����ȭ�� �ð�(0~1������ ���൵�� �ǹ��Ѵٰ� ���� ����)�� �ǹ�
        material.color = Color.Lerp(startColor, sunSetColor, normalizedPercent);
        return;
    }

    void CheckNight(float normalizedPercent)
    {
        // ���� ° ���ڴ� ����ȭ�� �ð�(0~1������ ���൵�� �ǹ��Ѵٰ� ���� ����)�� �ǹ�
        material.color = Color.Lerp(sunSetColor, endColor, normalizedPercent);
        return;
    }



}