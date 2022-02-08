using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SpotifyManager : SingletonBehaviour<SpotifyManager>
{
    [SerializeField]
    public GameObject canvas;
    public AudioSource player;
    public AudioClip[] musiclist;
    public GameObject text,playbutton;
    public Sprite playsprite, pausesprite;

    [SerializeField] CommitMessageTextComponent toUnlock;

    private int currentSong = -1;
    private bool isPaused = true;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (player.isPlaying == false && isPaused == false)
        {
            player.Stop();
            currentSong++;

            if (currentSong >= musiclist.Length)
            {
                currentSong = 0;
            }

            player.clip = musiclist[currentSong];
            player.Play();
            UpdateTitle();
        }
    }

    public void ToggleMusicPlayer()
    {
        canvas.SetActive(!canvas.activeSelf);
    }
    public void NextSong()
    {
        player.Stop();
        currentSong++;

        if (currentSong >= musiclist.Length)
        {
            currentSong = 0;
        }

        player.clip = musiclist[currentSong];
        player.Play();
        UpdateTitle();
    }

    public void PreviousSong() 
    {
        player.Stop();
        currentSong--;

        if (currentSong < 0)
        {
            currentSong = musiclist.Length-1;
        }

        player.clip = musiclist[currentSong];
        player.Play();
        UpdateTitle();
    }

    public void Play(int id)
    {
        currentSong = id -2;
        NextSong();
    }

    public void TogglePlay()
    {
        CommitMessageComposer.Instance.unlockCommit(toUnlock);

        if (!isPaused)
        {
            player.Pause();
            isPaused = true;
            playbutton.GetComponent<UnityEngine.UI.Image>().overrideSprite = playsprite;
        }
        else
        {
            player.UnPause();
            isPaused = false;
            playbutton.GetComponent<UnityEngine.UI.Image>().overrideSprite = pausesprite;
        }

    }
    public void UpdateTitle()
    {
        text.GetComponent<TMPro.TextMeshProUGUI>().text =(musiclist[currentSong].name);
    }
}
