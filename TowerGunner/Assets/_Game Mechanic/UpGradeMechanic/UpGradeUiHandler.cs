
using DG.Tweening;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpGradeUiHandler : MonoBehaviour
{
    private void Awake()
    {
        GameController.onHome += OnGameIni;
        GameController.onGameplay += OnGameComplete;
      //  GameController.onLevelComplete += OnGameComplete;

        UpgradeManager.onCharacterUIUpGrade += CharacterUiUpdate;
        UpgradeManager.onCharacterSpeedUIUpGrade += SpeedUiUpdate;
        UpgradeManager.onPowerUIUpGrade += PowerUiUpdate;
    }
    [FoldoutGroup("---- Component Refrences ----")]
    [InlineButton("LoadVisual", "Load")]
    [SerializeField] protected GameObject _visual;

    [Space(2)]
    [FoldoutGroup("---- Character Upgrade UI ----")]
    [SerializeField] protected Text _CharacterPriceText;
    [FoldoutGroup("---- Character Upgrade UI ----")]
    [SerializeField] protected Text _CharacterLevelText;
    [FoldoutGroup("---- Character Upgrade UI ----")]
    [SerializeField] protected GameObject _CharacterAdImage;
    [FoldoutGroup("---- Character Upgrade UI ----")]
    [SerializeField] protected GameObject _CharacterCashImage;

    [Space(2)]
    [FoldoutGroup("---- Power Upgrade UI ----")]
    [SerializeField] protected Text _PowerPriceText;
    [FoldoutGroup("---- Power Upgrade UI ----")]
    [SerializeField] protected Text _PowerLevelText;
    [FoldoutGroup("---- Power Upgrade UI ----")]
    [SerializeField] protected GameObject _PowerAdImage;
    [FoldoutGroup("---- Power Upgrade UI ----")]
    [SerializeField] protected GameObject _PowerCashImage;


    [Space(2)]
    [FoldoutGroup("---- Speed Upgrade UI ----")]
    [SerializeField] protected Text _SpeedPriceText;
    [FoldoutGroup("---- Speed Upgrade UI ----")]
    [SerializeField] protected Text _SpeedCharacterLevelText;
    [FoldoutGroup("---- Speed Upgrade UI ----")]
    [SerializeField] protected GameObject _CharacterSpeedAdImage;
    [FoldoutGroup("---- Speed Upgrade UI ----")]
    [SerializeField] protected GameObject _CharacterSpeedCashImage;




    void SpeedUiUpdate(int level, int price, bool isFree)
    {
        Debug.Log(level + "LEVEL");
        SwipeImage(_CharacterSpeedCashImage, _CharacterSpeedAdImage, isFree);
        _SpeedPriceText.text = price.ToString();
        _SpeedCharacterLevelText.text = level.ToString();

    }
    void CharacterUiUpdate(int level, int price, bool isFree)
    {

        SwipeImage(_CharacterCashImage, _CharacterAdImage, isFree);
        _CharacterPriceText.text = price.ToString();
        _CharacterLevelText.text = level.ToString();

    }
    void PowerUiUpdate(int level, int price, bool isFree)
    {

        SwipeImage(_PowerCashImage, _PowerAdImage, isFree);
        _PowerPriceText.text = price.ToString();
        _PowerLevelText.text = level.ToString();

    }
    void SwipeImage(GameObject cashButton, GameObject AdButton, bool isAd = false)
    {
        if (isAd)
        {
            cashButton.SetActive(false);
            AdButton.SetActive(true);
        }
        else
        {
            cashButton.SetActive(true);
            AdButton.SetActive(false);
        }
    }
    private void LoadVisual()
    {
        _visual = transform.GetChild(0).gameObject;
    }
    void OnGameIni()
    {
        _visual.SetActive(true);
    }
    void OnGameComplete()
    {
        _visual.SetActive(false);
        //FunctionTimer.Create(() => { _visual.SetActive(true); }, 1f);
    }
}
