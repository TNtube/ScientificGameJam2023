using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource soundSource;

    private float coolDown = 1f;
    private float indexTime = 0f;

    private bool transition = false;

    public void PlaySound(AudioClip _newAudioClip)
    {
        //audioPolling.PLAYING
        soundSource.clip = _newAudioClip;
        soundSource.Play();
    }
    
    public void PlayMusic(AudioClip _newAudioClip)
    {
        musicSource.clip = _newAudioClip;
        musicSource.Play();
    }
    
    public void SonorTransition()
    {
        indexTime += Time.deltaTime;

        if (indexTime > coolDown)
        {
            indexTime = 0f;
        }
    }
}
[Serializable]
public class AudioSetting
{
    public AudioClip clip;
    public float maxDistance;
}
