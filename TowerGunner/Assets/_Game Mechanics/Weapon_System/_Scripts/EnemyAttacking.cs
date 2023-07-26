using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttacking : MonoBehaviour
{
    [SerializeField] playerHealth player_health;
    
    [SerializeField] LayerMask insectLayer;
    [SerializeField] Transform attackPoint;
    [SerializeField] float attackRange;

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
        }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }


}
