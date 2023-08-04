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
        [SerializeField] HealthBarUI healthUI;
        [SerializeField] ParticleType DeathParticleType;
        [SerializeField] SoundType InsectDeathSoundType;
       

        public float currentHealth;

        private void Start()
        {

            currentHealth = totalHealth;
            healthUI.SetMaxHealth(totalHealth);
        }
        public void Damage(float damage)
        {
            
            if (damage <= insectMaxDamageTaken)
            
                currentHealth -= damage;
            else
                currentHealth -= insectMaxDamageTaken;


            if (currentHealth <= 0)
                Dead();
              
            SetUI();

        }

   
        private void SetUI()
        {
            healthUI.SetDamage(currentHealth);

        }
       

        public void Dead()
        {
            WalkerManager.InsectCounterManage();
            SoundManager.Instance.PlayOneShot(InsectDeathSoundType, 1f);
            ParticleManager.Instance?.PlayParticle(DeathParticleType, transform.position);
            Destroy(gameObject);

        }
    }
}
