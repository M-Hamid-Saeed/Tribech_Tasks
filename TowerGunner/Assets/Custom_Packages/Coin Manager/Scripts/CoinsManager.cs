using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System.Collections.Generic;
using TMPro;

public class CoinsManager : MonoBehaviour
{

    public static CoinsManager Instance;
    //References
    [Header("UI references")]
    [SerializeField] public Text coinUIText;
    
    [SerializeField] public GameObject animatedCoinPrefab;
    [SerializeField] public Transform target;
    public RectTransform canvasRect;
    public RectTransform cashContainer;
    public Camera mainCam;
    [SerializeField] DOTweenAnimation animatedCoin;
    [Space]
    [Header("Available coins : (coins to pool)")]
    [SerializeField] int maxCoins;
    Queue<GameObject> coinsQueue = new Queue<GameObject>();


    [Space]
    [Header("Animation settings")]
    [SerializeField] [Range(0.5f, 0.9f)] float minAnimDuration;
    [SerializeField] [Range(0.9f, 2f)] float maxAnimDuration;

    [SerializeField] Ease easeType;
    [SerializeField] float spread;
    public int CollectionValue;

    Vector3 targetPosition;


    private int _c = 0;

    public int Coins
    {
        get { return PlayerPrefs.GetInt("Coin"); }
        set
        {
            PlayerPrefs.SetInt("Coin", value);
        }
    }
    [SerializeField] bool isTesting;
    void Awake()
    {

        if (Instance == null)
        {
            Instance = this;
            
        }
        targetPosition = target.position;

        //Debug.Log("CashStartTime" + ExtendedPrefs.GetBool("CashStartTime"));
        //if (!ExtendedPrefs.GetBool("CashStartTime1"))
        //{
        //    ExtendedPrefs.SetBool("CashStartTime1", true);
        //    AddCoins(15);
        //}
        coinUIText.text = Coins.ToString();
       
        if (isTesting)
        {
            AddCoins(500);
        }
        GameController.onGameplay += AddCoinOnGamePlay;
        //prepare pool
        PrepareCoins();

    }

    void PrepareCoins()
    {
        GameObject coin;
        for (int i = 0; i < maxCoins; i++)
        {
            coin = Instantiate(animatedCoinPrefab);
            coin.transform.parent = cashContainer;
            coin.transform.localPosition = Vector3.zero;
            coin.transform.localScale = Vector3.one;
            coin.SetActive(false);
            coinsQueue.Enqueue(coin);
        }
    }

    void Animate(Vector3 collectedCoinPosition, int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            //check if there's coins in the pool
            if (coinsQueue.Count > 0)
            {
                
                //extract a coin from the pool
                GameObject coin = coinsQueue.Dequeue();
                coin.SetActive(true);

                //move coin to the collected coin pos
                var rectTransform = coin.GetComponent<RectTransform>();
                rectTransform.localPosition = collectedCoinPosition + new Vector3(Random.Range(-spread, spread), 0f, 0f);

                //animate coin to target position
                float duration = Random.Range(minAnimDuration, maxAnimDuration);
                coin.transform.DOMove(targetPosition, duration)
                .SetEase(easeType)/*.OnUpdate(() => { coin.transform.DOScale(0.08f, 1); })*/
                .OnComplete(() =>
                {
                    //executes whenever coin reach target position
                    coin.SetActive(false);
                    coinsQueue.Enqueue(coin);
                    coinUIText.text = Coins.ToString();
                    // Coins++;
                    //CurrencyManager.Instance.PlusCurrencyValue("Coins", 1);
                });
            }
        }
    }

    public void AddCoins(Vector3 collectedCoinPosition, int amount)
    {
        Animate(collectedCoinPosition, amount);
    }
    public bool CanDoTransaction(int amount)
    {
        Coins = PlayerPrefs.GetInt("Coin");
        if (Coins >= amount)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    public void AddCoins(int amount)
    {
        Coins += amount;

        animatedCoin.DORestartbyID("Rotate");
        AddCoinOnGamePlay();

    }
    public void DecCoins(int amount)
    {

        Coins -= amount;

        AddCoinOnGamePlay();


    }
    public void AddCoinOnGamePlay()
    {

        coinUIText.text = Coins.ToString();
       
    }

    public Vector2 WordPointToCanvasPoint(Camera camera, Vector3 worldPoint, RectTransform canvasRect)
    {
        Vector2 viewportPosition = camera.WorldToViewportPoint(worldPoint);
        Vector2 screenPosition = new Vector2(((viewportPosition.x * canvasRect.sizeDelta.x) - (canvasRect.sizeDelta.x * 0.5f)), ((viewportPosition.y * canvasRect.sizeDelta.y) - (canvasRect.sizeDelta.y * 0.5f)));
        return screenPosition;
    }

}
