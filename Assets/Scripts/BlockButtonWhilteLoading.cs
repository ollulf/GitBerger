using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlockButtonWhilteLoading : MonoBehaviour
{
    Button button;
    
    private void OnEnable()
    {
        LoadingHandler.OnSetLoadingState += SetButtonBlocked;
        button = GetComponent<Button>();
    }


    private void OnDisable()
    {
        LoadingHandler.OnSetLoadingState -= SetButtonBlocked;
    }
    private void SetButtonBlocked(bool blocked)
    {
        button.interactable = !blocked;
    }
}
