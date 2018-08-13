using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicController : MonoBehaviour
{
    public AudioClip[] Tracks;

    private int index;
    private AudioSource MusicPlayer;

    void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);
    }
    
    void Start ()
    {
        index = 0;
        MusicPlayer = GetComponent<AudioSource>();
        PlayTrack();
    }

    private void Update()
    {
        if (!MusicPlayer.isPlaying) PlayTrack();
    }

    void PlayTrack()
    {
        MusicPlayer.clip = Tracks[index];
        MusicPlayer.Play();

        index++;

        if (index >= Tracks.Length) index = 0;
    }
}
