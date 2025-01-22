using UnityEngine;

public class Papers : MonoBehaviour
{
    private float moveSpeed; // 책의 이동 속도
    private float gravity;   // 책이 떨어질 때 적용될 중력
    private bool isFalling = false; // 책이 떨어지고 있는지 확인

    public void Initialize(float speed, float gravityForce)
    {
        moveSpeed = speed;
        gravity = gravityForce;
    }

    private void Update()
    {
        if (!isFalling)
        {
            // x축 방향으로 이동
            transform.Translate(Vector3.right * moveSpeed * Time.deltaTime);
        }
        else
        {
            // 책이 떨어지기 시작하면 중력을 적용
            transform.Translate(Vector3.down * gravity * Time.deltaTime);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            // 벽에 부딪히면 떨어지기 시작
            isFalling = true;
        }
        else if (collision.gameObject.CompareTag("Ground"))
        {
            // 바닥에 닿으면 책의 움직임을 멈춤
            moveSpeed = 0;
            gravity = 0;
        }
    }
}
