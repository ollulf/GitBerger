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
        //destroy childs
        for (int i = transform.childCount - 1; i >= 0; i--)
            Destroy(transform.GetChild(i).gameObject);

        foreach (CommitMessageTextComponent component in components)
        {
            Instantiate(commitMessageUIPrefab, transform).Init(component);
        }
    }
}
