using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fireball : MonoBehaviour
{
    void Start()
    {
        
    }
    void Update()
    {
        
    }
    void OnTriggerEnter2D(Collider2D hit)
    {
        if(hit.CompareTag("Ground") || hit.CompareTag("Enemy"))
        {
            Destroy(this.gameObject);
        }
    }
}
