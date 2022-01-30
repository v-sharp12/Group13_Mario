using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class teleOverWorld : MonoBehaviour
{
    public Transform spawn;
    public GameObject player;
    public GameObject blk;
    public characterController playerController;
    public sideScrollLimiter limiter;
    public LayerMask playerLayer;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<characterController>();
        limiter = GameObject.FindGameObjectWithTag("Camera Bounds").GetComponent<sideScrollLimiter>();
        //blk = GameObject.Find("OverBG").GetComponent<GameObject>();
    }
    void Update()
    {
        goBack();
    }
    void OnTriggerEnter2D(Collider2D hit)
    {

    }
    public void goBack()
    {
        RaycastHit2D playercheck = Physics2D.Raycast(transform.position, -transform.right, .5f, playerLayer);
        if(playercheck.collider != null && Input.GetKeyDown(KeyCode.RightArrow) ||playercheck.collider != null && Input.GetKeyDown(KeyCode.D))
        {
            blk.SetActive(false);
            player.transform.position = spawn.transform.position;
            limiter.goAbove();
            limiter.setpos();
        }
    }
}
