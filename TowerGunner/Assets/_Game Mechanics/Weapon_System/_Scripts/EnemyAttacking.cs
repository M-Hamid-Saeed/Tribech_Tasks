using Character_Management;
using DG.Tweening;
using System.Collections;
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
            Vibration.Cancel();
            InsectHealth insect_health = insect.GetComponentInParent<InsectHealth>();
            player_health.Damage(insect_health.insectAttackingDamage);
            ReferenceManager.Instance.CameraShakeManager.ShakeCamera();
            Vibration.VibrateNope();
            ReferenceManager.Instance.playerHitAnimation.DORestartbyID("BloodHitEffect");
            Debug.Log("Player Attacked");
            insect_health.GetComponentInChildren<BoxCollider>().enabled = false;
            StartCoroutine(waitforDestroy(insect_health));
        }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }

    IEnumerator waitforDestroy(InsectHealth insect_health)
    {
      
        yield return new WaitForSeconds(.3f);
        insect_health.Dead();

    }


}
