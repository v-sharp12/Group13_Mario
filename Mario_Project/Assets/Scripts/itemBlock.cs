using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class itemBlock : MonoBehaviour
{
    public GameObject[] items;
    public Transform spawnPoint;
    public bool canSpawn;
    public int spawnIndex;
    void Start()
    {
        spawnPoint = transform.Find("itemSpawn");
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
        }
    }
}
