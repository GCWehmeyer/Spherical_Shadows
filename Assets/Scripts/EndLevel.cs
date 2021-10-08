using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndLevel : MonoBehaviour
{


    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Ball")
        {
            
            for (int i = 0; i < 1000; i++)
            {
                other.transform.localScale *= 1.001f;
            }

            nextLevel();
        }
    }


    void nextLevel()
    {
        if (SceneManager.GetActiveScene().buildIndex < 4)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        else
        {
            SceneManager.LoadScene(0);
        }
    }

}
