using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommitListHandler : SingletonBehaviour<CommitListHandler>
{
    [SerializeField] private CommitListUIElement commitUIPrefab;

    private List<Commit> commits = new List<Commit>();
    private CommitStateBase state;

    public static System.Action<string> OnNewPlayerPush;

    private void Start()
    {
        state = new SubmitState();
    }
    public void AddBergerCommit(Commit commit)
    {
        AddCommit(commit);
    }
    private void AddPlayerCommit(Commit commit)
    {
        foreach (Commit old in commits)
        {
            if (old.State == Commit.States.Local)
            {
                old.State = Commit.States.Origin;
                old.UpdateUI();
            }
        }

        AddCommit(commit);
    }
    private void AddCommit(Commit commit)
    {
        CommitListUIElement instance = Instantiate(commitUIPrefab, transform);
        instance.transform.SetSiblingIndex(0);
        commit.UIInstance = instance;
        commits.Add(commit);
        UpdatePositions();
    }
    public void UpdatePositions()
    {
        for (int i = 0; i < commits.Count; i++)
        {
            CommitListItemPosition position = CommitListItemPosition.Middle;
            if (i == 0) position = CommitListItemPosition.Last;
            if (i == commits.Count - 1) position = CommitListItemPosition.First;
            commits[i].UpdateUI(position);
        }
    }
    public void Pull()
    {
        if (CheckResultForBlock(state.TryPull()))
            return;

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
    public void Submit()
    {
        if (CheckResultForBlock(state.TrySubmit()))
            return;

        Commit newCommit = new Commit() { Author = Commit.Authors.Player, DateTime = System.DateTime.Now, Message = CommitMessageLineDisplayer.Instance.Text, State = Commit.States.Local };
        AddPlayerCommit(newCommit);
        CommitMessageLineDisplayer.Instance.Text = "";
        CommitMessageComposer.Instance.Start();
    }
    public void Push()
    {
        if (CheckResultForBlock(state.TryPush()))
            return;

        foreach (Commit old in commits)
        {
            if (old.State == Commit.States.Origin)
            {
                old.State = Commit.States.Old;
                old.UpdateUI();
            }
        }
        OnNewPlayerPush?.Invoke(commits[commits.Count - 1].Message);
    }
    private bool CheckResultForBlock(ActionResult result)
    {
        if (result.Type == ActionResult.Types.Error)
        {
            PopopMessageHandler.Instance.ShowError(result.ErrorMessage);
            return true;
        }

        state = result.SuccessState;
        return false;
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
