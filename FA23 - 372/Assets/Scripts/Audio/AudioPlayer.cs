using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioPlayer : MonoBehaviour, IPlaySound
{
    public AudioAsset AudioAsset;
    private AudioSource AudioSource;
    // Start is called before the first frame update
    void Start()
    {
        AudioSource = GetComponent<AudioSource>();
        AudioSource.clip = AudioAsset.AudioClip;
        if (AudioAsset.PlayOnEnable)
        {
            AudioSource.Play();
        }
    }

    public void Play()
    {
        throw new System.NotImplementedException();
    }

    public void Play(float delay)
    {
        throw new System.NotImplementedException();
    }
}
