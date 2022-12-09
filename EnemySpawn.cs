using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour{

    [SerializeField] GameObject[] enemies;
    
    float spawnRate = 1f;

    void Start(){

        StartCoroutine(SpawnNewEnemy());
    }

    void Update(){

        if (GameManager.instance.time <= 60){

            spawnRate = 5f;
        }

        if (GameManager.instance.time > 60 && GameManager.instance.time <= 100){

            spawnRate = 3f;
        }

        if (GameManager.instance.time > 100){

            spawnRate = 1f;
        }
    }

    IEnumerator SpawnNewEnemy(){

        while (true){

            yield return new WaitForSeconds(spawnRate);
            int n = Random.Range(0, enemies.Length);
            Instantiate(enemies[n], transform.position, transform.rotation);
        }
    }
}