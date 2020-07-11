using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomManager : MonoBehaviour
{
    private Transform PlayerTransform;

    public Transform TeleportGoal;

    public int enemySpawns = 0;

    private int enemy = 0;

    // Start is called before the first frame update
    void Start()
    {
        PlayerTransform = GameObject.Find("Player").transform;
        enemy = enemySpawns;
    }

    // Update is called once per frame
    void Update()
    {
        // if (enemy.isdeleted)
        // {
        //      enemy = enemy - 1;
        // }

        if (enemy <= 0)
        {
            PlayerTransform.position = TeleportGoal.position;
        }
    }
}
