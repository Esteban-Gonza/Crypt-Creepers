using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawn : MonoBehaviour {

    [SerializeField] int itemsDelay = 10;
    [SerializeField] GameObject[] items;

    void Start(){

        StartCoroutine(SpawnCkeckPointRoutine());
    }

    IEnumerator SpawnCkeckPointRoutine(){

        while (true){

            yield return new WaitForSeconds(itemsDelay);
            int n = Random.Range(0, items.Length);
            Instantiate(items[n], this.transform.position, this.transform.rotation);
        }
    }
}