using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindowsShutdownHandler : SingletonBehaviour<WindowsShutdownHandler>
{
    [SerializeField] TMPro.TextMeshProUGUI text;
    [SerializeField] TMPro.TextMeshProUGUI shutdown;

    [SerializeField] string msg;
    [SerializeField] string[] option0;
    [SerializeField] string[] option1;

    public void SwapText()
    {
        text.text = System.String.Format(msg, option0[Random.Range(0, option0.Length)], option1[Random.Range(0, option1.Length)]);
    }

    private void Start()
    {
        StartCoroutine(StartRoutine());
    }

    private IEnumerator StartRoutine()
    {
        yield return new WaitForSeconds(15);

        float t = 0;
        while (t < 1)
        {
            shutdown.text = "Updating windows.. " + t.ToString("%") + "%";
            yield return new WaitForSeconds(5);
            t += 0.01f;
        }

        Application.Quit();
    }
}
