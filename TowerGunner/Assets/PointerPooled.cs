using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AxisGames.Pooler;

public class PointerPooled : MonoBehaviour, IPooled<PointerPooled>
{
    public int poolID { get; set; }
    public ObjectPooler<PointerPooled> pool { get; set; }
   
}
