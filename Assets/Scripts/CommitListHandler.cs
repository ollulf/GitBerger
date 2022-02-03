using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommitListHandler : SingletonBehaviour<CommitListHandler>
{
    [SerializeField] private CommitListUIElement commitUIPrefab;

    private List<Commit> commits;
    private CommitStateBase state;
    private Commit current;

    public static System.Action<string> OnNewPlayerPush;

    private void Start()
    {
        commits = new List<Commit>();
        state = new PullState();
        PopupLoginBehaviour.OnLogin += OnLogin;
    }


    private void OnLogin ()
    {
        PopupLoginBehaviour.OnLogin -= OnLogin;
        AddComit(new Commit() { Author = Commit.Authors.Player, DateTime = DateTime.Now.Subtract(new TimeSpan(0, 2, 11)), Message = "Added new Unity Project", State = Commit.States.Local });
        AddComit(new Commit() { Author = Commit.Authors.Berger, DateTime = DateTime.Now.Subtract(new TimeSpan(0, 1, 3)), Message = "Added .gitignore and readme.md", State = Commit.States.Origin });
        UpdatePositions();
    }

    public void AddBergerCommit(Commit commit)
    {
        AddCommitDelayed(commit);
    }
    private void AddPlayerCommit(Commit commit)
    {
        foreach (Commit old in commits)
        {
            if (old.State == Commit.States.Local)
            {
                if (old.Author == Commit.Authors.Berger)
                    old.State = Commit.States.Origin;
                else
                    old.State = Commit.States.Old;

                old.UpdateUI();
            }
        }

        ChangesHandler.Instance.ClearChanges();

        AddCommitDelayed(commit);
    }
    private void AddCommitDelayed(Commit commit)
    {
        current = commit;
        LoadingHandler.Instance.Delay(1, () => AddComit(current));
    }

    private void AddComit(Commit commit)
    {
        CommitListUIElement instance = Instantiate(commitUIPrefab, transform);
        instance.transform.SetSiblingIndex(0);
        commit.UIInstance = instance;
        commits.Add(commit);
        UpdatePositions();
    }

    public void UpdatePositions()
    {
        Debug.Log(commits.Count);

        for (int i = 0; i < commits.Count; i++)
        {
            CommitListItemPosition position = CommitListItemPosition.Middle;
            if (i == 0) position = CommitListItemPosition.Last;
            if (i == commits.Count - 1) position = CommitListItemPosition.First;

            commits[i].UpdateUI(position);
        }
    }

    public void OpenPullWindow()
    {
        PopopMessageHandler.Instance.ShowModal("Pull from GITHUP", "Pull remote branch and merge them into your local branch from GITHUP!", "Pull", Pull);
    }

    public void Pull()
    {
        if (LoadingHandler.IsLoading)
            return;

        if (CheckResultForBlock(state.TryPull()))
            return;

        LoadingHandler.Instance.Delay(1, () =>
        {
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

            ChangesHandler.Instance.AddNewChanges();
        });
    }
    public void Submit()
    {
        if (LoadingHandler.IsLoading)
            return;

        if (CheckResultForBlock(state.TrySubmit()))
            return;

        LoadingHandler.Instance.Delay(1, () =>
        {
            Commit newCommit = new Commit() { Author = Commit.Authors.Player, DateTime = System.DateTime.Now, Message = CommitMessageLineDisplayer.Instance.Text, State = Commit.States.Local };
            AddPlayerCommit(newCommit);
            CommitMessageLineDisplayer.Instance.Text = "";
            CommitMessageComposer.Instance.Start();
        });
    }
    public void Push()
    {
        if (LoadingHandler.IsLoading)
            return;

        if (CheckResultForBlock(state.TryPush()))
            return;

        LoadingHandler.Instance.Delay(1, () =>
        {
            foreach (Commit old in commits)
            {
                if (old.State == Commit.States.Origin)
                {
                    old.State = Commit.States.Old;
                    old.UpdateUI();
                }
            }
            OnNewPlayerPush?.Invoke(commits[commits.Count - 1].Message);
        });
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
