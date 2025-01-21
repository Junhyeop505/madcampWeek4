using UnityEngine;

public class MovingChair1 : MonoBehaviour
{
    public float movementDistance = 5f; // X, Z 축의 이동 최대 거리
    public float movementSpeed = 1f;   // 이동 속도 (주기 설정)

    private Vector3 startingPosition;  // 시작 위치
    private float time;                // 시간을 저장 (움직임 계산에 사용)

    void Start()
    {
        // 시작 위치 저장
        startingPosition = transform.position;
    }

    void Update()
    {
        MoveChair();
    }

    void MoveChair()
    {
        // 시간에 따라 X축과 Z축 위치 계산 (Sin 함수를 사용)
        time += Time.deltaTime * movementSpeed;

        float xOffset = Mathf.Sin(time) * movementDistance; // X축 이동 값
        float zOffset = Mathf.Cos(time) * movementDistance; // Z축 이동 값

        // 현재 위치 업데이트
        transform.position = new Vector3(
            startingPosition.x + xOffset,
            startingPosition.y,
            startingPosition.z + zOffset
        );
    }
}
