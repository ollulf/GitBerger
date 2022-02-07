using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class PopupLoginBehaviour : MonoBehaviour
{
    [SerializeField] TMP_InputField nameField;
    [SerializeField] TMP_InputField passwordField;

    public static System.Action OnLogin;
    private int loginCounter = 0;

    public void TryLogin()
    {
        string nane = nameField.text;

        if (nameField.text == "")
            PopopMessageHandler.Instance.ShowError("Please use a correct user name associated with a GITHUP account!");
        else if (string.IsNullOrEmpty(passwordField.text))
            PopopMessageHandler.Instance.ShowError("Invalid password. Your password needs to be at least 6 characters but no more then (12*6)/9");
        else
        {
            switch (loginCounter++)
            {
                case 0:
                    PopopMessageHandler.Instance.ShowError("The name '" + nameField.text + "' could no be found in the GITHUP database. Please use a valid account name.");
                    break;
                case 1:
                    PopopMessageHandler.Instance.ShowError("The provided password is incorrect. Did you use enough special characters?");
                    break;

                default:
                    Success();
                    break;
            }
        }
    }

    private void Success()
    {
        Data.Instance.PlayerName = nameField.text;
        Data.Instance.Password = string.IsNullOrEmpty(passwordField.text) ? "password123" : passwordField.text;
        OnLogin?.Invoke();
        Destroy(transform.parent.gameObject);
    }

    public void TryClose()
    {
        RectTransform rect = transform as RectTransform;
        rect.anchoredPosition = new Vector2(Random.Range(-50, 50), Random.Range(-50, 50));
        EventSystem.current.SetSelectedGameObject(null);
    }
}
