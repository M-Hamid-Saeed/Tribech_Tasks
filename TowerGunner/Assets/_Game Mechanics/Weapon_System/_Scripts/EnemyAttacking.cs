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
    [SerializeField] DOTweenAnimation playerHitAnimation;
    [SerializeField] CameraShake_Management CameraShakerManager;



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
            CameraShakerManager.ShakeCamera();
            Vibration.VibrateNope();
            playerHitAnimation.DORestartById("BloodHitEffect");
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
        // insect.transform.DOScale(insect.transform.localScale * .5f, 2f);
        yield return new WaitForSeconds(.3f);
        insect_health.Dead();
        
       

    }


}
