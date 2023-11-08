using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public class UIWavesAndKills : MonoBehaviour
{

    [SerializeField] private CanvasGroup wavesAndKillsCanvasGroup;
    [SerializeField] private TMP_Text wavesTMPText, killsTMPText;
    

    [SerializeField] private bool invisibleOnEnable = true;

    private bool fadingIn, fadingOut = false;

    [SerializeField] private float fadeTimeInMS;
    
    private IEnumerator fadeCoroutine;
    public AnimationCurve fadeCurve;

    private void OnEnable()
    {
        wavesAndKillsCanvasGroup.alpha = invisibleOnEnable ? 0 : 1;
        // StartCoroutine(fadeInCoroutine());
        fadeIn("Wave One", "10/10", 2000f);
    }

    public void fadeIn()
    {
        if (fadingIn) return;
        StopAllCoroutines();
        StartCoroutine(fadeInCoroutine());
    }
    
    public void fadeIn(string waveText, string killsText)
    {
        if (fadingIn) return;
        StopAllCoroutines();
        wavesTMPText.text = waveText;
        killsTMPText.text = killsText;
        StartCoroutine(fadeInCoroutine());
    }
    
    public void fadeIn(string waveText, string killsText, float fadeTimeInMS)
    {
        if (fadingIn) return;
        StopAllCoroutines();
        fadeTimeInMS = fadeTimeInMS;
        wavesTMPText.text = waveText;
        killsTMPText.text = killsText;
        StartCoroutine(fadeInCoroutine());
    }

    public void setWaveAndKills(string waveText, string killsText)
    {
        wavesTMPText.text = waveText;
        killsTMPText.text = killsText;
    }

    public void setKills(string killsText)
    {
        killsTMPText.text = killsText;
    }

    public void fadeOut()
    {
        if (fadingOut) return;
        StartCoroutine(fadeOutCoroutine());
    }
    
    IEnumerator fadeInCoroutine()
    {
        wavesAndKillsCanvasGroup.alpha = 0;
        fadingIn = true;
        var currentTime = 0f;
        var increment = 1.0f / fadeTimeInMS;
        while (currentTime <= fadeTimeInMS)
        {
            currentTime += increment;
            wavesAndKillsCanvasGroup.alpha = EvaluateCurve(fadeCurve, Mathf.Lerp(1, 0, currentTime));
            // reloadCanvas.alpha = Mathf.Lerp(1, 0, currentTime);
            // print(killsCanvasGroup.alpha);
            yield return false;
        }
        fadingIn = false;
    }
    
    IEnumerator fadeOutCoroutine()
    {
        wavesAndKillsCanvasGroup.alpha = 1;
        fadingOut = true;
        var currentTime = 0f;
        var increment = 1.0f / fadeTimeInMS;
        while (currentTime <= fadeTimeInMS)
        {
            currentTime += increment;
            wavesAndKillsCanvasGroup.alpha = Mathf.Lerp(1, 0, currentTime);
            // print(killsCanvasGroup.alpha);
            yield return false;
        }
        fadingOut = false;
    }

    float EvaluateCurve(AnimationCurve curve, float inputData)
    {
        return curve.Evaluate(inputData);
    }
}

