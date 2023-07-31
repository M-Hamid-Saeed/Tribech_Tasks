using AxisGames.WeaponSystem;
using GameAssets.GameSet.GameDevUtils.Managers;
using MergeSystem;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

public enum GunType
{
	Pistol /*= 1 << 0*/,
	AKM /*= 1 << 1*/,
	M4 /*= 1 << 2*/
}

public class FiringSystem : Weapon
{
	public event Action onReloadStart,onReloadEnd;
	
	[SerializeField] BulletPooler bulletPooler;

	[SerializeField] Bullet _bullet;

	[SerializeField] InputManager input;

	[Header("----- Weapons Data-----------------")]
	[SerializeField] WeaponData weaponData;
	[Space]
	[SerializeField] private FireSound fireSoundSource;
	[Space]
	[Header("----- Weapons Fire Points----------")]
	[Space]
	[SerializeField] Gun[] guns;
	Gun currentGun;

	int weaponDamage;
	float currentAmo;
	ParticleSystem firingParticle;

	bool isAI;
	bool canShot = true;
	IEnumerator waitForHold;
	WaitForSeconds fireRateTime;
	float filltimer;
	Vector3 aimPoint;
	bool isReloading;

	public void Init()
	{
		if (weaponData == null) { Debug.LogError("Data Not Assigned in Fire System"); return; }
		LoadGun();
		//trail = TrailPool.Get();

		InitializeWeapon();
	}

	private void InitializeWeapon()
	{
		//TrailPool = new ObjectPool<TrailRenderer>(weaponData.CreateTrail);

		int megSize = weaponData.dataSheet.megSize;

		//pooler.Initialize(megSize, weaponData.bullet/*,this.transform*/);
		_bullet.speed = weaponData.dataSheet.bulletSpeed;
		weaponDamage = weaponData.dataSheet.damage;
		fireRateTime = new WaitForSeconds(weaponData.dataSheet.fireRate);
		waitForHold = new WaitUntil(() => !canShot);

		muzzlePoint = currentGun.firePoint;
		currentAmo = magSize;

		firingParticle = currentGun.muzleFlash;

		StartCoroutine(BulletShoot());
	}

	public override void Reload()
	{
		if (!isReloading)
		{
			isReloading = true;
			//if (fireSoundSource) fireSoundSource.PlayReload();
			currentAmo = weaponData.dataSheet.megSize; isReloading = false;
			
		}
	}

	public override void AddAmmo(float amount)
	{
		currentAmo += amount;
		//Vibration.VibratePop();
	}

	public override void Shot(Vector3 aimPoint)
	{
	//	if (!isReloading)
	//	{
			//if (currentAmo == 0) { Reload(); return; }
			this.aimPoint = aimPoint;
		SoundManager.Instance.PlayOneShot(SoundManager.Instance.shoot, .5f);
		//FunctionTimer.Create(() => { SoundManager.Instance.PlayOneShot(SoundManager.Instance.shoot, .5f); }, 0.5f);
		canShot = false;

		//}
		
	}

	private IEnumerator SoundDelay()
    {
		yield return new WaitForSeconds(.5f);
		SoundManager.Instance.PlayOneShot(SoundManager.Instance.shoot, .5f);
	}

	public Transform GetFirePoint()
	{
		return muzzlePoint;

	}
	private IEnumerator BulletShoot()
	{
		yield return waitForHold;
		while (!canShot)
		{
			//var bulletClone = pooler.GetNew();
			Bullet bulletClone = bulletPooler.GetNew();
			
			if(bulletClone == null) { continue; }
			
			bulletClone.SetDamage(weaponDamage);
			bulletClone.SetHitPosition(aimPoint);
			
			bulletClone.transform.position = muzzlePoint.position;
			bulletClone.Trigger((aimPoint - muzzlePoint.position).normalized);
		    

			PlayFiringPlartice(muzzlePoint);
			//currentAmo--;
			yield return fireRateTime;
			canShot = true;
		}

		yield return BulletShoot();
	}


	private void PlayFiringPlartice(Transform target)
	{
		if (firingParticle)
		{
			//firingParticle.transform.position = target.position;
			firingParticle.Play();
		}
	}
	public float CurrentAmmo
	{
		get { return currentAmo; }
	}
	
	public void SetData(WeaponData weaponData)
	{
		this.weaponData = weaponData;
	}

	private void LoadGun()
	{
		for (int i = 0; i < guns.Length; i++)
		{
			guns[i].gameObject.SetActive(false);
		}

		currentGun = guns[(int)weaponData.guntype];
		currentGun.gameObject.SetActive(true);
	}

	private void OnDestroy()
	{
		onReloadEnd   = null;
		onReloadStart = null;
	}
}