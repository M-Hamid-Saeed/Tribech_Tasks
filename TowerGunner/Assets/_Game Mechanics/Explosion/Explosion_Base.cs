using AxisGames.ParticleSystem;
using GameAssets.GameSet.GameDevUtils.Managers;
using System.Collections;
using UnityEngine;

//This class is base class for all explosives
namespace DarkVortex
{
    public class Explosion_Base : MonoBehaviour, IDamageable
    {
      
        [SerializeField] protected float MaxHealth;
        [SerializeField] protected float MaxDamageTaken;
        [SerializeField] protected float ExplosionDamage;
        [Space(3)]
        [SerializeField] GameObject explosionRange;
        [Space(3)]
        
        [SerializeField]  ParticleType  explosinParticleType;
        [SerializeField]  ParticleType  bulletHitParticleType;
        [SerializeField]  SoundType bulletHitsound;
        [SerializeField]  SoundType explosionSound;
        [SerializeField] protected float volume;

        public float currentHealth;

        private void Awake()
        {
           
            currentHealth = MaxHealth;
            if(explosionRange!=null)
             explosionRange.SetActive(false);
           
        }
        public void Damage(float damage, ContactPoint hitPoint)
        {
            if (damage <= MaxDamageTaken)
                currentHealth -= damage;
            else
                currentHealth -= MaxDamageTaken;

            if (currentHealth <= 0)
            {
               /* Disabling mesh at on explosive health = 0, but destroying object after
                sometime to make the explosion damage in explosion range*/
                gameObject.GetComponentInChildren<MeshRenderer>().enabled = false;
                PlayExplosionParticle();
                if(explosionRange)
                    explosionRange.SetActive(true);
                StartCoroutine(WaitForDestroy());

                /*Disabling drumexplosive visual collider as the gameobject will destroy 
                after some time so even after meshrenderer off, the collider can still get the bullet damage
                creating multiple explosion effects*/
                //Box collider in only on drumexplosive visual, should not disable the missle visual collider
                BoxCollider drumExplosiveCollider = gameObject.GetComponentInChildren<BoxCollider>();
                CapsuleCollider MissleCollider = gameObject.GetComponentInChildren<CapsuleCollider>();
                if (drumExplosiveCollider)
                    drumExplosiveCollider.enabled = false;
                if (MissleCollider)
                    MissleCollider.enabled = false;
                ReferenceManager.Instance?.CameraShakeManager.ShakeCamera();
            }

            PlayParticle_Sound(hitPoint.point);

        }


        private IEnumerator WaitForDestroy()
        {
            yield return new WaitForSeconds(.1f);
            Destroy(gameObject);
        }
        private void PlayExplosionParticle()
        {
            ParticleManager.Instance?.PlayParticle(explosinParticleType, new Vector3(transform.position.x,transform.position.y+1f,transform.position.z));
            SoundManager.Instance.PlayOneShot(explosionSound,1f);
        }



        public void PlayParticle_Sound(Vector3 collisionPoint)
        {
            ParticleManager.Instance?.PlayParticle(bulletHitParticleType, collisionPoint);
            SoundManager.Instance.PlayOneShot(bulletHitsound, 1f);
        }

    } 
}
