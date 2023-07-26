using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Collections.Generic;
using SWS;

namespace Character_Management {
    public enum SpawnState { SPAWNING, WAITING, COUNTING };

    public class insectWaveSpawner : MonoBehaviour
    {
        [SerializeField] private int totalWaves;
        [SerializeField] WalkerManager walkerManager;
        [SerializeField] float timeBetweenWaves = 5f;
        [SerializeField] float countDown = 2f;
        private SpawnState state;

        // Start is called before the first frame update
        void Start()
        {
            StartCoroutine(RunSpawner());
        }

        private IEnumerator RunSpawner()
        {
            // first time wait 2 seconds
            yield return new WaitForSeconds(countDown);

            while (true)
            {
                


            }
        }
    }
}
