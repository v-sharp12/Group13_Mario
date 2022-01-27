using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class itemBlock : MonoBehaviour
{
    public GameObject[] items;
    public Transform spawnPoint;
    public SpriteRenderer sprite;
    public Color col;
    public bool canSpawn;
    public int spawnIndex;
    public int currTaps;
    void Start()
    {
        spawnPoint = transform.Find("itemSpawn");
        sprite = GetComponent<SpriteRenderer>();
        canSpawn = true;
    }
    void Update()
    {
        
    }
    public void spawnItem()
    {
        if(canSpawn)
        {    
        GameObject item = Instantiate(items[spawnIndex], spawnPoint.position, Quaternion.identity);
        canSpawn = false;
        sprite.color = col;
        }
    }
}
