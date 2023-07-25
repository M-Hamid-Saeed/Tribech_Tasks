using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSwtich : MonoBehaviour
{
    [SerializeField] FiringSystem firingSystem;
    public List<WeaponData> weaponData;
    private void Awake()
    {
        firingSystem.SetData(weaponData[0]);
        WeaponInitialiaze();
    }
    private void Update()
    {
        GetCurrentWeapon();
    }
    public void GetCurrentWeapon()
    {
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            Debug.Log(weaponData[2]);
            firingSystem.SetData(weaponData[2]);
            WeaponInitialiaze();
        }

        else if (Input.GetKeyDown(KeyCode.Alpha2)) {
            Debug.Log(weaponData[1]);
            firingSystem.SetData(weaponData[1]);
            WeaponInitialiaze();
        }

           

        else if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            Debug.Log(weaponData[0]);
            firingSystem.SetData(weaponData[0]);
            WeaponInitialiaze();
        }
            

        
    }

    private void WeaponInitialiaze()
    {

        firingSystem.Init();
    }
}
