using AxisGames.ParticleSystem;
using AxisGames.Pooler;
using DG.Tweening;
using GameAssets.GameSet.GameDevUtils.Managers;
using System.Collections.Generic;
using UnityEngine;

namespace AxisGames
{
    namespace WeaponSystem
    {
        public class Bullet : MonoBehaviour, IPooled<Bullet>
        {
            public float speed;
            private Vector3 hitPos;
           

            [Space]
            [Header("Bullet Visuals")]
            [SerializeField] MeshRenderer visual;
            [SerializeField] TrailRenderer trailRenderer;
            [Space]
            [SerializeField] Rigidbody rigidbody;
            
            [SerializeField] ParticleType InsectHitParticleType;
            [SerializeField] ParticleType IronHitParticleType;
            [SerializeField] ParticleType DirtHitParticleType;
            [SerializeField] ParticleType WoodHitParticleType;
            [SerializeField] ParticleType RockHitParticleType;
            public int poolID { get; set; }
            public ObjectPooler<Bullet> pool { get; set; }

            float lifeTime = 2;
            Vector3 direction;
            int damage = 1;

            void Start()
            {
                if (rigidbody == null) rigidbody = GetComponent<Rigidbody>();

                rigidbody.useGravity = false;
            }

            public void Trigger(Vector3 direction)
            {
                EnableTrail(true);
                if (rigidbody)
                    rigidbody.velocity = Vector3.zero;
                this.direction = direction;
            }

            void FixedUpdate()
            {

                rigidbody.velocity = direction * (speed * Time.fixedDeltaTime);
                transform.rotation = Quaternion.LookRotation(direction.normalized, Vector3.up);
                lifeTime -= Time.deltaTime;
                if (lifeTime < 0)                                                                                
                {
                    //Debug.Log("Bullet Freed");
                   
                    pool.Free(this);
                    EnableTrail(false);
                    lifeTime = 2;
                }
            }

            private void OnCollisionEnter(Collision collision)
            {
                
                Vibration.Cancel();   
                IDamageable targetObject = collision.collider.GetComponentInParent<IDamageable>();
                if (targetObject != null)
                {
                    targetObject.Damage(damage);
                    Vibration.VibrateNope();
                }
                Debug.Log(collision.gameObject.tag);

                if (collision.gameObject.CompareTag("Insects"))

                    PlayParticle_Sound(InsectHitParticleType, collision, SoundManager.Instance.InsectHit, 1f);

                else if (collision.gameObject.CompareTag("Iron"))

                    PlayParticle_Sound(IronHitParticleType, collision, SoundManager.Instance.MetalHit, .3f);

                else if (collision.gameObject.CompareTag("Rock"))
                
                    PlayParticle_Sound(RockHitParticleType, collision, SoundManager.Instance.RockHit, .5f);
                
                else if (collision.gameObject.CompareTag("Wood"))
                
                    PlayParticle_Sound(WoodHitParticleType, collision, SoundManager.Instance.WoodenHit, .5f);

                
                pool.Free(this);
                EnableTrail(false);
               

            }
            
            private void PlayParticle_Sound(ParticleType particle, Collision collision, AudioClip audio, float volume)
            {
                ParticleManager.Instance?.PlayParticle(particle, collision.contacts[0].point);
                SoundManager.Instance.PlayOneShot(audio, volume);
            }
            public void SetColor(Material newColor)
            {
                visual.material = newColor;

                if (!trailRenderer) return;
                trailRenderer.material = newColor;
            }

            public void SetDamage(int damage)
            {
                this.damage = damage;
                Debug.Log(this.damage);

            }

            private void EnableTrail(bool state)
            {
                if (trailRenderer)
                {
                    trailRenderer.Clear();
                    trailRenderer.enabled = state;
                }
            }

            public void SetHitPosition(Vector3 pos)
            {
                this.hitPos = pos;
            }
        }
    }
}