﻿using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

[CreateAssetMenu]
public class CommitActionBehaviour : MonoBehaviour
{
    [SerializeField]
    [FormerlySerializedAs("match")]
    private string mainMatch;

    [SerializeField]
    private string[] additionalMatches;

    [SerializeReference]
    private List<Action> actions;

    public List<Action> SubActions => actions;

    public string DisplayName => "Action on: " + mainMatch + (additionalMatches.Length > 0 ? $"(+{additionalMatches.Length})" : "");

    [SerializeField]
    private ActionType addType;

    public bool bIsMatch(string _source)
    {
        if (additionalMatches.Length > 0)
        {
            foreach (var match in additionalMatches)
            {
                if (_source.Contains(match))
                    return true;
            }
        }

        return _source.Contains(mainMatch);
    }

    public override string ToString()
    {
        return "Action: " + mainMatch;
    }

    private void OnValidate()
    {
        name = DisplayName;
        if (addType == ActionType.None)
            return;

        switch (addType)
        {
            case ActionType.Event:
                actions.Add(new EventAction());
                break;
            case ActionType.BergerCommit:
                actions.Add(new BergerCommitAction());
                break;
            case ActionType.InstallApp:
                actions.Add(new InstallAction());
                break;
        }
        addType = ActionType.None;
    }
}

public enum ActionType
{
    None,
    Event,
    BergerCommit,
    InstallApp
}

[System.Serializable]
public abstract class Action
{
    [SerializeField]
    protected float delay;

    public float Delay => delay;

    public abstract void run();
}

[System.Serializable]
public class EventAction : Action
{
    [Header("Event")]
    public UnityEvent Event;

    public override void run()
    {
        Event?.Invoke();
    }
}

[System.Serializable]
public class BergerCommitAction : Action
{
    [Header("Berger Commits")]
    [SerializeField]
    [Multiline]
    string message;

    [SerializeField]
    ChibiBergerHandler.Emotion emotion;

    public override void run()
    {
        ChibiBergerHandler.Instance.Say(message, emotion);
        BergerAI.Instance.BergerCommits(message);
    }
}

[System.Serializable]
public class InstallAction : Action
{
    [Header("Install")]
    [SerializeField]
    string name;

    [SerializeField]
    Sprite icon;

    [SerializeField]
    float duration;

    [SerializeField]
    UnityEvent onCompleted;

    public override void run()
    {
        InstallationHandler.Instance.Install(name, icon, duration, () => onCompleted.Invoke());
    }
}

[System.Serializable]
public class UnlockAction : Action
{
    [Header("Unlock")]
    [SerializeField]
    CommitMessageTextComponent component;

    public override void run()
    {
        if (component != null)
            CommitMessageComposer.Instance.unlockCommit(component);
    }
}