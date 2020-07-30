using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VolumeControllerScript : MonoBehaviour
{
    private AudioSource mAudioSource;
    public float mVolume = 1f;

    private void Start()
    {
        mAudioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        mAudioSource.volume = mVolume;
    }

    public void SetVolume(float inVolume)
    {
        mVolume = inVolume;
    }
}
