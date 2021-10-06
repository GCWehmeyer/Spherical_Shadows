using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DevLevelSecret : MonoBehaviour
{
    [SerializeField] public GameObject teleporter; // trigger obj

    void toDevLevel()
    {
        SceneManager.LoadScene(4);
    }

    void OnTriggerEnter(Collider other) //When any object collides with the object this script is placed on
    {
        if (other.tag == teleporter.tag) // Ensures only the player can trigger the teleportation
        {
            toDevLevel();
        }
    }
}
