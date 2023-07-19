using UnityEngine;

public class testingController : MonoBehaviour
{

    // [SerializeField] AI_AnimationController animationController;
    [SerializeField] FiringSystem firingSystem;

    [SerializeField] float _rotateSpeed;
    [SerializeField] float maxRotationX;
    [SerializeField] float maxRotationY;
    [SerializeField] float lerpFactor;

    //Customized Touch Input 
    [SerializeField] InputManager touchInputManager;




    Vector3 mousePosition;
    public Vector3 lookDirection;

    private void Awake()
    {
        firingSystem.Init();
    }

    private void Update()
    {
        //  mousePosition = MouseWorldInput.GetPosition();

        LookRotation();
       // ShootControll();
    }

    private void ShootControll()
    {

        if (Input.GetMouseButton(0))
        {
            mousePosition = touchInputManager.GetPosition();
            firingSystem.Shot(mousePosition);
        }



    }

    private void LookRotation()
    {


        float targetLookAmountX = touchInputManager.Horizontal * _rotateSpeed;
        float targetLookAmountY = touchInputManager.Vertical * _rotateSpeed;


        lookDirection = new Vector3(targetLookAmountX, targetLookAmountY, transform.position.z).normalized;

        Quaternion rotTarget = Quaternion.LookRotation(lookDirection);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotTarget, lerpFactor * Time.deltaTime);

        Quaternion rot = transform.localRotation;
        rot.x = Mathf.Clamp(rot.x, -maxRotationX, maxRotationX);
        rot.y = Mathf.Clamp(rot.y, -maxRotationY, maxRotationY);
        transform.localRotation = rot;





    }

}
