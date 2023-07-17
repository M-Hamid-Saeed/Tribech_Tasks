using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    [SerializeField]private Vector3 prevMousePosition;
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
            //isMousePressed = true;
            Debug.Log("MOUSE CLICKED"+ Input.mousePosition);

            prevMousePosition = Input.mousePosition;
            Debug.Log("MOUSE " + prevMousePosition);
        }

        if (Input.GetMouseButton(0))
        {
            Debug.Log("MOUSE$ BUTTON DOWN " + prevMousePosition);

            //Calculating current touch pos
            touchDelta = Input.mousePosition - prevMousePosition;
            //Multiply by senstivity
            var deltaPosition = touchDelta * senstivity;
            //To get the exact position on the screen
            deltaPosition.x /= Screen.width / 2f;
            deltaPosition.y /= Screen.height / 2f;
            Horizontal = deltaPosition.x;
            Vertical = deltaPosition.y;
            Debug.Log("horiz" + Horizontal);
            Debug.Log("vertic" + Vertical);
            //prevMousePosition = Input.mousePosition;
           
        }
        else if (Input.GetMouseButtonUp(0))
        {
            Debug.Log("MOUSE$ UP " + prevMousePosition);

            prevMousePosition = Input.mousePosition;
            //isMousePressed = false;
        }
         
    }
   
    
}
