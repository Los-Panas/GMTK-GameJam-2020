using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public GameObject pauseMenuUI;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
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

   /* void Awake()
    {
        //Continue();
        //pauseMenuUI.SetActive(true);
        Time.timeScale = 1f;
        //GameIsPaused = false;
    }*/
    public void Continue ()
    {
        //GetComponent<PlayerController>().ableToFire = true;
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
        //Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void Pause()
    {
        //GetComponent<PlayerController>().ableToFire = false;
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
        //Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}
