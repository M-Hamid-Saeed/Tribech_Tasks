using AxisGames.ParticleSystem;
using GameAssets.GameSet.GameDevUtils.Managers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Explosion {
    public class Explosion_Base : MonoBehaviour, IDamageable
    {
        [SerializeField] float attackRange;
        [SerializeField] float MaxHealth;
        [SerializeField] float MaxDamageTaken;
        [SerializeField] float ExplosionDamage;
        [SerializeField] CameraShake_Management CameraShakeManager;
        [SerializeField]  ParticleType  ExplosinParticleType;
        public float currentHealth;

        private void Awake()
        {
            gameObject.GetComponent<BoxCollider>().enabled = false;
            currentHealth = MaxHealth;
        }
        public void Damage(float damage)
        {
            if (damage <= MaxDamageTaken)
                currentHealth -= damage;
            else
                currentHealth -= MaxDamageTaken;

            if (currentHealth <= 0)
            {
                gameObject.GetComponent<BoxCollider>().enabled = true;
                CameraShakeManager.ShakeCamera();
                PlayExplosionParticle();
                gameObject.GetComponentInChildren<MeshRenderer>().enabled = false;
                StartCoroutine(WaitForDestroy());
                
            }

        }

        private void OnTriggerStay(Collider other)
        {
            Debug.Log("IN THE TRIGGER");

            other.gameObject.GetComponentInParent<IDamageable>().Damage(ExplosionDamage);

        }

        private IEnumerator WaitForDestroy()
        {
            yield return new WaitForSeconds(0.5f);
            
            Destroy(gameObject);
        }
        private void PlayExplosionParticle()
        {
            ParticleManager.Instance?.PlayParticle(ExplosinParticleType, transform.position);
        }
    } 
}
