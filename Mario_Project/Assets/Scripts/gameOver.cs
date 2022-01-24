using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class gameOver : MonoBehaviour
{
    public TextMeshProUGUI livesNumber;
    public GameObject screenFade;
    public AudioClip gongSound;
    void Awake()
    {
        screenFade.SetActive(true);
    }
    void Start()
    {
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
        yield return new WaitForSeconds(3.5f);
        SceneManager.LoadScene("start");
    }
}
