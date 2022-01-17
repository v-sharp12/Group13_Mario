using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camOffset : MonoBehaviour
{
    public Transform player;
    public Vector3 followPos;
    void Start()
    {
        
    }
    void Update()
    {
        transform.position = new Vector3(player.position.x, transform.position.y);
    }
}
