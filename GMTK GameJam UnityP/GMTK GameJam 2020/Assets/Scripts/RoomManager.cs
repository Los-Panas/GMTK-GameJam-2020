using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RoomManager : MonoBehaviour
{
    int actualChild = 1;
    Transform[] children;
    public int points;
    Transform playerPos;

    public int maxPoints = 150;
    public int spherePoints = 50;
    public int maxPointsIncrease = 250;

    public int TotalPoints;

    public bool pointIncrement;

    public PlayerController playerController;
    private Teleport teleport;

    // Start is called before the first frame update
    void Start()
    {
        children = GetComponentsInChildren<Transform>(true);
        points = 0;
        GameObject Player = GameObject.Find("Player");
        playerPos = Player.GetComponent<Transform>();
        GameObject Teleport = GameObject.Find("Teleport");
        teleport = Teleport.GetComponent<Teleport>();
        TotalPoints = 0;
        pointIncrement = true;
    }

    // Update is called once per frame
    void Update()
    {
        var clones2 = GameObject.FindGameObjectsWithTag("Bullet");
        if (clones2.Length > 100)
        {
            Destroy(clones2[0]);
        }
            
        if (points >= maxPoints && children[actualChild] != null && teleport.inTeleport == true) //if points are 
        {
            children[actualChild].gameObject.SetActive(false);
            children[actualChild + 1].gameObject.SetActive(false);
            children[actualChild + 1].GetComponent<RandomPointOnMesh>().callOnce = false;

            if (actualChild + 3 <= children.Length)
            {
                FindObjectOfType<AudioManager>().Play("Nextlevel");
                children[actualChild + 2].gameObject.SetActive(true);
                children[actualChild + 3].gameObject.SetActive(true);
                children[actualChild + 3].GetComponent<RandomPointOnMesh>().callOnce = false;
                actualChild = actualChild + 2;
            }
            else
            {
                actualChild = 1;
                children[actualChild].gameObject.SetActive(true);
                children[actualChild + 1].gameObject.SetActive(true);
                children[actualChild + 1].GetComponent<RandomPointOnMesh>().callOnce = false;
            }

            
            var clones = GameObject.FindGameObjectsWithTag("Bullet");
            foreach (var clone in clones)
            {
                Destroy(clone);
            }

            var clones1 = GameObject.FindGameObjectsWithTag("PickUp");
            foreach (var clone in clones1)
            {
                Destroy(clone);
            }

            maxPoints = maxPoints + maxPointsIncrease;

            playerPos.position = new Vector3(0, 1.5f, 0);

            TotalPoints = TotalPoints + points;

            points = 0;

            playerController.HealthToMax();
        }

        GameObject.Find("ScoreText").GetComponent<Text>().text = "Coins: " + points.ToString();

        if (pointIncrement)
        {
            StartCoroutine(PointIncrementOverTime());
        }
        else
        {
            StopCoroutine(PointIncrementOverTime());
        }


        GameObject.Find("BarrierScoreText").GetComponent<Text>().text = "Coins for next teleporter: " + maxPoints.ToString();
        GameObject.Find("TotalScore").GetComponent<Text>().text = "Total coins: " + TotalPoints.ToString();
    }

    public void AddPoints()
    {
        points = points + spherePoints;
    }

    public IEnumerator PointIncrementOverTime()
    {
        points++;
        yield return new WaitForEndOfFrame();
    }
}
