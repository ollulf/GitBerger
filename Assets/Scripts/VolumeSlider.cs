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
        Debug.Log("SetMasterVolume" + slider.value);

        mixer.SetFloat("Volume", Mathf.Log(slider.value) * 20);
    }
}