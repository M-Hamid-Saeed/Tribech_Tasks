using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UIElements;

public class OffScreenIndicatorNew : MonoBehaviour
{
    public Camera gameCamera;
    [SerializeField] GameObject leftPointer;
    [SerializeField] Image rightPointer;
    public List<GameObject> enemiesList = new List<GameObject>();
    public bool shouldIndicateNext = true;
    private GameObject targetObject;
    private Dictionary<int, GameObject> enemies = new Dictionary<int, GameObject>();



    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Insects"))
        {
            GameObject enemy = other.gameObject;
            if (shouldIndicateNext)
            {
                shouldIndicateNext = false;
                targetObject = enemy;
                enemiesList.Add(enemy);
            }

                SetIndicator(targetObject.transform);
            
        }
    }

    void SetIndicator(Transform obj)
    {
        shouldIndicateNext = false;
        // get the position on screen, in screen coordinates
        Vector3 objPosition = obj.position;
        Vector3 objScreenPos = gameCamera.WorldToScreenPoint(objPosition);
        if (Mathf.Approximately(objScreenPos.z, 0))
        {
           // return;
        }

        // save half screen resulution because we will need it often
        Vector3 halfScreen = new Vector3(Screen.width, Screen.height) / 2;

        // we don't want the Z-Value in our center-vector because it would
        // cause problems when normalizing it
        Vector3 objScreenPosNoZ = objScreenPos;
        objScreenPosNoZ.z = 0;
        // get the vector from the center of the screen to the
        // calculated screen position
        Vector3 screenCenterPos = objScreenPosNoZ - halfScreen;

        // we have to invert the vector when we are looking away from the target
        // the vector is just projected on the view-plane, think looking in a mirror
        if (objScreenPos.z < 0)
        {
            screenCenterPos *= -1;
        }

        // debug check, if the ray is pointing in the wanted direction
        // can only be seen with gizmos enabled, in scene view (3D Mode only)
        //Debug.DrawRay(halfScreen, screenCenterPos.normalized * 100000, Color.red);    

        //0-1 is on screen, < 0.0 && > 1.0 is off screen. Over 10 it flips signs and behaves weird, show arrows and adjust opacity only in range to 10
        //Remove -0.5 to center the value to 0.0 and multiple it by 2 to make it -1 to 1
        Vector2 distance = new Vector2(((objScreenPos.x / Screen.width) - 0.5f) * 2f, ((objScreenPos.y / Screen.height) - 0.5f) * 2f);


        // check if the target is off the screen, then show the arrow
        if (objScreenPos.z < 0 || objScreenPos.x > Screen.width || objScreenPos.x < 0 || objScreenPos.y > Screen.height || objScreenPos.y < 0)
        {
            //Use the distance value from x,y that is furthest
            float maxDistance = Mathf.Max(Mathf.Abs(distance.x), Mathf.Abs(distance.y));
            //Enemy is off screen when the value is more than 1.0. Clamp the distance to 10.0f. Adjust opacity of arrow based on the distance
           /* if (maxDistance > 1.0f)
            {
                maxDistance = Mathf.Clamp(maxDistance - 1f, 0.0f, 10.0f);
                float opacityValue = (10f - maxDistance) / 10f;
                leftPointer.GetComponent<Image>().tintColor = new Color(1, 0, 0, opacityValue);
            }
            else
            {
                leftPointer.GetComponent<Image>().tintColor = new Color(1, 0, 0, 1);
            }*/

            // if you have a arrow on your symbol, pointing in the
            // direction, enable it here:
            leftPointer.SetActive(true);


            // rotate it to point towards the target position          
           // leftPointer.GetComponent<RectTransform>().rotation = Quaternion.FromToRotation(Vector3.up, screenCenterPos);

            // normalized ScreenCenterPosition
            Vector3 norSCP = screenCenterPos.normalized;

            // avoid dividing by zero
            if (norSCP.x == 0)
            {
                norSCP.x = 0.01f;
            }
            if (norSCP.y == 0)
            {
                norSCP.y = 0.01f;
            }

            // stretch the normalized screenCenterPosition so that X is at the edge
            Vector3 xScreenCP = norSCP * (halfScreen.x / Mathf.Abs(norSCP.x));
            // stretch the normalized screenCenterPosition so that Y is at the edge
            Vector3 yScreenCP = norSCP * (halfScreen.y / Mathf.Abs(norSCP.y));



            // compare the streched vectors in length and use the smaller one
            if (xScreenCP.sqrMagnitude < yScreenCP.sqrMagnitude)
            {
                objScreenPos = halfScreen + xScreenCP;
            }
            else
            {
                objScreenPos = halfScreen + yScreenCP;
            }
        }
        else
        {
            // if you have a arrow on your symbol, pointing in the
            // direction, disable it here:
            leftPointer.SetActive(false);
        }

        // clamp the result, so we can always see the full marker/tracker image
        float margin = 70;

        objScreenPos.z = 0;

        objScreenPos.x = Mathf.Clamp(objScreenPos.x, margin, Screen.width - margin);
        objScreenPos.y = Mathf.Clamp(objScreenPos.y, margin, Screen.height - margin);
        Quaternion rot = leftPointer.GetComponent<RectTransform>().rotation;
        if (objScreenPos.x < 500)
        {
            rot.y = 180;
            leftPointer.GetComponent<RectTransform>().rotation = rot;
        }
        else
        {
            rot.y = 0f;
            leftPointer.GetComponent<RectTransform>().rotation = rot;
        }

        leftPointer.GetComponent<RectTransform>().position = objScreenPos;
    }
}
