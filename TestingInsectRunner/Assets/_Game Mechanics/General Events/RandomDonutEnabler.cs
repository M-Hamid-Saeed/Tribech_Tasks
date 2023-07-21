using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomDonutEnabler : MonoBehaviour
{
    [SerializeField] GameObject[] donutVisuals;
    
    GameObject currentObject;

    private void Awake()
    {
        currentObject = donutVisuals[0];
    }

    private void OnEnable()
    {
        if (currentObject) { currentObject.SetActive(false); }

        currentObject = donutVisuals[Random.Range(0, donutVisuals.Length)];
        currentObject.SetActive(true);
    }

    private void OnDisable()
    {
        currentObject.SetActive(false);
    }
}
