using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class ShootingRangeChatter : MonoBehaviour
{
    [SerializeField] private Transform[] Waypoints;
    [SerializeField] private AudioClip[] clips;
    [SerializeField] private AudioSource ChatterObjects;
    [SerializeField] private List<AudioSource> ChatterObjectPool; 
    [SerializeField] private float timeToTravel;

    //
    [SerializeField] private int LiveChatterObjectLimit = 3;
    [SerializeField] private int LiveChatterObjects;
    
    [SerializeField] private float randomTimeMin, randomTimeMax, randomTime;

    [Range(0, 1)] public float voicesVolume = .8f;
    

    private void OnEnable()
    {
        for (int i = 0; i < LiveChatterObjectLimit; i++)
        {
            var CO = Instantiate(ChatterObjects); 
            CO.gameObject.SetActive(false);
            ChatterObjectPool.Add(CO);
        }
        StartCoroutine(RandomChatter());
    }

    IEnumerator RandomChatter()
    {
        while (true)
        {
            randomTime = Random.Range(randomTimeMin, randomTimeMax);
            yield return new WaitForSeconds(randomTime);
            if (LiveChatterObjects < LiveChatterObjectLimit)
            {
                StartCoroutine(playChatter());
            }
        }
    }

    AudioSource getChatterObject()
    {
        return ChatterObjectPool.First(CO => CO.gameObject.activeSelf == false);
    }
    
    IEnumerator playChatter()
    {
        print("spawning new chatter");
        LiveChatterObjects++;
        var startingWaypoint= Random.Range(0, Waypoints.Length);
        var destinationWaypoint = Random.Range(0, Waypoints.Length);
        var randomClip = clips[Random.Range(0, clips.Length)];
        var ChatterObject = getChatterObject();
        ChatterObject.gameObject.SetActive(true);
        ChatterObject.transform.position = Waypoints[startingWaypoint].position;
        ChatterObject.clip = randomClip;
        var randomPitch = Random.Range(.82f, 1.1f);
        ChatterObject.pitch = randomPitch;
        ChatterObject.volume = voicesVolume;
        ChatterObject.Play();
        float increment = 1 / timeToTravel;
        float runningTravel = 0;
        while (ChatterObject.isPlaying)
        {
            runningTravel += increment;
            ChatterObject.transform.position = Vector3.Lerp(Waypoints[startingWaypoint].position, Waypoints[destinationWaypoint].position, runningTravel);
            yield return null;
        }
        yield return new WaitForSeconds(3);
        ChatterObject.gameObject.SetActive(false);
        LiveChatterObjects--;
    }
}
