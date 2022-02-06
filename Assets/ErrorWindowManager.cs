using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ErrorWindowManager : MonoBehaviour
{
    void Start()
    {
        gameObject.SetActive(false);
    }

    public void ToggleErrorWindow()
    {
        if(gameObject.activeSelf)
        {
            gameObject.SetActive(false);
        }
        else
        {
            gameObject.SetActive(true);
        }
    }

    public void ActivateErrorWindow()
    {
        gameObject.GetComponent<AudioSource>().Play();
        gameObject.SetActive(true);
    }
}
