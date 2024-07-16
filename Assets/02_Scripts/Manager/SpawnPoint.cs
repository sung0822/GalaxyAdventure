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
        // 기즈모 색상을 설정
        Gizmos.color = raserColor;

        // 레이저 시작점
        Vector3 start = transform.position;
        // 레이저 끝점 (오브젝트의 forward 방향으로 laserLength 만큼 떨어진 위치)
        Vector3 end = start + transform.forward * laserLength;

        // 레이저를 나타내는 선을 그립니다.
        Gizmos.DrawLine(start, end);

        // 레이저 끝에 구를 그려서 레이저 끝점을 강조할 수 있습니다.
        Gizmos.DrawSphere(end, 0.1f);
    }
}
