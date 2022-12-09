using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

    public static UIManager instance;
    [SerializeField] Text healtText;
    [SerializeField] Text scoreText;
    [SerializeField] Text timeText;
    [SerializeField] Text finalScoreText;
    [SerializeField] GameObject gameOverScreen;

    AudioSource gameOverAudio;

    void Awake(){

        gameOverAudio = GetComponent<AudioSource>();

        if (instance == null){
        
            instance = this;
        }
    }

    public void UpdateUIScore(int newScore){

        scoreText.text = newScore.ToString();
    }

    public void UpdateUIHealt(int newHealt){

        healtText.text = newHealt.ToString();
    }

    public void UpdateUITime(int newTime){

        timeText.text = newTime.ToString();
    }

    public void ShowGameOverScreen(){

        gameOverScreen.SetActive(true);
        finalScoreText.text = "SCORE: " + GameManager.instance.Score;
        gameOverAudio.Play();
    }
}