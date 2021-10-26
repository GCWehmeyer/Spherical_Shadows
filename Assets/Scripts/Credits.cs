using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Credits : MonoBehaviour
{

    [Header("Keybinds")]
    [SerializeField] KeyCode end = KeyCode.Escape;
    [SerializeField] KeyCode endCont = KeyCode.JoystickButton0; // A


    // Update is called once per frame
    void Update()
    {
        if ((Input.GetKeyDown(end) || Input.GetKeyDown(endCont)))//can only jump if on floor
        {
            SceneManager.LoadScene(0);
        }
    }
}
