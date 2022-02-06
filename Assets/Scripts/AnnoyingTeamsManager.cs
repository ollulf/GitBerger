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

    public GameObject textWindow, window;
    private Animator anim;
    private AudioSource teamsSound;


    public void Start()
    {
        window.SetActive(false);
        anim = window.GetComponent<Animator>();
        teamsSound = gameObject.GetComponent<AudioSource>();
        StartCoroutine(SpawnTeamsMessage());
    }

    public IEnumerator SpawnTeamsMessage()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(minTime, maxTime));
            anim.Play("TeamsFadeIn", 1);
            teamsSound.Play();

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
    }

}
