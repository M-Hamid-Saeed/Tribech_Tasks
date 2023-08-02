using Character_Management;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EZCameraShake;

public class EnemyAttacking : MonoBehaviour
{
    [SerializeField] playerHealth player_health;
    [SerializeField] LayerMask insectLayer;
    [SerializeField] Transform attackPoint;
    [SerializeField] float attackRange;
    
    [SerializeField] ParticleSystem playerHitParticle;
    
    
    private void FixedUpdate()
    {
        GetHit();
    }



    void GetHit()
    {
        Collider[] insects = Physics.OverlapSphere(attackPoint.position, attackRange, insectLayer);

        foreach (Collider insect in insects)
        {
            InsectHealth insect_health = insect.GetComponentInParent<InsectHealth>();
            player_health.Damage(insect_health.insectAttackingDamage);
            //Instantiate(playerHitParticle, this.transform);
           // cameraShakeManager.ShakeCamera();

            
            StartCoroutine(waitforDestroy(insect_health));
        }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }

    IEnumerator waitforDestroy(InsectHealth insect_health)
    {
       // insect.transform.DOScale(insect.transform.localScale * .5f, 2f);
        yield return new WaitForSeconds(.5f);
        insect_health.currentHealth = 0;
        
    }


}
