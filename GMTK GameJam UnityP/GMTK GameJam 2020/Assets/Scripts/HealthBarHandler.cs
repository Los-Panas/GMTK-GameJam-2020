using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;   

public class HealthBarHandler : MonoBehaviour
{
    public Slider slider;

    public void SetMaxHealth(int health)
    {
        slider.maxValue = health;
        slider.value = health;
        Debug.Log("Max Health");
    }

    public void SetHealth(int health)
    {
        slider.value = health;
        Debug.Log("Minus Health");
    }
}
