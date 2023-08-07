/*using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System.Collections.Generic;
using System.Collections;
using TMPro;
using Sirenix.OdinInspector;
using AxisGames.BasicGameSet;

public class CoinsManager : SingletonLocal<CoinsManager>
{

    [BoxGroup("Coin Status")]
    [ReadOnly]
    [SerializeField] int Coins;
    [BoxGroup("Script Refrences")]
    [SerializeField] SceneLoad sceneLoad;

    [BoxGroup("Coin UI Refrences")]
    [TabGroup("Coin UI Refrences/t1", "Coin Text")]
    //[SerializeField] Text coinText;
    [SerializeField] TextMeshProUGUI coinText;
    [TabGroup("Coin UI Refrences/t1", "Coin Text")]
    [SerializeField] DOTweenAnimation coinTextAnim;


    [TabGroup("Coin UI Refrences/t1", "Animated Coin Data")]
    [InfoBox("Set Up to Make Animated Coins in Game")]
    [SerializeField] bool UseAnimatedCoins;
    [TabGroup("Coin UI Refrences/t1", "Animated Coin Data")]
    [ShowIf("UseAnimatedCoins")]
    [SerializeField] GameObject animatedCoinPrefab;
    [TabGroup("Coin UI Refrences/t1", "Animated Coin Data")]
    [ShowIf("UseAnimatedCoins")]
    [SerializeField] Transform Container;
    [TabGroup("Coin UI Refrences/t1", "Animated Coin Data")]
    [ShowIf("UseAnimatedCoins")]
    [SerializeField] Transform target;
    [TabGroup("Coin UI Refrences/t1", "Animated Coin Data")]
    [ShowIf("UseAnimatedCoins")]
    [SerializeField] Transform StartPos;
    [TabGroup("Coin UI Refrences/t1", "Animated Coin Data")]
    [ShowIf("UseAnimatedCoins")]
    [Space(5)]
    [SerializeField] int maxCoins;
    Queue<GameObject> coinsQueue = new Queue<GameObject>();

    [TabGroup("Coin UI Refrences/t1", "Animated Coin Data")]
    [ShowIf("UseAnimatedCoins")]
    [SerializeField] [Range(0.5f, 0.9f)] float minAnimDuration;
    [TabGroup("Coin UI Refrences/t1", "Animated Coin Data")]
    [ShowIf("UseAnimatedCoins")]
    [SerializeField] [Range(0.9f, 2f)] float maxAnimDuration;

    [TabGroup("Coin UI Refrences/t1", "Animated Coin Data")]
    [ShowIf("UseAnimatedCoins")]
    [SerializeField] Ease easeType;
    [TabGroup("Coin UI Refrences/t1", "Animated Coin Data")]
    [ShowIf("UseAnimatedCoins")]
    [SerializeField] float spreadX = 0.3f;
    [TabGroup("Coin UI Refrences/t1", "Animated Coin Data")]
    [ShowIf("UseAnimatedCoins")]
    [SerializeField] float spreadY = 0.3f;


    [BoxGroup("Particles", centerLabel: true)]
    [SerializeField] GameObject confiti;
    [BoxGroup("Particles", centerLabel: true)]
    [SerializeField] ParticleSystem dimondParticle;

    /// <summary>
    /// Privae Variables
    /// </summary>
    int BonusPoint;
    int CollectedCoins = 1;
    int Points = 0;
    Vector3 targetPosition;
    int numCompleted = 0; // this is use by bonus coins
    int vibrateCount = 0;

    private void Awake()
    {
        if (UseAnimatedCoins) PrepareCoins();
        CheckPreviousCoins();
    }

    void PrepareCoins()
    {
        if (animatedCoinPrefab == null || Container == null) { Debug.LogError("Coin Refrences not Assign !!"); return; }
        GameObject coin;
        for (int i = 0; i < maxCoins; i++)
        {
            coin = Instantiate(animatedCoinPrefab);
            coin.transform.SetParent(Container);
            coin.transform.position = Container.position;
            coin.transform.localScale = Vector3.one;
            coin.SetActive(false);
            coinsQueue.Enqueue(coin);
        }
    }

    public bool CanDoTransaction(int amount)
    {
        if (Coins >= amount)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void DeductCoins(int price)
    {
        Coins -= price;
        coinText.text = Coins.ToString();
        if (!coinTextAnim.hasOnComplete) coinTextAnim.DORestart();
        if (dimondParticle) dimondParticle.Play();
        SaveCoins();
        coinText.color = Color.red;
        StartCoroutine(nameof(ResetTextColor));
    }

    IEnumerator ResetTextColor()
    {
        yield return new WaitForSeconds(0.15f);
        coinText.color = Color.white;
        StopCoroutine(nameof(ResetTextColor));
    }

    public void CollectCoin(int value)
    {
        Coins += value;
        CollectedCoins += value;
        //Points++;
        if (!coinTextAnim.hasOnComplete) coinTextAnim.DORestart();
        coinText.text = Coins.ToString();
    }

    public int GetCollectedCoins()
    {
        return CollectedCoins;
    }

    public void SaveCoins()
    {
        SavedCoins = Coins;
    }

    private void CheckPreviousCoins()
    {
        if (SavedCoins == 0 && InitialCash == 0)
        {
            InitialCash = 1;
            Coins = int.Parse(coinText.text);
            SavedCoins = Coins;
        }
        else
        {
            Coins = SavedCoins;
            coinText.text = Coins.ToString();
        }
    }

    void Animate(int amount, Vector3 position)
    {

        if (amount <= 5)
        {
            vibrateCount = 2;
        }
        else if (amount <= 10)
        {
            vibrateCount = 4;
        }
        else
        {
            vibrateCount = 6;
        }

        targetPosition = target.position;
        for (int i = 0; i < amount; i++)
        {
            //check if there's coins in the pool
            if (coinsQueue.Count > 0)
            {
                //extract a coin from the pool
                GameObject coin = coinsQueue.Dequeue();
                coin.transform.position = position;
                coin.SetActive(true);

                //move coin to the collected coin pos
                //coin.transform.position = position + new Vector3
                //                                (Random.Range(-spreadX, spreadX),
                //                                Random.Range(-spreadY, spreadY), 0f);

                //animate coin to target position
                float duration = Random.Range(minAnimDuration, maxAnimDuration);

                coin.transform.DOMove(position + new Vector3
                                                (Random.Range(-spreadX, spreadX),
                                                Random.Range(-spreadY, spreadY), 0f), duration).OnComplete(() =>
             {

                 coin.transform.DOMove(targetPosition, duration)
                 .SetEase(easeType)
                 .OnComplete(() =>
                 {
                     //executes whenever coin reach target position
                     coin.SetActive(false);
                     coinsQueue.Enqueue(coin);
                     CollectCoin(1);

                     if (vibrateCount != 0)
                     {
                         vibrateCount--;
                         //ReferenceManager.instance.gameManager.PopVibrate();
                         //Debug.Log("Vibrate " + vibrateCount);
                     }
                     numCompleted--;
                     if (numCompleted == 0)
                     {
                         SaveCoins();
                         Invoke(nameof(RestartScene), 0.6f);
                     }

                 });
             });
            }
        }

    }

    private void RestartScene()
    {
        if (confiti) confiti.SetActive(false);
        DOTween.KillAll();
        StopAllCoroutines();
        sceneLoad.ReloadScene();
    }

    public void AddBonusCoins(int animateCount, int totalCash, Transform spawnPoint = null)
    {
        CollectCoin(totalCash - animateCount);

        Vector3 startPos = StartPos.position;
        if (spawnPoint) { startPos = spawnPoint.position; }
        //Instantiate(confiti, StartPos);
        if (confiti) confiti.SetActive(true);
        numCompleted = animateCount; // used to keep trake of animation completed to restart scene
        Animate(animateCount, startPos);
    }

    private void OnDestroy()
    {
        SaveCoins();
    }

    public static int SavedCoins
    {
        get
        {
            return PlayerPrefs.GetInt("savedcoins");
        }
        set
        {
            PlayerPrefs.SetInt("savedcoins", value);
        }
    }

    public static int InitialCash
    {
        get
        {
            return PlayerPrefs.GetInt("InitialCash");
        }
        set
        {
            PlayerPrefs.SetInt("InitialCash", value);
        }
    }

}
*/