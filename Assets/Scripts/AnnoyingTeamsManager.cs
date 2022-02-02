using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AnnoyingTeamsManager : MonoBehaviour
{
    [SerializeField]
    public string[] teamsMessages;
    public int minTime,maxTime;

    public GameObject textWindow,window;
    private Animator anim;


    public void Start()
    {
        window.SetActive(false);
        anim = window.GetComponent<Animator>();
        StartCoroutine(SpawnTeamsMessage());
    }

    public IEnumerator SpawnTeamsMessage()
    {

        while (true)
        {
            yield return new WaitForSeconds(Random.Range(minTime,maxTime));
            anim.Play("TeamsFadeIn",1);
            textWindow.GetComponent<TMPro.TextMeshProUGUI>().text = (teamsMessages[Random.Range(0, (teamsMessages.Length-1))]);
            window.SetActive(true);

            while (window.activeSelf)
            {
                yield return new WaitForSeconds(1);
            }
        }


    }


    public void CloseWindow()
    {
        window.SetActive(false);
    }

    public void CloseTeams()
    {
        StopAllCoroutines();
    }

}
