using AxisGames.Pooler;
using UnityEngine;

namespace AxisGames
{

	namespace WeaponSystem
	{
		public abstract class Weapon : MonoBehaviour
		{   
			protected Transform muzzlePoint1;
			protected Transform muzzlePoint2;
			protected float fireRate;
			protected Bullet bullet;
			protected int magSize;
			protected readonly ObjectPooler<Bullet> pooler = new ObjectPooler<Bullet>();

			//public void SetActive(bool value) => gameObject.SetActive(value);


			public abstract void Reload();

			public abstract void AddAmmo(float amount);

			public abstract void Shot(Vector3 aimPoint);

		}
	}
}