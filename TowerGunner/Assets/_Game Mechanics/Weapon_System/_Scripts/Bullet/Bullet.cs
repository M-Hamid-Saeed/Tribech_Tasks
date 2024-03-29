using AxisGames.ParticleSystem;
using AxisGames.Pooler;
using GameAssets.GameSet.GameDevUtils.Managers;
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
            [SerializeField] SoundType InsectHitSoundType;
            
            [SerializeField] ParticleType GroundHitParticleType;
            [SerializeField] SoundType GroundHitSoundType;
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

            void Update()
            {

                rigidbody.velocity = direction.normalized * (speed * Time.deltaTime);
                //transform.rotation = Quaternion.LookRotation(direction.normalized, Vector3.up);
                lifeTime -= Time.deltaTime;
                if (lifeTime < 0)                                                                                
                {
                    //Debug.Log("Bullet Freed");
                   
                    pool.Free(this);
                    EnableTrail(false);
                    lifeTime = 1.5f;
                }
            }

            private void OnCollisionEnter(Collision collision)
            {
               
                //ReferenceManager.Instance.crosshairAnimation.DORestartById("crossHairHit");
                
                Vibration.Cancel();   
                IDamageable targetObject = collision.collider.GetComponentInParent<IDamageable>();
                if (targetObject != null)
                {
                    targetObject.Damage(damage, collision.GetContact(0));
                    Vibration.VibrateNope();
                }
               
              //  Explosion_Base explosion_Base = collision.collider.GetComponentInParent<Explosion_Base>();

              /*  if (explosion_Base != null)
                
                   // ReferenceManager.Instance.crossHaironHit.SetActive(true);
                    explosion_Base.PlayBulletHitParticle_Sound(collision.GetContact(0).point);*/
                
                   // ReferenceManager.Instance.crossHaironHit.SetActive(true);
                   // Ground would be one in all scene, So playing sound and particle here,
                   // No need to implement separate class and implement onCollisionEnter on ground;
                 if (collision.gameObject.CompareTag("Ground"))
                {
                    ParticleManager.Instance?.PlayParticle(GroundHitParticleType, collision.GetContact(0).point);
                    SoundManager.Instance.PlayOneShot(GroundHitSoundType, .7f);
                }


                
                pool.Free(this);
                EnableTrail(false);


            }
           
            public void PlayParticle_Sound(ContactPoint collision)
            {
                ParticleManager.Instance?.PlayParticle(InsectHitParticleType, collision.point);
                SoundManager.Instance.PlayOneShot(InsectHitSoundType,1f);
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