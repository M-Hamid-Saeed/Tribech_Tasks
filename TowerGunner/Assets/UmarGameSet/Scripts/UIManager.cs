using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{

    [SerializeField] GameObject homePanel;
    [SerializeField] GameObject gamplayPanel;
    [SerializeField] GameObject competePanel;
    [SerializeField] GameObject levelFailPanel;


    [SerializeField] Text totalMisslesText;

    private int currentKillCount;
    int levelNo;
    public int totalInsects;

    [Header("Text Fields"), SerializeField] Text levelNoText;
    [Header("Text Fields"), SerializeField] Text KillText;

    void Awake()
    {
        GameController.onLevelComplete += OnLevelComplete;
        GameController.onGameplay += Gameplay;
        GameController.onLevelFail += LevelFail;
        GameController.onHome += Home;
    }
    private void Start()
    {
        totalInsects = ReferenceManager.Instance.walkerManager.insectCounter;
        Debug.Log("TOTAL INSECTS" + ReferenceManager.Instance.walkerManager.insectCounter);
        KillText.text = currentKillCount + "/" + totalInsects;
        totalMisslesText.text = ReferenceManager.Instance.levelManager.levels[LevelManager.CurrentLevelNumber].NoOfMissles.ToString();
    }

                          
    //Events Definations
    void Home()
    {
        ActivePanel(home: true);
    }

    void LevelFail()
    {

        ActivePanel(fail: true);
    }

    void Gameplay()
    {
        levelNo = PlayerPrefs.GetInt("LevelNumber");
        levelNo += 1;

        levelNoText.text = $"Level {levelNo.ToString("00")}";
        ActivePanel(gameplay: true);
        totalInsects = ReferenceManager.Instance.walkerManager.insectCounter;
        Debug.Log("TOTAL INSECTS" + ReferenceManager.Instance.walkerManager.insectCounter);
    }


    void OnLevelComplete()
    {
        Debug.Log("COMPLETED");

        ActivePanel(complete: true);
    }


    //Active Panels
    void ActivePanel(bool gameplay = false, bool home = false, bool complete = false, bool fail = false)
    {
        gamplayPanel.SetActive(gameplay);
        homePanel.SetActive(home);
        competePanel.SetActive(complete);
        levelFailPanel.SetActive(fail);
    }

    public void SetMissleUI()
    {
        totalMisslesText.text = ReferenceManager.Instance.controller.totalMissles.ToString();
    }

    // Buttons 


    public void TapToPlay()
    {
        GameController.changeGameState.Invoke(GameState.Gameplay);
    }

    public void AddKillCount(GameObject currentInsect)
    {
        currentKillCount++;
        KillText.text = currentKillCount + "/" + totalInsects;
        if (currentKillCount == totalInsects)
            GameController.changeGameState(GameState.Complete);


    }

}