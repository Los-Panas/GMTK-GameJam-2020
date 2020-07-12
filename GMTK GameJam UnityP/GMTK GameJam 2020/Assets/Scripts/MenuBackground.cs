using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuBackground : MonoBehaviour
{
    float timer;
    public float max_timer;

    public GameObject[] Poligons;
    int index = 0;

    void Start()
    {
        
    }

    void Update()
    {
        if (timer > max_timer)
        {
            ChangePoligon();
            timer = 0;
        }
        else
        {
            timer += Time.deltaTime;
        }

    }

    void ChangePoligon()
    {
        Poligons[index].SetActive(false);
        if (index >= 4)
            index = 0;
        else
            index++;
        Poligons[index].SetActive(true);

    }
}
