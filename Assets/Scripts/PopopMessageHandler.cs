using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopopMessageHandler : SingletonBehaviour<PopopMessageHandler>
{
    public void ShowError(string errorMessage)
    {
        Debug.LogError(errorMessage);
    }
}
