using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class ReloadUI : MonoBehaviour
{

    [SerializeField] private CanvasGroup reloadCanvas;

    [SerializeField]
    private bool invisibleOnEnable;

    private bool fadingIn, fadingOut = false;

    [SerializeField] private float fadeTimeInMS;
    
    private IEnumerator fadeCoroutine;

    private void OnEnable()
    {
        reloadCanvas.alpha = invisibleOnEnable ? 0 : 1;
        StartCoroutine(fadeInCoroutine());
    }

    public void fadeIn()
    {
        if (fadingIn) return;
        StopAllCoroutines();
        StartCoroutine(fadeInCoroutine());
    }

    public void fadeOut()
    {
        if (fadingOut) return;
        StartCoroutine(fadeOutCoroutine());
    }
    
    IEnumerator fadeInCoroutine()
    {
        reloadCanvas.alpha = 0;
        fadingIn = true;
        var currentTime = 0f;
        var increment = 1.0f / fadeTimeInMS;
        while (currentTime <= fadeTimeInMS)
        {
            currentTime += increment;
            reloadCanvas.alpha = Mathf.Lerp(0, 1, currentTime);
            yield return false;
        }
        fadingIn = false;
    }
    
    IEnumerator fadeOutCoroutine()
    {
        reloadCanvas.alpha = 1;
        fadingOut = true;
        var currentTime = 0f;
        var increment = 1.0f / fadeTimeInMS;
        while (currentTime <= fadeTimeInMS)
        {
            currentTime += increment;
            reloadCanvas.alpha = Mathf.Lerp(1, 0, currentTime);
            yield return false;
        }
        fadingOut = false;
    }
}
