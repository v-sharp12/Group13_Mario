using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class levelReset : MonoBehaviour
{
    public gameManager manager;
    public TextMeshProUGUI livesNumber;
    public GameObject screenFade;
    public AudioClip gongSound;
    void Awake()
    {
        screenFade.SetActive(true);
    }
    void Start()
    {
        manager = FindObjectOfType<gameManager>();
        livesNumber.text = ("x " + gameManager.lives);
        StartCoroutine("fade");
    }
    void Update()
    {

    }
    public IEnumerator fade()
    {
        yield return new WaitForSeconds(1.5f);
        screenFade.SetActive(false);
        AudioSource.PlayClipAtPoint(gongSound, transform.position, .5f);
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene("level1_1");
    }
}
