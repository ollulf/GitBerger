using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Clock : MonoBehaviour
{
    [SerializeField] TMP_Text textUI;
    private void Start()
    {
        StartCoroutine(ClockUpdateRoutine());
    }

    private IEnumerator ClockUpdateRoutine()
    {
        while (true)
        {
            textUI.text = DateTime.Now.ToShortTimeString();
            yield return new WaitForSeconds(10f);
        }
    }
}
