using UnityEngine;

public class PlaneShooting : MonoBehaviour
{
    public GameObject missilePrefab; // 미사일 프리팹 (Inspector에서 연결)
    public Transform launchPoint;   // 발사 위치 (Inspector에서 연결)
    public float missileSpeed = 10f; // 미사일 속도
    

    void Start()
    {
        // Inspector에서 연결 여부를 확인
        if (missilePrefab == null)
            Debug.LogError("Missile Prefab is not assigned. Please assign it in the Inspector.");
        if (launchPoint == null)
            Debug.LogError("LaunchPoint is not assigned. Please assign it in the Inspector.");
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Space key pressed");
            FireMissile();
        }
    }

    void FireMissile()
{
    if (missilePrefab == null || launchPoint == null)
        return; // 필요한 오브젝트가 없는 경우 실행하지 않음

    // 미사일 생성
    GameObject missile = Instantiate(missilePrefab, launchPoint.position, launchPoint.rotation);

    // 미사일의 회전 조정 (90도 회전)
    missile.transform.Rotate(90, 0, 0); // X축을 기준으로 90도 회전

    // Rigidbody로 이동 처리
    Rigidbody rb = missile.GetComponent<Rigidbody>();
    if (rb != null)
    {
        rb.linearVelocity = launchPoint.forward * missileSpeed; // 발사 방향을 Z축(정면)으로 설정
    }
}

}
