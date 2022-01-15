using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class characterController : MonoBehaviour
{
    Rigidbody2D rb;
    [SerializeField] private float xinput;
    [Header("Movement Variables")]
    public float moveSpeed;
    [Header("Movement Constraints")]
    public bool isFlipped;
    public bool shouldMove;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        xinput = Input.GetAxis("Horizontal");
        move();
    }
    public void move()
    {
        if (xinput>0)
        {
            rb.velocity = ( new Vector2(1 * moveSpeed * Time.deltaTime, 0f));
        }
        else if (xinput<0)
        {
            rb.velocity = ( new Vector2(-1 * moveSpeed * Time.deltaTime, 0f));
        }
    }
}
