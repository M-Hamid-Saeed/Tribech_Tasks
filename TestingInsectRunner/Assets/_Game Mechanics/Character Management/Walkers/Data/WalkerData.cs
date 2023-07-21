
using UnityEngine;

namespace Character_Management
{
    [System.Serializable]
    public struct WalkersInfo
    {
        public int spawnNumber;
        public AiWalker character;
    }

    [System.Serializable]
    public struct WalkersData
    {
        public WalkersInfo[] walkerlist;
    }

    [CreateAssetMenu(menuName = "Charater Management/Ai Walker Data Sheet")]
    public class WalkerData : ScriptableObject
    {
        public WalkersData[] walkerDataList;
    }
}