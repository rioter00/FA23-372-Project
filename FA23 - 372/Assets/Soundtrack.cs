using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Soundtrack : MonoBehaviour
{
    [SerializeField] private bool dontDestroy;
    [SerializeField] private AudioClip[] AudioClips;
    private AudioSource AudioSource;
    private int clipIndex = 0;

    private bool soundtrackStarted = false;

    private int frameCount;
    public int checkIfPlayingFrameModulo = 24;

    private void Start()
    {
        if (dontDestroy)
        {
            DontDestroyOnLoad(gameObject);
        }

        AudioSource = GetComponent<AudioSource>();
    }

    void setNextClip()
    {
        if (clipIndex >= AudioClips.Length)
        {
            clipIndex = 0;
        }
        
        if (AudioClips[clipIndex] != null)
        {
            AudioSource.clip = AudioClips[clipIndex];
            AudioSource.Play();
            soundtrackStarted = true;
        }
    }

    private void Update()
    {
        if (frameCount % checkIfPlayingFrameModulo == 0)
        {
            if (!AudioSource.isPlaying)
            {
                clipIndex++;
                setNextClip();
            }
        }
        else
        {
            frameCount++;
        }
    }
}
