using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class CommitMessageTextComponent : ScriptableObject
{
    public string Text => name;
    public CommitMessageTextComponent[] possibleFollowups;
}
