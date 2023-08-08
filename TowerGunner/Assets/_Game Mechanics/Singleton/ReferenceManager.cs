using Character_Management;
using DG.Tweening;
using UnityEngine;

public class ReferenceManager : MonoBehaviour
{
    public static ReferenceManager Instance { get; private set; }

    public InputManager input;
    public UIManager mainUIManager;
    public WalkerManager walkerManager;
    public CameraShake_Management CameraShakeManager;
    public DOTweenAnimation playerHitAnimation;
    public DOTweenAnimation crosshairAnimation;
    public GameObject crossHaironHit;
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        
        else
            DestroyImmediate(gameObject);
        crossHaironHit.SetActive(false);

    }

}
