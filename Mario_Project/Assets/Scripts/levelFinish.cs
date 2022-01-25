using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class levelFinish : MonoBehaviour
{
    public characterController player;
    public Transform playerTransform;
    void Start()
    {
       player = GameObject.Find("Player").GetComponent<characterController>();
       playerTransform = GameObject.Find("Player").GetComponent<Transform>();
    }
    void Update()
    {
        lockInPlace();
        levelAnimation();
    }
    void OnTriggerEnter2D(Collider2D hit)
    {
        if (hit.CompareTag("Player"))
        {
            Debug.Log("You win!");
            player.canMove = false;
        }
    }
    public void lockInPlace()
    {

    }
    public void levelAnimation()
    {

    }
}
