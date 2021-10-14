using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddHealth : MonoBehaviour
{
    public HealthBar healthBar;
    private PlayerMovement PlayerScript;  

    void OnTriggerEnter(Collider other) //When any object collides with the object this script is placed on
    {
        if (other.tag == player.tag) // Ensures only the player can trigger the teleportation
        {
            PlayerScript.health++;
            healthBar.getHealth(10);
        }
    }
}
