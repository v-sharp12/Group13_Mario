using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class levelFinish : MonoBehaviour
{
    public characterController player;
    void Start()
    {
       player = GameObject.Find("Player").GetComponent<characterController>(); 
    }
    void Update()
    {
        
    }
    void OnTriggerEnter2D(Collider2D hit)
    {
        if (hit.CompareTag("Player"))
        {
            Debug.Log("You win!");
        }
    }
}
