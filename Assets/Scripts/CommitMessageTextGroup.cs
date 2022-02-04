using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class CommitMessageTextGroup : CommitMessageTextComponent
{
    protected override CommitMessageTextComponent[] GetTextArray()
    {
        return Followups;
    }
}
