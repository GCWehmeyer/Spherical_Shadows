using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotateScript : MonoBehaviour
{
    public GameObject cameraToRotate;
    [SerializeField] float rotationSpeed = 8f;
    void Update()
    {
        rotateStopRotate();
    }

    void rotateStopRotate()
    {
        transform.RotateAround(cameraToRotate.transform.position, Vector3.down, rotationSpeed * Time.deltaTime);
    }
}
