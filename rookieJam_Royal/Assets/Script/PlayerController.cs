using UnityEngine;

public class PlayerController : MonoBehaviour
{

    //Headers are used to customize the inspector window, to distinguish betweeen different sets of variables 
    //by giving them headings
    [Header("---------------Input Section--------------------")]
    public FloatingJoystick fixedJoystick;
    public Canvas inputCanvas;
    private bool isJoystick;

    [Header("---------------Player Movement Section--------------------")]
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float runSpeed = 5f;
    [SerializeField] private float rotationSpeed = 2f;
    [SerializeField] private float acceleration = 2f;
    [SerializeField] private float velocityReducingFactor = 1f;
    [SerializeField] private float maxReducingValue = .3f;
    [SerializeField] private float groundRaycastDistance = 0.5f;
    [SerializeField] private float rotateMovementDeltaSpeed = 300f;
    [SerializeField] private Transform playerChild;
    [SerializeField]
    public Vector3 moveDirection = Vector3.zero; 
    public Rigidbody rb;
    [SerializeField] private InputManager input_manager;


    [Header("---Jumping Section----")]
    [SerializeField] private float jumpForce = 2f;
    [SerializeField] private float startFallingVelocity = 1.0f;
    [SerializeField] private float fallingRate = 1.5f;
    private bool isOnGround = true;
    private bool hasJumped = false;


    [Header("---Animation Section---")]
    public Animator animator;




    private void Awake()
    {

        rb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
      //  EnableJoystick();

    }
    private void EnableJoystick()
    {
        isJoystick = true;
        inputCanvas.gameObject.SetActive(true);
    }


    private void Update()
    {
        CheckGround();
        Inputs();
        Walking();
       

        ApplyDownForce();

    }

    private void Inputs()
    {
        //Taking input through axis
       // if (isJoystick)
       // {
            /* float horizontalInput = fixedJoystick.Direction.x;
             float verticalInput = fixedJoystick.Direction.y;*/
            float horizontalInput = input_manager.Horizontal;
            float verticalInput = input_manager.Vertical;

            Vector3 newMoveDirection = new Vector3(horizontalInput, verticalInput, 0f);
            moveDirection = newMoveDirection.normalized;
        Vector3 rotateDirection = moveDirection;
        rotateDirection.y = 0;
          
            // Rotate the player towards the movement direction
            if (moveDirection != Vector3.zero)
                playerChild.forward = Vector3.Slerp(playerChild.forward,rotateDirection, Time.deltaTime * rotationSpeed);
      //  }
    }

    private void Walking()
    {
        Vector3 moveVelocity;

        if (Input.GetKey(KeyCode.LeftShift))
        {
            moveVelocity = moveDirection * runSpeed;
            animator.SetBool("isRunning", true);
        }
        else
        {
            animator.SetBool("isWalking", moveDirection != Vector3.zero);
            animator.SetBool("isRunning", false);
            animator.SetBool("isDashing", false);

            moveVelocity = moveDirection * moveSpeed;
        }
        if (moveVelocity != Vector3.zero)
            velocityReducingFactor += acceleration * Time.deltaTime;

        else
            velocityReducingFactor -= acceleration * Time.deltaTime;


        velocityReducingFactor = Mathf.Clamp(velocityReducingFactor, maxReducingValue, 1);
        moveVelocity *= velocityReducingFactor;
        moveVelocity.y = rb.velocity.y;
        rb.velocity = Vector3.MoveTowards(rb.velocity, moveVelocity, rotateMovementDeltaSpeed * Time.deltaTime);

    }

    private void CheckGround()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit, groundRaycastDistance))
        {
            if (hit.collider.CompareTag("ground"))
            {
                isOnGround = true;
                hasJumped = false;
            }
        }
        else
        {
            isOnGround = false;
        }

    }




    public void Jump()
    {

        if ( isOnGround && !hasJumped  /*jumpCount < 2*/)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            animator.SetTrigger("isJumping");
            hasJumped = true;

        }
        ApplyDownForce();

    }

    private void ApplyDownForce()
    {

        // Check if the player is above a certain height or velocity threshold
        if ((rb.velocity.y < startFallingVelocity) && !isOnGround)
        {
            Debug.Log("Applying Down Force");
            rb.velocity += Physics.gravity * fallingRate * Time.deltaTime;
        }


    }
    /*private void Attacking2()
    {
        if (Input.GetMouseButtonDown(0))
        {
            // Check for double right-click

            if (Time.time - lastRightClickTime <= doubleClickDelay)
            {
                // Double right-click detected, trigger the animation
                animator.SetTrigger("Attack2");
                attackatPoint();
                swordTrail.enabled = true;
                startedAttacking = Time.time;
            }

            lastRightClickTime = Time.time;

            // this part makes sure that the trail is only rendered while attacking
            if (swordTrail.enabled && Time.time > startedAttacking + 0.5)
                swordTrail.enabled = false;
        }
    }*/

    /*  private void Defending()
      {
          if (Input.GetMouseButtonDown(1))
          {
              animator.SetTrigger("Defend");

          }
      }
      private void attackatPoint()
      {
          Collider[] hitEnemies = Physics.OverlapSphere(AttackPoint.position, attackRange, enemyLayer);

          foreach (Collider enemy in hitEnemies)
          {
              //  enemy.gameObject.GetComponent<EnemyHealthCombat>().TakeDamage(damage);
              Debug.Log(enemy.name);
          }
      }

      private void OnDrawGizmos()
      {
          Gizmos.DrawWireSphere(AttackPoint.position, attackRange);
      }*/

    /* private void PlayJumpParticle()
     {
         particlesParent.forward = -playerChild.transform.forward;
         particlesParent.Rotate(60, 0, 0);
         jumpParticles.Play();
     }*/

    /* private void SetSwordTrail(bool status)
     {
         swordTrail.enabled = status;


     }*/

}
