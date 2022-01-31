using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class koopa : MonoBehaviour
{
    public GameObject shell;
    void Start()
    {
        shell.SetActive(false);
    }
    public void dropShell()
    {
        shell.SetActive(true);
        shell.transform.parent = null;
    }
}
