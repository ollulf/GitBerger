using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CommitMessageComposer : SingletonBehaviour<CommitMessageComposer>
{

    [SerializeField] CommitMessageTextComponent[] componentsOnStart;
    [SerializeField] CommitMessageOptionUIElement commitMessageUIPrefab;
    [SerializeField] TMP_Text commitMessageTextUI;

    public void Start()
    {
        SetMessageOptions(componentsOnStart);
    }

    public void AddToMessage(CommitMessageTextComponent associated)
    {
        CommitMessageLineDisplayer.Instance.Text += " " + associated.Text;
        SetMessageOptions(associated.possibleFollowups);
    }

    private void SetMessageOptions(CommitMessageTextComponent[] components)
    {
        transform.DestroyAllChildren();
        foreach (CommitMessageTextComponent component in components)
        {
            foreach (CommitMessageTextComponent text in component.TextArray)
            {
                Instantiate(commitMessageUIPrefab, transform).Init(text);
            }
        }
    }
}
