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
    
    [Header("Variables")]
    [SerializeField]private float gameTime;
    public static int score;
    public static int coins;
    public static int lives;
    void awake()
    {

    }
    void Start()
    {
        gameTime = 400;
        //score = 0;
        //coins = 0;
    }
    void Update()
    {
        uiTextControl();
    }
    public void uiTextControl()
    {
        gameTime -= Time.deltaTime;
        timeNumber.text = ("" + Mathf.Round(gameTime));
        scoreNumber.text = ("" + score);
        coinNumber.text = ("" + coins);
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
}
