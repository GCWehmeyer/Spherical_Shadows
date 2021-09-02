using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallReturn : MonoBehaviour
{
    public Transform holdPosition;
    [SerializeField] KeyCode returnKey = KeyCode.T;
    Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

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
        rb.velocity = Vector3.zero;
        //debugging code
        /*Collider collider = GetComponent<Collider>();
        collider.enabled = true;*/
    }

}


/*
 Intended for debugging when collision checks fail on ball due to various issues
 */