using UnityEngine;

public class CustomInputManager : MonoBehaviour
{
    private Vector3 initialMousePosition;
    private Vector3 currentMousePosition;

    private bool isDragging = false;
    private float dragDistanceThreshold = 5f; // Minimum distance to consider as a drag

    public Vector3 DragDelta { get; private set; }

    public float Horizontal { get; private set; }
    public float Vertical { get; private set; }

    private void Update()
    {
        // Track mouse position
        currentMousePosition = Input.mousePosition;

        if (Input.GetMouseButtonDown(0))
        {
            // Record initial mouse position on mouse button down
            initialMousePosition = currentMousePosition;
            isDragging = false;
        }

        if (Input.GetMouseButton(0))
        {
            // Check if mouse movement exceeds the drag distance threshold
            if (!isDragging && Vector3.Distance(initialMousePosition, currentMousePosition) >= dragDistanceThreshold)
            {
                isDragging = true;
            }

            // Calculate the drag delta
            if (isDragging)
            {
                DragDelta = currentMousePosition - initialMousePosition;

                // Calculate the normalized horizontal and vertical values
                Horizontal = DragDelta.x / Screen.width;
                Vertical = DragDelta.y / Screen.height;
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            // Reset drag delta, horizontal, and vertical on mouse button up
            DragDelta = Vector3.zero;
            Horizontal = 0f;
            Vertical = 0f;
            isDragging = false;
        }
    }
}
