using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class levelFinish : MonoBehaviour
{
    public characterController player;
    public Transform playerTransform;
    public Transform poleEnd;
    public float timeElapsed;
    public float travelTime;
    public bool slidePole;

    void Start()
    {
       player = GameObject.Find("Player").GetComponent<characterController>();
       playerTransform = GameObject.Find("Player").GetComponent<Transform>();
       poleEnd = GameObject.Find("poleBase").GetComponent<Transform>();
       slidePole = false;
    }
    void Update()
    {
        lockInPlace();
        levelAnimation();
        if(slidePole)
        {
        timeElapsed += Time.deltaTime;
        float percentDone = timeElapsed / travelTime;
        playerTransform.position = Vector3.Lerp(playerTransform.position, poleEnd.position, percentDone);            
        }
    }
    void OnTriggerEnter2D(Collider2D hit)
    {
        if (hit.CompareTag("Player"))
        {
            Debug.Log("You win!");
            player.canMove = false;
            slidePole = true;
        }
    }
    public void lockInPlace()
    {

    }
    public void levelAnimation()
    {

    }
}
