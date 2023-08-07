
using Character_Management;
using UnityEngine;
using UnityEngine.UI;

public class playerHealth : MonoBehaviour
{
    [SerializeField] float totalHealth;
    [SerializeField] HealthBarUI healthUI; 
    [SerializeField] float playerMaxDamageTaken;
    [SerializeField] Text scoreText;
    [SerializeField] Text killCountText;
    private int totalInsects;

    private float currentHealth;
    private int currentScore;
    private int currentKillCOunt;
    private void Start()
    {
        currentHealth = totalHealth;
        healthUI.SetMaxHealth(totalHealth);
        totalInsects = WalkerManager.insectCounter;
        killCountText.text = currentKillCOunt + "/" + totalInsects;
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
    }

    public void PlayerScore(int score)
    {
        currentScore += score;
        scoreText.text = "" + currentScore;
    }
    public void AddKill()
    {
        currentKillCOunt++;
        killCountText.text = currentKillCOunt + "/" + totalInsects;
    }


}
