using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class levelFinishCalc : MonoBehaviour
{
    public characterController player;
    public Transform groundChecker;
    public LayerMask finishLayer;
    void Start()
    {
        player = GetComponent<characterController>();
        groundChecker = transform.Find("ground_checker");
    }
    void Update()
    {
        detectFinish();
    }
    public void detectFinish()
    {
        RaycastHit2D detectLevelFinish = Physics2D.Raycast(groundChecker.position, -transform.up, .5f, finishLayer);
        if (detectLevelFinish.collider != null)
        {
            if(detectLevelFinish.collider.tag == ("highScore"))
            {
                Debug.Log(""+ detectLevelFinish.collider.name);
                player.canMove = false;
                player.rb.velocity= new Vector2(0,player.rb.velocity.y);
            }
            else if(detectLevelFinish.collider.tag == ("medScore"))
            {
                Debug.Log(""+ detectLevelFinish.collider.name);
                player.canMove = false;
                player.rb.velocity= new Vector2(0,player.rb.velocity.y);
            }
            else if(detectLevelFinish.collider.tag == ("lowScore"))
            {
                Debug.Log(""+ detectLevelFinish.collider.name);
                player.canMove = false;
                player.rb.velocity= new Vector2(0,player.rb.velocity.y);
            }
        }
    }
}
