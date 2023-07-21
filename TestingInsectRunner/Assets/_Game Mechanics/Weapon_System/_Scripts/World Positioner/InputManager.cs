using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    [SerializeField]private Vector3 prevMousePosition;
    public Vector3 prevMouseUpPosition;
    public Vector3 touchDelta;
    private float horizontal;
    private float vertical;
    public float senstivity;
    public bool isMouseUp = false;
    float xRotation = 0f;
    float yRotation = 0f;
    private Vector3 rotated;
    //Mouse Position 
    [Header("------Mouse Position------")]
    [SerializeField] Camera camera;
    public float shootRange;
    [SerializeField] private LayerMask targetLayerMask;


    public float Horizontal
    {
        get { return horizontal; }
        set { horizontal = value; }
    }
    public float Vertical
    {
        get { return vertical; }
        set { vertical = value; }
    }
    private void Update()
    {
        MouseInput();
      // newMouseInput();
    }
    public void MouseInput()
    {
        
        
        if (Input.GetMouseButtonDown(0))
        {
            
            Debug.Log("MOUSE CLICKED"+ Input.mousePosition);

            prevMousePosition = Input.mousePosition;
            
        }

        if (Input.GetMouseButton(0))
        {
            // Drag Calculate
            touchDelta = Input.mousePosition - prevMousePosition;
             Vector2 delta= touchDelta * senstivity * Time.deltaTime;
             
            //To get the exact position on the screen
            rotated.x += delta.x;
            rotated.y += delta.y;
       
            Horizontal = rotated.x;
            Vertical = rotated.y;
            prevMousePosition = Input.mousePosition;

            // 
        }
        else if (Input.GetMouseButtonUp(0))
        {
            
            isMouseUp = true;
        }
         
    }


    public float mouseX;
    public float mouseY;
    void newMouseInput()
    {
        if(Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved)
        {
             mouseX = Input.GetTouch(0).deltaPosition.x * senstivity;
            
             mouseY = Input.GetTouch(0).deltaPosition.y * senstivity;
        }
    }
    


    public Vector3 GetPosition()
    {
        RaycastHit hit;
        if (Physics.Raycast(camera.transform.position, camera.transform.forward, out hit, shootRange))
            return hit.point;
        else
        {
            return camera.transform.position + camera.transform.forward * shootRange;
        }
        
    }


}