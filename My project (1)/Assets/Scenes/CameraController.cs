using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target;  // 포커스할 대상
    public float speed = 2.0f;

    private bool isMoving = false;

    void Update()
    {
        if (isMoving)
        {
            Vector3 newPosition = Vector3.Lerp(transform.position, target.position, Time.deltaTime * speed);
            transform.position = newPosition;

            if (Vector3.Distance(transform.position, target.position) < 0.1f)
            {
                isMoving = false;
            }
        }
    }

    public void StartFocus()
    {
        isMoving = true;
    }
}
