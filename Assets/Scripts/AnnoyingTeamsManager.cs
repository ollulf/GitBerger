using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AnnoyingTeamsManager : SingletonBehaviour<AnnoyingTeamsManager>
{
    [SerializeField]
    public string[] teamsMessages;

    [SerializeField]
    public string[] replace0;

    [SerializeField]
    public string[] replace1;

    public int minTime, maxTime;

    [Tooltip("x=> messages sent, y= min/max multiplyer")]
    public AnimationCurve curveTimeOverMessages = AnimationCurve.Constant(0, 25, 1);

    public GameObject textWindow, window;
    private Animator anim;
    private AudioSource teamsSound;

    private int iterations;

    private bool isInstalled;

    public bool IsInstalled => isInstalled;

    public void Start()
    {
        isInstalled = true;
        window.SetActive(false);
        anim = window.GetComponent<Animator>();
        teamsSound = gameObject.GetComponent<AudioSource>();
        StartCoroutine(SpawnTeamsMessage());
    }

    public IEnumerator SpawnTeamsMessage()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(minTime, maxTime) * curveTimeOverMessages.Evaluate(iterations));
            anim.Play("TeamsFadeIn", 1);
            teamsSound.Play();

            iterations++;
            string message = randomIn(teamsMessages);

            message = string.Format(message, randomIn(replace0), randomIn(replace1));

            textWindow.GetComponent<TMPro.TextMeshProUGUI>().text = message;
            window.SetActive(true);

            while (window.activeSelf)
            {
                yield return new WaitForSeconds(1);
            }
        }


    }

    private T randomIn<T>(T[] arr)
    {
        return arr[Random.Range(0, (arr.Length - 1))];
    }

    public void CloseWindow()
    {
        window.SetActive(false);
    }

    public void CloseTeams()
    {
        StopAllCoroutines();
        CloseWindow();
        isInstalled = false;
    }

}
