using System;
using UnityEngine;

public class SoundEffectsController : MonoBehaviour
{
    [SerializeField]
    private AudioSource audioSource;

    [SerializeField] private AudioClip winSoundEffect;

    [SerializeField] private AudioClip reelStopSoundEffect;

    private void Start()
    {
        if (!audioSource)
        {
            audioSource = GetComponent<AudioSource>();
        }

        if (!audioSource)
        {
            Debug.LogError(String.Format("Audio source not assigned or found on {0}", gameObject.name));
        }
    }

    public void PlayWinSoundEffect()
    {
        audioSource.PlayOneShot(winSoundEffect);
    }

    public void playReelStopSoundEffect()
    {
        audioSource.PlayOneShot(reelStopSoundEffect);
    }
}
