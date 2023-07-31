using Character_Management;
using GameAssets.GameSet.GameDevUtils.Managers;
using UnityEngine;

public class Controller : MonoBehaviour
{

    // [SerializeField] AI_AnimationController animationController;
    [SerializeField] FiringSystem firingSystem;

    [SerializeField] float _rotateSpeed;
    [SerializeField] float maxRotationX;
    [SerializeField] float maxRotationY;
    [SerializeField] float lerpFactor;
    [SerializeField] WeaponAnimation_Controller animationController;

    //Customized Touch Input 
    [SerializeField] InputManager touchInputManager;

    public int insectcounter = 0;


    Vector3 mousePosition;
    public Vector3 lookDirection;

    private void Awake()
    {
        firingSystem.Init();
    }

    private void Update()
    {
        //  mousePosition = MouseWorldInput.GetPosition();

        insectcounter = WalkerManager.insectCounter;
        if (Input.GetMouseButton(0))
            LookRotation();
        ShootControll();
    }

    private void ShootControll()
    {

        if (Input.GetMouseButton(0))
        {
            animationController.ShootingAnimation(true);
            mousePosition = touchInputManager.GetPosition();
            firingSystem.Shot(mousePosition);
            
        }
        else
            animationController.ShootingAnimation(false);
    }
    
    private void LookRotation()
    {

       
        float targetLookAmountX = -touchInputManager.Horizontal* _rotateSpeed;
        float targetLookAmountY = touchInputManager.Vertical * _rotateSpeed;

        
        lookDirection = new Vector3(targetLookAmountX, targetLookAmountY, transform.position.z).normalized;
        
        Quaternion rotTarget = Quaternion.LookRotation(lookDirection);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotTarget, lerpFactor * Time.deltaTime);
        
        Quaternion rot = transform.rotation;
        rot.x = Mathf.Clamp(rot.x, -maxRotationX, maxRotationX) ;
        rot.y = Mathf.Clamp(rot.y, -maxRotationY, maxRotationY);
        transform.rotation = rot;

    }
   
}
