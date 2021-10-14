using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider slider;
    public int maxHealth = 100;                    //initialize Max health
    public int currentHealth;                     //Initialize current health   

    public void takeDamage(int damage)
    {
        slider.value -= damage;
    }

    public void getHealth(int heal)
    {
        slider.value += heal;
    }
    
    public void SetMaxHealth(int health)
    {
        slider.maxValue = maxHealth;
        slider.value = health;
    }
    
    public void SetHealth(int health)
    {
        slider.value = health;
    }
}
