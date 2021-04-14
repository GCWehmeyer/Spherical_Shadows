using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCamera : MonoBehaviour
{
    [SerializeField] Transform cameraPosition;

    void Update()
    {
        transform.position = cameraPosition.position;
    }
}

/* MoveCamera Class
 * Used as the script for a "camera holder" object in the project that is seperate to the player object
 * Combined with certian additions in the movement and look scripts allows for a smother looking experience
 * ie eliminates mild stuuering/jittering
 */
