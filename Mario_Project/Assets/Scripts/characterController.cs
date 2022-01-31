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
    public Animator anim;
    public SpriteRenderer sprite;
    public sideScrollLimiter limiter;
    public Transform checkpointTransform;
    public TrailRenderer trail;
    
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
    public LayerMask pipeLayer;
    
    [Header("Movement Variables")]
    public float moveSpeed;
    public float jumpPower;
    public float checkGroundRadius;
    public float currentSpeed;
    public float sprintScale;
    public Color full;
    public Color flikr;
    [Header("Movement Constraints")]
    public bool canMove;
    public bool isFlipped;
    public bool isGrounded;
    public bool isDead;
    public bool facingRight = true;
    public bool movingRight;
    public bool travelRight;
    public bool isImmune;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        manager = GameObject.Find("GameManager").GetComponent<gameManager>();
        power = GetComponent<powerController>();
        groundChecker = transform.Find("ground_checker");
        firePoint = transform.Find("firePoint");
        headPoint = transform.Find("headPoint");
        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        limiter = GameObject.FindGameObjectWithTag("Camera Bounds").GetComponent<sideScrollLimiter>();
        trail = GetComponent<TrailRenderer>();
        isDead = false;
        movingRight = false;
        sprintScale = 1;
        canMove = true;
        travelRight = false;
        power.bigBox.isTrigger = false;
        power.smallBox.isTrigger = false;
        if(gameManager.checkpointPassed == true)
        {
            transform.position = checkpointTransform.position;
            limiter.setpos();
        }
    }
    void Update()
    {
        xinput = Input.GetAxis("Horizontal");
        Debug.Log(""+Input.GetAxis("Horizontal"));
        Jump();
        fireRay();        
        playerFlip();
        win();
        currentSpeed = rb.velocity.x;

        if(Input.GetKeyDown(KeyCode.LeftShift) && power.fireFlowerEquipped == false && isGrounded || Input.GetKeyDown(KeyCode.RightShift) && power.fireFlowerEquipped == false && isGrounded || Input.GetKeyDown(KeyCode.Z) && power.fireFlowerEquipped == false && isGrounded)
        {
            sprintScale = 1.5f;
        }
        else if(Input.GetKeyUp(KeyCode.LeftShift)|| Input.GetKeyUp(KeyCode.RightShift) || Input.GetKeyUp(KeyCode.Z))
        {
            sprintScale = 1;             
        }

        if(xinput > 0.1f && isGrounded || xinput < -0.1f && isGrounded)
        {
            anim.SetBool("isMoving", true);
        }
        else if(xinput < 0.1f && xinput > -0.1f || !isGrounded)
        {
            anim.SetBool("isMoving", false);
        }
    }
    void FixedUpdate()
    {
        move();
        isOnGround();
        if(isGrounded)
        rb.AddForce((transform.up * 10f), ForceMode2D.Force);
        if(!isGrounded)
        rb.AddForce((-transform.up * 25f), ForceMode2D.Force);
    }
    void isOnGround()
    {
        Collider2D collider = Physics2D.OverlapCircle(groundChecker.position, checkGroundRadius, groundLayer);
        Collider2D colliderBrick = Physics2D.OverlapCircle(groundChecker.position, checkGroundRadius, brickLayer);
        Collider2D colliderItemBlock = Physics2D.OverlapCircle(groundChecker.position, checkGroundRadius, itemBlockLayer);
        if (collider != null || colliderBrick != null || colliderItemBlock != null)
        {
            isGrounded = true;
            anim.SetBool("isGrounded", true);
        }
        else
        {
            isGrounded = false;
            anim.SetBool("isGrounded", false);
        }
    }    
    public void move()
    {
        if(canMove)
        {
            rb.velocity = new Vector2(xinput * (moveSpeed * sprintScale) * Time.deltaTime, rb.velocity.y);

            if(Input.GetKeyDown(KeyCode.Space) && isGrounded || Input.GetKeyDown(KeyCode.X) && isGrounded)
            {
                //rb.velocity = new Vector2(rb.velocity.x, jumpPower);
                //AudioSource.PlayClipAtPoint(jumpSound, transform.position, .2f);                
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
    public void Jump()
    {
        if(canMove)
        if(Input.GetKeyDown(KeyCode.Space) && isGrounded || Input.GetKeyDown(KeyCode.X) && isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpPower);
            AudioSource.PlayClipAtPoint(jumpSound, transform.position, .2f);
        }
    }
    public void fireRay()
    {
        Collider2D collider = Physics2D.OverlapCircle(headPoint.position, checkGroundRadius, groundLayer);
        if (collider != null)
        {
            rb.AddForce(-transform.up * (jumpPower/5), ForceMode2D.Impulse);
        }

        RaycastHit2D itemCollider = Physics2D.BoxCast(headPoint.position, new Vector2(0.4f,0.1f), 0f, transform.up,checkGroundRadius, itemBlockLayer);
        if (itemCollider.collider != null )
        {
            itemBlock block = itemCollider.collider.gameObject.GetComponent<itemBlock>();
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
                rb.velocity = new Vector2(rb.velocity.x, -jumpPower/4);
                Destroy(brickCollider.gameObject);
            }
        }
         
        RaycastHit2D underCheck = Physics2D.Raycast(groundChecker.position, -transform.up, 0.3f, pipeLayer);
        if(underCheck.collider != null && Input.GetKeyDown(KeyCode.S) || underCheck.collider != null && Input.GetKeyDown(KeyCode.DownArrow))
        {
            Debug.Log("hi");
            underCheck.collider.gameObject.SetActive(false);
        }
        
        RaycastHit2D rayDown = Physics2D.BoxCast(groundChecker.position, new Vector2(.75f,.2f), 0f, -transform.up, .35f, enemyLayer);
        if(rayDown.collider != null && !isDead && !isImmune)
        {
            goomba goo = rayDown.collider.GetComponent<goomba>();
            if(goo.dead == false)
            {
                manager.addScore(100);
                AudioSource.PlayClipAtPoint(coinSound, transform.position, .75f);                 
            }
            goo.StartCoroutine("die");           
            rb.velocity = new Vector2(rb.velocity.x, jumpPower/2f);
        }
    }    

    public void playerFlip() 
    {
        if (rb.velocity.x > 0 && !facingRight && xinput > 0)
        {
            facingRight = !facingRight;
            transform.Rotate(0f, 180f, 0f);
        }
        else if (rb.velocity.x < 0 && facingRight && xinput < 0)
        {
            facingRight = !facingRight;
            transform.Rotate(0f, 180f, 0f);
        }
    }
    public void die()
    {
        if(!isDead)
        {
            canMove = false;
            isDead = true;
            rb.velocity = new Vector2(0,0);
            power.bigBox.isTrigger = true;
            power.smallBox.isTrigger = true;
            rb.velocity = new Vector2(rb.velocity.x, jumpPower/2);
            anim.SetBool("isDead", true);
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
    }
    public void dieHazard()
    {
        if(!isDead)
        {
            canMove = false;
            isDead = true;
            rb.velocity = new Vector2(0,0);
            GetComponent<BoxCollider2D>().isTrigger = true;
            rb.AddForce(transform.up * jumpPower/1.5f * Time.deltaTime, ForceMode2D.Impulse);
            anim.SetBool("isDead", true);
            manager.loseLife(1);
            if(gameManager.lives>0)
            {
                manager.StartCoroutine("resetLevel");
            }
            else if (gameManager.lives<=0)
            {
                manager.StartCoroutine("gameOverHazard");
            } 
        }
    }
    public void win()
    {
        if(travelRight)
        {
            rb.velocity = new Vector2(1 * moveSpeed/2.5f * Time.deltaTime, rb.velocity.y);
            anim.SetBool("levelWin", true);
        }
        
        RaycastHit2D touchingPlatform = Physics2D.Raycast(groundChecker.position, -transform.up, .5f, finishLayer);
        if(touchingPlatform.collider != null && travelRight)
        {
            rb.AddForce(transform.up * (jumpPower / 100f), ForceMode2D.Impulse);
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
            dieHazard();
        }
    }
    public IEnumerator losePower()
    {
        isImmune = true;        
        Physics2D.IgnoreLayerCollision(6, 8, true);        
        yield return new WaitForSecondsRealtime(.6f);
        sprite.color = flikr;
        yield return new WaitForSecondsRealtime(0.1f);
        sprite.color = full;
        yield return new WaitForSecondsRealtime(0.1f);
        sprite.color = flikr;
        yield return new WaitForSecondsRealtime(0.1f);
        sprite.color = full;
        yield return new WaitForSecondsRealtime(0.1f);
        sprite.color = flikr;
        yield return new WaitForSecondsRealtime(0.1f);
        sprite.color = full;
        yield return new WaitForSecondsRealtime(0.1f);
        Physics2D.IgnoreLayerCollision(6, 8, false);
        isImmune = false;
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
    }
}