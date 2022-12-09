using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour{

    [SerializeField] int healt = 10;
    [SerializeField] float fireRate;
    [SerializeField] float blinkRate;
    [SerializeField] float invulnerableTime;
    [SerializeField] bool invulnerable;
    [SerializeField] Camera followCamera;
    [SerializeField] Transform aim;
    [SerializeField] Transform bullet;
    [SerializeField] AudioClip itemSound;
    [SerializeField] AudioClip bulletSound;

    float h, v;
    bool gunLoaded = true;
    bool powerShootEnable;

    public float speed = 5;

    Vector3 movementDirection;
    Vector2 facingDirection;

    Animator playerAnimator;
    AudioSource playerAudioSource;
    SpriteRenderer playerSpriteRenderer;
    

    public int Healt{

        get => healt;

        set{

            healt = value;
            UIManager.instance.UpdateUIHealt(healt);
        }
    }

    void Start(){
        
        playerAnimator = GetComponent<Animator>();
        playerSpriteRenderer = GetComponent<SpriteRenderer>();
        playerAudioSource = GetComponent<AudioSource>();
        UIManager.instance.UpdateUIHealt(healt);
    }

    void Update(){
        
        //Player movement
        ReadInput();
        this.transform.position += movementDirection * Time.deltaTime * speed;

        //Aim movement
        facingDirection = followCamera.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        aim.position = this.transform.position + (Vector3)facingDirection.normalized;

        //Animations
        UpdateAnimations();

        //Shoot
        if(Input.GetMouseButtonDown(0) && gunLoaded){

            Shoot();
        }
    }

    void ReadInput(){

        h = Input.GetAxis("Horizontal");
        v = Input.GetAxis("Vertical");
        movementDirection.x = h;
        movementDirection.y = v;
    }

    void UpdateAnimations(){

        playerAnimator.SetFloat("Speed", movementDirection.magnitude);

        if(aim.position.x > this.transform.position.x){

            playerSpriteRenderer.flipX = true;
        }
        else if(aim.position.x < this.transform.position.x){

            playerSpriteRenderer.flipX = false;
        }
    }

    void Shoot(){

        gunLoaded = false;

        float angle = Mathf.Atan2(facingDirection.y, facingDirection.x) * Mathf.Rad2Deg;
        Quaternion targetRotation = Quaternion.AngleAxis(angle, Vector3.forward);
        Transform bulletClone = Instantiate(bullet, this.transform.position, targetRotation);
        playerAudioSource.PlayOneShot(bulletSound);

        if (powerShootEnable){

            bulletClone.GetComponent<Bullet>().powerShoot = true;
        }

        StartCoroutine(ReloadGun());
    }

    public void TakeDamage(){

        if (invulnerable)
            return;

        Healt -= 3;
        fireRate = 3;
        powerShootEnable = false;
        invulnerable = true;
        CameraController.instance.Shake(5, 5, 0.5f);
        StartCoroutine(MakeVulnerableAgain());

        if(Healt == 0){

            GameManager.instance.gameOver = true;
            UIManager.instance.ShowGameOverScreen();
            playerAudioSource.Play();
        }
    }

    void OnTriggerEnter2D(Collider2D collision){

        if (collision.CompareTag("PowerUp")){

            switch (collision.GetComponent<PowerUp>().powerUpType){

                case PowerUp.PowerUpType.fireRateIncrease:
                    fireRate /= 2;
                    playerAudioSource.PlayOneShot(itemSound);
                    break;

                case PowerUp.PowerUpType.powerShoot:
                    powerShootEnable = true;
                    playerAudioSource.PlayOneShot(itemSound);
                    break;

                default:
                    break;
            }
            Destroy(collision.gameObject, 0.1f);
        }
    }

    IEnumerator ReloadGun(){

        yield return new WaitForSeconds(fireRate);
        gunLoaded = true;
    }

    IEnumerator MakeVulnerableAgain(){

        StartCoroutine(BlinkTime());
        yield return new WaitForSeconds(invulnerableTime);
        invulnerable = false;
    }

    IEnumerator BlinkTime(){

        int t = 10;
        while (t >= 0){

            playerSpriteRenderer.enabled = false;
            yield return new WaitForSeconds(t * blinkRate);
            playerSpriteRenderer.enabled = true;
            yield return new WaitForSeconds(t * blinkRate);
            t--;
        }
    }
}