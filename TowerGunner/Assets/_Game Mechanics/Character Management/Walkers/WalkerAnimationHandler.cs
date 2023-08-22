using Character_Management;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkerAnimationHandler : MonoBehaviour
{
    AiWalker walker;
    float AnimationHold_Time;
    private void Start()
    {
        walker = GetComponentInParent<AiWalker>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            // Quaternion rot = collision.gameObject.transform.localRotation;

            TweenImpact();
           
        }
        
        
    }


    private void TweenImpact()
    {                            
        // transform.DOShakeRotation(.1f, 10, 10, 30);
        transform.DOShakeRotation(.1f,12f,10,90);
        
        transform.parent.DOMove(new Vector3(transform.parent.position.x - 0.06f, transform.parent.position.y, transform.parent.position.z - 0.07f), 0.1f);
        //transform.DOPunchPosition(new Vector3(transform.position.x, transform.position.y, transform.position.z - 0.2f),.1f);
        // collision.gameObject.transform.localRotation = rot;
    }
    
   
}
