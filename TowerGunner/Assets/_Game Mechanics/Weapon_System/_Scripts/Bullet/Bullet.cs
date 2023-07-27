using AxisGames.ParticleSystem;
using AxisGames.Pooler;
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

			[SerializeField] ParticleType particleType;
			public int poolID { get; set; }
			public ObjectPooler<Bullet> pool { get; set; }

			float lifeTime = 3;
			Vector3 direction;
			int damage = 1;

			void Start()
			{
				if(rigidbody == null) rigidbody = GetComponent<Rigidbody>();
				
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
				rigidbody.velocity = direction * (speed * Time.deltaTime);
				transform.rotation = Quaternion.LookRotation(direction.normalized, Vector3.up);
				lifeTime -= Time.deltaTime;
				if (lifeTime < 0)
				{
					//Debug.Log("Bullet Freed");
					pool.Free(this);
					EnableTrail(false);
					lifeTime = 5;
				}
			}

			private void OnCollisionEnter(Collision collision)
			{
				IDamageable insect = collision.collider.GetComponentInParent<IDamageable>();
				if (insect != null)
				{
					insect.Damage(damage);
                    Debug.Log("DAMAGE DONE   " + damage);

                }

                ParticleManager.Instance?.PlayParticle(particleType, hitPos);

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