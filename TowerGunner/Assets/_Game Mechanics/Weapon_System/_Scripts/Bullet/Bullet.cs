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
                {
                    SoundManager.Instance.PlayOneShot(SoundManager.Instance.InsectHit, 1f);
                    ParticleManager.Instance?.PlayParticle(InsectHitParticleType, collision.contacts[0].point);
                }
                if (collision.gameObject.CompareTag("Iron"))
                {
                    Debug.Log("IRON HIT");

                    SoundManager.Instance.PlayOneShot(SoundManager.Instance.MetalHit, 1f);
                    ParticleManager.Instance?.PlayParticle(IronHitParticleType, collision.contacts[0].point);
                }
                if (collision.gameObject.CompareTag("Rock"))
                {
                    SoundManager.Instance.PlayOneShot(SoundManager.Instance.RockHit, .3f);
                    ParticleManager.Instance?.PlayParticle(RockHitParticleType, collision.contacts[0].point);
                }
                if (collision.gameObject.CompareTag("Wood"))
                {
                    SoundManager.Instance.PlayOneShot(SoundManager.Instance.WoodenHit, .3f);
                    ParticleManager.Instance?.PlayParticle(WoodHitParticleType, collision.contacts[0].point);
                }
                pool.Free(this);
                EnableTrail(false);
               

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