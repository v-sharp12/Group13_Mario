using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sideScrollLimiter : MonoBehaviour
{
    public characterController player;
    public Transform playerTransform;
    public Vector3 followPos;
    public float trackOffset;
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<characterController>();
        playerTransform = GameObject.Find("Player").GetComponent<Transform>();
    }
    void Update()
    {
        if(player.movingRight == true && Vector2.Distance(this.transform.position, playerTransform.position) > trackOffset)
        {
            transform.position = new Vector3(playerTransform.position.x -  trackOffset, transform.position.y, transform.position.z);
        }
    }
}
