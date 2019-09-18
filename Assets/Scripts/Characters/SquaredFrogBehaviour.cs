using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquaredFrogBehaviour : MonoBehaviour, ICharacterable
{
    public AudioClip HopEffect;
    public List<AudioClip> buckEffects;

    private AudioSource _audioPlayer;

    void Start()
    {
        _audioPlayer = GetComponent<AudioSource>();
    }

    void ICharacterable.HopAudioPlay()
    {
        _audioPlayer.PlayOneShot(HopEffect, 0.05f);
    }

    void ICharacterable.Killed()
    {
        throw new System.NotImplementedException();
    }
}
