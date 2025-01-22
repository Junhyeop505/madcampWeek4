using UnityEngine;

public class MovingChair2 : MonoBehaviour
{
    public float movementDistance = 3f; // X, Z 축의 이동 최대 거리
    public float movementSpeed = 1f;   // 이동 속도 (주기 설정)
    public float phaseOffset = 0f;     // 시간에 따른 Phase Offset (시작 시간 차이)

    private Vector3 startingPosition;  // 각 의자의 시작 위치
    private float time;                // 시간을 저장 (움직임 계산에 사용)

    void Start()
    {
        // 의자의 초기 위치 저장
        startingPosition = transform.position;

        // Phase Offset과 속도를 난수로 초기화
        phaseOffset = Random.Range(0f, 2f * Mathf.PI); // 0~2π 사이 난수
        movementSpeed = Random.Range(0.5f, 2f);       // 0.5~2 사이 난수
    }

    void Update()
    {
        MoveChair();
    }

    void MoveChair()
    {
        // 시간에 따라 X축과 Z축 위치 계산 (Sin 함수 사용)
        time += Time.deltaTime * movementSpeed;

        float xOffset = Mathf.Sin(time) * movementDistance; // X축 이동 값
        float zOffset = Mathf.Cos(time + phaseOffset) * movementDistance; // Z축 이동 값

        // 각 의자의 시작 위치를 기준으로 이동
        transform.position = new Vector3(
            startingPosition.x + xOffset,
            startingPosition.y,
            startingPosition.z + zOffset
        );
    }
}
