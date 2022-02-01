using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CommitMessageLineDisplayer : SingletonBehaviour<CommitMessageLineDisplayer>
{
    public string Text;
    [SerializeField] TMP_Text lineUI;

    private void Start()
    {
        StartCoroutine(DisplayLineWithBlinkyRoutine());
    }

    IEnumerator DisplayLineWithBlinkyRoutine()
    {
        bool showBlinky = true;
        while (true)
        {
            yield return new WaitForSeconds(0.5f);
            showBlinky = !showBlinky;

            lineUI.text = Text + (showBlinky ? "|" : "");
        }
    }
}
