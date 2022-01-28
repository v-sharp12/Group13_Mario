using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sideScrollLimiter : MonoBehaviour
{
    public characterController player;
    public Transform playerTransform;
    public Vector3 followPos;
    public float trackOffset;
    public Vector3 regY;
    public Vector3 underY;

    void Awake()
    {
        playerTransform = GameObject.Find("Player").GetComponent<Transform>();        
    }
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<characterController>();
    }
    void Update()
    {
        if(player.movingRight == true && Vector2.Distance(this.transform.position, playerTransform.position) > trackOffset)
        {
            transform.position = new Vector3(playerTransform.position.x -  trackOffset, transform.position.y, transform.position.z);
        }
        regY = new Vector3(transform.position.x, 1.5f);
        underY = new Vector3(transform.position.x, -13f);
    }
    public void goUnderground()
    {
        transform.position = underY;
    }
    public void goAbove()
    {
        transform.position = regY;
    }
    public void setpos()
    {
        transform.position = new Vector3(playerTransform.position.x -  trackOffset, transform.position.y, transform.position.z);
    }
}
