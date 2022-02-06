using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class CommitMessageTextComponent : ScriptableObject
{
    [SerializeField] private CommitMessageTextComponent[] possibleFollowups;
    public string custom = "";

    public string Text => !string.IsNullOrEmpty(custom) ? custom : name;
    public CommitMessageTextComponent[] TextArray => GetTextArray();
    public CommitMessageTextComponent[] Followups => possibleFollowups;

    protected virtual CommitMessageTextComponent[] GetTextArray()
    {
        return new CommitMessageTextComponent[] { this };
    }

}
