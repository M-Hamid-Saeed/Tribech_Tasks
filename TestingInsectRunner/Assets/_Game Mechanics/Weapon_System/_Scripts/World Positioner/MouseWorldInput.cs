using UnityEngine;
public class MouseWorldInput : MonoBehaviour
{
   // private static MouseWorldInput instance;

    [SerializeField] Camera camera;
    public float shootRange;
    [SerializeField] private LayerMask targetLayerMask;

   

    public  Vector3 GetPosition()
    {
        RaycastHit hit;
        if(Physics.Raycast(camera.transform.position, camera.transform.forward, out hit, shootRange))
            return hit.point;
        return hit.point;
    }
}
