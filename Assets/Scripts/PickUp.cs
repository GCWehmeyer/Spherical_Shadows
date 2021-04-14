﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    public Transform holdPosition;

    [Header("Keybinds")]
    [SerializeField] KeyCode pickUpKey = KeyCode.E;

    public float rangePickUp = 3f; //distance for pickup
    public float objectMovementSpeed = 200f; //speed of object once picked up
    private GameObject heldObject;


    void Update()
    {
        if(Input.GetKeyDown(pickUpKey))
        {
            if (heldObject == null)// if no object
            {
                RaycastHit itemHit;
                if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out itemHit, rangePickUp))
                {
                    PickUpObject(itemHit.transform.gameObject);//picks up object with rigidbidy that is hit with the raycast
                }
            }
            else
            {
                DropThrowObject();
            }
        }

        if (heldObject != null)
        {
            MoveObject();//if holding an object, move it
        }
    }

    void MoveObject()
    {
        if (Vector3.Distance(heldObject.transform.position, holdPosition.position) > 0.1f)//move if hold_position is not where object is
        {
            Vector3 moveDirection = (holdPosition.position - heldObject.transform.position);
            heldObject.GetComponent<Rigidbody>().AddForce(moveDirection * objectMovementSpeed);
        }
    }

    void PickUpObject(GameObject pickUpObject)
    {
        if (pickUpObject.GetComponent<Rigidbody>())//only works if object has rigid body
        {
            Rigidbody objectRigidBody = pickUpObject.GetComponent<Rigidbody>();
            objectRigidBody.useGravity = false;
            objectRigidBody.freezeRotation = true;
            objectRigidBody.drag = 5;

            objectRigidBody.transform.parent = holdPosition;
            heldObject = pickUpObject;
        }
    }

    void DropThrowObject()
    {
        Rigidbody heldRigidbody = heldObject.GetComponent<Rigidbody>();
        heldRigidbody.useGravity = true;
        heldRigidbody.drag = 1;
        heldRigidbody.freezeRotation = false;
        heldRigidbody.AddForce(transform.forward * 25f, ForceMode.Impulse);

        heldObject.transform.parent = null;
        heldObject = null;
    }

    /* Usage
     * Add a GameObject child to the player camera
     * Set the object to a desired position infront of camera
     * add script to camera object
     * set child object as hold position variable
     */


    /*Additional Notes
     * Bug(s):
     *      is currently possibly to pick up player object -> need to add check or layer to resolve
     */

}