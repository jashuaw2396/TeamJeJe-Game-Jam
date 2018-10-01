using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource backgroundPlayer;
    public AudioSource sfxPlayer;

    public AudioClip backgroundMusic;
    public AudioClip doorOpen;
    public AudioClip achievement;
    public AudioClip itemCollect;
    public AudioClip landSound;

    // Use this for initialization
    void Start ()
    {
        PlaySong(backgroundMusic);
	}

    public void PlaySong(AudioClip _audio)
    {
        backgroundPlayer.clip = _audio;
        backgroundPlayer.Play();
    }

    public void PlaySFX(AudioClip _audio)
    {
        sfxPlayer.clip = _audio;
        sfxPlayer.Play();
    }
}
