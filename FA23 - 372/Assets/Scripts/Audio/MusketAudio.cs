using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class MusketAudio : MonoBehaviour
{
    
    [SerializeField] private Musket musket;
    [SerializeField] private AudioSource[] musketAudioSources = new AudioSource[4];

    [SerializeField] private AudioAsset musketPowder, musketBulletLoad;
    [SerializeField] private AudioAssetArray musketTampClips;
    [SerializeField] private AudioAsset musketShoot;

    private GunState previousGunState = GunState.RELOADING;
    private ReloadingState previousReloadingState = ReloadingState.RELOADINGSTAGE3;
    private float prevPowderVal = 0;
    private bool powderIsPlaying = false;
    private int prevTampCount = 0;
    private bool bulletLoaded = false;

    AudioSource getNewAudioSource()
    {
        return musketAudioSources.First(source => !source.isPlaying);
    }
    
    private void Start()
    {
        for (var i = 0; i < musketAudioSources.Length; i++)
        {
            musketAudioSources[i] = gameObject.AddComponent(typeof(AudioSource)) as AudioSource;
        }
        // playBulletLoad();
        // playMusketShoot();
        // playPowderSound();
        // playBulletTamp();
    }

    private void Update()
    {
        // too expensive and hard to change because of the way musket is written..
        // should have been event driven
        if (musket.gState == GunState.RELOADING)
        {
            checkReloadingState();
        }

        checkGunState();
    }

    void checkGunState()
    {
        if (musket.gState == previousGunState) return;
        // musketAudioSource.Stop();
        prevTampCount = 0;
        
        if (musket.gState == GunState.NOTREADY && previousGunState == GunState.READYTOFIRE)
        {
            playMusketShoot();
        }
        previousGunState = musket.gState;
    }

    void checkReloadingState()
    {
        if (musket.rState != previousReloadingState)
        {
            // musketAudioSource.Stop();
            prevTampCount = 0;
        }
        if (musket.rState == ReloadingState.RELOADINGSTAGE1)
        {
            playPowderSound();
        }

        if (musket.rState == ReloadingState.RELOADINGSTAGE2 && previousReloadingState == ReloadingState.RELOADINGSTAGE1)
        {
            foreach (var musketAudioSource in musketAudioSources)
            {
                musketAudioSource.Stop();
            }
        }
        
        if (musket.rState == ReloadingState.RELOADINGSTAGE2)
        {
            bulletLoaded = false;
            // playBulletLoad();
        }
        
        if (musket.rState == ReloadingState.RELOADINGSTAGE3)
        {
            if (!bulletLoaded)
            {
                playBulletLoad();
                bulletLoaded = true;
            }
            if (musket.tamps != prevTampCount)
            {
                playBulletTamp();
            }
        }


        
        previousReloadingState = musket.rState;
    }

    void playBulletTamp()
    {
        var _source = getNewAudioSource();
        print("playing tamp");
        var _rnd = Random.Range(0, musketTampClips.AudioClips.Length);
        var _rndClip = musketTampClips.AudioClips[_rnd];
        var _pitchVariance = Random.Range(-musketTampClips.PitchVarianceFloat, musketTampClips.PitchVarianceFloat);
        var _newPitch = 1 - _pitchVariance;
        _source.clip = _rndClip;
        _source.pitch = _newPitch;
        _source.volume = musketTampClips.Loudness;
        _source.Play();
        prevTampCount = musket.tamps;
    }

    void playPowderSound()
    {
        var _source = getNewAudioSource();
        print("playpowdersound");
        if (musket.Powder > 0f && !powderIsPlaying)
        {
            print("starting powder");
            powderIsPlaying = true;
            _source = getNewAudioSource();
            _source.clip = musketPowder.AudioClip;
            var _pitchVariance = Random.Range(-musketPowder.PitchVarianceFloat, musketPowder.PitchVarianceFloat);
            var _newPitch = 1 - _pitchVariance;
            _source.pitch = _newPitch;
            _source.volume = musketPowder.Loudness;
            _source.Play();
        }
        if(prevPowderVal == musket.Powder && powderIsPlaying)
        {
            _source.Stop();
            powderIsPlaying = false;
            prevPowderVal = 0;
            print("stopping powder");
        }
        prevPowderVal = musket.Powder;
    }

    void playMusketShoot()
    {
        print("playing shoot");
        var _source = getNewAudioSource();
        var _rndClip = musketShoot.AudioClip;
        var _pitchVariance = Random.Range(-musketShoot.PitchVarianceFloat, musketShoot.PitchVarianceFloat);
        var _newPitch = 1 - _pitchVariance;
        _source.clip = _rndClip;
        _source.pitch = _newPitch;
        _source.volume = musketShoot.Loudness;
        _source.Play();
    }

    void playBulletLoad()
    {
        var _source = getNewAudioSource();
        var _rndClip = musketBulletLoad.AudioClip;
        var _pitchVariance = Random.Range(-musketBulletLoad.PitchVarianceFloat, musketBulletLoad.PitchVarianceFloat);
        var _newPitch = 1 - _pitchVariance;
        _source.clip = _rndClip;
        _source.pitch = _newPitch;
        _source.volume = musketBulletLoad.Loudness;
        _source.Play();
        print("played bullet load");
    }
    
}
