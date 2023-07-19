using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestingScript : MonoBehaviour
{
    [SerializeField] float _rotateSpeed;
    [SerializeField] float maxRotationX;
    [SerializeField] float maxRotationY;
    [SerializeField] float lerpFactor;
    private Vector3 touchStartPos;
   
    //Customized Touch Input 
    [SerializeField] InputManager touchInputManager;
    public Vector3 lookDirection;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        NewMouseMove();
       // LookRotation();
    }
    private void LookRotation()
    {
        float targetLookAmountX = touchInputManager.Horizontal * _rotateSpeed;
        float targetLookAmountY = touchInputManager.Vertical * _rotateSpeed;

        //Input from InputManager Script

        lookDirection = new Vector3(targetLookAmountX, targetLookAmountY, transform.position.z).normalized;

        // lookDirection = (mousePosition - transform.position).normalized; 

        //transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(lookDirection), lerpFactor * Time.deltaTime);
        // Making Angle
        //transform.rotation = initialRotation * Quaternion.Euler(lookDirection);
        Quaternion rotTarget = Quaternion.LookRotation(lookDirection);
        // Debug.Log("ROTATION TARGET" + rotTarget);

        transform.rotation = Quaternion.Slerp(transform.rotation, rotTarget, lerpFactor * Time.deltaTime);
        // transform.eulerAngles = Vector3.Lerp(transform.eulerAngles, lookDirection, lerpFactor * Time.deltaTime);
        // transform.Rotate(transform.forward + lookDirection* lerpFactor);
        Quaternion rot = transform.rotation;
        rot.x = Mathf.Clamp(rot.x, -maxRotationX, maxRotationX);
        rot.y = Mathf.Clamp(rot.y, -maxRotationY, maxRotationY);
        transform.rotation = rot;

    }






    public float REFRESH_TIME = 0.1f;
    float refreshDelta = 0.0f;
    private Vector3 deltaPosition;
    private Vector3 diff;
    private float senstivity;
    void NewMouseMove()
    {
        float smoothingTime = 10.0f; //decrease value to move faster
        if (Input.GetMouseButtonDown(0))
        {

            touchStartPos = Input.mousePosition;
            refreshDelta = 0.0f;
        }
        if (Input.GetMouseButton(0))
        {

            refreshDelta += Time.deltaTime;
            Vector3 diff = (Input.mousePosition - touchStartPos);
            Vector3 finalPos = transform.rotation * diff;
           /* finalPos.x /= Screen.width / 2f;
            finalPos.y /= Screen.height / 2f;*/
            if (refreshDelta >= REFRESH_TIME)
            {

                refreshDelta = 0.0f;
                touchStartPos = Input.mousePosition;
            }

            transform.forward = Vector3.Lerp(transform.forward, new Vector3(finalPos.x, finalPos.y, transform.position.z), Time.deltaTime / smoothingTime);

        }
    }
}
