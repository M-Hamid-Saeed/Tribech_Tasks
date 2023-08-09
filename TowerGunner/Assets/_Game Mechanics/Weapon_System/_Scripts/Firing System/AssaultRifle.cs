using System.Collections;
using UnityEngine;

namespace AxisGames
{
	namespace WeaponSystem
	{
		public class AssaultRifle : Weapon
		{
			bool canShot = true;
			WaitForSeconds fireRateTime;
			float currentAmo;
			Vector3 aimPoint;
			IEnumerator waitForHold;
			
			void Start()
			{
				currentAmo = magSize;
				fireRateTime = new WaitForSeconds(fireRate);
				waitForHold = new WaitUntil(() => !canShot);
				pooler.Initialize(magSize, bullet);
                StartCoroutine(BulletShoot());
            }

			void Update()
			{
				if (Input.GetMouseButton(1))
				{
					if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out RaycastHit hit, Mathf.Infinity))
					{
						Shot(hit.point);
					}
				}
                if (Input.GetKeyDown(KeyCode.R))
                {
					Reload();
                }
			}

			public override void Reload()
			{
				currentAmo = magSize;
			}

			public override void Shot(Vector3 aimPoint)
			{
				if (currentAmo == 0) return;
				this.aimPoint = aimPoint;
				canShot = false;
			}

			private IEnumerator BulletShoot()
			{
				yield return waitForHold;
				while (!canShot)
				{
					currentAmo--;
					var bulletClone = pooler.GetNew();
					bulletClone.transform.position = muzzlePoint.position;
					bulletClone.Trigger((aimPoint - muzzlePoint.position).normalized);
					//bulletClone.transform.rotation = Quaternion.LookRotation(aimPoint - muzzlePoint.position, Vector3.up);
					yield return fireRateTime;
					canShot = true;
				}

				yield return BulletShoot();
			}

            public override void AddAmmo(float amount)
            {
                throw new System.NotImplementedException();
            }
        }
	}
}
