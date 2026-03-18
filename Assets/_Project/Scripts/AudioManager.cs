using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip flipSound, matchSound, mismatchSound, levelCompleteSound;

    private void Awake()
    {
        if (Instance == null) Instance = this;
    }

    public void PlayFlipSound()
    {
        audioSource.PlayOneShot(flipSound);
    }

    public void PlayMatchSound()
    {
        audioSource.PlayOneShot(matchSound);
    }

    public void PlayMismatchSound()
    {
        audioSource.PlayOneShot(mismatchSound);
    }

    public void PlayLevelCompleteSound()
    {
        audioSource.PlayOneShot(levelCompleteSound);
    }
}