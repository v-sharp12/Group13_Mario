using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shell : MonoBehaviour
{
    gameManager manager;
    Rigidbody2D rb;
    Transform leftT;
    Transform rightT;
    Transform topT;
    [SerializeField]private LayerMask playerLayer;
    [SerializeField]private LayerMask enemyLayer;
    [SerializeField]private LayerMask obstacleLayer;
    public bool goRight;
    public bool goLeft;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        manager = GameObject.Find("GameManager").GetComponent<gameManager>();
        leftT = transform.Find("left");
        rightT = transform.Find("right");
        topT = transform.Find("top");
        goLeft = false;
        goRight = false;
    }
    void Update()
    {
        if(goLeft)
        {
            rb.velocity = new Vector2(-7.5f, rb.velocity.y);
        }
        else if(goRight)
        {
            rb.velocity = new Vector2(7.5f, rb.velocity.y);
        }
        
        RaycastHit2D left = Physics2D.Raycast(leftT.position, -transform.right, .3f, playerLayer);
        RaycastHit2D right = Physics2D.Raycast(rightT.position, transform.right, .3f, playerLayer);
        RaycastHit2D top = Physics2D.Raycast(topT.position, transform.up, .3f, playerLayer);
        if(left.collider != null)
        {
            goRight = true;
            characterController playerHit = left.collider.GetComponent<characterController>();
            playerHit.rb.velocity = new Vector2(playerHit.rb.velocity.x, playerHit.jumpPower/2f);
        }
        if(right.collider != null)
        {
            goLeft = true;
            characterController playerHit = right.collider.GetComponent<characterController>();
            playerHit.rb.velocity = new Vector2(playerHit.rb.velocity.x, playerHit.jumpPower/2f);
        }
        if(top.collider != null)
        {
            goLeft = true;
            characterController playerHit = top.collider.GetComponent<characterController>();
            playerHit.rb.velocity = new Vector2(playerHit.rb.velocity.x, playerHit.jumpPower/2f);
        }
        
        RaycastHit2D gooLeft = Physics2D.BoxCast(leftT.position, new Vector2(.2f,.75f), 0f, -transform.up, .35f, enemyLayer);
        RaycastHit2D gooRight = Physics2D.BoxCast(rightT.position, new Vector2(.2f,.75f), 0f, -transform.up, .35f, enemyLayer);
        if(gooLeft.collider != null)
        {
            goomba goo = gooLeft.collider.GetComponent<goomba>();
            if(goo.dead == false)
            {
                manager.addScore(100);
                Destroy(this.gameObject);
                //AudioSource.PlayClipAtPoint(coinSound, transform.position, .75f);                 
            }
            goo.StartCoroutine("die");           
        }
        else if(gooRight.collider != null)
        {
            goomba goo = gooRight.collider.GetComponent<goomba>();
            if(goo.dead == false)
            {
                manager.addScore(100);
                Destroy(this.gameObject);
                //AudioSource.PlayClipAtPoint(coinSound, transform.position, .75f);                 
            }
            goo.StartCoroutine("die");           
        }
        
        RaycastHit2D rayLeft = Physics2D.Raycast(leftT.position, -transform.right, .25f, obstacleLayer);
        RaycastHit2D rayRight = Physics2D.Raycast(rightT.position, transform.right, .25f, obstacleLayer);
        if(rayLeft.collider != null || rayLeft.collider != null)
        {
            Destroy(this.gameObject);
        }
    }
}
