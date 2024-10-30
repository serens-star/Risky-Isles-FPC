using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class BackgroundMusicController : MonoBehaviour
{
    public AudioSource backgroundMusicSource;
    public AudioMixerSnapshot defaultSnapshot;
    public AudioMixerSnapshot softBackgroundSnapshot;
    public float transitionTime = 0.5f;
    void Start()
    {
        PlayMusic();
    }

    public void PlayMusic()
    {
        if (backgroundMusicSource != null && !backgroundMusicSource.isPlaying)
        {
            backgroundMusicSource.Play();
        }
    }

    public void StopMusic()
    {
        if (backgroundMusicSource != null && backgroundMusicSource.isPlaying)
        {
            backgroundMusicSource.Stop();
        }
    }

    public void SetMusicVolume(float volume)
    {
        if (backgroundMusicSource != null)
        {
            backgroundMusicSource.volume = volume;
        }
    }

    public void SoftenBackgroundMusic()
    {
        if (softBackgroundSnapshot != null)
        {
          softBackgroundSnapshot.TransitionTo(transitionTime);
        }
    }

    public void RestoreBackgroundMusic()
    {
        if (defaultSnapshot != null)
        {
            defaultSnapshot.TransitionTo(transitionTime);
        }
    }
}
