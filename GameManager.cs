using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour{
    
    public static GameManager instance;

    [SerializeField] int score;
    [SerializeField] AudioSource PlayButtonAudio;

    public int time = 30;
    public int difficulty = 1;
    public bool gameOver;

    public int Score{

        get => score;
        
        set{

            score = value;
            UIManager.instance.UpdateUIScore(score);
            if(score % 1000 == 0){

                difficulty++;
            }
        }
    }

    void Awake(){

        if (instance == null){
        
            instance = this;
        }
    }

    void Start(){

        StartCoroutine(CountDown());
        UIManager.instance.UpdateUIScore(score);
    }

    IEnumerator CountDown(){

        while(time > 0){

            yield return new WaitForSeconds(1);
            time--;
            UIManager.instance.UpdateUITime(time);
        }

        gameOver = true;
        UIManager.instance.ShowGameOverScreen();
    }

    void Update(){

        if (Input.GetKey(KeyCode.P)){

            PauseMenu.instance.PauseGame();
        }
    }

    public void PlayAgain(){

        SceneManager.LoadScene("Game");
    }
}