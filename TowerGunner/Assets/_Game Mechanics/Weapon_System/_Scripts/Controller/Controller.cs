using Character_Management;
using GameAssets.GameSet.GameDevUtils.Managers;
using UnityEngine;
using DarkVortex;
public class Controller : MonoBehaviour
{

    // [SerializeField] AI_AnimationController animationController;
    [SerializeField] FiringSystem firingSystem;

    [SerializeField] float _rotateSpeed;
    [SerializeField] float maxRotationX;
    [SerializeField] float maxRotationY;
    [SerializeField] float lerpFactor;
    [SerializeField] WeaponAnimation_Controller animationController;
    
    private bool canPlay = true;
    [SerializeField] GameObject Missile;
    [SerializeField] Transform missileFirePoint;
    //Customized Touch Input 

    public int insectcounter = 0;


    Vector3 mousePosition;
    private Vector3 lookDirection;

    private void Awake()
    {
        
       
        GameController.onHome += OnGameStart;
        GameController.onGameplay += OnGamePlay;      
        GameController.onLevelComplete += OnGameStart;
        GameController.onLevelFail += OnGameStart;     
        Vibration.Init();
        
    }

    private void Update()
    {
        if (canPlay)
        {
            LookRotation();
            ShootControll();
        }
    }

    private void ShootControll()
    {

        if (Input.GetMouseButton(0))
        {
            animationController.ShootingAnimation(true);

            mousePosition = ReferenceManager.Instance.input.GetPosition();
            firingSystem.Shot(mousePosition);

        }
        else
        {
            animationController.ShootingAnimation(false);
            ReferenceManager.Instance?.machineGunAnimation.DOPause();
            ReferenceManager.Instance?.machineGunShakeAnimation.DOPause();
        }
    }
   
    private void LookRotation()
    {

        float targetLookAmountX = ReferenceManager.Instance.input.Horizontal* _rotateSpeed;
        float targetLookAmountY = -ReferenceManager.Instance.input.Vertical * _rotateSpeed;

        
        lookDirection = new Vector3(targetLookAmountX, targetLookAmountY, transform.position.z).normalized;
        
        Quaternion rotTarget = Quaternion.LookRotation(lookDirection);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotTarget, lerpFactor * Time.deltaTime);
        
        Quaternion rot = transform.rotation;
        rot.x = Mathf.Clamp(rot.x, -maxRotationX, maxRotationX) ;
        rot.y = Mathf.Clamp(rot.y, -maxRotationY, maxRotationY);
        transform.rotation = rot;
    }

    private void OnGamePlay()
    {
       canPlay = true;
    }
    private void OnGameStart()
    {
        canPlay = false;
    }

    private void spawnMissle()
    {
        
        GameObject missle = Instantiate(Missile, new Vector3(missileFirePoint.position.x,missileFirePoint.position.y,missileFirePoint.position.z),Quaternion.identity);
        Debug.Log(missle.name);
        Vector3 dir = ReferenceManager.Instance.input.GetPosition()- missle.transform.position;
        missle.GetComponent<Missile>().SetDirection(dir);

    }
    public void OnMissileButtonPressed()
    {
        canPlay = false;
        spawnMissle();
        if (Input.GetMouseButtonUp(0))
            canPlay = true;
    }

}
