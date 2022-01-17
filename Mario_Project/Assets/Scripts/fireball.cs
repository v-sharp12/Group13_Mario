using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fireball : MonoBehaviour
{
    public float speed;
    [HideInInspector] public Rigidbody2D rb;

    private float initalVelocity { get { return calculateInitialVelocity(); } }

    // private float gravity;

    public void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void Update()
    {
        Vector3 velocity = rb.velocity;
        velocity.x = transform.right.x * speed;
        rb.velocity = velocity;
    }

    private float calculateInitialVelocity(float height = 1.5f, float velocityX = 1f, float airTime = 1f)
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
    void OnTriggerEnter2D(Collider2D hit)
    {
        if(hit.CompareTag("Ground") || hit.CompareTag("Enemy"))
        {
            Destroy(this.gameObject);
        }
    }
}
