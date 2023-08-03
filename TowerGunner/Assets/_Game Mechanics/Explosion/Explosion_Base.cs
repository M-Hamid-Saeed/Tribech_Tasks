using AxisGames.ParticleSystem;
using GameAssets.GameSet.GameDevUtils.Managers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DarkVortex {
    public class Explosion_Base : MonoBehaviour, IDamageable
    {
        [SerializeField] float attackRange;
        [SerializeField] float MaxHealth;
        [SerializeField] float MaxDamageTaken;
        [SerializeField] float ExplosionDamage;
        [Space(3)]
        [SerializeField] GameObject explosionRange;
        [Space(3)]
        [SerializeField] CameraShake_Management CameraShakeManager;
        [SerializeField]  ParticleType  ExplosinParticleType;
        [SerializeField]  ParticleType  BulletHitParticleType;
        [SerializeField]  SoundType soundType;
        [SerializeField]  SoundType ExplosionSound;
        [SerializeField] float volume;

        public float currentHealth;

        private void Awake()
        {
           // gameObject.GetComponent<BoxCollider>().enabled = false;
            currentHealth = MaxHealth;
            explosionRange.SetActive(false);
        }
        public void Damage(float damage)
        {
            if (damage <= MaxDamageTaken)
                currentHealth -= damage;
            else
                currentHealth -= MaxDamageTaken;

            if (currentHealth <= 0)
            {
                gameObject.GetComponentInChildren<BoxCollider>().enabled = false;
                gameObject.GetComponentInChildren<MeshRenderer>().enabled = false;
                CameraShakeManager.ShakeCamera();
                PlayExplosionParticle();
                
                if(explosionRange)
                    explosionRange?.SetActive(true);
                StartCoroutine(WaitForDestroy());
                
            }

        }


        private IEnumerator WaitForDestroy()
        {
            yield return new WaitForSeconds(1f);
            Destroy(gameObject);
        }
        private void PlayExplosionParticle()
        {
            ParticleManager.Instance?.PlayParticle(ExplosinParticleType, transform.position);
            SoundManager.Instance.PlayOneShot(ExplosionSound,1f);
        }
        


        public void PlayParticle_Sound(Vector3 collisionPoint)
        {
            SoundManager.Instance.PlayOneShot(soundType, volume);
            ParticleManager.Instance?.PlayParticle(BulletHitParticleType, collisionPoint);

        }
       
    } 
}
