using SWS;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Character_Management
{

    public class WalkerManager : MonoBehaviour
    {
        [Header("--------- Path Refrences --------")]
        [SerializeField] PathManager[] pathList;
        public List<GameObject> walkersListsForPaths;

        [Space(5)]
        [Header("--------- Walker Prefabs --------")]
        [SerializeField] Transform Container;
        [SerializeField] WalkerData walkerDataSheet;
        [SerializeField] InsectPooler insectPooler;

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
        [SerializeField] float spawnWaitTime = 5f;
        public int insectCounter;
       

        public int currentDataIndex;
        int pathIndex;

        private void Awake()
        {
            Debug.Log("In awake");

            if (!walkerDataSheet) { Debug.Log("Walker Data Not Assigned !!"); return; }
            if (!Container) { Debug.Log("Container Not Assigned !!"); return; }
            currentDataIndex = LevelManager.CurrentLevelNumber;
            if (currentDataIndex > walkerDataSheet.walkerDataList.Length - 1)
                currentDataIndex %= walkerDataSheet.walkerDataList.Length;
            for (int j = 0; j < walkerDataSheet.walkerDataList[currentDataIndex].walkerlist.Length; j++)
            {
                insectCounter += walkerDataSheet.walkerDataList[currentDataIndex].walkerlist[j].spawnNumber;
            }
         
            GameController.onGameplay += LoadWalkers;
           
        }

        private void HouseManager_OnHouseComplete()
        {
            StartCoroutine(nameof(LoadSpecificWalker));
        }

        IEnumerator LoadSpecificWalker()
        {
            yield return new WaitForSeconds(3);

            //  currentDataIndex = HouseManager.CurrentHouse;

            int newWlakerIndex = walkerDataSheet.walkerDataList[currentDataIndex].walkerlist.Length;

            SetWalkers(walkerDataSheet.walkerDataList[currentDataIndex].walkerlist[newWlakerIndex - 1].character,
                       walkerDataSheet.walkerDataList[currentDataIndex].walkerlist[newWlakerIndex - 1].spawnNumber, startWalk: true);
        }

        public void LoadWalkers()
        {



            for (int i = 0; i < walkerDataSheet.walkerDataList[currentDataIndex].
                                            walkerlist.Length; i++)
            {

                StartCoroutine(SetWalkers(walkerDataSheet.walkerDataList[currentDataIndex].walkerlist[i].character,
                       walkerDataSheet.walkerDataList[currentDataIndex].walkerlist[i].spawnNumber));
            }
        }
        IEnumerator SetWalkers(AiWalker prefab, int count, bool startWalk = true)
        {

            {
                pathIndex = 0;
                for (int i = 0; i < count; i++)
                {
                    yield return new WaitForSeconds(spawnWaitTime);
                    if (pathIndex > pathList.Length - 1) { pathIndex = 0; }
                    //AiWalker walker = Instantiate(prefab, Container);
                    //insectCounter++;
                    AiWalker walker = insectPooler.GetNew();
                    walker.GetComponentInChildren<Collider>().enabled = true;
                    Debug.Log("POOLING INSECT");


                    walker.transform.localPosition = pathList[pathIndex].transform.localPosition;
                   
                    walker.GetComponentInChildren<BoxCollider>().enabled = true;
                    Quaternion rot = walker.transform.localRotation;
                    rot.z = 0f;
                    walker.Initialize(pathList[pathIndex],
                                    m_WalkType,
                                    Random.Range(m_MinSpeed, m_MaxSpeed), startWalk);
                   walkersListsForPaths.Add(walker.gameObject);
                    //WalkersListForPaths[pathIndex].Remove
                    pathIndex++;

                }

            }
        }
       
       




    }
}
