using UnityEngine;
public class MouseWorldInput : MonoBehaviour
{
    private static MouseWorldInput instance;

    [SerializeField] Camera camera;
    [SerializeField] private LayerMask targetLayerMask;

    private void Awake()
    {
        instance = this;
    }

    public static Vector3 GetPosition()
    {
        Ray ray = instance.camera.ScreenPointToRay(Input.mousePosition);
        Physics.Raycast(ray, out RaycastHit rayCastHit, float.MaxValue, instance.targetLayerMask);
        return rayCastHit.point;
    }
}
