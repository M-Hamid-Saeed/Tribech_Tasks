using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarUI : MonoBehaviour
{
    [SerializeField] Slider healthSlider;
    
    
    // Start is called before the first frame update
    
    public void SetMaxHealth(float maxHealth)
    {
        healthSlider.maxValue = maxHealth;
        healthSlider.value = maxHealth;
    }
    public void SetDamage(float health)
    {
        healthSlider.value = health;
    }

   
}
