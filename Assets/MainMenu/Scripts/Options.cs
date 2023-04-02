using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class Options : MonoBehaviour
{
    [SerializeField] private AudioMixer audioMixer;

    public void SliderMusic(float _volume)
    {

        audioMixer.SetFloat("MusicVol",_volume);
    }

    public void SliderSFX(float _volume)
    {
        audioMixer.SetFloat("SFXVol",_volume);
    }
    public void SliderMaster(float _volume)
    {
        audioMixer.SetFloat("MasterVol",_volume);
    }
}

