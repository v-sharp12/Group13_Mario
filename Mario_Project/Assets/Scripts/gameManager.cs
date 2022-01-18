using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class gameManager : MonoBehaviour
{
    [Header("References")]
    public TextMeshProUGUI scoreNumber;
    public TextMeshProUGUI worldNumber;
    public TextMeshProUGUI timeNumber;
    
    [Header("Variables")]
    [SerializeField]private float gameTime;
    [SerializeField]private int score;
    void Start()
    {
        gameTime = 0;
        score = 0;
    }
    void Update()
    {
        uiTextControl();
    }
    public void uiTextControl()
    {
        gameTime = gameTime + Time.deltaTime;
        timeNumber.text = ("" + Mathf.Round(gameTime));
        scoreNumber.text = ("" + score);
    }
    public void addScore(int amount)
    {
        score += amount;
    }
}
