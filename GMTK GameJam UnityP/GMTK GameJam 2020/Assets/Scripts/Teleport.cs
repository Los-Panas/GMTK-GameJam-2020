using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    public bool inTeleport = false;
    private RoomManager roomManager;

    void Start()
    {
        GameObject Walls = GameObject.Find("Walls");
        roomManager = Walls.GetComponent<RoomManager>();
    }

    void Update()
    {
        if(roomManager.points >= roomManager.maxPoints)
        {
            gameObject.GetComponent<Renderer>().enabled = true;
        }
        else
        {
            gameObject.GetComponent<Renderer>().enabled = false;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            inTeleport = true;
        }
    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            inTeleport = false;
        }
    }
}
