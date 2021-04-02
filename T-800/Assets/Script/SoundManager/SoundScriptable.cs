using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

[CreateAssetMenu(fileName = "NewSound", menuName = "Sound")]
public class SoundScriptable : ScriptableObject
{
    public string naming = "NamingSound";

    public AudioClip[] clip = { };

    [Range(0, 1)]
    public float volume = .5f;

    public float pitch = 1;

    public bool loop;

    public AudioMixerGroup mixer = null;

    public void Play()
    {
        AudioSource l_Source = SoundManager.GetSource(this);
        l_Source.clip = clip[Random.Range(0, clip.Length)];
        l_Source.pitch = Random.Range(.8f, 1.2f);
        l_Source.Play();
    }

    public void PlayOnce()
    {
        AudioSource l_Source = SoundManager.GetSource(this);
        l_Source.clip = clip[Random.Range(0, clip.Length)];
        l_Source.pitch = Random.Range(.8f, 1.2f);
        if(!l_Source.isPlaying)
        {
            l_Source.Play();
        }
    }
}

