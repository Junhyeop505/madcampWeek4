using UnityEngine;

public class GooseController : MonoBehaviour
{
    public float walkSpeed = 10f;        // 걷는 속도
    public Transform[] waypoints;      // 경로 점
    public float waypointThreshold = 0.5f; // 웨이포인트 도달 거리
    public float rotationSpeed = 5f;   // 회전 속도 (덜컹거림 방지)
    public float wobbleAngle = 10f;    // 뒤뚱거림 각도
    public float wobbleSpeed = 5f;     // 뒤뚱거림 속도

    private int currentWaypointIndex = 0;
    private Quaternion targetRotation; // 목표 회전값
    private bool isRotating = false;   // 회전 중인지 확인
    private float wobbleTimer = 0f;    // 뒤뚱거림 타이머

    void Update()
    {
        if (isRotating)
        {
            RotateLeft();
        }
        else
        {
            MoveAlongPath();
        }

        Wobble();
    }

    void MoveAlongPath()
    {
        if (waypoints == null || waypoints.Length == 0)
        {
            Debug.LogWarning("Waypoints 배열이 설정되지 않았습니다.");
            return;
        }

        // 현재 웨이포인트 위치
        Vector3 targetPosition = waypoints[currentWaypointIndex].position;

        // 방향 계산
        Vector3 direction = (targetPosition - transform.position).normalized;

        // 이동
        transform.position += direction * walkSpeed * Time.deltaTime;

        // 웨이포인트에 도달했는지 확인
        if (Vector3.Distance(transform.position, targetPosition) < waypointThreshold)
        {
            // 다음 웨이포인트로 이동 전 왼쪽으로 90도 회전
            currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length;

            // 목표 회전값 계산 (왼쪽으로 90도)
            targetRotation = Quaternion.Euler(0f, transform.eulerAngles.y - 90f, 0f);
            isRotating = true; // 회전 시작
        }
    }

    void RotateLeft()
    {
        // 현재 회전값에서 목표 회전값까지 서서히 회전
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

        // 목표 회전에 근접하면 회전 종료
        if (Quaternion.Angle(transform.rotation, targetRotation) < 1f)
        {
            transform.rotation = targetRotation; // 최종 정렬
            isRotating = false; // 회전 종료
        }
    }

    void Wobble()
    {
        // 뒤뚱거림 계산
        wobbleTimer += Time.deltaTime * wobbleSpeed;
        float wobble = Mathf.Sin(wobbleTimer) * wobbleAngle;

        // 현재 회전값에 wobble을 추가 (z축)
        Vector3 currentRotation = transform.eulerAngles;
        currentRotation.z = wobble;
        transform.eulerAngles = currentRotation;
    }
}
