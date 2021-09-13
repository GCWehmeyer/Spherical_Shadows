using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatforms : MonoBehaviour
{
    public Transform startPosition;
    public Transform endPosition;
    private bool flipCheck = true;

    public float movementSpeed = 1.0f;
    private float startTime;
    private float movementDistance;

    void Start()
    {
        startTime = Time.time;
        movementDistance = Vector3.Distance(startPosition.position, endPosition.position);
    }

    void Update()
    {
        if(flipCheck)
        {
            float completedMovement = (Time.time - startTime) * movementSpeed;
            float percentCovered = completedMovement / movementDistance;
            transform.position = Vector3.Lerp(startPosition.position, endPosition.position, percentCovered);
        }
        else if(!flipCheck)
        {
            float completedMovement = (Time.time - startTime) * movementSpeed;
            float percentCovered = completedMovement / movementDistance;
            transform.position = Vector3.Lerp(endPosition.position, startPosition.position, percentCovered);
        }

        if(transform.position == endPosition.position)
        {
            flipCheck = false;
            startTime = Time.time;
            movementDistance = Vector3.Distance(endPosition.position, startPosition.position);

        }
        else if (transform.position == startPosition.position)
        {
            flipCheck = true;
            startTime = Time.time;
            movementDistance = Vector3.Distance(startPosition.position, endPosition.position);

        }

    }
    
}
