using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour{

    [SerializeField] int damage = 3;
    [SerializeField] float speed = 5;

    public Renderer rend;

    public bool powerShoot;

    void Start(){
        
        Destroy(gameObject, 5);
        rend = GetComponent<Renderer>();
    }

    void Update() {

        transform.position += transform.right * Time.deltaTime * speed;
    }

    void OnTriggerEnter2D(Collider2D collision){

        if (collision.CompareTag("Enemy")){

            collision.GetComponent<Enemy>().TakeDamage();

            if(!powerShoot)
                Destroy(gameObject);
            
            if(damage == 0){
                Destroy(gameObject);
            }
        }
    }
}