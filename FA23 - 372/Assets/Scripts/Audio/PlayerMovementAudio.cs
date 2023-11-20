using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class PlayerMovementAudio : MonoBehaviour
{
    [SerializeField] private PlayerMovement movement;
    [SerializeField] private AudioSource walkingAudioSource, dashingAudioSource;

    [SerializeField] private AudioAssetArray walkingAudioAsset, dashAudioAsset;

    [SerializeField] private float walkingIntervalInSeconds;
    [SerializeField] private float dashingIntervalInSeconds;
    [SerializeField] private bool playingWalking, playingDashing;
    
    // Start is called before the first frame update
    void Start()
    {
        if (walkingAudioSource == null)
        {
            walkingAudioSource = gameObject.AddComponent(typeof(AudioSource)) as AudioSource;
        }
        
        if (dashingAudioSource == null)
        {
            dashingAudioSource = gameObject.AddComponent(typeof(AudioSource)) as AudioSource;
        }

        StartCoroutine(walkingAudio());
        StartCoroutine(dashingAudio());
    }


    IEnumerator walkingAudio()
    {
        while (true)
        {
            if (movement.currentState == PlayerMovement.MovementState.moving && !playingDashing)
            {
                var _rnd = Random.Range(0, walkingAudioAsset.AudioClips.Length);
                var _rndClip = walkingAudioAsset.AudioClips[_rnd];
                var _pitchVariance = Random.Range(0, walkingAudioAsset.PitchVarianceFloat * 2);
                var _newPitch = 1 - walkingAudioAsset.PitchVarianceFloat + _pitchVariance;
                walkingAudioSource.clip = _rndClip;
                walkingAudioSource.pitch = _newPitch;
                walkingAudioSource.Play();
                yield return new WaitForSeconds(walkingIntervalInSeconds);
            }

            yield return null;
        }
    }
    
    IEnumerator dashingAudio()
    {
        while (true)
        {
            if (movement.currentState == PlayerMovement.MovementState.dashing)
            {
                var _rnd = Random.Range(0, dashAudioAsset.AudioClips.Length);
                var _rndClip = dashAudioAsset.AudioClips[_rnd];
                var _pitchVariance = Random.Range(0, dashAudioAsset.PitchVarianceFloat * 2);
                var _newPitch = 1 - dashAudioAsset.PitchVarianceFloat + _pitchVariance;
                dashingAudioSource.clip = _rndClip;
                dashingAudioSource.pitch = _newPitch;
                dashingAudioSource.Play();
                yield return new WaitForSeconds(1);
            }

            yield return null;
        }
    }

    private void OnDestroy()
    {
        StopAllCoroutines();
    }
}
