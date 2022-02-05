using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;

public class BergerAI : SingletonBehaviour<BergerAI>
{
    [SerializeField] CommitAction[] actions;
    [SerializeField] CommitActionObject[] actionObjects;

    private void OnEnable()
    {
        CommitListHandler.OnNewPlayerPush += ReactToPlayerCommit;
    }
    private void OnDisable()
    {
        CommitListHandler.OnNewPlayerPush -= ReactToPlayerCommit;
    }
    private void ReactToPlayerCommit(string str)
    {
        List<CommitAction> matches = new List<CommitAction>();

        foreach (CommitAction action in actions)
        {
            if (action.bIsMatch(str))
                matches.Add(action);
        }

        foreach (CommitAction action in actionObjects)
        {
            if (action.bIsMatch(str))
                matches.Add(action);
        }

        foreach (var match in matches)
        {
            Debug.Log("Triggered " + match.ToString());
            match.run();
        }

        string reactionMessage = GetReactionToPlayerCommit(str);
        
    }

    private string GetReactionToPlayerCommit(string str)
    {
        string[] answers = new string[] {
            "For you it's HERR Berger!",
                "This commit at least once a week!",
                "Continue like this and you will not get your certificate!",
                "Capitalism is failing!",
                "Wanna play Minetest later?"
        };

        return answers[UnityEngine.Random.Range(0, answers.Length)];
    }

    public void BergerCommits(string msg)
    {
        StartCoroutine(BergerReactionRoutine(msg));
    }

    private IEnumerator BergerReactionRoutine(string reaction)
    {
        float waitTime = UnityEngine.Random.Range(1f, 2f);
        yield return new WaitForSeconds(waitTime);
        Commit newCommit = new Commit() { Author = Commit.Authors.Berger, DateTime = DateTime.Now, Message = reaction, State = Commit.States.Origin };
        CommitListHandler.Instance.AddBergerCommit(newCommit);
    }

    private void OnValidate()
    {
        foreach (var action in actions)
            action.validate();
    }
}