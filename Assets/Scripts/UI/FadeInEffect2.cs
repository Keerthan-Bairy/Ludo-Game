using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeInEffect2 : MonoBehaviour
{
    public Image mImage;

    private void Update()
    {
        RemoveImage();
    }
    public void RemoveImage()
    {
        StartCoroutine(IRemoveImage());
    }

    IEnumerator IRemoveImage()
    {
        yield return new WaitForSeconds(1f);
        mImage.gameObject.SetActive(false);
    }
}
