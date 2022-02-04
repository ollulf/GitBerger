using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class CommitMessageTextComponent : ScriptableObject
{
    public string Text => name;
    public CommitMessageTextComponent[] TextArray => GetTextArray();
    public CommitMessageTextComponent[] possibleFollowups;
    protected virtual CommitMessageTextComponent[] GetTextArray()
    {
        return new CommitMessageTextComponent[] { this };
    }

}
