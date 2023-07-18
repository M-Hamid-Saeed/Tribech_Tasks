using AxisGames.ParticleSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{

    [SerializeField] AI_AnimationController animationController;
    [SerializeField] FiringSystem firingSystem;

    [SerializeField] float _rotateSpeed;


    Vector3 mousePosition;
    Vector3 lookDirection;

    private void Awake()
    {
        firingSystem.Init();
    }

    private void Update()
    {
        mousePosition = MouseWorldInput.GetPosition();
        LookRotation();

        ShootControll();
    }

    private void ShootControll()
    {
        if (Input.GetMouseButtonDown(0))
        {
            animationController.ShootingAnimation(true);
            mousePosition = MouseWorldInput.GetPosition();

            firingSystem.Shot(mousePosition);
        }

        if (Input.GetMouseButtonUp(0))
        {
            animationController.ShootingAnimation(false);
            animationController.Idle();
        }
    }

    private void LookRotation()
    {
        lookDirection = (mousePosition - transform.position).normalized;

        transform.forward = Vector3.Lerp(transform.forward, lookDirection, _rotateSpeed * Time.deltaTime);
    }

}
