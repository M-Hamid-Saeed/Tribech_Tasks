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
    [SerializeField] WeaponAnimation_SoundController animationController;
    
    private bool canPlay = true;
    [SerializeField] GameObject Missile;
    [SerializeField] Transform missileFirePoint;
    //Customized Touch Input 

    public int insectcounter = 0;

    public int totalMissles;
    Vector3 mousePosition;
    private Vector3 lookDirection;

    private void Awake()
    {

        Vibration.Init();
        GameController.onHome += OnGameStart;
        GameController.onGameplay += OnGamePlay;      
        GameController.onLevelComplete += OnGameStart;
        GameController.onLevelFail += OnGameStart;
        totalMissles = ReferenceManager.Instance.levelManager.levels[LevelManager.CurrentLevelNumber].NoOfMissles;
        
        
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
            if(canPlay)
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
        rotTarget.x = Mathf.Clamp(rotTarget.x, -maxRotationX, maxRotationX);
        rotTarget.y = Mathf.Clamp(rotTarget.y, -maxRotationY, maxRotationY);
        rotTarget.z = Mathf.Clamp(rotTarget.z, 0, 0);
        //rotTarget.z = 0;
        transform.rotation = Quaternion.Lerp(transform.rotation, rotTarget, lerpFactor * Time.deltaTime);

        Quaternion RequiredRot = transform.rotation;
        //RequiredRot.x = Mathf.Clamp(RequiredRot.x, -maxRotationX, maxRotationX) ;
        //RequiredRot.y = Mathf.Clamp(RequiredRot.y, -maxRotationY, maxRotationY);
        //RequiredRot.z = 0f;       
        //transform.rotation = Quaternion.Euler(RequiredRot.x,RequiredRot.y,RequiredRot.z);
        //transform.rotation = RequiredRot;
        
   
    }

    private void OnGamePlay()
    {
       canPlay = true;
    }
    private void OnGameStart()
    {
        canPlay = false;
        animationController.ShootingAnimation(false);
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
        
        if ( totalMissles <= 0)
            return;
        canPlay = false;
        spawnMissle();
        totalMissles--;
        if (Input.GetMouseButtonUp(0))
            canPlay = true;
        ReferenceManager.Instance.mainUIManager.SetMissleUI();
    }

}
