using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    public bool inTeleport = false;
    private RoomManager roomManager;
    bool play_tp_enable = true;
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

            if(play_tp_enable)
            {
                FindObjectOfType<AudioManager>().Play("TpEnable");
                play_tp_enable = false;
            }
            
        }
        else
        {
            gameObject.GetComponent<Renderer>().enabled = false;
            play_tp_enable = true;
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
