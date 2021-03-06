using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class goomba : MonoBehaviour
{
    [Header("References")]
    public Rigidbody2D rb;
    public LayerMask obstacleLayer;
    public LayerMask boundsLayer;
    public LayerMask playerLayer;
    public Transform leftFire;
    public Transform rightFire;
    public characterController player;
    public GameObject playerGameObject;
    public powerController powerupControl;
    public koopa koo;

    [Header("Variables")]
    public bool isKoopa;

    public bool goingRight;
    public bool startMoving;
    public bool dead;
    public bool isGrounded;
    public float speed;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.Find("Player").GetComponent<characterController>();
        playerGameObject = GameObject.Find("Player");
        powerupControl = GameObject.Find("Player").GetComponent<powerController>();
        leftFire = transform.Find("fireLeft");
        rightFire = transform.Find("fireRight");
        koo = GetComponent<koopa>();
        Physics.IgnoreLayerCollision(6, 6, true);
        startMoving = false;
    }
    void Update()
    {
        isOnGround();
        if(Vector3.Distance(transform.position, playerGameObject.transform.position) < 15)
        {
            startMoving = true;
        }
        fireRay();
    }
    void FixedUpdate()
    {
        if(startMoving)
        {
            if(goingRight)
            {
                rb.velocity = new Vector2(speed, rb.velocity.y);
            }
                else if(!goingRight)
            {
                rb.velocity = new Vector2(-speed, rb.velocity.y);
            }            
        }
        if(isGrounded)
        rb.AddForce(transform.up, ForceMode2D.Force);
        else if(!isGrounded)
        rb.AddForce(-transform.up * 3f, ForceMode2D.Force);
    }
    public void fireRay()
    {
        RaycastHit2D boundsCheck = Physics2D.Raycast(leftFire.position, -transform.right, .25f, boundsLayer);
        if(boundsCheck.collider != null)
        {
            Destroy(this.gameObject);
        }
        RaycastHit2D rayLeft = Physics2D.Raycast(leftFire.position, -transform.right, .25f, obstacleLayer);
        RaycastHit2D rayRight = Physics2D.Raycast(rightFire.position, transform.right, .25f, obstacleLayer);                
        if(rayLeft.collider != null)
        {
            goingRight = true;
        }
        else if(rayRight.collider != null)
        {
            goingRight = false;
        }
        RaycastHit2D playerLeft = Physics2D.BoxCast(leftFire.position, new Vector2(.1f, .5f), 0f, -transform.right, .25f, playerLayer);
        RaycastHit2D playerRight = Physics2D.BoxCast(rightFire.position, new Vector2(.1f, .5f), 0f, transform.right, .25f, playerLayer);
        RaycastHit2D playerDown = Physics2D.BoxCast(transform.position, new Vector2(.5f, .1f), 0f, -transform.up, .35f, playerLayer);

        if(playerLeft.collider != null && powerupControl.starManEquipped == false && powerupControl.fireFlowerEquipped == false && powerupControl.bigMushroomEquipped == false)
        {
            if(player.isDead == false && player.isImmune == false && !dead)
            {
                player.die();                   
                player.isDead = true;               
            }
        }
        else if(playerLeft.collider != null && powerupControl.starManEquipped)
        {
            if(player.isDead == false)
            {
                player.manager.addScore(100);                   
                StartCoroutine("die");
                AudioSource.PlayClipAtPoint(player.coinSound, transform.position, .75f);          
            }
        }
        else if(playerLeft.collider != null && powerupControl.fireFlowerEquipped || playerLeft.collider != null && powerupControl.bigMushroomEquipped)
        {
            if(player.isDead == false && powerupControl.fireFlowerEquipped && powerupControl.bigMushroomEquipped)
            {
                powerupControl.StartCoroutine("loseShroomFast");
                player.StartCoroutine("losePower");
            }
            else if(player.isDead == false && powerupControl.fireFlowerEquipped)
            {
                powerupControl.fireFlowerEquipped = false;
                player.StartCoroutine("losePower");
            }
            else if(player.isDead == false && powerupControl.bigMushroomEquipped)
            {
                powerupControl.bigMushroomEquipped = false;
                powerupControl.StartCoroutine("loseShroomFast");
                player.StartCoroutine("losePower");
            }
        }
        else if(playerRight.collider != null && powerupControl.starManEquipped == false && powerupControl.fireFlowerEquipped == false && powerupControl.bigMushroomEquipped == false)
        {
            if(player.isDead == false && player.isImmune == false && !dead)
            {
                player.die();                   
                player.isDead = true;
            }
        }
        else if(playerRight.collider != null && powerupControl.starManEquipped)
        {
            if(player.isDead == false)
            {
                player.manager.addScore(100);                   
                StartCoroutine("die");
                AudioSource.PlayClipAtPoint(player.coinSound, transform.position, .75f);            
            }
        }
        else if(playerRight.collider != null && powerupControl.fireFlowerEquipped || playerRight.collider != null && powerupControl.bigMushroomEquipped)
        {
            if(player.isDead == false && powerupControl.fireFlowerEquipped && powerupControl.bigMushroomEquipped)
            {
                powerupControl.StartCoroutine("loseShroomFast");
                player.StartCoroutine("losePower");
            }
            else if(player.isDead == false && powerupControl.fireFlowerEquipped)
            {
                powerupControl.fireFlowerEquipped = false;
                player.StartCoroutine("losePower");
            }
            else if(player.isDead == false && powerupControl.bigMushroomEquipped)
            {
                powerupControl.bigMushroomEquipped = false;
                powerupControl.StartCoroutine("loseShroomFast");
                player.StartCoroutine("losePower");
            }
        }
        
        if(!dead)
        {
            if(playerDown.collider != null && powerupControl.starManEquipped == false)
            {
                if(player.isDead == false && player.isImmune == false && !dead)
                {
                    player.die();                   
                    player.isDead = true;
                }
            }
            else if(playerDown.collider != null && powerupControl.starManEquipped)
            {
                if(player.isDead == false)
                {
                    player.manager.addScore(100);                   
                    StartCoroutine("die");
                    AudioSource.PlayClipAtPoint(player.coinSound, transform.position, .75f);
                }
            }            
        }
    }
    public IEnumerator die()
    {
        if(isKoopa)
        {
            koo.dropShell();
        }         
        dead = true;
        rb.velocity = new Vector2(0,0);
        GetComponent<BoxCollider2D>().enabled = false;
        rb.velocity = new Vector2(0, 5f);
        this.gameObject.SetActive(false);
        yield return new WaitForSeconds(0f);        
    }
    public IEnumerator hitWithPower()
    {
        yield return new WaitForSeconds(0f);  
    }
    void isOnGround()
    {
        Collider2D collider = Physics2D.OverlapCircle(transform.position, .3f, obstacleLayer);
        if (collider != null)
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }
    }
    void OnCollisionEnter2D(Collision2D col)
    {

    }
}
