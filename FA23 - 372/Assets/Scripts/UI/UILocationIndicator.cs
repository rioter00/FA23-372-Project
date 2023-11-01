using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public class UILocationIndicator : MonoBehaviour
{

    [SerializeField] private CanvasGroup killsCanvasGroup;

    [SerializeField] private bool invisibleOnEnable = true;

    private bool fadingIn, fadingOut = false;

    [SerializeField] private float fadeTimeInMS;
    
    private IEnumerator fadeCoroutine;
    [SerializeField] private TMP_Text LocationText;
    public AnimationCurve fadeCurve;

    private void OnEnable()
    {
        killsCanvasGroup.alpha = invisibleOnEnable ? 0 : 1;
        // StartCoroutine(fadeInCoroutine());
        fadeIn("Dungeon", 2000f);
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
        LocationText.text = locationText;
        StartCoroutine(fadeInCoroutine());
    }

    public void setLocation(string locationText)
    {
        LocationText.text = locationText;
    }

    public void fadeOut()
    {
        if (fadingOut) return;
        StartCoroutine(fadeOutCoroutine());
    }
    
    IEnumerator fadeInCoroutine()
    {
        killsCanvasGroup.alpha = 0;
        fadingIn = true;
        var currentTime = 0f;
        var increment = 1.0f / fadeTimeInMS;
        while (currentTime <= fadeTimeInMS)
        {
            currentTime += increment;
            killsCanvasGroup.alpha = EvaluateCurve(fadeCurve, Mathf.Lerp(1, 0, currentTime));
            // reloadCanvas.alpha = Mathf.Lerp(1, 0, currentTime);
            print(killsCanvasGroup.alpha);
            yield return false;
        }
        fadingIn = false;
    }
    
    IEnumerator fadeOutCoroutine()
    {
        killsCanvasGroup.alpha = 1;
        fadingOut = true;
        var currentTime = 0f;
        var increment = 1.0f / fadeTimeInMS;
        while (currentTime <= fadeTimeInMS)
        {
            currentTime += increment;
            killsCanvasGroup.alpha = Mathf.Lerp(1, 0, currentTime);
            print(killsCanvasGroup.alpha);
            yield return false;
        }
        fadingOut = false;
    }

    float EvaluateCurve(AnimationCurve curve, float inputData)
    {
        return curve.Evaluate(inputData);
    }
}

