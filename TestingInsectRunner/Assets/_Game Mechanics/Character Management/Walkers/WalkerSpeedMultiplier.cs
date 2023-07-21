
using System;
using UnityEngine;


namespace Character_Management { }

public class WalkerSpeedMultiplier : MonoBehaviour
{
    public static event Action<float> onSpeedMultiplied, onSpeedNormalized;

    [Header(" ------ Multiplier Settings ------")]
    [SerializeField] float speed = 2;
    [SerializeField] float multiplierCooldown = 1f;

    float timer;

    bool taping;
    bool canControll;

    private void Awake()
    {
      //  GameController.onHome += GameController_onHome;
      //  GameController.onMergeArea += GameController_onMergeArea;
    }

    private void Update()
    {
        if (canControll && taping)
        {
            timer += Time.deltaTime;

            if (timer >= 0.1f) { onSpeedMultiplied?.Invoke(speed); }
            if(timer >= multiplierCooldown) { taping = false; onSpeedNormalized?.Invoke(0); }

#if !UNITY_EDITOR
            if (Input.GetMouseButtonDown(0)) { Vibration.VibratePop(); } // for small haptic on every click
#endif
        }
    }

    public void TapTap()
    {
        timer = 0;
        taping = true;
    }

    private void GameController_onHome()
    {
        canControll = true;
        timer = multiplierCooldown;
    }
    private void GameController_onMergeArea()
    {
        canControll = false;
        timer = multiplierCooldown;
        onSpeedNormalized?.Invoke(0);
    }

    private void OnDestroy()
    {
        onSpeedMultiplied = null;
        onSpeedNormalized = null;
    }
}
