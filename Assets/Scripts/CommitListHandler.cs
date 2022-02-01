using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommitListHandler : SingletonBehaviour<CommitListHandler>
{
    [SerializeField] CommitListUIElement commitUIPrefab;

    public static System.Action<string> OnNewPlayerCommit;
    public void AddPlayerCommit(Commit commit)
    {
        AddCommit(commit);
        OnNewPlayerCommit?.Invoke(commit.Message);
    }

    internal void AddBergerCommit(Commit commit)
    {
        AddCommit(commit);
    }

    private void AddCommit(Commit commit)
    {
        CommitListUIElement instance = Instantiate(commitUIPrefab, transform);
        instance.Display(commit, CommitListItemPosition.Middle);
        instance.transform.SetSiblingIndex(0);
    }
}

public class Commit
{
    public enum Authors
    {
        Player,
        Berger
    }
    public enum States
    {
        Old,
        Local,
        Origin
    }

    public Authors Author;
    public string Message;
    public DateTime DateTime;
    public States State;

}
