using UnityEngine;

public class SwerveMovement : MonoBehaviour
{
    public float swerveSpeed = 5f;
    public float maxSwerveAmount = 2f;
    public float forwardSpeed = 5f;
    public InputManager input;
    private Rigidbody rb;
    private float targetSwerveAmount = 0f;
   

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
       
            
        
        
    }

    private void FixedUpdate()
    {
        Movement();
    }

    private void Movement()
    {
        Debug.Log(input.Horizontal);
        targetSwerveAmount = Mathf.Clamp(input.Horizontal * swerveSpeed, -maxSwerveAmount, maxSwerveAmount);
        Debug.Log(targetSwerveAmount);
        // Move the player forward constantly
        Vector3 forwardVelocity = new Vector3(0f, 0f, forwardSpeed);
        rb.velocity = forwardVelocity;

        // Move the player horizontally using swerve movement
        float displacementX = targetSwerveAmount * Time.fixedDeltaTime;
        float modifiedSwerveSpeed = swerveSpeed / forwardSpeed;

        Vector3 targetVelocity = new Vector3(targetSwerveAmount, rb.velocity.y, 0f); // Set forward speed to 0

        // Calculate the time factor for swerve movement
        float time = Mathf.Abs(displacementX / modifiedSwerveSpeed);

        // Apply the swerve movement using MovePosition
        Vector3 newPosition = rb.position + targetVelocity * modifiedSwerveSpeed * time;
        newPosition.z = rb.position.z + forwardSpeed * Time.fixedDeltaTime; // Maintain forward speed
        rb.MovePosition(newPosition);

    }



}
