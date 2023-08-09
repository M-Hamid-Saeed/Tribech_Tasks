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
            Quaternion rot = transform.localRotation;
            TweenImpact(rot);
        }
        
        
    }

    private void TweenImpact(Quaternion rot)
    {
        transform.DOShakeRotation(.5f,20,10,90);
        transform.localRotation = rot;
    }
   
}
