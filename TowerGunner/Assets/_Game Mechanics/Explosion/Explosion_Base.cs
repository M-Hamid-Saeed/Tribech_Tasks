using AxisGames.ParticleSystem;
using GameAssets.GameSet.GameDevUtils.Managers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DarkVortex {
    public class Explosion_Base : MonoBehaviour, IDamageable
    {
      
        [SerializeField] protected float MaxHealth;
        [SerializeField] protected float MaxDamageTaken;
        [SerializeField] protected float ExplosionDamage;
        [Space(3)]
        [SerializeField] GameObject explosionRange;
        [Space(3)]
        [SerializeField] CameraShake_Management CameraShakeManager;
        [SerializeField]  ParticleType  ExplosinParticleType;
        [SerializeField]  ParticleType  BulletHitParticleType;
        [SerializeField]  SoundType MetalsoundType;
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
                gameObject.GetComponentInChildren<Collider>().enabled = false;
                gameObject.GetComponentInChildren<MeshRenderer>().enabled = false;
                CameraShakeManager.ShakeCamera();
                PlayExplosionParticle();
                
                if(explosionRange)
                    explosionRange.SetActive(true);
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
            ParticleManager.Instance?.PlayParticle(ExplosinParticleType, new Vector3(transform.position.x,transform.position.y+1f,transform.position.z));
            SoundManager.Instance.PlayOneShot(ExplosionSound,1f);
        }
        


        public void PlayParticle_Sound(Vector3 collisionPoint)
        {
            SoundManager.Instance.PlayOneShot(MetalsoundType, volume);
            ParticleManager.Instance?.PlayParticle(BulletHitParticleType, collisionPoint);

        }
       
    } 
}
