using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeSlider : MonoBehaviour
{
    [SerializeField] private Slider slider;
    [SerializeField] private AudioMixer mixer;

    public void SetMasterVolume(Slider volume)
    {
            mixer.SetFloat("Volume", Mathf.Max(Mathf.Log(slider.value) * 20, -80f));
        /*
        if (slider.value < 0.001f)
            mixer.SetFloat("Volume", 0);
        else
        */
        float value;
        mixer.GetFloat("Volume", out value);
        Debug.Log("GetMasterVolume" +  value);
    }
}