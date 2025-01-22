using UnityEngine;

public class WaterSprayer : MonoBehaviour
{
    public GameObject waterPrefab; // 물방울 Prefab
    public Transform firePoint;    // 물이 발사되는 위치
    public float launchForce = 20f; // 발사 초기 속도
    public float spawnInterval = 0f; // 물방울 생성 간격

    private float spawnTimer; // 생성 간격 타이머

    void Update()
    {
        // 타이머를 증가
        spawnTimer += Time.deltaTime;

        // 일정 시간 간격으로 물방울 생성
        if (spawnTimer >= spawnInterval)
        {
            SpawnWater();
            spawnTimer = 0f;
        }
    }

    void SpawnWater()
    {
        // 물방울 생성
        GameObject water = Instantiate(waterPrefab, firePoint.position, firePoint.rotation);

        // 물방울에 초기 힘 적용 (위 방향으로 발사)
        Rigidbody rb = water.GetComponent<Rigidbody>();
        Vector3 launchDirection = firePoint.up; // 위쪽 방향
        rb.AddForce(launchDirection * launchForce, ForceMode.Impulse);

        // 일정 시간 후 물방울 제거
        Destroy(water, 2f);
    }
}
