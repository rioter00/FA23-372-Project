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
    [SerializeField] private Slider sweetspotMinSlider, sweetspotMaxSlider, powderMinSlider, powderMaxSlider;
    [SerializeField] private bool fadingIn, fadingOut, visible = false;
    private IEnumerator fadeCoroutine;

    [Header("Slider Handles")] public Image DefaultHandle;
    public Image AdequateHandle;
    
    [Header(("Prompts"))]
    public string ReloadPrompt;
    public string PourPowderPrompt;
    public string LoadBulletPrompt;
    public string TampPrompt;
    public string ReadyToFirePrompt;

    public TMP_Text Prompt;
    
    private void OnEnable()
    {
        setSweetSpotBounds();
        reloadCanvas.alpha = invisibleOnEnable ? 0 : 1;
        visible = !invisibleOnEnable;
    }

    void setSweetSpotBounds()
    {
        sweetspotMinSlider.value = musket.sweetMin;
        sweetspotMaxSlider.value = musket.sweetMax;
        //
        powderMinSlider.value = musket.minPowder;
        powderMaxSlider.value = musket.maxPowder;
    }
    
    private void Update()
    {
        if (musket.gState == GunState.NOTREADY)
        {
            updatePrompt(ReloadPrompt);
            showSliders(false);
        }
        
        if (musket.gState == GunState.NOTREADY && !visible && !fadingIn)
        {
            fadeIn();
        }
        
        if (musket.gState == GunState.RELOADING)
        {
            UpdateReloadState(musket.rState);
        }
        
        if (musket.gState == GunState.READYTOFIRE && !fadingOut && visible)
        {
            fadeOut();
        }
    }

    private void fadeIn()
    {
        if (fadingIn) return;
        StopAllCoroutines();
        StartCoroutine(fadeInCoroutine());
    }

    private void fadeOut()
    {
        if (fadingOut) return;
        StartCoroutine(fadeOutCoroutine());
    }
    
    IEnumerator fadeInCoroutine()
    {
        // reloadCanvas.alpha = 0;
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
                // print("Powder: " + musket.Powder);
                setSweetSpotBounds();
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
        sweetspotMinSlider.gameObject.SetActive(state);
        sweetspotMaxSlider.gameObject.SetActive(state);
        powderMinSlider.gameObject.SetActive(state);
        powderMaxSlider.gameObject.SetActive(state);
    }

    void updateSliderValue(float value)
    {
        powderSlider.value = value;
    }
}