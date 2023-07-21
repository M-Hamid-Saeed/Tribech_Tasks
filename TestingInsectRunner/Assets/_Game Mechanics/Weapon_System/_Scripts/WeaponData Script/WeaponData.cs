using AxisGames.WeaponSystem;
using UnityEngine;


[CreateAssetMenu(fileName = "Weapon Data", menuName = "ScriptableObjects/WeaponData")]
public class WeaponData : ScriptableObject
{
    #region Data Container Defination
   
    
    [System.Serializable]
    public struct Data
    {
        public int       megSize;
        public float     fireRate;
        public int       damage;
    }

    #endregion

    [Header("----- Weapons Data ---------")]
    public GunType guntype;
    
    [Space]
    public Bullet bullet;
    
    [Space]
    public Data dataSheet;
}
