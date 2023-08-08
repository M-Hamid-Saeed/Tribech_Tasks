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
        [SerializeField] playerHealth playerUI;
    
        [SerializeField] ParticleType DeathParticleType;
        [SerializeField] SoundType InsectDeathSoundType;
        [SerializeField] int killScore;



        public float currentHealth;

        private void Start()
        {

            currentHealth = totalHealth;
            UI.SetMaxHealth(totalHealth);
        }
        public void Damage(float damage)
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
            ReferenceManager.Instance.crossHaironHit.SetActive(false);
        }

   
        private void SetUI()
        {
            UI.SetDamage(currentHealth);
            
        }
       
        public void Dead()
        {
           // ReferenceManager.Instance.walkerManager.InsectCounterManage();
            ReferenceManager.Instance.mainUIManager.AddKillCount();
            SoundManager.Instance?.PlayOneShot(InsectDeathSoundType, .7f);
            //playerUI.PlayerScore(killScore);
            //playerUI.AddKill();
            ParticleManager.Instance?.PlayParticle(DeathParticleType, transform.position);
            this.currentHealth = totalHealth;
            
            gameObject.GetComponent<AiWalker>().FreePool();
            
        }

        public void AddScore()
        {
            CoinsManager.Instance?.AddCoins(killScore);
            CoinsManager.Instance.AddCoins(CoinsManager.Instance.WordPointToCanvasPoint(CoinsManager.Instance.mainCam, transform.position, CoinsManager.Instance.canvasRect), 2);
        }
    }
}
