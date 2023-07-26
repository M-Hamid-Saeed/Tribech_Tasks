using System;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{

    [SerializeField] GameObject homePanel;
    [SerializeField] GameObject gamplayPanel;
    [SerializeField] GameObject competePanel;
    [SerializeField] GameObject levelFailPanel;



    [Header("Text Fields"), SerializeField] Text levelNoText;

    void Awake()
    {
        GameController.onLevelComplete += OnLevelComplete;
        GameController.onGameplay += Gameplay;
        GameController.onLevelFail += LevelFail;
        GameController.onHome += Home;
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
        int levelNo = PlayerPrefs.GetInt("LevelNumber");
        levelNo += 1;
        levelNoText.text = $"Level {levelNo.ToString("00")}";
        ActivePanel(gameplay: true);
    }

    void OnLevelComplete()
    {
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


    // Buttons 


    public void TapToPlay()
    {
        GameController.changeGameState.Invoke(GameState.Gameplay);

    }



}