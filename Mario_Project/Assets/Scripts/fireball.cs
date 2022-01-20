using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fireball : MonoBehaviour
{
    public characterController player;
    public Rigidbody2D rb;  
    public LayerMask enemyLayer;
    public LayerMask groundLayer;
    public Transform firepointLeft;
    public Transform firepointRight;
    [Header("Variables")]  
    public float lifeTime;
    public float lifespan = 5f; // The lenght of time before destroying fireball.
    public float speed;
    private float initalVelocity { get { return calculateInitialVelocity(); } }
    public void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        firepointLeft = transform.Find("rayFireLeft");
        firepointRight = transform.Find("rayFireRight");
        lifeTime = 0f;
    }

    public void Update()
    {
        Vector3 velocity = rb.velocity;
        velocity.x = transform.right.x * speed;
        rb.velocity = velocity;
        liveLife();
        fireRay();
    }
    public void liveLife()
    {
        lifeTime = lifeTime + Time.deltaTime;
        if(lifeTime>lifespan)
        {
            Destroy(this.gameObject);
        }
    }
    private float calculateInitialVelocity(float height = 1.5f, float velocityX = 1f, float airTime = 1.5f)
    {
        return (2 * height * velocityX) / airTime;
    }
    public void OnCollisionEnter2D(Collision2D col)
    {
        for (int i = 0; i < col.contactCount; i++)
            if (Vector3.Dot(Vector3.down, col.GetContact(i).normal) < -0.5f)
            {
                Vector3 velocity = rb.velocity;
                velocity.y = initalVelocity;
                rb.velocity = velocity;
            }
    }
    public void fireRay()
    {
        RaycastHit2D rayLeft = Physics2D.Raycast(firepointLeft.position, -transform.right, .2f, enemyLayer);
        RaycastHit2D rayRight = Physics2D.Raycast(firepointRight.position, transform.right, .2f, enemyLayer);
        if(rayRight.collider != null)
        {
            player.gameManager.addScore(100);
            Destroy(rayRight.collider.gameObject);
            Destroy(this.gameObject);
        }
        else if(rayLeft.collider != null)
        {

            player.gameManager.addScore(100);
            Destroy(rayLeft.collider.gameObject);
            Destroy(this.gameObject);
        }
        RaycastHit2D wallRayLeft = Physics2D.Raycast(firepointLeft.position, -transform.right, .2f, groundLayer);
        RaycastHit2D wallRayRight = Physics2D.Raycast(firepointRight.position, transform.right, .2f, groundLayer);
        if(wallRayRight.collider != null)
        {
            Destroy(this.gameObject);
        }
        else if(wallRayLeft.collider != null)
        {
            Destroy(this.gameObject);
        }
    }
    void OnTriggerEnter2D(Collider2D hit)
    {

    }
    void OnDrawGizmosSelected()
    {
        Debug.DrawRay(firepointLeft.position, -transform.right, Color.green, .1f);
        Debug.DrawRay(firepointRight.position, transform.right, Color.green, .1f);
    }
}
