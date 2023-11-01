using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public class UIKillsRemaining : MonoBehaviour
{

    [SerializeField] private CanvasGroup reloadCanvas;

    [SerializeField] private bool invisibleOnEnable = true;

    private bool fadingIn, fadingOut = false;

    [SerializeField] private float fadeTimeInMS;
    
    private IEnumerator fadeCoroutine;
    [SerializeField] private TMP_Text KillsText;
    public AnimationCurve fadeCurve;

    private void OnEnable()
    {
        reloadCanvas.alpha = invisibleOnEnable ? 0 : 1;
        // StartCoroutine(fadeInCoroutine());
        fadeIn("10/10", 2000f);
    }

    public void fadeIn()
    {
        if (fadingIn) return;
        StopAllCoroutines();
        StartCoroutine(fadeInCoroutine());
    }
    
    public void fadeIn(string locationText)
    {
        if (fadingIn) return;
        StopAllCoroutines();
        locationText = locationText;
        StartCoroutine(fadeInCoroutine());
    }
    
    public void fadeIn(string locationText, float fadeTimeInMS)
    {
        if (fadingIn) return;
        StopAllCoroutines();
        fadeTimeInMS = fadeTimeInMS;
        KillsText.text = locationText;
        StartCoroutine(fadeInCoroutine());
    }

    public void setLocation(string locationText)
    {
        KillsText.text = locationText;
    }

    public void fadeOut()
    {
        if (fadingOut) return;
        StartCoroutine(fadeOutCoroutine());
    }

    public void UpdateText(string newText)
    {
        KillsText.text = newText;
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
            reloadCanvas.alpha = EvaluateCurve(fadeCurve, Mathf.Lerp(1, 0, currentTime));
            // reloadCanvas.alpha = Mathf.Lerp(1, 0, currentTime);
            print(reloadCanvas.alpha);
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
            print(reloadCanvas.alpha);
            yield return false;
        }
        fadingOut = false;
    }

    float EvaluateCurve(AnimationCurve curve, float inputData)
    {
        return curve.Evaluate(inputData);
    }
}

