using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChickenBehaviour : MonoBehaviour, ICharacterable
{
    public AudioClip HopEffect;
    public List<AudioClip> buckEffects;

    private AudioSource _audioSource;

    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    void ICharacterable.HopAudioPlay()
    {
        _audioSource.PlayOneShot(HopEffect, 0.05f);
    }

    void ICharacterable.Killed()
    {
        throw new System.NotImplementedException();
    }
}
