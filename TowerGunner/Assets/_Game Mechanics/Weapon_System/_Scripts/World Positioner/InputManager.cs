using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    [SerializeField] private Vector3 prevMousePosition;
    public Vector3 prevMouseUpPosition;
    public Vector3 touchDelta;
    private float horizontal;
    private float vertical;
    public float senstivity;
    public Gun currentGun;
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
            prevMousePosition = Input.mousePosition;
        }

        if (Input.GetMouseButton(0))
        {
            // Drag Calculate
            touchDelta = prevMousePosition-Input.mousePosition ;
            Vector2 delta = touchDelta * senstivity * Time.deltaTime;

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
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved)
        {
            mouseX = Input.GetTouch(0).deltaPosition.x * senstivity;

            mouseY = Input.GetTouch(0).deltaPosition.y * senstivity;
        }
    }



    /* public Vector3 GetPosition()
     {
         Ray screenPointToRay = camera.ViewportPointToRay(new Vector3(0.5f, 0.5f));

         // Define a custom starting point for the ray (gun muzzle fire point)
         Vector3 rayStartPoint = currentGun.firePoint.position;

         if (Physics.Raycast(rayStartPoint, camera.transform.forward, out RaycastHit hit, shootRange))
         {
             return hit.point;
         }
         else
         {
             // If no hit point, return a point along the forward direction from the gun's muzzle
             return rayStartPoint + camera.transform.forward * shootRange;
         }
     }*/
    public Vector3 GetPosition()
    {
        Ray screenPointToRay = camera.ViewportPointToRay(new Vector2(Screen.width/2f,Screen.height/2f));

        if (Physics.Raycast(screenPointToRay, out RaycastHit hit,shootRange))
        {
            return hit.point;
        }
        else
        {
            // If no hit point, return a point along the camera's forward direction
            return camera.transform.position + camera.transform.forward * shootRange;
        }
    }


}
