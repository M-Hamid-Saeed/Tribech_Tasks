using AxisGames.ParticleSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{

   // [SerializeField] AI_AnimationController animationController;
    [SerializeField] FiringSystem firingSystem;

    [SerializeField] float _rotateSpeed;
    [SerializeField] float maxRotationAmount;
    //Customized Touch Input 
    [SerializeField] InputManager touchInputManager;
    


    Vector3 mousePosition;
    Vector3 lookDirection;

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

      /*  if (touchInputManager.isMousePressed)
        {
            mousePosition = MouseWorldInput.GetPosition();
            firingSystem.Shot(mousePosition);
        }*/
            
       
    
    }

    private void LookRotation()
    {
        if (Input.GetMouseButton(0))
        {
            float targetLookAmountX = touchInputManager.Horizontal * _rotateSpeed;// Mathf.Clamp(touchInputManager.Horizontal * _rotateSpeed, -maxRotationAmount, maxRotationAmount);
            float targetLookAmountY = touchInputManager.Vertical * _rotateSpeed;//Mathf.Clamp(touchInputManager.Vertical * _rotateSpeed, -maxRotationAmount, maxRotationAmount);

            lookDirection = new Vector3(targetLookAmountX, targetLookAmountY, transform.position.z).normalized;

            /* lookDirection = (mousePosition - transform.position).normalized; */

            transform.forward = Vector3.Lerp(transform.forward, lookDirection, _rotateSpeed * Time.deltaTime);
        }
    }

}
