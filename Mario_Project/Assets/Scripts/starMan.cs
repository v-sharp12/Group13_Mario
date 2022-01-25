using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class starMan : MonoBehaviour
{
    public Rigidbody2D rb;
    public LayerMask obstacleLayer;
    public LayerMask boundsLayer;
    public LayerMask playerLayer;
    public LayerMask groundLayer;
    public Transform leftFire;
    public Transform rightFire;
    public Transform upFire;
    public Transform downFire;
    public characterController player;
    public powerController powerupControl;
    public bool goingRight;
    public bool isGrounded;
    public float speed;
    public float checkGroundRadius;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.Find("Player").GetComponent<characterController>();
        powerupControl = GameObject.Find("Player").GetComponent<powerController>();
        leftFire = transform.Find("fireLeft");
        rightFire = transform.Find("fireRight");
        upFire = transform.Find("fireUp");
        downFire = transform.Find("fireDown");
    }
    void Update()
    {
        isOnGround();        
        fireRay();        
        if(goingRight)
        {
            rb.velocity = new Vector2(speed, rb.velocity.y);
        }
        else if(!goingRight)
        {
            rb.velocity = new Vector2(-speed, rb.velocity.y);
        }
        if(!isGrounded)
        {
            rb.AddForce(-transform.up * 5, ForceMode2D.Force);
        }
        else if(isGrounded)
        {
            
        }
    }
    public void fireRay()
    {
        RaycastHit2D boundsCheck = Physics2D.Raycast(leftFire.position, -transform.right, .5f, boundsLayer);
        
        RaycastHit2D rayLeft = Physics2D.Raycast(leftFire.position, -transform.right, .5f, obstacleLayer);
        RaycastHit2D rayRight = Physics2D.Raycast(rightFire.position, transform.right, .5f, obstacleLayer);
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
        RaycastHit2D playerLeft = Physics2D.Raycast(leftFire.position, -transform.right, .5f, playerLayer);
        RaycastHit2D playerRight = Physics2D.Raycast(rightFire.position, transform.right, .5f, playerLayer);
        RaycastHit2D playerUp = Physics2D.Raycast(upFire.position, transform.up, .5f, playerLayer);
        RaycastHit2D playerDown = Physics2D.Raycast(downFire.position, -transform.up, .5f, playerLayer);
        if(playerLeft.collider != null)
        {
            powerupControl.starManEquipped = true;
            Destroy(this.gameObject);
        }
        else if(playerRight.collider != null)
        {
            powerupControl.starManEquipped = true;
            Destroy(this.gameObject);
        }
        if(playerUp.collider != null)
        {
            powerupControl.starManEquipped = true;
            Destroy(this.gameObject);
        }
        else if(playerDown.collider != null)
        {
            powerupControl.starManEquipped = true;
            Destroy(this.gameObject);
        }
    }
    void isOnGround()
    {
        Collider2D collider = Physics2D.OverlapCircle(downFire.position, checkGroundRadius, groundLayer);
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
