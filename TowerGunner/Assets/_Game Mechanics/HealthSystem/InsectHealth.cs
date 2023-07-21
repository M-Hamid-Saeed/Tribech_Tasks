using UnityEngine;

public class InsectHealth : MonoBehaviour, IDamageable
{
    [SerializeField] float totalHealth;
    [SerializeField] float insectMaxDamageTaken;
    public float insectAttackingDamage;
    [SerializeField] HealthBarUI healthUI;
    
    [SerializeField] playerHealth player_health;
    private float currentHealth;

    private void Start()
    {
       
        currentHealth = totalHealth;
        healthUI.SetMaxHealth(totalHealth);
    }
    public void Damage(float damage)
    {
        //if (damage <= insectMaxDamageTaken)
            currentHealth -= damage;     
       
       /* else

            currentHealth -= insectMaxDamageTaken;*/
       
        if (currentHealth <= 0)
            Destroy(gameObject);
        
        SetUI();

    }
    private void SetUI()
    {

        healthUI.SetDamage(currentHealth);

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Attacking Range"))
        {
            player_health.Damage(insectAttackingDamage);
        }
    }
}
