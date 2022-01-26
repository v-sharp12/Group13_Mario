using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class levelFinishCalc : MonoBehaviour
{
    public characterController player;
    public gameManager manager;
    public Transform groundChecker;
    public LayerMask finishLayer;
    public bool checkForFinish;
    void Start()
    {
        player = GetComponent<characterController>();
        manager = GameObject.Find("GameManager").GetComponent<gameManager>();
        groundChecker = transform.Find("ground_checker");
        checkForFinish =  true;
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
            if(detectLevelFinish.collider.tag == ("highScore") && checkForFinish)
            {
                Debug.Log(""+ detectLevelFinish.collider.name);
                checkForFinish = false;
                player.canMove = false;
                player.rb.velocity= new Vector2(0,player.rb.velocity.y);
                AudioSource.PlayClipAtPoint(player.coinSound, player.transform.position, 1f);
                manager.addScore(1000);
                manager.StartCoroutine("finishLevel");
            }
            else if(detectLevelFinish.collider.tag == ("medScore") && checkForFinish)
            {
                Debug.Log(""+ detectLevelFinish.collider.name);
                checkForFinish = false;
                player.canMove = false;
                player.rb.velocity= new Vector2(0,player.rb.velocity.y);
                AudioSource.PlayClipAtPoint(player.coinSound, player.transform.position, 1f);
                manager.addScore(500);
                manager.StartCoroutine("finishLevel");
            }
            else if(detectLevelFinish.collider.tag == ("lowScore") && checkForFinish)
            {
                Debug.Log(""+ detectLevelFinish.collider.name);
                checkForFinish = false;
                player.canMove = false;
                player.rb.velocity= new Vector2(0,player.rb.velocity.y);
                AudioSource.PlayClipAtPoint(player.coinSound, player.transform.position, 1f);
                manager.addScore(100);
                manager.StartCoroutine("finishLevel");
            }
        }
    }
}
