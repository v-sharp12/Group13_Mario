using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class coinBlock : MonoBehaviour
{
    public GameObject[] items;
    public Transform spawnPoint;
    public SpriteRenderer sprite;
    public Color col;
    public bool canSpawn;
    public bool startTimer;
    public int spawnIndex;
    public int currTaps;
    public float timeToTap;
    public float delay;
    void Start()
    {
        spawnPoint = transform.Find("itemSpawn");
        sprite = GetComponent<SpriteRenderer>();
        canSpawn = true;
        startTimer = false;
        timeToTap = 10;
        delay = 0;
    }
    void Update()
    {
        delay = Mathf.Clamp(delay -= Time.deltaTime, 0 , .4f);
        if(startTimer)
        {
            timeToTap = Mathf.Clamp(timeToTap -= Time.deltaTime, 0 , 8);            
        }
        if(timeToTap == 0)
        {
            canSpawn = false;
            sprite.color = col;
        }
        else if(currTaps>=10)
        {
            canSpawn = false;
            sprite.color = col;
        }
    }
    public void spawnItem()
    {
        if(canSpawn && delay == 0)
        {    
            GameObject item = Instantiate(items[spawnIndex], spawnPoint.position, Quaternion.identity);
            currTaps = Mathf.Clamp(currTaps+=1, 0, 10);
            delay = .4f;
            startTimer = true;
        }
    }
}
