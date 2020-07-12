using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    public GameObject Player;
    static public bool change_state;
    [SerializeField]
    private float offset;

 

    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            change_state = !change_state;
        }


        float distance = Vector3.Distance(transform.position, Player.transform.position);

        if (change_state && distance > offset)
        {
            transform.position = Vector3.MoveTowards(transform.position, Player.transform.position, -17 * Time.deltaTime);
        }
        else if (distance > offset)
        {
            transform.position = Vector3.MoveTowards(transform.position, Player.transform.position, 25 * Time.deltaTime);
        }
    }
}
