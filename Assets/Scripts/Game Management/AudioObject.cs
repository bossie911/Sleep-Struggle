using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Playables;

[System.Serializable]
public class AudioObject
{
    public string name;


    public float speed;
    [Range(.1f, 2f)]
    public float pitch;
    [Range(0f, 1f)]
    public float volume;

    public AudioClip audioClip;

    AudioSource source;


    void PlaySound() {
    }

    public void setSource(AudioSource newSource) {
        source = newSource;
    }
    public AudioSource getSource() {
        return source;
    }
}
