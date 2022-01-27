using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class characterController : MonoBehaviour
{
    [SerializeField] private float xinput;
    [SerializeField] private float spaceInput;
    
    [Header("References")]
    public Rigidbody2D rb;
    public gameManager manager; 
    public powerController power;   
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
    public LayerMask coinLayer;
    public LayerMask enemyLayer;
    public LayerMask finishLayer;
    
    [Header("Movement Variables")]
    public float moveSpeed;
    public float jumpPower;
    public float checkGroundRadius;
    public float currentSpeed;
    public float sprintScale;
    
    [Header("Movement Constraints")]
    public bool canMove;
    public bool isFlipped;
    public bool isGrounded;
    public bool isDead;
    public bool facingRight = true;
    public bool movingRight;
    public bool travelRight;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        manager = GameObject.Find("GameManager").GetComponent<gameManager>();
        power = GetComponent<powerController>();
        groundChecker = transform.Find("ground_checker");
        firePoint = transform.Find("firePoint");
        headPoint = transform.Find("headPoint");
        movingRight = false;
        sprintScale = 1;
        canMove = true;
        travelRight = false;
        GetComponent<BoxCollider2D>().isTrigger = false;
    }
    void Update()
    {
        xinput = Input.GetAxis("Horizontal");
        isOnGround();          
        move();   
        fireRay();        
        playerFlip();
        currentSpeed = rb.velocity.x;
        if(travelRight)
        {
            rb.velocity = new Vector2(1 * moveSpeed/3f, rb.velocity.y);
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
    public void move()
    {
        if(canMove)
        {
            rb.velocity = new Vector2((moveSpeed * sprintScale) * xinput, rb.velocity.y);
            if(Input.GetKeyDown(KeyCode.LeftShift) && power.fireFlowerEquipped == false || Input.GetKeyDown(KeyCode.RightShift) && power.fireFlowerEquipped == false || Input.GetKeyDown(KeyCode.Z) && power.fireFlowerEquipped == false)
            {
                sprintScale = 1.5f;
            }
            else if(Input.GetKeyUp(KeyCode.LeftShift)|| Input.GetKeyUp(KeyCode.RightShift) || Input.GetKeyUp(KeyCode.Z))
            {
                sprintScale = 1;              
            }

            if(Input.GetKeyDown(KeyCode.Space) && isGrounded || Input.GetKeyDown(KeyCode.X) && isGrounded)
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
    public void fireRay()
    {
        Collider2D collider = Physics2D.OverlapCircle(headPoint.position, checkGroundRadius, groundLayer);
        if (collider != null)
        {
            rb.AddForce(-transform.up * (jumpPower/5), ForceMode2D.Impulse);
        }

        Collider2D itemCollider = Physics2D.OverlapCircle(headPoint.position, checkGroundRadius, itemBlockLayer);
        if (itemCollider != null )
        {
            itemBlock block = itemCollider.gameObject.GetComponent<itemBlock>();
            block.spawnItem();
        }

        Collider2D coinCollider = Physics2D.OverlapCircle(headPoint.position, checkGroundRadius, coinLayer);
        if (coinCollider != null)
        {
            coinBlock block = coinCollider.gameObject.GetComponent<coinBlock>();
            Debug.Log("" + block.name);
            block.spawnItem();
        }
        
        Collider2D brickCollider = Physics2D.OverlapCircle(headPoint.position, checkGroundRadius, brickLayer);
        if (brickCollider != null)
        {
            if(power.bigMushroomEquipped)
            {
                rb.AddForce(-transform.up * (jumpPower/5), ForceMode2D.Impulse);
                Destroy(brickCollider.gameObject);
            }
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
    void OnTriggerEnter2D(Collider2D hit)
    {
        if(hit.CompareTag("Finish"))
        {
            SceneManager.LoadScene("gameWin");
        }
        if(hit.CompareTag("Death Hazard"))
        {
            die();
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