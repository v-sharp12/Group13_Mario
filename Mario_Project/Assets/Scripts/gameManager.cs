using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class gameManager : MonoBehaviour
{
    [Header("References")]
    public TextMeshProUGUI scoreNumber;
    public TextMeshProUGUI worldNumber;
    public TextMeshProUGUI timeNumber;
    public TextMeshProUGUI coinNumber;
    public characterController player;
    public Transform playerTransform;
    public Vector3 underWorldSpawn;
    
    [Header("Variables")]
    [SerializeField]private float gameTime;
    public bool trackTime;
    public static int score;
    public static int coins;
    public static int lives;
    public int additiveScore;
    public static bool underWorld;
    public static bool checkpointPassed;
    
    [Header("Sounds")]
    public AudioClip coinSound;
    void awake()
    {
        if(checkpointPassed)
        {

        }
    }
    void Start()
    {
        gameTime = 400;
        trackTime = true;
        player = FindObjectOfType<characterController>();
        playerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }
    void Update()
    {
        uiTextControl();
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }
    public void uiTextControl()
    {
        if (trackTime)
        {
            gameTime -= Time.deltaTime;            
        }
        timeNumber.text = ("" + Mathf.Round(gameTime));
        scoreNumber.text = ("" + score);
        coinNumber.text = ("" + coins);
        if(gameTime<=0)
        {
            if(gameManager.lives>0)
            {
                StartCoroutine("resetLevel");
            }
            else if (gameManager.lives<=0)
            {
                StartCoroutine("gameOver");
            }   
        }
    }
    public void addScore(int amount)
    {
        score += amount;
    }
    public void addCoin(int amount)
    {
        coins += amount;
    }
    public void loseLife(int amount)
    {
        lives -= amount;
    }
    public IEnumerator resetLevel()
    {
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene("resetLevelTransition");
    }
    public IEnumerator gameOver()
    {
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene("gameOver");
    }
    public IEnumerator gameOverHazard()
    {
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene("gameOver");
    }
    public void calculateScore()
    {
        if(gameTime >= 300)
        {
            additiveScore += 1000;
        }
        else if (gameTime < 300 && gameTime >= 200)
        {
            additiveScore += 500;
        }
        else if (gameTime < 200 && gameTime >= 100)
        {
            additiveScore += 250;
        }
        else if (gameTime < 100 && gameTime >= 0)
        {
            additiveScore += 100;
        }
    }
    public void addToScore()
    {
        AudioSource.PlayClipAtPoint(coinSound, playerTransform.position, 1f);
        score = score + additiveScore;
    }
    public IEnumerator finishLevel()
    {
        player.anim.SetBool("levelWin", true);
        trackTime = false;
        calculateScore();
        yield return new WaitForSeconds(2f);
        addToScore();
        player.travelRight = true;
    }
}
