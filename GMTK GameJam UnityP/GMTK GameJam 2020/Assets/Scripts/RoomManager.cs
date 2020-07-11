using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomManager : MonoBehaviour
{
    private Transform PlayerTransform;

    public Transform TeleportGoal;

    public int enemySpawns = 0;
    // Start is called before the first frame update
    void Start()
    {
        PlayerTransform = GameObject.Find("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.O)) //if no enemy of that spawner is alive
        {
            PlayerTransform.position = TeleportGoal.position;
            //activate next spawner, could be an array of spawners
        }
    }
}
