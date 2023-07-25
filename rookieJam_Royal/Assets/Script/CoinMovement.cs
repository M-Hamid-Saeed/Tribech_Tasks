using UnityEngine;

public class CoinMovement : MonoBehaviour
{
    [Header("----------References-------")]
    [SerializeField] private Transform coinVisual;
    [SerializeField] private Transform playerVisual;

    [Header("----------Movement-------")]
    [SerializeField] private float rotationSpeed;
    [SerializeField] private float moveSpeed;
    private Rigidbody rb;
    private bool shouldMove = false;
    private Vector3 initialPosition;

    private void Awake()
    {
        
        rb = GetComponent<Rigidbody>();
    }
    private void Start()
    {
        initialPosition = transform.position;
    }

    private void Update()
    {
        if (shouldMove)
        {
            Move();
        }
    }

    private void Move()
    {
        // Calculate the target rotation
        Quaternion targetRotation = Quaternion.LookRotation(playerVisual.position - transform.position);
        targetRotation *= Quaternion.Euler(targetRotation.eulerAngles.x, 0f, 0f); // Lock rotation around y and z axes

        // Smoothly rotate the coin towards the player's position
        coinVisual.rotation = Quaternion.Slerp(coinVisual.rotation, targetRotation, Time.deltaTime * rotationSpeed);

        // Move the coin towards the player

        transform.position = Vector3.MoveTowards(transform.position, playerVisual.position, moveSpeed * Time.deltaTime);
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("OuterBoundary"))
        {
            shouldMove = true;
        }
        else if (other.gameObject.CompareTag("InnerBoundary"))
            Destroy(gameObject);
        
    }
}
