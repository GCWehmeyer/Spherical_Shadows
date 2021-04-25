using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallReturn : MonoBehaviour
{
    public Transform holdPosition;
    [SerializeField] KeyCode returnKey = KeyCode.T;


    void Update()
    {
        if (Input.GetKeyDown(returnKey))
        {
            returnBall();
        }
    }

    void returnBall()
    {
        transform.position = holdPosition.position;
    }

}


/*
 Intended for debugging when collision checks fail on ball due to various issues
 */