using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommitSubmitter : MonoBehaviour
{
    public void Submit()
    {
        Commit newCommit = new Commit() { Author = Commit.Authors.Player, DateTime = System.DateTime.Now, Message = CommitMessageLineDisplayer.Instance.Text, State = Commit.States.Local };
        CommitListHandler.Instance.AddPlayerCommit(newCommit);
        CommitMessageLineDisplayer.Instance.Text = "";
        CommitMessageComposer.Instance.Start();
    }
}
