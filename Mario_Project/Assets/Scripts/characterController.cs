using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class characterController : MonoBehaviour
{
    [SerializeField] private float xinput;
    [Header("References")]
    Rigidbody2D rb;
    public Transform groundChecker;
    public Transform firePoint;
    public LayerMask groundLayer;
    public GameObject fireballProjectile;
    public gameManager gameManager;
    
    [Header("Movement Variables")]
    public float moveSpeed;
    public float jumpPower;
    public float checkGroundRadius;
    
    [Header("Movement Constraints")]
    public bool isFlipped;
    public bool isGrounded;
    public bool facingRight = true;
    public bool fireFlowerEquipped;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        gameManager = GameObject.Find("GameManager").GetComponent<gameManager>();
        groundChecker = transform.Find("ground_checker");
        firePoint = transform.Find("firePoint");
        fireFlowerEquipped = false;
    }
    void Update()
    {
        xinput = Input.GetAxis("Horizontal");
        move();
        shootFireball();
        isOnGround();
        playerFlip();
    }
    public void move()
    {
        rb.velocity = new Vector2(moveSpeed * xinput, rb.velocity.y);
        if(Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(transform.up * jumpPower);
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
    void isOnGround()
    {
        Collider2D collider = Physics2D.OverlapCircle(groundChecker.position, checkGroundRadius, groundLayer);
        if (collider != null)
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
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(groundChecker.position, checkGroundRadius);
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(firePoint.position, new Vector3(.25f,.25f,.25f));
    }
}
