using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class PopupLoginBehaviour : MonoBehaviour
{
    [SerializeField] TMP_InputField nameField;

    public static System.Action OnLogin;

    public void TryLogin ()
    {
        string nane = nameField.text;

        if (nameField.text == "")
            PopopMessageHandler.Instance.ShowError("Please use a correct user name associated with a GITHUP account!");
        else
        {
            switch (Random.Range(0, 4))
            {
                case 0:
                case 1:
                    Success();
                    break;

                case 2:
                    PopopMessageHandler.Instance.ShowError("The provided password is incorrect. Did you use enough special characters?");
                    break;

                default:
                    PopopMessageHandler.Instance.ShowError("The name '" + nameField.text + "' could no be found in the GITHUP database. Please use a valid account name.");
                    break;
            }
        }
    }

    private void Success()
    {
        Data.Instance.PlayerName = nameField.text;
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
