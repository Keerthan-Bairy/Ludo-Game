using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasMenuFader : MonoBehaviour
{
    [SerializeField] Animator mAnimator;
    [SerializeField] GameObject mPlayMenu;
 

    public void OnPlayButtonDown()
    {
        mAnimator.SetTrigger("FadeIn");
        mPlayMenu.SetActive(true);
    }
}
