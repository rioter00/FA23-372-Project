using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Reticle : MonoBehaviour
{
    public Sprite ReadyToFireImage, NotReadyToFireImage;
    [SerializeField] private Image ReticleImage;
    
    // Start is called before the first frame update
    void Start()
    {
        ReticleImage.sprite = ReadyToFireImage;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
