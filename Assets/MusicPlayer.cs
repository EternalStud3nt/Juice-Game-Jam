using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    [SerializeField] AudioClip menuMusic;
    [SerializeField] AudioClip gameplayMusic;
    [SerializeField] AudioClip sickoModeMusic;

    private AudioSource audioSource;
    public static MusicPlayer Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayMenuMusic()
    {
        audioSource.clip = menuMusic;
        audioSource.Play();
    }

    public void PlayGameplayMusic()
    {
        audioSource.clip = gameplayMusic;
        audioSource.Play();
    }

    public void PlaySickoMode()
    {
        audioSource.clip = sickoModeMusic;
        audioSource.Play();
    }
}
