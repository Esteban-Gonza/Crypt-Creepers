using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour{

    [SerializeField] int healt = 5;
    [SerializeField] int scorePoints = 100;
    [SerializeField] float speed = 3;
    [SerializeField] AudioClip impactSound;

    Transform playerPosition;
    AudioSource enemyAudioSource;

    void Start(){
        
        playerPosition = FindObjectOfType<PlayerController>().transform;
        enemyAudioSource = GetComponent<AudioSource>();
    }

    void Update(){
       
        Vector2 direction = playerPosition.position - transform.position;
        transform.position += (Vector3)direction/direction.magnitude * Time.deltaTime * speed;
    }

    public void TakeDamage(){

        healt--;

        if(healt == 0){

            GameManager.instance.Score += scorePoints;
            Destroy(gameObject, 0.1f);
            enemyAudioSource.Play();
        }
    }

    void OnTriggerEnter2D(Collider2D collision) {

        if (collision.CompareTag("Player")){

            collision.GetComponent<PlayerController>().TakeDamage();
            enemyAudioSource.PlayOneShot(impactSound);
        }
    }
}