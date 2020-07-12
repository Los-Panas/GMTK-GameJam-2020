using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RoomManager : MonoBehaviour
{
    int actualChild = 1;
    Transform[] children;
    public int points;

    // Start is called before the first frame update
    void Start()
    {
        children = GetComponentsInChildren<Transform>(true);
        points = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (points >= 100 && children[actualChild] != null) //if points are 
        {
            children[actualChild].gameObject.SetActive(false);
            children[actualChild + 1].gameObject.SetActive(false);
            children[actualChild + 2].gameObject.SetActive(true);
            children[actualChild + 3].gameObject.SetActive(true);
            actualChild = actualChild + 2;
            //TODO: borrar balas en la escena
            points = 0;
        }

        if(Input.GetKeyDown(KeyCode.O))
        {
            AddPoints();
        }

        if(actualChild > children.Length)
        {
            //GO TO WIN MENU
        }
    }

    public void AddPoints()
    {
        points = points + 50;
    }
}
