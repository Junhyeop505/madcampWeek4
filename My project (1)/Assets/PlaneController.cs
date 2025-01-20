using UnityEngine;

public class PlaneController : MonoBehaviour
{
    public float rotationSpeed = 100f; // 회전 속도

    void Update()
    {
        // 좌우 방향키로 회전 조종
        float horizontalInput = Input.GetAxis("Horizontal"); // A/D 또는 ←/→
        transform.Rotate(Vector3.up, horizontalInput * rotationSpeed * Time.deltaTime); // Y축 기준 회전
    }
}
