using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InstallBar : MonoBehaviour
{
    [SerializeField] Slider slider;
    [SerializeField] Image logo;
    [SerializeField] TMPro.TextMeshProUGUI nameText;
    [SerializeField] AnimationCurve[] installingCurves;

    public void Init(string _name, Sprite _icon, float _duration, System.Action _onCompleted, bool overrideText)
    {
        StartCoroutine(InstallRoutine(_name, _icon, _duration, _onCompleted, overrideText));
    }

    private IEnumerator InstallRoutine(string _name, Sprite _icon, float _duration, System.Action _onCompleted, bool overrideText)
    {
        logo.sprite = _icon;
        nameText.text = overrideText ? _name : "Installing " + _name;
        AnimationCurve curve = installingCurves[UnityEngine.Random.Range(0, installingCurves.Length)];
        float t = 0;

        while (t < _duration)
        {
            slider.value = curve.Evaluate(t / _duration);
            yield return null;
            t += Time.deltaTime;
        }

        _onCompleted?.Invoke();

        Destroy(gameObject);
    }
}