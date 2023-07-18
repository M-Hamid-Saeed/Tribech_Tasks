using AxisGames.ParticleSystem;
using System.Collections;
using System.Collections.Generic;
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

        /*  if (touchInputManager.isMousePressed)
          {
              mousePosition = MouseWorldInput.GetPosition();
              firingSystem.Shot(mousePosition);
          }*/



    }

    private void LookRotation()
    {


        float targetLookAmountX = Mathf.Clamp(touchInputManager.Horizontal * _rotateSpeed, -maxRotationX, maxRotationX);
        float targetLookAmountY = Mathf.Clamp(touchInputManager.Vertical * _rotateSpeed, -maxRotationY, maxRotationY);

        //Input from InputManager Script
        lookDirection = new Vector3(targetLookAmountX, targetLookAmountY, transform.position.z).normalized;

        /* lookDirection = (mousePosition - transform.position).normalized; */

        transform.forward = Vector3.Lerp(transform.forward, lookDirection, lerpFactor * Time.deltaTime);
        Debug.Log(lookDirection);



    }

}
