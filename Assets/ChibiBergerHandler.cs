using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChibiBergerHandler : SingletonBehaviour<ChibiBergerHandler>
{
    [SerializeField] Animator animator;
    [SerializeField] string[] animatorNamePerEmotion;
    [SerializeField] TMPro.TextMeshProUGUI text;
    [SerializeField] GameObject speechbubble;

    public enum Emotion
    {
        Idle,
        Angry,
        Git,
        Happy,
        Music,
        Objection,
    }

    public void Say(string _message, Emotion _emotion)
    {
        animator.Play(animatorNamePerEmotion[(int)_emotion]);
        text.gameObject.SetActive(true);
        text.text = _message;
        speechbubble.SetActive(true);

        StartCoroutine(SayRoutine());
    }

    private IEnumerator SayRoutine()
    {
        yield return new WaitForSeconds(4);

        text.text = "";
        text.gameObject.SetActive(false);
        speechbubble.SetActive(false);
        animator.Play(animatorNamePerEmotion[(int)Emotion.Idle]);
    }

}
