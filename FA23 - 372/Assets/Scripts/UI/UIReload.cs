using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Debug = System.Diagnostics.Debug;

public class UIReload : MonoBehaviour
{
    [SerializeField] private Musket musket;
    
    [SerializeField] private CanvasGroup reloadCanvas;
    [SerializeField] private bool invisibleOnEnable;
    [SerializeField] private float fadeTimeInMS;
    [SerializeField] private Slider powderSlider;
    [SerializeField] private GameObject sweetspotMarkers;
    [SerializeField] private bool fadingIn, fadingOut, visible = false;
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
        visible = !invisibleOnEnable;
        // ChangeReloadState(GunState.NOTREADY, ReloadingState.RELOADINGSTAGE2);
        // StartCoroutine(fadeInCoroutine());
    }

    private void Update()
    {
        if (musket.gState == GunState.NOTREADY)
        {
            updatePrompt(ReloadPrompt);
            showSliders(false);
        }
        
        // UnityEngine.Debug.Log($"{musket.gState} : {musket.rState}");
        if (musket.gState == GunState.NOTREADY && !visible && !fadingIn)
        {
            StartCoroutine(fadeInCoroutine());
        }

        if (musket.gState == GunState.RELOADING)
        {
            UpdateReloadState(musket.rState);
        }

        if (musket.gState == GunState.READYTOFIRE && !fadingOut && visible)
        {
            StartCoroutine(fadeOutCoroutine());
        }
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
        while (reloadCanvas.alpha < 1)
        {
            currentTime += increment;
            reloadCanvas.alpha = Mathf.Lerp(0, 1, currentTime);
            yield return null;
        }
        fadingIn = false;
        visible = true;
    }
    
    IEnumerator fadeOutCoroutine()
    {
        reloadCanvas.alpha = 1;
        fadingOut = true;
        var currentTime = 0f;
        var increment = 1.0f / fadeTimeInMS;
        while (reloadCanvas.alpha > 0f)
        {
            currentTime += increment;
            reloadCanvas.alpha = Mathf.Lerp(1, 0, currentTime);
            yield return null;
        }
        visible = false;
        fadingOut = false;
    }

    void updatePrompt(string prompt)
    {
        Prompt.text = prompt;
    }

    void UpdateReloadState(ReloadingState reloadingState)
    {
        switch (reloadingState)
        {
            case ReloadingState.RELOADINGSTAGE1:
                updatePrompt(PourPowderPrompt);
                showSliders(true);
                print("Powder: " + musket.Powder);
                updateSliderValue(musket.Powder);
                break;
            case ReloadingState.RELOADINGSTAGE2:
                updatePrompt(LoadBulletPrompt);
                showSliders(false);
                break;
            case ReloadingState.RELOADINGSTAGE3:
                updatePrompt(TampPrompt);
                showSliders(false);
                break;
            default:
                break;
        }
    }

    void showSliders(bool state)
    {
        powderSlider.gameObject.SetActive(state);
        sweetspotMarkers.SetActive(state);
    }

    void updateSliderValue(float value)
    {
        powderSlider.value = value;
    }
}