using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommitListHandler : SingletonBehaviour<CommitListHandler>
{
    [SerializeField] CommitListUIElement commitUIPrefab;

    List<Commit> commits = new List<Commit>();

    public static System.Action<string> OnNewPlayerCommit;
    public void AddPlayerCommit(Commit commit)
    {
        foreach (Commit old in commits)
        {
            if (old.State == Commit.States.Local)
            {
                old.State = Commit.States.Old;
                old.UpdateUI();
            }
        }

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
        commit.UIInstance = instance;
        commits.Add(commit);
    }

    public void Pull()
    {
        int index = 0;
        for (int i = 0; i < commits.Count; i++)
        {
            bool last = i == commits.Count - 1;

            CommitListItemPosition position = CommitListItemPosition.Middle;
            if (i == 0) position = CommitListItemPosition.Last;
            if (last) position = CommitListItemPosition.First;

            Commit commit = commits[i];
            commit.State = last ? Commit.States.Local : Commit.States.Old;
            commit.UpdateUI(position);
        }
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
    public CommitListUIElement UIInstance;

    public void UpdateUI(CommitListItemPosition position)
    {
        UIInstance.Display(this, position);
    }

    public void UpdateUI()
    {
        UIInstance.Display(this);
    }
}
