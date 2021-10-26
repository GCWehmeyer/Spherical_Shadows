using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddHealth : MonoBehaviour
{
    private Transform Target;
    public HealthBar healthBar;
    private PlayerMovement PlayerScript;

    private void Start()
    {
        Target = GameObject.FindGameObjectWithTag("Player").transform;
        if (Target != null)
        {
            PlayerScript = Target.GetComponent<PlayerMovement>();
        }
    }

        void OnTriggerEnter(Collider other) //When any object collides with the object this script is placed on
    {
        if (other.tag == "Player") // Ensures only the player can trigger the teleportation
        {
            if(healthBar.health < 100)
            {
                healthBar.health+=10;
                healthBar.getHealth(10);
            }
            
        }
    }
}
