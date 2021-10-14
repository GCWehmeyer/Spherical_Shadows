﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddHealth : MonoBehaviour
{
    public HealthBar healthBar;
    private PlayerMovement PlayerScript;  

    [SerializeField] public Transform teleportDestination; //where
    [SerializeField] public GameObject teleportee; // what/who
    [SerializeField] public GameObject teleporter; // trigger obj

    void OnTriggerEnter(Collider other) //When any object collides with the object this script is placed on
    {
        if (other.tag == teleporter.tag) // Ensures only the player can trigger the teleportation
        {
            teleportee.transform.position = teleportDestination.position; // Position shift
            PlayerScript.health++;
            healthBar.getHealth(10);
        }
    }
}
