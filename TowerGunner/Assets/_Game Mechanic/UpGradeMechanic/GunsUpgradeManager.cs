using System;
using UnityEngine;
using UnityEngine.UI;

public class GunsUpgradeManager : MonoBehaviour
{

    public static event Action<GunType> onGunUpGrade;
    public Button[] GunsButtonCards;


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
            case GunType.LMG:
                weaponData.guntype = GunType.LMG;
                break;
            default:
                weaponData.guntype = GunType.M4;
                break;
        }

        onGunUpGrade?.Invoke(guntype);
        Debug.Log("GUNTYPE " + gunType);
        if (GunsButtonCards[gunType]) 
        {
           if(!GunsButtonCards[gunType].gameObject.activeSelf)
            GunsButtonCards[gunType].gameObject.SetActive(true);
           for(int i = 0; i < gunType; i++)
            {
                GunsButtonCards[i].gameObject.SetActive(true);
            }
            //if (gunType != 0)
              // if (GunsButtonCards[gunType].transform.parent.GetChild(0).gameObject.activeSelf)
                //GunsButtonCards[gunType].transform.parent.GetChild(0).gameObject.SetActive(false);
        }
        Debug.Log(GunsButtonCards[gunType]);
        ReferenceManager.Instance?.firingSystem.init();
        
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
