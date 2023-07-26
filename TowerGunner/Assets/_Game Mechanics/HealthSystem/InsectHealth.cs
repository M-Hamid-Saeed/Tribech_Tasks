using UnityEngine;

public class InsectHealth : MonoBehaviour, IDamageable
{
    [SerializeField] float totalHealth;
    [SerializeField] float insectMaxDamageTaken;
    public float insectAttackingDamage;
    [SerializeField] HealthBarUI healthUI;
    
  
    public float currentHealth;

    private void Start()
    {
       
        currentHealth = totalHealth;
        healthUI.SetMaxHealth(totalHealth);
    }
    public void Damage(float damage)
    {
        if (damage <= insectMaxDamageTaken)
            currentHealth -= damage;     
       
       else

            currentHealth -= insectMaxDamageTaken;
       
        if (currentHealth <= 0)
            Destroy(gameObject);
        
        SetUI();

    }
    private void SetUI()
    {

        healthUI.SetDamage(currentHealth);

    }

}
