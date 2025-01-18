using UnityEngine;

public class Aerodynamics : MonoBehaviour
{
    public Rigidbody rb;
    public float liftCoefficient = 0.5f; // Adjust for realism
    public float dragCoefficient = 0.2f;
    public float wingArea = 0.2f; // Approximate wing area
    public float airDensity = 1.225f; // kg/m^3 at sea level

    public float throwForceMagnitude = 50f;
    public float throwAngle = 70f;

    private void Start()
    {
        if (rb == null)
        {
            rb = GetComponent<Rigidbody>();
        }
        transform.rotation = Quaternion.Euler(0,Random.Range(0,360f),0);
        rb.centerOfMass = new Vector3(0, -0.05f, 0);
        SimulateThrow();
    }

    void FixedUpdate()
    {
        //Vector3 velocity = rb.linearVelocity;
        //float speed = velocity.magnitude;

        //if (speed > 0.1f)
        //{
        //    Vector3 liftDirection = Vector3.Cross(velocity, transform.right).normalized;
        //    float lift = liftCoefficient * 0.5f * airDensity * speed * speed * wingArea;
        //    rb.AddForce(liftDirection * lift);

        //    // Calculate drag
        //    Vector3 dragDirection = -velocity.normalized;
        //    float drag = dragCoefficient * 0.5f * airDensity * speed * speed * wingArea;
        //    rb.AddForce(dragDirection * drag);
        //}
        ApplyAerodynamics();
    }

    private void SimulateThrow()
    {
        Vector3 localThrowDirection = Quaternion.Euler(-throwAngle, 0, 0) * Vector3.forward;
        Vector3 worldThrowDirection = transform.TransformDirection(localThrowDirection);
        rb.AddForce(worldThrowDirection * throwForceMagnitude, ForceMode.Impulse);

    }

    private void ApplyAerodynamics()
    {
        Vector3 localVelocity = transform.InverseTransformDirection(rb.linearVelocity);
        float speed = localVelocity.magnitude;
        if (speed > 0.1f)
        {
            Vector3 localLiftDirection = Vector3.Cross(localVelocity.normalized, Vector3.right).normalized;
            float lift = liftCoefficient * 0.5f * airDensity * speed * speed * wingArea;

            Vector3 localDragDirection = -localVelocity.normalized;
            float drag = dragCoefficient * 0.5f * airDensity * speed * speed * wingArea;

            Vector3 worldLift = transform.TransformDirection(localLiftDirection) * lift;
            Vector3 worldDrag = transform.TransformDirection(localDragDirection) * drag;

            rb.AddForce(worldLift);
            rb.AddForce(worldDrag);


        }
    }
}

