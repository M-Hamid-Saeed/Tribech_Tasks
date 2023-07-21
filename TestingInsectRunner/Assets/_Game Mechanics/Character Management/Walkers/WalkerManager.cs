using UnityEngine;
using System.Collections;
using SWS;

namespace Character_Management
{

    public class WalkerManager : MonoBehaviour
    {
        [Header("--------- Path Refrences --------")]
        [SerializeField] PathManager[] pathList;

        [Space(5)]
        [Header("--------- Walker Prefabs --------")]
        [SerializeField] Transform  Container;
        [SerializeField] WalkerData walkerDataSheet;

        [Space(5)]
        [Header("--------- Basic Walker --------")]
      //  [SerializeField] CharacterLevel basic_walkerLevel = CharacterLevel.level2;
        [SerializeField] int basic_walkerCount = 10;

        [Space(5)]
        [Header("--------- Walker Settings --------")]
        [SerializeField] splineMove.LoopType m_WalkType = splineMove.LoopType.loop;
        [Space]
        [SerializeField] float m_MinSpeed = 0.5f;
        [SerializeField] float m_MaxSpeed = 1f;

        int currentDataIndex;
        int pathIndex;

        private void Awake()
        {
            if (!walkerDataSheet) { Debug.Log("Walker Data Not Assigned !!"); return; }
            if (!Container) { Debug.Log("Container Not Assigned !!"); return; }

         //   currentDataIndex = HouseManager.CurrentHouse;

         //   HouseManager.OnHouseComplete += HouseManager_OnHouseComplete;

            LoadWalkers();
        }

        private void HouseManager_OnHouseComplete()
        {
            StartCoroutine(nameof(LoadSpecificWalker));
        }

        IEnumerator LoadSpecificWalker()
        {
            yield return new WaitForSeconds(1);

          //  currentDataIndex = HouseManager.CurrentHouse;

            int newWlakerIndex = walkerDataSheet.walkerDataList[currentDataIndex].walkerlist.Length;

            SetWalkers(walkerDataSheet.walkerDataList[currentDataIndex].walkerlist[newWlakerIndex - 1].character,
                       walkerDataSheet.walkerDataList[currentDataIndex].walkerlist[newWlakerIndex - 1].spawnNumber,startWalk:true);
        }

        private void LoadWalkers()
        {
            for (int i = 0; i < walkerDataSheet.walkerDataList[currentDataIndex].
                                            walkerlist.Length; i++)
            {
                SetWalkers(walkerDataSheet.walkerDataList[currentDataIndex].walkerlist[i].character,
                       walkerDataSheet.walkerDataList[currentDataIndex].walkerlist[i].spawnNumber);
            }
        }
        private void SetWalkers(AiWalker prefab,int count,bool startWalk = true)
        {
            pathIndex = 0;
            for (int i = 0; i < count; i++)
            {
                if(pathIndex > pathList.Length - 1) { pathIndex = 0; }

                AiWalker walker = Instantiate(prefab,Container);

                walker.transform.localPosition = pathList[pathIndex].transform.localPosition;
                walker.Initialize(pathList[pathIndex],
                                m_WalkType, 
                                Random.Range(m_MinSpeed, m_MaxSpeed), startWalk);
                pathIndex++;
            }
        }
    }
}