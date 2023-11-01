using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public class UIReload : MonoBehaviour
{

    [SerializeField] private CanvasGroup reloadCanvas;
    [SerializeField] private bool invisibleOnEnable;
    [SerializeField] private float fadeTimeInMS;
    [SerializeField] private GameObject powderSlider;
    [SerializeField] private GameObject sweetspotMarkers;
    private bool fadingIn, fadingOut = false;
    private IEnumerator fadeCoroutine;
    

    [Header(("Prompts"))]
    public string ReloadPrompt;
    public string PourPowderPrompt;
    public string LoadBulletPrompt;
    public string TampPrompt;
    public string ReadyToFirePrompt;

    public TMP_Text Prompt;
    
    
    private void OnEnable()
    {
        reloadCanvas.alpha = invisibleOnEnable ? 0 : 1;
        ChangeState(GunState.NOTREADY, ReloadingState.RELOADINGSTAGE2);
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

    void updatePrompt(string prompt)
    {
        Prompt.text = prompt;
    }

    void ChangeState(GunState gunState, ReloadingState reloadingState)
    {
        switch (reloadingState)
        {
            case ReloadingState.RELOADINGSTAGE1:
                updatePrompt(PourPowderPrompt);
                powderSlider.SetActive(true);
                sweetspotMarkers.SetActive(true);
                break;
            case ReloadingState.RELOADINGSTAGE2:
                updatePrompt(LoadBulletPrompt);
                powderSlider.SetActive(false);
                sweetspotMarkers.SetActive(false);
                break;
            case ReloadingState.RELOADINGSTAGE3:
                updatePrompt(TampPrompt);
                break;
            default:
                break;
        }
    }
    
}