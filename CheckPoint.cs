using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour{

    [SerializeField] int addedTime = 10;

    AudioSource checkSound;

    void Start(){
        
        checkSound = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D collision){

        if (collision.CompareTag("Player")){

            GameManager.instance.time += addedTime;
            checkSound.Play();
            Destroy(gameObject, 0.1f);
        }
    }
}