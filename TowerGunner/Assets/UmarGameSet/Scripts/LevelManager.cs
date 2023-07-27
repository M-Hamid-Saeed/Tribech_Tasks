using System;
using UnityEngine;


[Serializable]
public struct LevelInfo
{

    public Transform levelData;
    public Transform playerSpawn;
    public Transform cameraPosition;

}

public class LevelManager : MonoBehaviour
{

    [SerializeField] LevelInfo[] levels;

    [SerializeField] Transform player, camera;

    [HideInInspector] public LevelInfo currentLevel;

    private bool isTesting;
    public static int CurrentLevelNumber
    {
        get { return PlayerPrefs.GetInt("LevelNumber"); }

    }

    public void Awake()
    {
        ActiveLevel();
        GameController.onLevelComplete += OnLevelComplete;
    }

    void OnLevelComplete()
    {
        var levelNo = PlayerPrefs.GetInt("LevelNumber", 0);
        levelNo++;
        PlayerPrefs.SetInt("LevelNumber", levelNo);
    }

    void ActiveLevel()
    {
        int levelNo = PlayerPrefs.GetInt("LevelNumber");
        if (levelNo > levels.Length - 1)
            levelNo %= levels.Length;
        currentLevel = levels[levelNo];
        currentLevel.levelData.gameObject.SetActive(true);
        if (player && currentLevel.playerSpawn)
        {
            player.transform.SetPositionAndRotation(currentLevel.playerSpawn.position, currentLevel.playerSpawn.rotation);
        }

        if (camera && currentLevel.cameraPosition)
        {
            camera.transform.SetPositionAndRotation(currentLevel.cameraPosition.position, currentLevel.cameraPosition.rotation);
        }
    }

}