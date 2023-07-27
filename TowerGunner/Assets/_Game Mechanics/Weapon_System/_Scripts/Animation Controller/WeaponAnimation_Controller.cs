using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponAnimation_Controller : MonoBehaviour
{
    [SerializeField] Animator animator;



    public void ShootingAnimation(bool state)
    {
        animator.SetBool("isFiring", state);
    }
}
