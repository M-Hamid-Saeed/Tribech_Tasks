using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkerAnimationHandler : MonoBehaviour
{
   

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
            TweenImpact();
        
        
    }

    private void TweenImpact()
    {
        transform.DOShakeRotation(1f,20,10,90);
    }
    private void ScaleImpact()
    {
        TweenImpact();
        transform.DOScale(transform.localScale *2f ,2f);
    }
}
