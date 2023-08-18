using AxisGames.WeaponSystem;
using GameAssets.GameSet.GameDevUtils.Managers;
using System;
using System.Collections;
using UnityEngine;


public enum GunType
{
    M4 = 0,
    DoubleUzi = 1,
    LMG = 2,
    ShotGun = 3,
    MachineGun = 4
}

public class FiringSystem : Weapon
{
    public event Action onReloadStart, onReloadEnd;

    [SerializeField] BulletPooler bulletPooler;

    [SerializeField] Bullet _bullet;

    [SerializeField] InputManager input;

    [Header("----- Weapons Data-----------------")]
    [SerializeField] WeaponData weaponData;
    [Space]
    [SerializeField] private FireSound fireSoundSource;
    [Space]
    [Header("----- Weapons Fire Points----------")]
    [SerializeField] Gun[] guns;
    Gun currentGun;
    [Space]


    int weaponDamage;
    float currentAmo;
    ParticleSystem firingParticle1;

    bool leftGun = true;
    bool canShot = true;
    IEnumerator waitForHold;
    WaitForSeconds fireRateTime;
    float filltimer;
    public Vector3 aimPoint;
    bool isReloading;
    private float lastShootTime;
    private GunType currentGunType;

    private void Awake()
    {
        GunsUpgradeManager.onGunUpGrade += CurrentSelectedGun;

        // StartCoroutine(BulletShoot(currentGunType));
    }
    private void Start()
    {
        init();
        StartCoroutine(BulletShoot(currentGunType));
    }
    private void CurrentSelectedGun(GunType gunType)
    {


        currentGunType = gunType;
        init();
        //StartCoroutine(BulletShoot(currentGunType));
    }
    public void init()
    {
        LoadGun();
        InitializeWeapon();
    }
    private void InitializeWeapon()
    {
        int megSize = weaponData.dataSheet.megSize;
        _bullet.speed = weaponData.dataSheet.bulletSpeed;
        weaponDamage = weaponData.dataSheet.damage;
        fireRateTime = new WaitForSeconds(weaponData.dataSheet.fireRate);
        waitForHold = new WaitUntil(() => !canShot);

        muzzlePoint1 = currentGun.GetComponent<Gun>().firePoint1;
        muzzlePoint2 = currentGun.GetComponent<Gun>().firePoint2;
        currentAmo = magSize;
        currentGunType = weaponData.guntype;
        firingParticle1 = currentGun.muzleFlash1;



    }

    public override void Reload()
    {
        if (!isReloading)
        {
            isReloading = true;
            currentAmo = weaponData.dataSheet.megSize; isReloading = false;

        }
    }

    public override void AddAmmo(float amount)
    {
        currentAmo += amount;
    }

    public override void Shot(Vector3 aimPoint)
    {
        if (!isReloading)
        {
            this.aimPoint = aimPoint;
            canShot = false;
            if (Time.time > weaponData.dataSheet.fireRate + lastShootTime)
            {
                lastShootTime = Time.time;
                SoundManager.Instance.PlayShootSound(SoundManager.Instance?.shoot, .3f);
                SoundManager.Instance.PlayShootSound(SoundManager.Instance?.bulletShellSound, 1f);
            }

        }

    }


    public IEnumerator BulletShoot(GunType gunType)
    {

        ReferenceManager.Instance.crossHaironHit.SetActive(false);
        yield return waitForHold;

        if (gunType == GunType.DoubleUzi)
        {
            leftGun = !leftGun;
            SimpleBulletShooting();
        }
        else if (currentGunType == GunType.ShotGun)
            SpreadBulletShooting();
        else


            SimpleBulletShooting();



        yield return fireRateTime;
        canShot = true;
        yield return BulletShoot(currentGunType);
    }

    private void SimpleBulletShooting()
    {

        CrossHairAnimation();
        Bullet bulletClone = bulletPooler.GetNew();
        if (bulletClone == null) Debug.LogError("BULLET CLONE IS NULL");

        if (currentGunType != GunType.DoubleUzi && currentGunType != GunType.ShotGun)

            BulletShootingLogic(bulletClone, muzzlePoint1, muzzlePoint1.rotation, new Vector3(0, 0, 0));

        else
        {
            if (leftGun)
            {
                Debug.Log("LEFT GUN");
                BulletShootingLogic(bulletClone, muzzlePoint1, muzzlePoint1.rotation, new Vector3(0, 0, 0));
            }
            else
            {
                Debug.Log("Else");
                BulletShootingLogic(bulletClone, muzzlePoint2, muzzlePoint2.rotation, new Vector3(0, 0, 0));

            }
        }
    }
    private void SpreadBulletShooting()
    {
        CrossHairAnimation();
        Bullet[] bulletClones = new Bullet[3];

        for (int i = 0; i < 3; i++)
        {
            bulletClones[i] = bulletPooler.GetNew();
        }
        foreach (Bullet bulletClone in bulletClones)
        {
            Vector3 spreadAimPoint = new Vector3(aimPoint.x + UnityEngine.Random.Range(-2.5f, 2.5f), aimPoint.y + UnityEngine.Random.Range(-2.5f, 2.5f), aimPoint.z);
            BulletShootingLogic(bulletClone, muzzlePoint1, muzzlePoint1.rotation, spreadAimPoint);
        }
    }
    private void CrossHairAnimation()
    {
        ReferenceManager.Instance.crossHaironHit.SetActive(true);
        ReferenceManager.Instance.crosshairAnimation.DORestartbyID("crossHairHit");
    }


    private void BulletShootingLogic(Bullet bulletClone, Transform spawnPos, Quaternion rot, Vector3 spreadAimPoint)
    {
        if (currentGunType == GunType.ShotGun)
        {
            aimPoint = spreadAimPoint;
            Vector3 pos = spawnPos.position;
            //pos.x = spawnPos.position.x + UnityEngine.Random.Range(-.02f, .02f);
           
            bulletClone.transform.position = pos;
        }
        else
            bulletClone.transform.position = spawnPos.position;
        bulletClone.SetDamage(weaponDamage);
        bulletClone.SetHitPosition(aimPoint);
        bulletClone.Trigger((aimPoint - spawnPos.position).normalized);

        PlayFiringPlartice(spawnPos, firingParticle1);

    }
    private void PlayFiringPlartice(Transform target, ParticleSystem firingParticle)
    {
        firingParticle.transform.position = target.position;
        firingParticle.Play();
    }
    public float CurrentAmmo
    {
        get { return currentAmo; }
    }

    public void SetData(WeaponData weaponData)
    {
        this.weaponData = weaponData;
    }

    public void LoadGun()
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
        onReloadEnd = null;
        onReloadStart = null;

    }
}