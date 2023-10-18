using System;
using UnityEngine;

public enum AudioCategory {
    Movement,
    Weapon, 
    FX, 
    Alert,
    Enemy, 
    Character
} 

[System.Serializable]
public struct AudioAsset
{
    public AudioClip AudioClip;
    public bool PlayOnEnable;
    public float Loudness;
    public AudioCategory Category;
    public bool PlayAsMono;
    public bool RandomizePitch;
    public float RandomPitchPercent;
}
