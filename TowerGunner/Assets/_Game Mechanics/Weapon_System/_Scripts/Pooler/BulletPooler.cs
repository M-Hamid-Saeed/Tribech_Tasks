using UnityEngine;
using AxisGames.Pooler;
using AxisGames.WeaponSystem;
using System;

public class BulletPooler : MonoBehaviour
{
    [SerializeField] Bullet bullet;
    [SerializeField] int poolSize;


    ObjectPooler<Bullet> pooler = new ObjectPooler<Bullet>();


    private void Awake()
    {
        pooler.Initialize(poolSize, bullet, this.transform);
    }

    internal Bullet GetNew()
    {

        return pooler.GetNew();
    }

}
