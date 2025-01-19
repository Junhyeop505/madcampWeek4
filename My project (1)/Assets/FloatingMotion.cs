using UnityEngine;

public class FloatingMotion : MonoBehaviour
{
    public float speed = 2.0f; // 위아래로 움직이는 속도
    public float amplitude = 0.5f; // 움직이는 거리

    private Vector3 startPosition;

    void Start()
    {
        // 오브젝트의 시작 위치 저장
        startPosition = transform.position;
    }

    void Update()
    {
        // 사인파를 사용해 위아래로 움직이는 모션 구현
        float newY = startPosition.y + Mathf.Sin(Time.time * speed) * amplitude;
        transform.position = new Vector3(startPosition.x, newY, startPosition.z);
    }
}
