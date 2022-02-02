using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadingHandler : SingletonBehaviour<LoadingHandler>
{
    [SerializeField] Image loadingImageUI;
    public static bool IsLoading => Instance.loadingCoroutine != null;
    public static System.Action<bool> OnSetLoadingState;


    Coroutine loadingCoroutine;

    public void Delay(float delayInSeconds, System.Action delayedAction)
    {
        SetLoading(true);
        loadingCoroutine = StartCoroutine(DelayRoutine(delayInSeconds, delayedAction));
    }


    private IEnumerator DelayRoutine(float delayInSeconds, Action delayedAction)
    {
        yield return new WaitForSeconds(delayInSeconds);
        delayedAction?.Invoke();
        SetLoading(false);
    }
    private void SetLoading(bool isLoading)
    {
        loadingImageUI.enabled = isLoading;
        OnSetLoadingState?.Invoke(isLoading);
    }
}
