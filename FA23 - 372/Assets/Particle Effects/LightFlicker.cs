using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightFlicker : MonoBehaviour
{
    [SerializeField] float flickerInstensity = 0.2f;
    [SerializeField] float flickerSpeed = 0.3f;
    [SerializeField] float flickerRandomness = 0.2f;

    private float time;
    private float startingIntensity;
    private Light light;

    void Start()
    {
        light = GetComponent<Light>();
        startingIntensity = light.intensity;
    }

    void Update()
    {
        time += Time.deltaTime * (1 - Random.Range(-flickerRandomness, flickerRandomness)) * Mathf.PI;
        light.intensity = startingIntensity + Mathf.Sin(time * flickerSpeed) * flickerInstensity;
    }
}
