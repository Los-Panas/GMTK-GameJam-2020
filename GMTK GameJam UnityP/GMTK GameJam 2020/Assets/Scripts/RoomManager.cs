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

    public PlayerController playerController;
    
    // Start is called before the first frame update
    void Start()
    {
        children = GetComponentsInChildren<Transform>(true);
        points = 0;
        GameObject Player = GameObject.Find("Player");
        playerPos = Player.GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (points >= maxPoints && children[actualChild] != null) //if points are 
        {
            children[actualChild].gameObject.SetActive(false);
            children[actualChild + 1].gameObject.SetActive(false);
            if(actualChild + 3 <= children.Length)
            {
                children[actualChild + 2].gameObject.SetActive(true);
                children[actualChild + 3].gameObject.SetActive(true);
            }
            else
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }

            actualChild = actualChild + 2;

            //TODO: borrar balas en la escena
            var clones = GameObject.FindGameObjectsWithTag("Bullet");
            foreach (var clone in clones)
            {
                Destroy(clone);
            }

            playerPos.position = new Vector3(0, 1.5f, 0);

            points = 0;

            playerController.HealthToMax();
        }

        GameObject.Find("ScoreText").GetComponent<Text>().text = points.ToString();
        StartCoroutine(PointIncrementOverTime());
    }

    public void AddPoints()
    {
        points = points + 50;
    }

    IEnumerator PointIncrementOverTime()
    {
        points++;
        yield return new WaitForEndOfFrame();
    }
}
