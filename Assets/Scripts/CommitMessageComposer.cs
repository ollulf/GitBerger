using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CommitMessageComposer : SingletonBehaviour<CommitMessageComposer>
{
    [SerializeField] CommitMessageTextComponent[] components;
    [SerializeField] CommitMessageTextComponent[] lockedComponents;
    [SerializeField] CommitMessageOptionUIElement commitMessageUIPrefab;

    [SerializeField] CommitMessageTextComponent noneVisible;

    private HashSet<CommitMessageTextComponent> lockedSet = new HashSet<CommitMessageTextComponent>();
    private HashSet<CommitMessageTextComponent> consumedSet = new HashSet<CommitMessageTextComponent>();

    private void Start()
    {
        foreach (var comp in lockedComponents)
            lockCommit(comp);

        clearOptions();
    }

    public void UpdateCommitOptions()
    {
        CommitMessageLineDisplayer.Instance.Text = "";
        SetMessageOptions(components);
    }

    public void AddToMessage(CommitMessageTextComponent associated)
    {
        CommitMessageLineDisplayer.Instance.Text += " " + associated.Text;

        if (associated.Followups.Length == 0)
        {
            consumedSet.Add(associated);
            clearOptions();
            CommitSubmitter.Instance.setCanSubmit(true);
        }
        else
            SetMessageOptions(associated.Followups);
    }

    public void lockCommit(CommitMessageTextComponent _component)
    {
        lockedSet.Add(_component);
    }

    public void unlockCommit(CommitMessageTextComponent _component)
    {
        lockedSet.Remove(_component);
        if (CommitListHandler.Instance.State.GetType() == typeof(SubmitState) && CommitMessageLineDisplayer.Instance.Text == "")
            UpdateCommitOptions();
    }

    public bool IsUnlocked(CommitMessageTextComponent _component)
    {
        return !lockedSet.Contains(_component);
    }

    private void clearOptions()
    {
        transform.DestroyAllChildren();
    }

    private void SetMessageOptions(CommitMessageTextComponent[] _components)
    {
        CommitSubmitter.Instance.setCanSubmit(false);
        clearOptions();

        int counter = 0;

        foreach (CommitMessageTextComponent component in _components)
        {
            foreach (CommitMessageTextComponent text in component.TextArray)
            {
                if (IsComponentUsable(text))
                {
                    Instantiate(commitMessageUIPrefab, transform).Init(text);
                    counter++;
                }
            }
        }

        if (counter == 0)
            Instantiate(commitMessageUIPrefab, transform).Init(noneVisible);
    }

    private bool IsComponentUsable(CommitMessageTextComponent _component, int depth = 0)
    {
        if (depth > 10)
        {
            Debug.LogError("Circular selection on " + _component);
            return false;
        }

        if (!IsUnlocked(_component))
            return false;

        if (consumedSet.Contains(_component))
            return false;
     
        if (_component.Followups.Length == 0)
            return true;

        foreach (var followup in _component.Followups)
        {
            if (IsComponentUsable(followup, depth + 1))
                return true;
        }
        return false;
    }
}
