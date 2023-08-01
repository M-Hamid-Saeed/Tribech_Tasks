using AxisGames.ParticleSystem;
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
        [SerializeField] ParticleType particleType;

        public float currentHealth;

        private void Start()
        {

            currentHealth = totalHealth;
            healthUI.SetMaxHealth(totalHealth);
        }
        public void Damage(float damage)
        {
            if (damage <= insectMaxDamageTaken)
            {
                currentHealth -= damage;
            }

            else

                currentHealth -= insectMaxDamageTaken;

            if (currentHealth <= 0)
            {
                PlayDeathParticle();
                WalkerManager.insectCounter--;
                Destroy(gameObject);
            }
           
            SetUI();

        }

   
        private void SetUI()
        {
            healthUI.SetDamage(currentHealth);

        }
       

        private void PlayDeathParticle()
        {
            ParticleManager.Instance?.PlayParticle(particleType,transform.position);
        }


    }
}
