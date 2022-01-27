using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fireFlower : MonoBehaviour
{
    public Rigidbody2D rb;
    
    [Header("Layer Masks")]
    public LayerMask obstacleLayer;
    public LayerMask boundsLayer;
    public LayerMask playerLayer;
    public LayerMask groundLayer;
    
    [Header("Transforms")]
    public Transform leftFire;
    public Transform rightFire;
    public Transform upFire;
    public Transform downFire;
    
    [Header("References")]
    public characterController player;
    public powerController powerupControl;
    public gameManager manager;
    
    [Header("Variables")]
    public bool goingRight;
    public bool isGrounded;
    public float speed;
    public float checkGroundRadius;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.Find("Player").GetComponent<characterController>();
        powerupControl = GameObject.Find("Player").GetComponent<powerController>();
        manager = GameObject.Find("GameManager").GetComponent<gameManager>();
        leftFire = transform.Find("fireLeft");
        rightFire = transform.Find("fireRight");
        upFire = transform.Find("fireUp");
        downFire = transform.Find("fireDown");
    }
    void Update()
    {
        isOnGround();        
        fireRay();
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
            powerupControl.fireFlowerEquipped = true;
            manager.addScore(1000);
            Destroy(this.gameObject);
        }
        else if(playerRight.collider != null)
        {
            powerupControl.fireFlowerEquipped = true;
            manager.addScore(1000);
            Destroy(this.gameObject);
        }
        if(playerUp.collider != null)
        {
            powerupControl.fireFlowerEquipped = true;
            manager.addScore(1000);
            Destroy(this.gameObject);
        }
        else if(playerDown.collider != null)
        {
            powerupControl.fireFlowerEquipped = true;
            manager.addScore(1000);
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
