using UnityEngine;

public class FlyingBook : MonoBehaviour
{
    public float rotationSpeed = 360f; // 책의 회전 속도 (도/초)
    public float launchForce = 10f;    // 책이 날아가는 힘
    public float upwardForce = 5f;     // 책이 위로 날아가는 추가 힘
    public float lifetime = 5f;        // 책이 사라지기까지의 시간

    private bool isFlying = false;     // 책이 날아가는 상태 여부
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        if (rb == null)
        {
            Debug.LogError("Rigidbody가 책에 없습니다. Rigidbody를 추가하세요.");
            return;
        }
    }

    public void Fly()
    {
        if (isFlying) return; // 이미 날아가는 책은 무시
        isFlying = true;

        // 책에 회전 효과 추가
        rb.AddTorque(Random.onUnitSphere * rotationSpeed);

        // 책을 앞으로 + 위로 날아가게 하는 힘 추가
        Vector3 launchDirection = transform.forward + Vector3.up;
        rb.AddForce(launchDirection.normalized * launchForce + Vector3.up * upwardForce, ForceMode.Impulse);

        // 일정 시간 후 오브젝트 제거
        Destroy(gameObject, lifetime);
    }
}
