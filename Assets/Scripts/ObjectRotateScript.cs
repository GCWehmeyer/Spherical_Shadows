using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectRotateScript : MonoBehaviour
{
    public GameObject objectToRotate;
    [SerializeField] float rotationSpeed = 150f;
    void Update()
    {
        rotateStopRotate();
    }

    void rotateStopRotate()
    {
        transform.RotateAround(objectToRotate.transform.position, Vector3.down, rotationSpeed * Time.deltaTime);
    }
}


/*When added to a GameObject, it will currently rotate at the set speed*/