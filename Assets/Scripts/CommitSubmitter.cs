using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CommitSubmitter : SingletonBehaviour<CommitSubmitter>
{

    Button button;

    private bool canSubmit;
    private bool bLoading;

    private void OnEnable()
    {
        LoadingHandler.OnSetLoadingState += OnLoadingStateChanged;
        button = GetComponent<Button>();
    }


    private void OnDisable()
    {
        LoadingHandler.OnSetLoadingState -= OnLoadingStateChanged;
    }


    private void OnLoadingStateChanged(bool _value)
    {
        bLoading = _value;
        UpdateButtonInteractable();
    }

    private void UpdateButtonInteractable()
    {
        button.interactable = !bLoading && canSubmit;
    }

    public void setCanSubmit(bool _value)
    {
        canSubmit = _value;
        UpdateButtonInteractable();
    }

    public void Submit()
    {
        CommitListHandler.Instance.Submit();
    }
}
