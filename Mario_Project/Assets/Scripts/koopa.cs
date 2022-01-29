using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class koopa : MonoBehaviour
{
public Rigidbody2D rb;
    public LayerMask obstacleLayer;
    public LayerMask boundsLayer;
    public LayerMask playerLayer;
    public Transform leftFire;
    public Transform rightFire;
    public characterController player;
    public GameObject playerGameObject;
    public powerController powerupControl;
    public bool goingRight;
    public bool startMoving;
    public float speed;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.Find("Player").GetComponent<characterController>();
        playerGameObject = GameObject.Find("Player");
        powerupControl = GameObject.Find("Player").GetComponent<powerController>();
        leftFire = transform.Find("fireLeft");
        rightFire = transform.Find("fireRight");
        Physics.IgnoreLayerCollision(6, 6, true);
        startMoving = false;
    }
    void Update()
    {
        if(Vector3.Distance(transform.position, playerGameObject.transform.position) < 15)
        {
            startMoving = true;
        }
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
        fireRay();
    }
    public void fireRay()
    {
        RaycastHit2D boundsCheck = Physics2D.Raycast(leftFire.position, -transform.right, .5f, boundsLayer);
        if(boundsCheck.collider != null)
        {
            Destroy(this.gameObject);
        }
        RaycastHit2D rayLeft = Physics2D.Raycast(leftFire.position, -transform.right, .5f, obstacleLayer);
        RaycastHit2D rayRight = Physics2D.Raycast(rightFire.position, transform.right, .5f, obstacleLayer);                
        if(rayLeft.collider != null)
        {
            goingRight = true;
        }
        else if(rayRight.collider != null)
        {
            goingRight = false;
        }
        RaycastHit2D playerLeft = Physics2D.Raycast(leftFire.position, -transform.right, .5f, playerLayer);
        RaycastHit2D playerRight = Physics2D.Raycast(rightFire.position, transform.right, .5f, playerLayer);        
        if(playerLeft.collider != null && powerupControl.starManEquipped == false)
        {
            if(player.isDead == false)
            {
                player.die();                   
                player.isDead = true;               
            }
        }
        else if(playerLeft.collider != null && powerupControl.starManEquipped)
        {
            if(player.isDead == false)
            {
                Destroy(this.gameObject);
                AudioSource.PlayClipAtPoint(player.coinSound, transform.position, .75f);
                player.manager.addScore(100);            
            }
        }
        else if(playerRight.collider != null && powerupControl.starManEquipped == false)
        {
            if(player.isDead == false)
            {
                player.die();                   
                player.isDead = true;
            }
        }
        else if(playerRight.collider != null && powerupControl.starManEquipped)
        {
            if(player.isDead == false)
            {
                Destroy(this.gameObject);
                AudioSource.PlayClipAtPoint(player.coinSound, transform.position, .75f);
                player.manager.addScore(100);             
            }
        }
    }
    void OnCollisionEnter2D(Collision2D col)
    {

    }
}
