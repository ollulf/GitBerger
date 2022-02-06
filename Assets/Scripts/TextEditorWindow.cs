using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TextEditorWindow : MonoBehaviour
{
    [SerializeField] GameObject window;
    [SerializeField] TMP_InputField inputField;
    [SerializeField] CommitMessageTextComponent toUnlock;
    private string text = "Dear Diary today I played Minecraft and got killed by a creeper while mining diamonds.I was very sad...";

    public void Open()
    {
        window.SetActive(true);
        inputField.text = text;
        CommitMessageComposer.Instance.unlockCommit(toUnlock);
    }
    public void Close()
    {
        text = inputField.text;
        window.SetActive(false);
    }
}
