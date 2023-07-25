using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTrail : MonoBehaviour
{

    public float followSpeed = 30f;
    public float rotationSpeed;
    private float initialPositionY;
    private Rigidbody rb;
    public GameObject head;
    public PlayerController controller;

    [Header("Spawn Section")]
    private List<Vector3> positionList = new List<Vector3>();
    private List<GameObject> body = new List<GameObject>();
    private float spawnOffset = 0.5f;
    public GameObject spawningObject;


    private void Awake()
    {

        rb = GetComponent<Rigidbody>();
    }
    void Start()
    {
        initialPositionY = head.transform.position.y;
        for (int i = 0; i < 5; i++)
        {
            GrowBody();

        }
    }

    private void Update()
    {
       // SavePositionHistory();
    }

    private void GrowBody()
    {
        GameObject trailObject = Instantiate(spawningObject, GetSpawnPosition(), transform.rotation);
        body.Add(trailObject);
        trailObject.transform.SetParent(transform);

    }
    private Vector3 GetSpawnPosition()
    {

        Vector3 spawnPos = new Vector3(head.transform.position.x, initialPositionY, head.transform.position.z - spawnOffset);
        spawnOffset += 0.5f;
        return spawnPos;

    }
    public void SavePositionHistory()
    {
        Vector3 moveToPosition = head.transform.position;
        float offsetMultiplier = 0.2f;

        foreach (var part in body)
        {

            positionList.Add(part.transform.position);
            Vector3 lastAdded_BodyPartPosition = positionList[positionList.Count - 1];

            Vector3 targetPosition = new Vector3(moveToPosition.x, moveToPosition.y, moveToPosition.z );

            part.transform.position = Vector3.Lerp(lastAdded_BodyPartPosition, targetPosition, Time.deltaTime * followSpeed);
            Vector3.Slerp(part.transform.forward, controller.moveDirection, Time.deltaTime * rotationSpeed);
            moveToPosition = part.transform.position;
            offsetMultiplier += 0.2f;
        }


    }
}
