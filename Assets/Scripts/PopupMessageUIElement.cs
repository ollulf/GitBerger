using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PopupMessageUIElement : MonoBehaviour
{
    [SerializeField] Sprite errorSprite;

    [SerializeField] Image iconImageUI;
    [SerializeField] TMP_Text headerTextUI, textTextUI;
    [SerializeField] Button[] buttonUIs;

    public enum Types
    {
        Error,
    }

    private void Start()
    {
        transform.position += new Vector3(UnityEngine.Random.Range(-50, 50), UnityEngine.Random.Range(-50, 50), 0);
    }

    internal void Init(Types error, string errorMessage)
    {
        iconImageUI.sprite = errorSprite;
        headerTextUI.text = "Fatal Error";
        textTextUI.text = errorMessage;
        SetButtonContent();
    }

    private void SetButtonContent(string text1 = "", System.Action action1 = null, string text2 = "", System.Action action2 = null)
    {
        buttonUIs[0].gameObject.SetActive(false);
        buttonUIs[1].gameObject.SetActive(false);
    }
}
