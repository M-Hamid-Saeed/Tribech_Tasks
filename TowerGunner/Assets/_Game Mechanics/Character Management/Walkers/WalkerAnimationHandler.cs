using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkerAnimationHandler : MonoBehaviour
{
   

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            Quaternion rot = collision.gameObject.transform.localRotation;
            TweenImpact(rot,collision);
        }
        
        
    }

    private void TweenImpact(Quaternion rot, Collision collision)
    {
        transform.DOShakeRotation(.2f, 20, 10, 40);
        collision.gameObject.transform.localRotation = rot;
    }
   
}
