using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AxisGames.Pooler;
using Character_Management;

public class InsectPooler : MonoBehaviour
{
    [SerializeField] AiWalker[] insectPrefab;
    [SerializeField] int poolSize;


    ObjectPooler<AiWalker> pooler = new ObjectPooler<AiWalker>();


    private void Awake()
    {
            pooler.RandomInitialize(poolSize * insectPrefab.Length, insectPrefab, this.transform);
    }

    public AiWalker GetNew()
    {
        return pooler.GetNew();
    }
}
