using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour{
    
    public static PauseMenu instance;
    public GameObject pauseMenu;
    public GameObject pauseButton;

    void Awake(){

        if (instance == null){
        
            instance = this;
        }
    }

    public void PauseGame(){

        Time.timeScale = 0f;
        pauseButton.SetActive(false);
        pauseMenu.SetActive(true);
    }

    public void ResumeGame(){

        Time.timeScale = 1f;
        pauseButton.SetActive(true);
        pauseMenu.SetActive(false);
    }

    public void QuitGame(){

        Application.Quit();
    }
}