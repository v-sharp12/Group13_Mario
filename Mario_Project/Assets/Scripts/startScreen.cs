using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class startScreen : MonoBehaviour
{
    void Start()
    {
        
    }
    void Update()
    {

    }
    public void startgame()
    {
        gameManager.lives = 3;
        gameManager.score = 0;
        gameManager.coins = 0;
        gameManager.checkpointPassed = false;
        SceneManager.LoadScene("resetLevelTransition");
    }
}
