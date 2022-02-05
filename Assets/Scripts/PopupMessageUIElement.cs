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
        Modal,
    }

    private void Start()
    {
        transform.position += new Vector3(UnityEngine.Random.Range(-50, 50), UnityEngine.Random.Range(-50, 50), 0);
    }

    internal void Init(Types modal, string headerText, string textText, string buttonText, System.Action buttonAction)
    {
        headerTextUI.text = headerText;
        textTextUI.text = textText;
        SetButtonContent(buttonText, buttonAction, "Cancle", Close);
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
        HandleButton(buttonUIs[0], text1, action1);
        HandleButton(buttonUIs[1], text2, action2);
    }

    private void HandleButton(Button button, string text, System.Action action)
    {
        button.gameObject.SetActive(text != "");
        button.GetComponentInChildren<TMP_Text>().text = text;
        button.onClick.RemoveAllListeners();
        if (action != null)
        {
            button.onClick.AddListener(new UnityEngine.Events.UnityAction(action));
            button.onClick.AddListener(Close);
        }
    }

    public void Close()
    {
        Destroy(gameObject);
    }
}
