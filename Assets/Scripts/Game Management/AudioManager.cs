using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AudioManager : MonoBehaviour
{
    public AudioObject[] objects;

    public bool LoopMusic;

    bool hasStarted;

    // Start is called before the first frame update
    void Awake()
    {
        foreach (AudioObject a in objects)
        {
            a.setSource(gameObject.AddComponent<AudioSource>());
            a.getSource().pitch = a.pitch;
            a.getSource().volume = a.volume;
            a.getSource().clip = a.audioClip;
        }
        Play("music");
    }


    void Update()
    {
        if (LoopMusic)
        {
            foreach (AudioObject a in objects)
            {
                if (a.name == "music")
                {
                    AudioSource source = a.getSource();
                    if (!source.isPlaying && hasStarted)
                    {
                        Play("music");
                    }
                }
            }
        }
    }

    public void Play(string name)
    {
        Debug.Log("play " + name);
        AudioObject a = Array.Find(objects, AudioObject => AudioObject.name == name);
        a.getSource().Play();
    }



}
