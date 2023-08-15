
using Character_Management;
using UnityEngine;
using UnityEngine.UI;

public class playerHealth : MonoBehaviour
{
    [SerializeField] float totalHealth;
    [SerializeField] HealthBarUI healthUI; 
    [SerializeField] float playerMaxDamageTaken;
    private float currentHealth;
    private void Awake()
    {
        UpgradeManager.onHealthUpgrade += SetMaxDamage;
    }
    private void Start()
    {
        currentHealth = totalHealth;
        healthUI.SetMaxHealth(totalHealth);

    }
    private void SetMaxDamage(float maxDamageTaken)
    {
        playerMaxDamageTaken = maxDamageTaken;
    }
    public void Damage(float damage)
    {
        if (damage <= playerMaxDamageTaken)
            currentHealth -= damage;
        else
            currentHealth -= playerMaxDamageTaken;

        if (currentHealth <= 0)
            levelOver();
        
        SetUI();
    }
    private void SetUI()
    {
        Debug.Log("SETTING UI");
        healthUI.SetDamage(currentHealth);
    }


    private void levelOver()
    {
        GameController.changeGameState(GameState.Fail);
        StopAllCoroutines();
        
    }

   



}
