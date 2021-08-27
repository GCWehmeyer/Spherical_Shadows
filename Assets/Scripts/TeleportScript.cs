using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportScript : MonoBehaviour
{
    
    //The object that this is placed on must have a collider of some kind - The IsTrigger option must also be ticked !!!!!!

    [SerializeField] public Transform teleportDestination;
    [SerializeField] public GameObject player;
    /*Variables: 
     * teleportDestination must be any gameobject placed at the desired position of teleportation
     * player MUST BE ASSIGNED TO ONLY THE "Player" SUB-OBJECT IN THE PLAYERFULL PREFAB!!!!! 
     *  If teleportation is not acting as intended chech above is correct!!!
     * */



    void OnTriggerEnter(Collider other) //When any object collides with the object this script is placed on
    {
        if (other.tag == "Player" || other.tag == "Teleportable") // Ensures only the player can trigger the teleportation
        {
            player.transform.position = teleportDestination.position; // Position shift
        }
    }


}
/*Currently, only the player can make use of the "portals" - depending on further progress in development this is all that is needed 
 */