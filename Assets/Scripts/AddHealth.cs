using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AddHealth : MonoBehaviour
{
    public Slider slider;

    public void getHealth(int heal)
    {
        slider.value += heal;
    }
}
