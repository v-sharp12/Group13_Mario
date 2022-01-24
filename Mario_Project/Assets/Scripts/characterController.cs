using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class characterController : MonoBehaviour
{
    [SerializeField] private float xinput;
    [SerializeField] private float spaceInput;
    [Header("References")]
    public Rigidbody2D rb;
    public GameObject fireballProjectile;
    public gameManager manager;    
    public Transform groundChecker;
    public Transform firePoint;
    public Transform headPoint;
    [Header("Audio")]
    public AudioClip jumpSound;
    public AudioClip coinSound;
    [Header("Layer Masks")]
    public LayerMask groundLayer;
    public LayerMask brickLayer;
    public LayerMask itemBlockLayer;
    public LayerMask enemyLayer;
    public LayerMask finishLayer;
    
    [Header("Movement Variables")]
    public float moveSpeed;
    public float jumpPower;
    public float checkGroundRadius;
    public float currentSpeed;
    
    [Header("Movement Constraints")]
    public bool canMove;
    public bool isFlipped;
    public bool isGrounded;
    public bool isDead;
    public bool facingRight = true;
    public bool movingRight;
    public bool fireFlowerEquipped;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        manager = GameObject.Find("GameManager").GetComponent<gameManager>();
        groundChecker = transform.Find("ground_checker");
        firePoint = transform.Find("firePoint");
        headPoint = transform.Find("headPoint");
        fireFlowerEquipped = false;
        movingRight = false;
        canMove = true;
        GetComponent<BoxCollider2D>().isTrigger = false;
    }
    void Update()
    {
        xinput = Input.GetAxis("Horizontal");
        move();
        shootFireball();
        isOnGround();
        playerFlip();
        fireRay();
        currentSpeed = rb.velocity.x;
    }
    public void move()
    {
        if(canMove)
        {
            rb.velocity = new Vector2(moveSpeed * xinput, rb.velocity.y);
            if(Input.GetKeyDown(KeyCode.Space) && isGrounded)
            {
                rb.AddForce(transform.up * jumpPower, ForceMode2D.Impulse);
                AudioSource.PlayClipAtPoint(jumpSound, transform.position, .2f);
            }
            if(xinput > .01f && rb.velocity.x > 0.1f && facingRight)
            {
                movingRight = true;
            }
            else if (facingRight != true || rb.velocity.x <= 0.1f)
            {
                movingRight = false;
            }            
        }
    }
    public void shootFireball()
    {
        if(Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.RightShift) /*&& fireFlowerEquipped*/)
        {
            GameObject projectile = Instantiate(fireballProjectile, firePoint.position, Quaternion.identity);
            fireball fireball = projectile.GetComponent<fireball>();
            fireball.player = GetComponent<characterController>();
            if(!facingRight)
            {
                projectile.transform.Rotate(0f, 180f, 0f);
            }
            Rigidbody2D fireballRb = projectile.GetComponent<Rigidbody2D>();
            fireballRb.AddForce(transform.right * 5, ForceMode2D.Impulse);
        }
    }
    public void fireRay()
    {
        Collider2D collider = Physics2D.OverlapCircle(headPoint.position, checkGroundRadius, groundLayer);
        if (collider != null)
        {
            rb.AddForce(-transform.up * (jumpPower/4), ForceMode2D.Impulse);
        }

        Collider2D itemCollider = Physics2D.OverlapCircle(headPoint.position, checkGroundRadius, itemBlockLayer);
        if (itemCollider != null)
        {
            itemBlock block = itemCollider.gameObject.GetComponent<itemBlock>();
            block.spawnItem();
        }
        
        RaycastHit2D rayDown = Physics2D.BoxCast(groundChecker.position, new Vector2(.2f,.2f), 0f, -transform.up, .25f, enemyLayer);
        if(rayDown.collider != null && !isDead)
        {
            Destroy(rayDown.collider.gameObject);
            AudioSource.PlayClipAtPoint(coinSound, transform.position, .75f);
            manager.addScore(100);
            rb.AddForce(transform.up * (jumpPower * 1.25f), ForceMode2D.Impulse);
        }
    }
    void isOnGround()
    {
        Collider2D collider = Physics2D.OverlapCircle(groundChecker.position, checkGroundRadius, groundLayer);
        Collider2D colliderBrick = Physics2D.OverlapCircle(groundChecker.position, checkGroundRadius, brickLayer);
        Collider2D colliderItemBlock = Physics2D.OverlapCircle(groundChecker.position, checkGroundRadius, itemBlockLayer);
        if (collider != null || colliderBrick != null || colliderItemBlock != null)
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }
    }
    public void playerFlip() 
    {
        if (rb.velocity.x > 0 && !facingRight)
        {
            facingRight = !facingRight;
            transform.Rotate(0f, 180f, 0f);
        }
        else if (rb.velocity.x < 0 && facingRight)
        {
            facingRight = !facingRight;
            transform.Rotate(0f, 180f, 0f);
        }
    }
    public void die()
    {
        canMove = false;
        rb.velocity = new Vector2(0,0);
        GetComponent<BoxCollider2D>().isTrigger = true;
        rb.AddForce(transform.up * jumpPower/2f, ForceMode2D.Impulse);
        manager.loseLife(1);
        if(gameManager.lives>0)
        {
            manager.StartCoroutine("resetLevel");
        }
        else if (gameManager.lives<=0)
        {
            manager.StartCoroutine("gameOver");
        }

    }
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(groundChecker.position, checkGroundRadius);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(headPoint.position, checkGroundRadius);
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(firePoint.position, new Vector3(.25f,.25f,.25f));
        Gizmos.color = Color.red;
        //Gizmos.DrawWireCube(groundChecker.position, new Vector3(.25f,.25f,.25f));
    }
}
