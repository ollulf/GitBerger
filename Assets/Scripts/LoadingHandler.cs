using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadingHandler : SingletonBehaviour<LoadingHandler>
{
    [SerializeField] Image loadingImageUI;
    public static bool IsLoading => Instance.loadingCoroutine != null;
    Coroutine loadingCoroutine;

    public void Delay(float delayInSeconds, System.Action delayedAction)
    {
        SetLoadingVisulizationActive(true);
        loadingCoroutine = StartCoroutine(DelayRoutine(delayInSeconds, delayedAction));
    }


    private IEnumerator DelayRoutine(float delayInSeconds, Action delayedAction)
    {
        yield return new WaitForSeconds(delayInSeconds);
        delayedAction?.Invoke();
        SetLoadingVisulizationActive(false);
    }
    private void SetLoadingVisulizationActive(bool active)
    {
        loadingImageUI.enabled = active;
    }
}
