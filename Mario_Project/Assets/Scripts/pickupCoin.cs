using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pickupCoin : MonoBehaviour
{
    public gameManager gameManager;
    public AudioClip coinPickup;
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<gameManager>();
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.CompareTag("Player"))
        {
            gameManager.addScore(200);
            gameManager.addCoin(1);
            AudioSource.PlayClipAtPoint(coinPickup, transform.position, 1f);
            this.gameObject.SetActive(false);
            //Destroy(this.gameObject);
        }
    }
}
