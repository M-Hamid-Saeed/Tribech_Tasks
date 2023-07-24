using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class playerHealth : MonoBehaviour,IDamageable
{
    [SerializeField] float totalHealth;
    [SerializeField] HealthBarUI healthUI; 
    private float currentHealth;

    private void Start()
    {
        currentHealth = totalHealth;
        healthUI.SetMaxHealth(totalHealth);
       
    }
    public void Damage(float damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            levelOver();
        }
        SetUI();
    }

    private void SetUI()
    {
        healthUI.SetDamage(currentHealth);
    }

    private void levelOver()
    {
        Debug.Log("LEVEL OVER");
    }
}
