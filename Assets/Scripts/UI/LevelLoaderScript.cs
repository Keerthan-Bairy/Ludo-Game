using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class LevelLoaderScript : MonoBehaviour
{

    public Animator mAnimator;
   
    public void Start()
    {
        FadeToLevel();
    }

    public void FadeToLevel()
    {
        StartCoroutine(LoadSplashScreen());
    }
    IEnumerator LoadSplashScreen()
    {
        yield return new WaitForSeconds(5f);
        mAnimator.SetTrigger("FadeOut");
    }

    public void OnSplashScreenAnimationComplete()
    {
        SceneManager.LoadScene(1);
    }
   
}
