using Character_Management;
using System;
using UnityEngine;


[Serializable]
public struct LevelInfo
{

    public Transform levelData;
    public WalkerManager walkerManager;
    public int NoOfMissles;
    public Material skyBoxMaterial;
   
}

public class LevelManager : MonoBehaviour
{

    public LevelInfo[] levels;
   public GunsUpgradeManager gunUpgradeManager;

 //   [SerializeField] Transform player, camera;

    [HideInInspector] public LevelInfo currentLevel;

    [SerializeField] bool isTesting;
    [SerializeField] int levelNoTesting;
    public static int CurrentLevelNumber
    {
        get { return PlayerPrefs.GetInt("LevelNumber"); }

    }

    public void Awake()
    {
        if (isTesting)
            ActiveLevelTesting();
        else
            ActiveLevel();
        GameController.onLevelComplete += OnLevelComplete;
        int gunNum = PlayerPrefs.GetInt("GunType");
        gunUpgradeManager.onWeaponButtonPressed(gunNum);

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
        int gunNum = PlayerPrefs.GetInt("GunType");
        gunUpgradeManager.onWeaponButtonPressed(gunNum);
        Debug.Log("GUNNUM" + gunNum);
        currentLevel.levelData.gameObject.SetActive(true);
        ReferenceManager.Instance.walkerManager = levels[levelNo].walkerManager;
        Debug.Log(levelNo);
        int levelNoTemp = PlayerPrefs.GetInt("LevelNumber");
        if (levelNoTemp == 0)
        {
            
            gunUpgradeManager.onWeaponButtonPressed(0);
            PlayerPrefs.SetInt("GunType",0 );
        }
        else if ( levelNoTemp % 5 == 0)
        {
            
            gunUpgradeManager.onWeaponButtonPressed(levelNoTemp / 5);
            PlayerPrefs.SetInt("GunType", levelNoTemp / 5);
        }
        RenderSettings.skybox = currentLevel.skyBoxMaterial;
        /* if (player && currentLevel.playerSpawn)
         {
             player.transform.SetPositionAndRotation(currentLevel.playerSpawn.position, currentLevel.playerSpawn.rotation);
         }

         if (camera && currentLevel.cameraPosition)
         {
             camera.transform.SetPositionAndRotation(currentLevel.cameraPosition.position, currentLevel.cameraPosition.rotation);
         }*/
    }
    void ActiveLevelTesting()
    {
   
        
        PlayerPrefs.SetInt("LevelNumber", levelNoTesting);
        
        int levelNo = PlayerPrefs.GetInt("LevelNumber");

        if (levelNo > levels.Length - 1)
            levelNo %= levels.Length;
        currentLevel = levels[levelNo];

        currentLevel.levelData.gameObject.SetActive(true);
        ReferenceManager.Instance.walkerManager = levels[levelNo].walkerManager;
        if(levelNoTesting == 0)
        {
            gunUpgradeManager.onWeaponButtonPressed(0);
        }
       
        else if (levelNoTesting % 5 == 0)
        {
            Debug.Log("LEVEL NO " + levelNoTesting);
            gunUpgradeManager.onWeaponButtonPressed((levelNoTesting) / 5);
        }

        RenderSettings.skybox = levels[levelNoTesting].skyBoxMaterial;

    }

}