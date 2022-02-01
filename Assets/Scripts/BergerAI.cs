using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BergerAI : SingletonBehaviour<BergerAI>
{
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
        string reactionMessage = GetReactionToPlayerCommit(str);
        StartCoroutine(BergerReactionRoutine(reactionMessage));
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

    private IEnumerator BergerReactionRoutine(string reaction)
    {
        float waitTime = UnityEngine.Random.Range(1f, 2f);
        yield return new WaitForSeconds(waitTime);
        Commit newCommit = new Commit() { Author = Commit.Authors.Berger, DateTime = DateTime.Now, Message = reaction, State = Commit.States.Origin };
        CommitListHandler.Instance.AddBergerCommit(newCommit);
    }
}
