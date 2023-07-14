using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stacking : MonoBehaviour
{
    public float swerveSpeed = 5f;
    public float maxSwerveAmount = 2f;
    public float forwardSpeed = 5f;
    public float followSpeed = 30f;
    public float initialPositionY;

    public GameObject head;
    public InputManager input;
    private Rigidbody rb;
    private float targetSwerveAmount = 0f;
    public float maxY;
    public float minY;
    private Vector3 headClampPosition;
    

    [Header("Spawn Section")]
    private List<Vector3> positionList = new List<Vector3>();
    private List<GameObject> body = new List<GameObject>();
    private float spawnOffset = 0.5f;
    public GameObject spawningObject;



    private void Awake()
    {
        
        rb = GetComponent<Rigidbody>();
    }
    private void Start()
    {
        initialPositionY = head.transform.position.y;
        GrowBody();
        GrowBody();
        GrowBody();
       
    }
    private void Update()
    {

       
            headClampPosition = new Vector3(head.transform.position.x, Mathf.Clamp(head.transform.position.y, minY, maxY), head.transform.position.z);
            head.transform.position = headClampPosition;// Vector3.Lerp(head.transform.position,headClampPosition,Time.deltaTime);
        
        Movement();
        SavePositionHistory();
    }


    private void Movement()
    {
        //Move the player forward constantly
        Vector3 forwardVelocity = new Vector3(0f, 0f, forwardSpeed);
        rb.velocity = forwardVelocity;
        
        targetSwerveAmount = Mathf.Clamp(input.Vertical * swerveSpeed, -maxSwerveAmount, maxSwerveAmount);

        Vector3 targetVelocity = new Vector3(rb.velocity.x, targetSwerveAmount, 0f); // Set forward speed to 0

        // Apply the swerve movement using MovePosition
        Vector3 newPosition = rb.position + targetVelocity * Time.deltaTime*swerveSpeed;
        //newPosition.z = rb.position.z + forwardSpeed * Time.deltaTime * 100; // Maintain forward speed
        rb.MovePosition(newPosition);
       
    }
  

   
    private void GrowBody()
    {
        GameObject trailObject = Instantiate(spawningObject, GetSpawnPosition(), transform.rotation);
        body.Add(trailObject);
        

    }
    private Vector3 GetSpawnPosition()
    {

        Vector3 spawnPos = new Vector3(head.transform.position.x, initialPositionY, head.transform.position.z + spawnOffset);

        spawnOffset -= 5f;
        return spawnPos;

    }
    IEnumerator SpawnWaitTime()
    {
        yield return new WaitForSeconds(5f);
    }
    private void SavePositionHistory()
    {
        Vector3 moveToPosition = head.transform.position;
        float offsetMultiplier = 0.5f;

        foreach (var part in body)
        {
           
            positionList.Add(part.transform.position);
            Vector3 lastAdded_BodyPartPosition = positionList[positionList.Count - 1];
            

            Vector3 targetPosition = new Vector3(moveToPosition.x, moveToPosition.y, moveToPosition.z + offsetMultiplier);

            part.transform.position = Vector3.Lerp(lastAdded_BodyPartPosition, targetPosition, Time.deltaTime * followSpeed);

            moveToPosition = part.transform.position;
            offsetMultiplier -= 0.7f;
        }
    }
    
    private void OnTriggerEnter(Collider collision)
    {
        Renderer powerColor = collision.gameObject.GetComponent<Renderer>();
        if (collision.gameObject.CompareTag("PowerPositive"))
        {
            for (int i = 0; i < 10; i++)
            {
                GrowBody();
            }
            foreach (var part in body)
            {
                Renderer partRendered = part.GetComponent<Renderer>();
                partRendered.material.color = powerColor.material.color;
            }
        }
        else if (collision.gameObject.CompareTag("PowerNegative"))
        {
            body.RemoveRange(body.Count - 7, 5);
            for(int i = body.Count-7; i < body.Count-2; i++)
            {
                Destroy(body[i]);
            }
            foreach (var part in body)
            {
                Renderer partRendered = part.GetComponent<Renderer>();
                partRendered.material.color = powerColor.material.color;
            }
        }
    }


}
