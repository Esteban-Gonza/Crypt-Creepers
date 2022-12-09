using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water : MonoBehaviour{

    [SerializeField] float speedReduction = 0.5f;
    float originalSpeed;

    AudioSource waterSound;
    PlayerController player;

    void Start(){
        
        player = FindObjectOfType<PlayerController>();
        waterSound = GetComponent<AudioSource>();
        originalSpeed = player.speed;
    }

    void OnTriggerEnter2D(Collider2D collision){

        if (collision.CompareTag("Player")){

            player.speed *= speedReduction;
            waterSound.Play();
        }
    }

    void OnTriggerExit2D(Collider2D collision){
    
        player.speed = originalSpeed;
        waterSound.Stop();
    }
}