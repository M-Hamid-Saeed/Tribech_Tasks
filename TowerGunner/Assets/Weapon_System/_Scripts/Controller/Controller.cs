using UnityEngine;

public class Controller : MonoBehaviour
{

    // [SerializeField] AI_AnimationController animationController;
    [SerializeField] FiringSystem firingSystem;

    [SerializeField] float _rotateSpeed;
    [SerializeField] float maxRotationX;
    [SerializeField] float maxRotationY;
    [SerializeField] float lerpFactor;
    private Quaternion initialRotation;
    private bool isFirstTouch = true;

    //Customized Touch Input 
    [SerializeField] InputManager touchInputManager;




    Vector3 mousePosition;
    public Vector3 lookDirection;
    private Vector3 initialLookDirection;
    Quaternion rot;
    private void Awake()
    {
        // firingSystem.Init();

    }
    private void Start()
    {
        // initialRotation = transform.rotation;
        rot = transform.rotation;
    }

    private void Update()
    {
        //  mousePosition = MouseWorldInput.GetPosition();

        LookRotation();
        //ShootControll();

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

        Quaternion lookRotation = Quaternion.LookRotation(lookDirection);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * lerpFactor);
        //transform.forward = Vector3.Lerp(transform.forward, lookDirection, Time.deltaTime * lerpFactor);


        rot = transform.rotation;
        rot.x = Mathf.Clamp(rot.x, -maxRotationX, maxRotationX);
        rot.y = Mathf.Clamp(rot.y, -maxRotationY, maxRotationY);
        transform.rotation = rot;


    }


}
