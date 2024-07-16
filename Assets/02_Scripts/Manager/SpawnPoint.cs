using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    [SerializeField] public int index = 0;

    public static string commonName = "Point_";

    [SerializeField] private Color raserColor = Color.red;
    private float laserLength = 100.0f;

    private void Start()
    {
        transform.name = commonName + index.ToString();
    }
    void OnDrawGizmos()
    {
        // ����� ������ ����
        Gizmos.color = raserColor;

        // ������ ������
        Vector3 start = transform.position;
        // ������ ���� (������Ʈ�� forward �������� laserLength ��ŭ ������ ��ġ)
        Vector3 end = start + transform.forward * laserLength;

        // �������� ��Ÿ���� ���� �׸��ϴ�.
        Gizmos.DrawLine(start, end);

        // ������ ���� ���� �׷��� ������ ������ ������ �� �ֽ��ϴ�.
        Gizmos.DrawSphere(end, 0.1f);
    }
}
