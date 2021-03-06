﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public GameObject pauseMenuUI;
    private PlayerController ablefire;
    //public Image cursor;

    void Start()
    {
        GameObject Player = GameObject.Find("Player");
        ablefire = Player.GetComponent<PlayerController>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P) || Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused)
            {
                Continue();
            }
         else {
            Pause();
         }
        }
        


    }

    void Awake()
    {
        //Cursor.visible = false;
    }
    public void Continue ()
    {
        ablefire.ableToFire = true;
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
        //Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        GameObject.Find("Walls").GetComponent<RoomManager>().pointIncrement = true;
    }

    public void Pause()
    {
        ablefire.ableToFire = false;
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
        //Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        GameObject.Find("Walls").GetComponent<RoomManager>().pointIncrement = false;
    }

    public void QuitGame()
    {
        Debug.Log("Quit!");
        Application.Quit();
    }
}
