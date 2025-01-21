using UnityEngine;

public class ShootingNeob : MonoBehaviour
{
    public GameObject missilePrefab; // 미사일 프리팹 (Inspector에서 연결)
    public Transform launchPoint;   // 발사 위치 (Inspector에서 연결)
    public float launchForce = 20f; // 초기 발사 속도
    public float upwardForce = 20f; // 위로 향하는 추가 힘
    public float fireRate = 2f;     // 발사 주기 (초 단위)

    void Start()
    {
        // Inspector에서 연결 여부를 확인
        if (missilePrefab == null)
            Debug.LogError("Missile Prefab is not assigned. Please assign it in the Inspector.");
        if (launchPoint == null)
            Debug.LogError("LaunchPoint is not assigned. Please assign it in the Inspector.");

        // 2초 간격으로 FireMissile 호출 시작
        InvokeRepeating(nameof(FireMissile), 0f, fireRate);
    }

    void FireMissile()
    {
        if (missilePrefab == null || launchPoint == null)
            return; // 필요한 오브젝트가 없는 경우 실행하지 않음

        // 미사일 생성
        GameObject missile = Instantiate(missilePrefab, launchPoint.position, launchPoint.rotation);

        // 미사일의 Y축을 90도 회전
        missile.transform.rotation *= Quaternion.Euler(0, 90, 0); // 현재 회전에 Y축으로 90도 추가

        // Rigidbody 컴포넌트를 가져오기
        Rigidbody rb = missile.GetComponent<Rigidbody>();
        if (rb != null)
        {
            // 초기 속도: 발사 방향(앞으로) + 위로 향하는 힘
            Vector3 launchVelocity = launchPoint.forward * launchForce + Vector3.up * upwardForce;
            rb.linearVelocity = launchVelocity; // Rigidbody의 속도 설정

            // 중력 활성화
            rb.useGravity = true;
        }
    }
}
