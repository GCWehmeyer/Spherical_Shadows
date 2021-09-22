using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    public Camera cam;
    private float screenCenterX = Screen.width / 2;
    private float screenCenterY = Screen.height / 2;
    public Transform holdPosition;
    public Transform throwPosition;
    public float ballScale = 4f;
    [Header("Keybinds")]
    [SerializeField] KeyCode pickUpKey = KeyCode.E;

    public float rangePickUp = 3f; //distance for pickup
    public float objectMovementSpeed = 200f; //speed of object once picked up
    public float throwForce = 5f;
    private GameObject heldObject;
    public int hasHeldObject = 0;


    void Update()
    {
        if (Input.GetKeyDown(pickUpKey))
        {
            // if no object
            if (heldObject == null)
            {
                RaycastHit itemHit;
                if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out itemHit, rangePickUp))
                {
                    //picks up object with rigidbidy that is hit with the raycast
                    PickUpObject(itemHit.transform.gameObject);
                }
            }
            else
            {
                DropThrowObject();
            }
        }

        if (heldObject != null)
        {
            //if holding an object, move it
            MoveObject();
        }
    }

    void MoveObject()
    {
        //move if hold_position is not where object is
        if (Vector3.Distance(heldObject.transform.position, holdPosition.position) > 0.1f)
        {
            Vector3 moveDirection = (holdPosition.position - heldObject.transform.position);
            heldObject.GetComponent<Rigidbody>().AddForce(moveDirection * objectMovementSpeed);
        }
    }

    void PickUpObject(GameObject pickUpObject)
    {
        //only works if object has rigid body and is not player
        if (pickUpObject.GetComponent<Rigidbody>() && !pickUpObject.CompareTag("Player"))
        {
            // finding all neccessary references
            Rigidbody objectRigidBody = pickUpObject.GetComponent<Rigidbody>(); 
            Collider pickUpObjectCollider = pickUpObject.GetComponent<Collider>();

            //Shrink ball
            pickUpObject.transform.localScale *= 1/ballScale;

            //disable various physics interactions
            objectRigidBody.useGravity = false;
            objectRigidBody.freezeRotation = true;
            objectRigidBody.drag = 5;
            //isKinamatic could replace???
            
            pickUpObjectCollider.enabled = false;
            
            objectRigidBody.transform.parent = holdPosition;
            heldObject = pickUpObject;
            hasHeldObject = 1;
        }
    }

    void DropThrowObject()
    {
        //move object to throwPosition
        heldObject.transform.position = throwPosition.position;

        //finding screen center and throw direction
        //the last point of the vector is the distance away from the player the ball will travel wrt the camera plane
        //currently a okayish mid range distance - automate value with a fucntion may be possible BUT highly costly
        Vector3 screenCenter = cam.ScreenToWorldPoint(new Vector3(screenCenterX, screenCenterY, 5));
        Vector3 throwDirection = screenCenter - heldObject.transform.position;


        // finding all neccessary references
        Rigidbody heldRigidbody = heldObject.GetComponent<Rigidbody>();
        Collider heldObjectCollider = heldObject.GetComponent<Collider>();
        
        //Unshrink ball
        heldObject.transform.localScale *= ballScale;
        
        //Re-enable physics interactions
        heldRigidbody.useGravity = true;
        heldRigidbody.drag = 1;
        heldRigidbody.freezeRotation = false;

        heldObjectCollider.enabled = true;
        
        //Actual throw functionality
        //normalized to just get direction and not the full vector
        heldRigidbody.AddForce(throwDirection.normalized * throwForce, ForceMode.Impulse);
        hasHeldObject = 0;
        heldObject.transform.parent = null;
        heldObject = null;
    }
}

    /* Usage
     * Add a GameObject child to the player camera
     * Set the object to a desired position infront of camera
     * add script to camera object
     * set child object as hold position variable
     * 
     * Ensure player is not grabable:
     * set all player components (Player/... ; Player/Capsule ; etc) under the player tag
     * player should no not be able to be grabbed thanks to line53* *may change
     */