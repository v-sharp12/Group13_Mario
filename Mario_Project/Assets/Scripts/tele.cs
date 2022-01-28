using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tele : MonoBehaviour
{
    public Transform spawn;
    public GameObject player;
    public characterController playerController;
    public sideScrollLimiter limiter;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<characterController>();
        limiter = GameObject.FindGameObjectWithTag("Camera Bounds").GetComponent<sideScrollLimiter>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter2D(Collider2D hit)
    {
        if(hit.CompareTag("Player"))
        {
            player.transform.position = spawn.transform.position;
            limiter.goUnderground();
            limiter.setpos();
            //StartCoroutine("push");
        }
    }
    public IEnumerator push()
    {
        yield return new WaitForSeconds(1f);
        playerController.rb.AddForce(transform.right * 1.5f, ForceMode2D.Impulse);
    }
}
