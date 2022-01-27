using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class quickCoin : MonoBehaviour
{
    public gameManager gameManager;
    public AudioClip coinPickup;
    public float lifeTime = 0.5f;
    public float age;
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<gameManager>();
        age = 0;
        gameManager.addScore(200);
        gameManager.addCoin(1);
        AudioSource.PlayClipAtPoint(coinPickup, transform.position, 1f);
    }
    void Update()
    {
        age += Time.deltaTime;
        if(age>=lifeTime)
        {
            Destroy(this.gameObject);
        }
    }
}
