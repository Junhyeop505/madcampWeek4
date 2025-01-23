using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class Aerodynamics : MonoBehaviour
{
    public Rigidbody rb;
    public float liftCoefficient = 0.07f; // Adjust for realism
    public float dragCoefficient = 0.02f; // Adjust for realism
    public float wingArea = 0.2f; // Approximate wing area
    public float airDensity = 1.225f; // kg/m^3 at sea level

    public float maxPullForce = 4f; // Maximum force applied during slingshot
    public float maxPullDistance = 5f; // Maximum pull distance


    private bool isPulling = false;
    private bool released = false;
    private Vector3 pullStartPosition;
    private Vector3 pullEndPosition;
    private Vector3 originalPosition;
    private float yaw = 0f;
    private float pitch = 0f;

    public float minSpeedThreshold = 0.05f;
    public float stopTimer = 0f;
    public float stopDelay = 5f;
    private bool isStopped = false;

    private float startZPosition;

    private int remainingBoosts = 1;
    public float boostForce = 2f;

    private void Start()
    {
        if (rb == null)
        {
            rb = GetComponent<Rigidbody>();
        }

        rb.centerOfMass = new Vector3(0, -0.05f, 0);
        rb.isKinematic = true; // Start stationary
        originalPosition = transform.position;
        startZPosition = transform.position.z;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (!IsPointerOverUI())
            {
                PerformGameAction();
            }
        }
        HandleSlingshotInput();
        AdjustPlaneTilt();
        AlignPlaneWithVelocity();
        HandleBoostInput();
        //if (released)
        //{
        //    ControlPlaneWithMouse();
        //}
    }

    private void HandleBoostInput()
    {
        if (Input.GetKeyDown(KeyCode.Space) && remainingBoosts > 0)
        {
            ApplyBoost();
            remainingBoosts--;
        }
    }

    private void ApplyBoost()
    {
        Vector3 boostDirection = transform.forward;
        rb.AddForce(boostDirection.normalized*boostForce,ForceMode.Impulse);
    }

    private void ControlPlaneWithMouse()
    {
        Vector3 mousePosition=Input.mousePosition;
        float screenCenterX = Screen.width / 2;
        float screenCenterY=Screen.height / 2;

        float xOffset=(mousePosition.x-screenCenterX)/screenCenterX;
        float yOffset=(mousePosition.y-screenCenterY)/screenCenterY;

        float rotationSpeed = 5f;
        float maxTiltAngle = 45f;

        float targetYaw=Mathf.Clamp(xOffset*maxTiltAngle,-maxTiltAngle,maxTiltAngle);
        float targetPitch = Mathf.Clamp(-yOffset * maxTiltAngle, -maxTiltAngle, maxTiltAngle);

        Quaternion targetRotation=Quaternion.Euler(targetPitch,targetYaw,0);
        transform.rotation=Quaternion.Slerp(transform.rotation,targetRotation,Time.deltaTime);
    }

    bool IsPointerOverUI()
    {
        return EventSystem.current.IsPointerOverGameObject();
    }

    void PerformGameAction()
    {
        Debug.Log("Clicked");
    }

    private void FixedUpdate()
    {
        if ((!isPulling) && released) // Only apply aerodynamics when not pulling
        {
            ApplyAerodynamics();
            //AdjustPlaneTilt();
            CheckForStopCondition();
            TrackDistance();
        }
    }

    private void TrackDistance()
    {
        float distanceTraveled=transform.position.z-startZPosition;
        if (distanceTraveled > PlaneData.Instance.planeTravelDistance)
        {
            PlaneData.Instance.planeTravelDistance = distanceTraveled;
        }
    }

    private void CheckForStopCondition()
    {
        if (rb.linearVelocity.magnitude < minSpeedThreshold)
        {
            stopTimer += Time.fixedDeltaTime;
            if (stopTimer >= stopDelay && !isStopped)
            {
                isStopped = true;
                SceneManager.LoadScene("GameOverScene");
            }
        }
        else
        {
            stopTimer = 0f;
        }
    }

    private void AlignPlaneWithVelocity()
    {
        Vector3 velocity = rb.linearVelocity;
        if (velocity.magnitude > 0.1f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(velocity.normalized, Vector3.up);
            rb.MoveRotation(Quaternion.Slerp(rb.rotation, targetRotation, Time.deltaTime * 5f));
        }
    }

    private void AdjustPlaneTilt()
    {
        float tiltAmount = 1.5f;
        float maxTiltAngle = 45f;
        float rollInput = Input.GetAxis("Horizontal");
        float pitchInput = Input.GetAxis("Vertical");
        Vector3 currentRotation = transform.eulerAngles;
        float currentPitch = (currentRotation.x > 180) ? currentRotation.x - 360 : currentRotation.x;
        float currentRoll = (currentRotation.z > 180) ? currentRotation.z - 360 : currentRotation.z;
        float newPitch = Mathf.Clamp(currentPitch + pitchInput * tiltAmount, -maxTiltAngle, maxTiltAngle);
        float newRoll = Mathf.Clamp(currentRoll - rollInput * tiltAmount, -maxTiltAngle, maxTiltAngle);
        transform.rotation = Quaternion.Euler(newPitch, currentRotation.y, newRoll);
    }

    private void HandleSlingshotInput()
    {
        if (Input.GetMouseButtonDown(0)) // Start pulling
        {
            isPulling = true;
            pullStartPosition = GetMouseWorldPosition(); // Capture initial mouse position
            rb.isKinematic = true; // Disable physics temporarily
        }

        if (Input.GetMouseButton(0) && isPulling) // During pull
        {
            pullEndPosition = GetMouseWorldPosition(); // Update pull position dynamically
            AdjustPlanePositionAndRotation();
        }

        if (Input.GetMouseButtonUp(0) && isPulling) // Release
        {
            isPulling = false;
            rb.isKinematic = false;
            ReleaseSlingshot();
        }
    }

    private Vector3 GetMouseWorldPosition()
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = 10f;
        return Camera.main.ScreenToViewportPoint(mousePosition);
    }

    private void AdjustPlanePositionAndRotation()
    {
        float mouseX = Input.GetAxis("Mouse X") * 100f * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * 100f * Time.deltaTime;

        yaw -= mouseX;
        pitch += mouseY;
        Vector3 pullVector = pullEndPosition - originalPosition;
        float pullDistance = Mathf.Clamp(pullVector.magnitude, 0, maxPullDistance); // Limit pull distance

        if (pullDistance > 0.1f)
        {
            Vector3 pullOffset = Quaternion.Euler(pitch, yaw, 0) * Vector3.forward * (-pullDistance);
            transform.position = originalPosition + pullOffset;
            transform.rotation = Quaternion.LookRotation(-pullOffset.normalized, Vector3.up);
        }
    }

    private void ReleaseSlingshot()
    {
        Vector3 pullVector = pullEndPosition - originalPosition;
        float pullMagnitude = Mathf.Clamp(pullVector.magnitude, 0, maxPullDistance); // Limit pull force

        // Launch direction based on pull vector
        Vector3 forceDirection = (originalPosition - transform.position).normalized;
        float force = pullMagnitude / maxPullDistance * maxPullForce;

        Debug.Log($"Launching with Force: {forceDirection * force}");

        // Apply the release force
        rb.AddForce(forceDirection * force, ForceMode.Impulse);
        released = true;
    }

    private void ApplyAerodynamics()
    {
        Vector3 velocity = rb.linearVelocity;
        float speed = velocity.magnitude;

        // Calculate lift force perpendicular to velocity and plane's right vector
        Vector3 liftDirection = Vector3.Cross(velocity.normalized, transform.right).normalized;
        float lift = liftCoefficient * 0.5f * airDensity * Mathf.Pow(speed, 2) * wingArea;

        // Calculate drag force opposite to velocity
        Vector3 dragDirection = -velocity.normalized;
        float drag = dragCoefficient * 0.5f * airDensity * Mathf.Pow(speed, 2) * wingArea;

        // Apply lift and drag forces
        rb.AddForce(liftDirection * lift);
        rb.AddForce(dragDirection * drag);
    }
}