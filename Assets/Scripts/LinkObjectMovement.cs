using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinkObjectMovement : MonoBehaviour
{
    [SerializeField] Transform linkedObjectPosition;
    public Vector3 offset = new Vector3(0, 0, 0);
   
    void Update()
    {
        this.transform.position = linkedObjectPosition.position + offset;
    }
}

/* LinkObjectMovement Class
 * example use:
 * Used as the script for a "camera holder" object in the project that is seperate to the player object
 * Combined with certian additions in the movement and look scripts allows for a smother looking experience
 * ie eliminates mild stuuering/jittering
 * 
 * used to move one object along with another
 */
