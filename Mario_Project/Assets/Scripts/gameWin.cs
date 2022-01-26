using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class gameWin : MonoBehaviour
{
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
        if(Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene("start");
        }
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }
    public IEnumerator fade()
    {
        yield return new WaitForSeconds(1.5f);
        screenFade.SetActive(false);
        AudioSource.PlayClipAtPoint(gongSound, transform.position, 1f);
    }
}
