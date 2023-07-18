using UnityEngine;

public class Controller : MonoBehaviour
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
        ShootControll();
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

        //Input from InputManager Script
        lookDirection = new Vector3(targetLookAmountX, targetLookAmountY, transform.position.z).normalized;

        // lookDirection = (mousePosition - transform.position).normalized; 

        transform.forward = Vector3.Lerp(transform.forward, lookDirection, lerpFactor * Time.deltaTime);
        Quaternion rot = transform.localRotation;
        rot.x = Mathf.Clamp(rot.x, -maxRotationX, maxRotationX);
        rot.y = Mathf.Clamp(rot.y, -maxRotationY, maxRotationY);
        transform.localRotation = rot;





    }

}
