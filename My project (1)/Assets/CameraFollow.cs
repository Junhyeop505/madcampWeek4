using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class CameraFollow : MonoBehaviour
{
    public Transform target; // The object the camera will follow
    public Vector3 Offset=new Vector3(0,5,-30); // Offset position from the target
    public float smoothSpeed = 0.2f; // Smoothness factor for camera movement
    public Vector3 fixedRotation = new Vector3(30, 0, 0);
    public bool dynamicOffset = true;
    public Vector3 fixedPosition = new Vector3(0, 10, -30);
    private bool isFollowing = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    //void Start()
    //{
        
    //}

    // Update is called once per frame
    void LateUpdate()
    {
        if (Input.GetMouseButtonDown(0))
        {
            isFollowing = false;
        }
        if (Input.GetMouseButtonUp(0))
        {
            isFollowing = true;
        }
        if (isFollowing && target != null)
        {
            Vector3 currentOffset = Offset;
            Rigidbody targetRigidbody = target.GetComponent<Rigidbody>();

            if(dynamicOffset && targetRigidbody != null)
            {
                float speed = targetRigidbody.linearVelocity.magnitude;
                currentOffset += new Vector3(0, 0, -speed * 0.1f);
            }

            Vector3 desiredPosition = target.TransformPoint(currentOffset);

            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

            transform.position = smoothedPosition;
            transform.LookAt(target);
            //if (dynamicOffset && targetRigidbody != null)
            //{
            //    float speed = targetRigidbody.linearVelocity.magnitude;
            //    currentOffset += new Vector3(0, 0, -speed * 0.1f);
            //}
            //// Calculate the desired position
            //Vector3 desiredPosition = target.TransformPoint(Offset);

            //// Smoothly interpolate between the camera's current position and the desired position
            //Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

            //// Apply the smoothed position
            //transform.position = smoothedPosition;

            //Quaternion targetRotation = Quaternion.LookRotation(target.position - transform.position);

            //// Optionally, make the camera always look at the target
            //transform.LookAt(target);
        }
        else
        {
            transform.position = fixedPosition;
            transform.rotation = Quaternion.Euler(fixedRotation);
        }
    }
}
