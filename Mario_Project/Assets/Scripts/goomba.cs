using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class goomba : MonoBehaviour
{
    public Rigidbody2D rb;
    public LayerMask obstacleLayer;
    public LayerMask boundsLayer;
    public LayerMask playerLayer;
    public Transform leftFire;
    public Transform rightFire;
    public characterController player;
    public bool goingRight;
    public float speed;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.Find("Player").GetComponent<characterController>();
        leftFire = transform.Find("fireLeft");
        rightFire = transform.Find("fireRight");
        Physics.IgnoreLayerCollision(6, 6, true);
    }
    void Update()
    {
        if(goingRight)
        {
            rb.velocity = new Vector2(speed, rb.velocity.y);
        }
        else if(!goingRight)
        {
            rb.velocity = new Vector2(-speed, rb.velocity.y);
        }
        fireRay();
    }
    public void fireRay()
    {
        RaycastHit2D boundsCheck = Physics2D.Raycast(leftFire.position, -transform.right, .5f, boundsLayer);
        
        RaycastHit2D rayLeft = Physics2D.Raycast(leftFire.position, -transform.right, .5f, obstacleLayer);
        RaycastHit2D rayRight = Physics2D.Raycast(rightFire.position, transform.right, .5f, obstacleLayer);
        
        RaycastHit2D playerLeft = Physics2D.Raycast(leftFire.position, -transform.right, .5f, playerLayer);
        RaycastHit2D playerRight = Physics2D.Raycast(rightFire.position, transform.right, .5f, playerLayer);
        if(boundsCheck.collider != null)
        {
            Destroy(this.gameObject);
        }        
        if(rayLeft.collider != null)
        {
            goingRight = true;
        }
        else if(rayRight.collider != null)
        {
            goingRight = false;
        }
        if(playerLeft.collider != null)
        {
            if(player.isDead == false)
            {
                player.isDead = true;
                player.die();                
            }
        }
        else if(playerRight.collider != null)
        {
            if(player.isDead == false)
            {
                player.isDead = true;
                player.die();                
            }
        }
    }
    void OnCollisionEnter2D(Collision2D col)
    {

    }
}
