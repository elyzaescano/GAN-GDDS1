using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using System;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    public Music[] music;
    void Awake()
    {
        if (instance == null)
            instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);


        foreach (Music m in music)
        {
            m.source = gameObject.AddComponent<AudioSource>();
            m.source.clip = m.clip;

            m.source.volume = m.volume;
            m.source.pitch = m.pitch;
            m.source.loop = m.loop;
            m.source.outputAudioMixerGroup = m.audioMixer;

        }

    }

    private void Start()
    {
        Play("Menu");

    }
    public void Play(string name)
    {
        Music m = Array.Find(music, music => music.name == name);
        if (m == null)
            return;
        m.source.Play();
    }

    public void Stop(string name)
    {
        Music m = Array.Find(music, music => music.name == name);
        if (m == null)
            return;
        m.source.Stop();
    }



}
 