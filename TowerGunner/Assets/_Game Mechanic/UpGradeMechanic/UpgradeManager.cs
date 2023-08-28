
using Sirenix.OdinInspector;
using System;
using UnityEngine;

public class UpgradeManager : MonoBehaviour
{


    public static event Action<Transform> onCharacterUpGrade;
    public static event Action<int> onPowerUpgrade;
    public static event Action<float> onHealthUpgrade;
    public static event Action<float,int> onCharacterSpeedUpGrade;
    public static event Action<int, int, bool> onCharacterUIUpGrade;
    public static event Action<int, int, bool> onCharacterSpeedUIUpGrade;
    public static event Action<int, int, bool> onPowerUIUpGrade;
    public static event Action<int, int, bool> onHealthUIUpGrade;

    [Space(2)]
    [FoldoutGroup("---- Character Upgrade Data ----")]
    [SerializeField] protected int characterLevel;
    [FoldoutGroup("---- Character Upgrade Data ----")]
    [SerializeField] protected int characterPrice;
    [FoldoutGroup("---- Character Upgrade Data ----")]
    [SerializeField] protected int characterIncrementPrice;
    [FoldoutGroup("---- Character Upgrade Data ----")]
    [SerializeField] protected Transform characterAnimation;

    [Space(2)]
    [FoldoutGroup("---- Speed Upgrade Data ----")]
    [SerializeField] protected int characterSpeedLevel;
    [FoldoutGroup("---- Speed Upgrade Data ----")]
    [SerializeField] protected int characterSpeedPrice;
    [FoldoutGroup("---- Speed Upgrade Data ----")]
    [SerializeField] protected int characterSpeedIncrementPrice;
    [FoldoutGroup("---- Speed Upgrade Data ----")]
    [SerializeField] protected Transform speedAnimation;
    [FoldoutGroup("---- Speed Upgrade Data ----")]
    [SerializeField] protected float defultspeed;
    [FoldoutGroup("---- Speed Upgrade Data ----")]
    [SerializeField] protected int defultBulletSpeed;
    [FoldoutGroup("---- Speed Upgrade Data ----")]
    [SerializeField] protected float incrementalSpeedFector;
    [FoldoutGroup("---- Speed Upgrade Data ----")]
    [SerializeField] protected int incrementalBulletSpeedFactor;


    [Space(2)]
    [FoldoutGroup("---- Power Upgrade Data ----")]
    [SerializeField] protected int characterPowerLevel;
    [FoldoutGroup("---- Power Upgrade Data ----")]
    [SerializeField] protected int characterPowerPrice;
    [FoldoutGroup("---- Power Upgrade Data ----")]
    [SerializeField] protected int characterPowerIncrementPrice;
    [FoldoutGroup("---- Power Upgrade Data ----")]
    [SerializeField] protected int defaultPower;
    [FoldoutGroup("---- Power Upgrade Data ----")]
    [SerializeField] protected int incrementalPowerFactor;
    [FoldoutGroup("---- Power Upgrade Data ----")]
    [SerializeField] protected Transform PowerAnimation;

    [Space(2)]
    [FoldoutGroup("---- Health Upgrade Data ----")]
    [SerializeField] protected int characterHealthLevel;
    [FoldoutGroup("---- Health Upgrade Data ----")]
    [SerializeField] protected int characterHealthPrice;
    [FoldoutGroup("---- Health Upgrade Data ----")]
    [SerializeField] protected int characterHealthIncrementPrice;
    [FoldoutGroup("---- Health Upgrade Data ----")]
    [SerializeField] protected int defaultHealth;
    [FoldoutGroup("---- Health Upgrade Data ----")]
    [SerializeField] protected float incrementalHealthFactor;
    [FoldoutGroup("---- Health Upgrade Data ----")]
    [SerializeField] protected Transform HealthAnimation;




    private void Awake()
    {
        GameController.onLevelComplete += SetUiData;

        if (GameStartInit == 0)
        {
            GameStartInit = 1;
            SpeedLevel = 1;
            characterLevel = 1;
            Speed = defultspeed;
            Power = defaultPower;
            BulletSpeed = defultBulletSpeed;
            PowerLevel = 1;
            HealthLevel = 1;
            Health = defaultHealth;
            Debug.Log("INCREMENTAL START PRICE" + characterPowerIncrementPrice);
            SaveCharacterData();
            SaveCharacterSpeedData();
            SaveCharacterPowerData();
            SaveCharacterHealthData();
            onCharacterSpeedUpGrade?.Invoke(Speed,BulletSpeed);
            onPowerUpgrade?.Invoke(Power);
            onHealthUpgrade?.Invoke(Health);
            

        }
        else
        {
            GetData();
        }
    }
    private void Start()
    {
        SetUiData();
    }
    void GetData()
    {
        characterPrice = CurrentCharacterPrice;
        characterPowerPrice = CurrentPowerPrice;
        characterSpeedPrice = CurrentCharacterSpeedPrice;
        characterIncrementPrice = IncrementalCharacterPrice;
        characterPowerIncrementPrice = IncrementalPowerPrice;
        characterSpeedIncrementPrice = IncrementalCharacterSpeedPrice;
        //characterLevel = (PlayerPrefManager.CharacterLevel) + 1;
        characterSpeedLevel = SpeedLevel;
        characterHealthIncrementPrice = IncrementalHealthPrice;
        characterHealthPrice = CurrentHealthPrice; 
        onCharacterSpeedUpGrade?.Invoke(Speed,BulletSpeed);
        onPowerUpgrade?.Invoke(Power);
        onHealthUpgrade?.Invoke(Health);
    }
    void SaveCharacterData()
    {

        CurrentCharacterPrice = characterPrice;
        IncrementalCharacterPrice = characterIncrementPrice;

    }
    void SaveCharacterPowerData()
    {
        IncrementalPowerPrice = characterPowerIncrementPrice;
        
        PowerLevel = characterPowerLevel;
        CurrentPowerPrice = characterPowerPrice;
    }
    void SaveCharacterHealthData()
    {
        IncrementalHealthPrice = characterHealthIncrementPrice;
        HealthLevel = characterHealthLevel;
        CurrentHealthPrice = characterHealthPrice;
    }
    void SaveCharacterSpeedData()
    {
        IncrementalCharacterSpeedPrice = characterSpeedIncrementPrice;
        SpeedLevel = characterSpeedLevel;
        CurrentCharacterSpeedPrice = characterSpeedPrice;
    }
    public void UpgradeCharacter(bool isFree = false)
    {

        if (CoinsManager.Instance.CanDoTransaction(characterPrice) || isFree)
        {
            Debug.Log("UpGrade Character");
            //  PlayerPrefManager.CharacterLevel++;
            // characterLevel = (PlayerPrefManager.CharacterLevel) + 1;
            // ReferenceManager.Instance.characterController.RestData();
            //  onCharacterUpGrade?.Invoke(ReferenceManager.Instance.characterController.transform);
            if (!isFree)
            {
                CoinsManager.Instance.DecCoins(characterPrice);
                characterPrice += IncrementalCharacterPrice;
            }
            SaveCharacterData();
        }
        else
        {
            //MediationManager Rewarded AdPlay
            Debug.Log("Rewarded Ad Play");
        }
        SetUiData();
    }

    public void UpGradeSpeed(bool isFree = false)
    {
        if (CoinsManager.Instance.CanDoTransaction(characterSpeedPrice) || isFree)
        {
            characterSpeedLevel++;
            Speed -= incrementalSpeedFector;
            BulletSpeed += incrementalBulletSpeedFactor;
            onCharacterSpeedUpGrade?.Invoke(Speed,BulletSpeed);
            if (!isFree)
            {
                CoinsManager.Instance.DecCoins(characterSpeedPrice);
                characterSpeedPrice += IncrementalCharacterSpeedPrice;
            }
            SaveCharacterSpeedData();
        }
        else
        {
            //MediationManager Rewarded AdPlay
            Debug.Log("Rewarded Ad Play");

        }
        SetUiData();
    }
    public void UpgradePower(bool isFree = false)
    {
        if (CoinsManager.Instance.CanDoTransaction(characterPowerPrice) || isFree)
        {
            characterPowerLevel++;
            Power += incrementalPowerFactor;
            onPowerUpgrade?.Invoke(Power);
            if (!isFree)
            {
                CoinsManager.Instance.DecCoins(characterPowerPrice);
                Debug.Log(characterPowerPrice + "" + "Before UPDATING");
                Debug.Log(IncrementalPowerPrice + "incrementtal price");
                characterPowerPrice += IncrementalPowerPrice;
                Debug.Log(characterPowerPrice + "" + "AFTER UPDATING");
            }
            SaveCharacterPowerData();
        }
        else
        {
            //MediationManager Rewarded AdPlay
            Debug.Log("Rewarded Ad Play");

        }
        SetUiData();
    }
    public void UpgradeHealth(bool isFree = false)
    {
        if (CoinsManager.Instance.CanDoTransaction(characterHealthPrice) || isFree)
        {
            characterHealthLevel++;
            Health -= incrementalHealthFactor;
            onHealthUpgrade?.Invoke(Health);
            if (!isFree)
            {
                Debug.Log(characterHealthPrice + "" + "Before UPDATING Health Price");
                CoinsManager.Instance.DecCoins(characterHealthPrice);
               
                characterHealthPrice += IncrementalHealthPrice;
                Debug.Log(characterHealthPrice + "" + "Before UPDATING Health Price");
            }
            SaveCharacterHealthData();
        }
        else
        {
            //MediationManager Rewarded AdPlay
            Debug.Log("Rewarded Ad Play");

        }
        SetUiData();
    }
    public void onReset()
    {

        SpeedLevel = 1;
        characterLevel = 1;
        characterSpeedLevel = 1;
        characterSpeedPrice = 50;
        Speed = defultspeed;
        SaveCharacterData();
        SaveCharacterSpeedData();
        onCharacterSpeedUpGrade?.Invoke(Speed,BulletSpeed);
        SetUiData();
    }
    void SetUiData()
    {
        if (CoinsManager.Instance.CanDoTransaction(characterPrice))
        {
            onCharacterUIUpGrade?.Invoke(characterLevel, characterPrice, false);
        }
        else
        {
            onCharacterUIUpGrade?.Invoke(characterLevel, characterPrice, true);
        }

        if (CoinsManager.Instance.CanDoTransaction(characterSpeedPrice))
        {
            onCharacterSpeedUIUpGrade?.Invoke(characterSpeedLevel, characterSpeedPrice, false);
        }
        else
        {
            onCharacterSpeedUIUpGrade?.Invoke(characterSpeedLevel, characterSpeedPrice, true);
        }
        if (CoinsManager.Instance.CanDoTransaction(characterPowerPrice))
        {
            onPowerUIUpGrade?.Invoke(characterPowerLevel, characterPowerPrice, false);
        }
        else
        {
            onPowerUIUpGrade?.Invoke(characterPowerLevel, characterPowerPrice, true);
        }

        if (CoinsManager.Instance.CanDoTransaction(characterHealthPrice))
        {
            onHealthUIUpGrade?.Invoke(characterHealthLevel, characterHealthPrice, false);
        }
        else
        {
            onHealthUIUpGrade?.Invoke(characterHealthLevel, characterHealthPrice, true);
        }




    }

    #region DataBase

    int CurrentPowerPrice
    {
        get { return PlayerPrefs.GetInt("CurrentPowerPrice"); }
        set { PlayerPrefs.SetInt("CurrentPowerPrice", value); }
    }

    int CurrentCharacterPrice
    {
        get { return PlayerPrefs.GetInt("CurrentCharacterPrice"); }
        set { PlayerPrefs.SetInt("CurrentCharacterPrice", value); }
    }
    int CurrentCharacterSpeedPrice
    {
        get { return PlayerPrefs.GetInt("CurrentCharacterSpeedPrice"); }
        set { PlayerPrefs.SetInt("CurrentCharacterSpeedPrice", value); }
    }
    int IncrementalCharacterPrice
    {
        get { return PlayerPrefs.GetInt("IncrementalCharacterPrice"); }
        set { PlayerPrefs.SetInt("IncrementalCharacterPrice", value); }
    }
    int IncrementalPowerPrice
    {
        get { return PlayerPrefs.GetInt("IncrementalPowerPrice"); }
        set { PlayerPrefs.SetInt("IncrementalPowerPrice", value); }
    }

    int IncrementalCharacterSpeedPrice
    {
        get { return PlayerPrefs.GetInt("IncrementalCharacterSpeedPrice"); }
        set { PlayerPrefs.SetInt("IncrementalCharacterSpeedPrice", value); }
    }

    int GameStartInit
    {
        get { return PlayerPrefs.GetInt("GameStartInit"); }
        set { PlayerPrefs.SetInt("GameStartInit", value); }
    }
    int SpeedLevel
    {
        get { return PlayerPrefs.GetInt("SpeedLevel"); }
        set { PlayerPrefs.SetInt("SpeedLevel", value); }
    }
    float Speed
    {
        get { return PlayerPrefs.GetFloat("Speed"); }
        set { PlayerPrefs.SetFloat("Speed", value); }
    }
    int BulletSpeed
    {
        get { return PlayerPrefs.GetInt("BulletSpeed"); }
        set { PlayerPrefs.SetInt("BulletSpeed", value); }
    }
    int PowerLevel
    {
        get { return PlayerPrefs.GetInt("PowerLevel"); }
        set { PlayerPrefs.SetInt("PowerLevel", value); }
    }
    int Power
    {
        get { return PlayerPrefs.GetInt("Power"); }
        set { PlayerPrefs.SetInt("Power", value); }
    }
    float Health
    {
        get { return PlayerPrefs.GetFloat ("Health"); }
        set { PlayerPrefs.SetFloat("Health", value); }
    }
    int HealthLevel
    {
        get { return PlayerPrefs.GetInt("HealthLevel"); }
        set { PlayerPrefs.SetInt("HealthLevel", value); }
    }
    int CurrentHealthPrice
    {
        get { return PlayerPrefs.GetInt("CurrentHealthPrice"); }
        set { PlayerPrefs.SetInt("CurrentHealthPrice", value); }
    }
    int IncrementalHealthPrice
    {
        get { return PlayerPrefs.GetInt("IncrementalHealthPrice"); }
        set { PlayerPrefs.SetInt("IncrementalHealthPrice", value); }
    }
    #endregion
    private void OnDestroy()
    {
        onCharacterUpGrade = null;
        onCharacterSpeedUIUpGrade = null;
        onCharacterUIUpGrade = null;
        onPowerUIUpGrade = null;
        onPowerUpgrade = null;
        onHealthUpgrade = null;
        onHealthUIUpGrade = null;
    }
}
