using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider slider;

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
        slider.maxValue = health;
    }
    
    public void SetHealth(int health)
    {
        slider.value = health;
    }
}
