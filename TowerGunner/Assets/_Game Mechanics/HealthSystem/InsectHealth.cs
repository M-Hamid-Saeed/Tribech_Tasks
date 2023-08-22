using AxisGames.ParticleSystem;
using GameAssets.GameSet.GameDevUtils.Managers;
using System.Collections;
using UnityEngine;
namespace Character_Management
{
    public class InsectHealth : MonoBehaviour, IDamageable
    {
        [SerializeField] float totalHealth;
        [SerializeField] float insectMaxDamageTaken;
        public float insectAttackingDamage;
        [SerializeField] HealthBarUI UI;

    
        [SerializeField] ParticleType deathParticle;
        [SerializeField] SoundType insectDeathSound;
        [SerializeField] SoundType bulletHitSound;
        [SerializeField] ParticleType bulletHitParticle;
        [SerializeField] int killScore;



        public float currentHealth;

        private void Start()
        {

            currentHealth = totalHealth;
            UI.SetMaxHealth(totalHealth);
        }
        public void Damage(float damage, ContactPoint hitPoint)
        {
           
            if (damage <= insectMaxDamageTaken)
            
                currentHealth -= damage;
            else
                currentHealth -= insectMaxDamageTaken;


            if (currentHealth <= 0)
            {
                Dead();
                AddScore();
            }
              
            SetUI();
            PlayParticle_Sound(hitPoint.point);
            ReferenceManager.Instance.crossHaironHit.SetActive(false);
        }

   
        private void SetUI()
        {
            UI.SetDamage(currentHealth);
            
        }
       
        public void Dead()
        {
            
            ReferenceManager.Instance.mainUIManager.AddKillCount(this.gameObject);
            SoundManager.Instance?.PlayOneShot(insectDeathSound, .8f);
            this.GetComponentInChildren<BoxCollider>().enabled = false;
            ParticleManager.Instance?.PlayParticle(deathParticle, transform.position);
            gameObject.GetComponent<AiWalker>().FreePool();
            this.currentHealth = totalHealth;
            ReferenceManager.Instance.offscreenIndicator.shouldIndicateNext = true;
        }


        public void PlayParticle_Sound(Vector3 collisionPoint)
        {
            ParticleManager.Instance?.PlayParticle(bulletHitParticle, collisionPoint);
            SoundManager.Instance.PlayOneShot(bulletHitSound, 1f);
        }
        public void AddScore()
        {
            CoinsManager.Instance?.AddCoins(killScore);
            CoinsManager.Instance.AddCoins(CoinsManager.Instance.WordPointToCanvasPoint(CoinsManager.Instance.mainCam, transform.position, CoinsManager.Instance.canvasRect), 2);
        }
    }
}
