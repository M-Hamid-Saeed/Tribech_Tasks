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
        for(int i = 0; i<insectPrefab.Length;i++)
         pooler.Initialize(poolSize, insectPrefab[i], this.transform);
    }

    public AiWalker GetNew()
    {
        return pooler.GetNew();
    }
}
