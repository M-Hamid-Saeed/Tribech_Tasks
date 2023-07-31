
using AxisGames.WeaponSystem;
using UnityEngine;



[CreateAssetMenu(fileName = "Weapon Data", menuName = "ScriptableObjects/WeaponData")]
public class WeaponData : ScriptableObject
{
    #region Data Container Defination


    [System.Serializable]
    public struct Data
    {
        public int megSize;
        public float fireRate;
        public int damage;
        public float bulletSpeed;
    }

    #endregion

    [Header("----- Weapons Data ---------")]
    public GunType guntype;

    [Space]
    public Bullet bullet;

    [Space]
    public Data dataSheet;

    [Header("----- Trail Renderer ---------")]
    public TrailConfigScriptableObject TrailConfig;



    public TrailRenderer CreateTrail()
    {
        GameObject instance = new GameObject("Bullet Trail");
        TrailRenderer trail = instance.AddComponent<TrailRenderer>();
        trail.colorGradient = TrailConfig.Color;
        trail.material = TrailConfig.Material;
        trail.widthCurve = TrailConfig.WidthCurve;
        trail.time = TrailConfig.Duration;
        trail.minVertexDistance = TrailConfig.MinVertexDistance;

        trail.emitting = false;
        trail.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;

        return trail;
    }

}
