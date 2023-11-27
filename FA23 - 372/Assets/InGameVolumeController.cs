using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class InGameVolumeController : MonoBehaviour
{
    public AudioMixer mixer;
    public Slider musicVolumeSlider, sfxVolumeSlider;
    
    // Start is called before the first frame update

    private void OnEnable()
    {
        getSFXVolume();
        getMusicVolume();
    }
    
    private void getSFXVolume()
    {
        sfxVolumeSlider.value = PlayerPrefs.HasKey("sfxVolume") ? PlayerPrefs.GetFloat("sfxVolume") : 1f;
    }
    
    private void getMusicVolume()
    {
        musicVolumeSlider.value = PlayerPrefs.HasKey("musicVolume") ? PlayerPrefs.GetFloat("musicVolume") : 1f;
    }


    public void setSFXVolume(float value)
    {
        mixer.SetFloat("sfxVolume", Mathf.Log10(value) * 40);
        PlayerPrefs.SetFloat("sfxVolume", value);
    }
    
    public void setMusicVolume(float value)
    {
        mixer.SetFloat("musicVolume", Mathf.Log10(value) * 40);
        PlayerPrefs.SetFloat("musicVolume", value);
    }
    
    private void OnDisable()
    {
        PlayerPrefs.Save();
    }
}
