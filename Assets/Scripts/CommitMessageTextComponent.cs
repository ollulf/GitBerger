using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class CommitMessageTextComponent : ScriptableObject
{
    public bool Unlocked = true;
    public string Text => name == "Custom" ? custom : name;
    public CommitMessageTextComponent[] TextArray => GetTextArray();
    [SerializeField] private CommitMessageTextComponent[] possibleFollowups;
    public CommitMessageTextComponent[] Followups => possibleFollowups;

    public string custom = "";
    protected virtual CommitMessageTextComponent[] GetTextArray()
    {
        return new CommitMessageTextComponent[] { this };
    }

}
