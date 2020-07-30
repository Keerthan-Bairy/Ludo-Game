using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeInEffect : MonoBehaviour
{
    public Image mImage;
    public void SetAlpha()
    {
        mImage.gameObject.SetActive(true);
        mImage.canvasRenderer.SetAlpha(1.0f);
    }
    public void FadeIn()
    {
        mImage.CrossFadeAlpha(0, 1.5f, false);

    }



}
