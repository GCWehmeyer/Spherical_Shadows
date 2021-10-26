using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;
    public Enemy enemy;
    public HealthBar healthBar;
    
    // Start is called before the first frame update
    private void Start()
    {
        currentHealth = maxHealth;
		healthBar.SetMaxHealth(maxHealth);
        healthBar.SetHealth(currentHealth);
    }

    // Update is called once per frame
    private void Update()
    {
        if (currentHealth > 0)
		{
			TakeDamage(10);
		}
    }

    public void TakeDamage(int damage)
	{
		currentHealth -= damage;
		healthBar.takeDamage(damage);
	}

    public void addHealth(int health)
    {
        healthBar.getHealth(health);
    }
}
