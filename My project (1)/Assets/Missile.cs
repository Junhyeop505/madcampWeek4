using UnityEngine;

public class Missile : MonoBehaviour
{
    public float lifetime = 5f; // 미사일의 수명
    public GameObject explosionPrefab; // 폭발 효과 프리팹

    void Start()
    {
        // 일정 시간 후 미사일 삭제
        Destroy(gameObject, lifetime);
    }

    void OnTriggerEnter(Collider other)
    {
        // 충돌한 오브젝트가 "Enemy" 태그를 가진 경우
        if (other.CompareTag("Enemy"))
        {
            // 폭발 효과 생성
            if (explosionPrefab != null)
            {
                Instantiate(explosionPrefab, transform.position, Quaternion.identity);
            }

            // 적 오브젝트 제거
            Destroy(other.gameObject);

            // 미사일 제거
            Destroy(gameObject);
        }
    }
}
