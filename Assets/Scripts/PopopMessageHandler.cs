using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopopMessageHandler : SingletonBehaviour<PopopMessageHandler>
{
    [SerializeField] PopupMessageUIElement popupPrefab;
    public void ShowError(string errorMessage)
    {
        SpawnPopup().Init(PopupMessageUIElement.Types.Error, errorMessage);
    }

    private PopupMessageUIElement SpawnPopup()
    {
        return Instantiate(popupPrefab, transform);
    }

    internal void ShowModal(string headerText, string textText, string buttonText, System.Action buttonAction)
    {
        SpawnPopup().Init(PopupMessageUIElement.Types.Modal, headerText, textText, buttonText, buttonAction);
    }
}
