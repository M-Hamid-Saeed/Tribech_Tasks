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
    public DOTweenAnimation machineGunAnimation;
    public DOTweenAnimation machineGunShakeAnimation;
    public GameObject crossHaironHit;
    public OffScreenIndicatorNew offscreenIndicator;
    public FiringSystem firingSystem;
    public LevelManager levelManager;
    




    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        
        else
            DestroyImmediate(gameObject);
        crossHaironHit.SetActive(false);
        Debug.Log("Walker Manager Insects" + walkerManager.insectCounter);
    }
 
}
