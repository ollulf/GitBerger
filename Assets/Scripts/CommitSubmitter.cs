using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommitSubmitter : MonoBehaviour
{
    public void Submit()
    {
        CommitListHandler.Instance.Submit();
    }
}
