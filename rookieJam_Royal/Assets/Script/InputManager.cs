using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    private Vector3 prevMousePosition;
    public Vector3 touchDelta;
    private float horizontal;
    private float vertical;
    public float senstivity;
    public bool isMousePressed = false;
    
    
    
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
    }
    public void MouseInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            //First Touch
            isMousePressed = true;
            prevMousePosition = Input.mousePosition;
        }

        if (Input.GetMouseButton(0))
        {
            
            //Calculating current touch pos
            touchDelta = Input.mousePosition - prevMousePosition;
            //Multiply by senstivity
            var deltaPosition = touchDelta * senstivity;
            //To get the exact position on the screen
            deltaPosition.x /= Screen.width / 2f;
            deltaPosition.y /= Screen.height / 2f;
            Horizontal = deltaPosition.x;
            Vertical = deltaPosition.y;
            prevMousePosition = Input.mousePosition;
            Debug.Log(deltaPosition.y);
            //Debug.Log(currentMousePosition);
        }
        if (Input.GetMouseButtonUp(0))
        {
            isMousePressed = false;
        }
         
    }
    private void ConvertToWorldSpace(Vector2 mousePos)
    {
        Camera.main.ScreenToWorldPoint(mousePos);
    }
    
}
