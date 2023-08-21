using UnityEngine;
using AxisGames.Pooler;
using System;

public class IndicatorImagePooler : MonoBehaviour
{
    [SerializeField] PointerPooled indicatorImage;
    [SerializeField] int poolSize;
    

    ObjectPooler<PointerPooled> pooler = new ObjectPooler<PointerPooled>();


    private void Awake()
    {
            pooler.Initialize(poolSize, indicatorImage, this.transform);
    }

    internal PointerPooled GetNew()
    {
        PointerPooled pointerPooled = pooler.GetNew();
        pointerPooled.transform.SetParent(this.transform);
          return pooler.GetNew();
    }
}