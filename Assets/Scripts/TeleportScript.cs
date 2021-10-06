using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportScript : MonoBehaviour
{
    
    //The object that this is placed on must have a collider of some kind - The IsTrigger option must also be ticked !!!!!!

    [SerializeField] public Transform teleportDestination; //where
    [SerializeField] public GameObject teleportee; // what/who
    [SerializeField] public GameObject teleporter; // trigger obj

    /*Variables: 
     * teleportDestination must be any gameobject placed at the desired position of teleportation
     * item MUST BE ASSIGNED TO ONLY THE "Player" SUB-OBJECT IN THE PLAYERFULL PREFAB when aiming for player teleport
     *  If teleportation is not acting as intended chech above is correct!!!
     * */



    void OnTriggerEnter(Collider other) //When any object collides with the object this script is placed on
    {
        if (other.tag == teleporter.tag) // Ensures only the player can trigger the teleportation
        {
            teleportee.transform.position = teleportDestination.position; // Position shift
        }
    }


}
/*TO DO:
 * set up variables for each use case:
 * if ball then teleport item
 * if player then teleport player
 * if teleportable then teleport teleportable
 */