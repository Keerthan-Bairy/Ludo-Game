using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManagerScript : MonoBehaviour
{
    [SerializeField] AudioClip[] mAudioClips;
    public static AudioClip mHomeSound, mKillSound, mMoveSound,mDiceSound,mLoadSound,mGameOver;
    static AudioSource mAudioSource;
    void Start()
    {
        mHomeSound = mAudioClips[0];
        mKillSound = mAudioClips[1];
        mMoveSound = mAudioClips[2];
        mDiceSound = mAudioClips[3];
        mLoadSound = mAudioClips[4];
        mGameOver = mAudioClips[5];
        mAudioSource = GetComponent<AudioSource>();
    }
    public static void PlaySound(string inClip)
    {
        switch (inClip)
        {
            case "Home":
                mAudioSource.PlayOneShot(mHomeSound);
                break;
            case "Kill":
                mAudioSource.PlayOneShot(mKillSound);
                break;
            case "Move":
                mAudioSource.PlayOneShot(mMoveSound);
                break;
            case "Dice":
                mAudioSource.PlayOneShot(mDiceSound);
                break;
            case "Load":
                mAudioSource.PlayOneShot(mLoadSound);
                break;
            case "GameOver":
                mAudioSource.PlayOneShot(mGameOver);
                break;
        }
    }
}
