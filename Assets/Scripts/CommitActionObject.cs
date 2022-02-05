using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu]
public class CommitActionObject : ScriptableObject
{
    [SerializeField]
    private CommitAction action;

    public static implicit operator CommitAction(CommitActionObject b) => b.action;

    private void OnValidate()
    {
        action.validate();
    }
}


[System.Serializable]
public class CommitAction
{
    [SerializeField]
    private string match;

    [SerializeReference]
    private List<Action> actions;

    [SerializeField]
    private ActionType addType;

    public bool bIsMatch(string _source)
    {
        return _source.Contains(match);
    }

    public void run()
    {
        foreach (var item in actions)
            item.run();

    }

    public override string ToString()
    {
        return "Action: " + match;
    }

    public void validate()
    {
        if (addType == ActionType.None)
            return;

        switch (addType)
        {
            case ActionType.Event:
                actions.Add(new EventAction());
                break;
            case ActionType.BergerEmote:
                actions.Add(new BergerEmoteAction());
                break;
            case ActionType.Comment:
                actions.Add(new CommentAction());
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
    BergerEmote,
    Comment,
    BergerCommit,
    InstallApp
}

[System.Serializable]
public abstract class Action
{
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
public class BergerEmoteAction : Action
{
    [Header("BergerEmote")]
    [SerializeField]
    string message;

    public override void run()
    {
        //BergerChibi.instance.say(message,state);
        Debug.Log("Berger says " + message);
    }
}

[System.Serializable]
public class CommentAction : Action
{
    [Header("Comment")]
    [SerializeField]
    [Multiline]
    string message;

    public override void run()
    {
        Debug.Log("CMT:" + message);
    }
}

[System.Serializable]
public class BergerCommitAction : Action
{
    [Header("Berger Commits")]
    [SerializeField]
    [Multiline]
    string message;

    public override void run()
    {
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
