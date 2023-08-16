using System;
using UnityEngine;

public class GunsUpgradeManager : MonoBehaviour
{

    public static event Action<GunType> onGunUpGrade;


    [Header("----- Weapons Data-----------------")]
    [SerializeField] WeaponData weaponData;

    private void Start()
    {
        UpgradeManager.onCharacterSpeedUpGrade += UpgradeGunSpeed;
        UpgradeManager.onPowerUpgrade += UpgradeBulletPower;
        onGunUpGrade?.Invoke(weaponData.guntype);
    }
    public void onWeaponButtonPressed(int gunType)
    {
        GunType guntype = (GunType)gunType;

        switch (guntype)
        {
            case GunType.M4:
                weaponData.guntype = GunType.M4;
                break;
            case GunType.DoubleUzi:
                weaponData.guntype = GunType.DoubleUzi;
                break;
            case GunType.ShotGun:
                weaponData.guntype = GunType.ShotGun;
                break;
            case GunType.MachineGun:
                weaponData.guntype = GunType.MachineGun;
                break;
            default:
                weaponData.guntype = GunType.M4;
                break;
        }
        onGunUpGrade?.Invoke(guntype);
        // ReferenceManager.Instance?.firingSystem.init();


        // StartCoroutine(ReferenceManager.Instance.firingSystem.BulletShoot(guntype));
    }
    private void UpgradeGunSpeed(float speed, int bulletSpeed)
    {
        weaponData.dataSheet.fireRate = speed;
        weaponData.dataSheet.bulletSpeed = bulletSpeed;
    }
    private void UpgradeBulletPower(int bulletDamagePower)
    {
        weaponData.dataSheet.damage = bulletDamagePower;
    }

    private void OnDestroy()
    {
        onGunUpGrade = null;
    }

}
