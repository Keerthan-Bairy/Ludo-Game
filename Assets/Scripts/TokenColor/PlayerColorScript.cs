using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class PlayerColorScript : MonoBehaviour
{
    private ColorScript mColor;
    private void Start()
    {
        mColor = new ColorScript();
        mColor = JsonUtility.FromJson<ColorScript>(File.ReadAllText(Application.persistentDataPath + "/Color.json"));
        if (gameObject.CompareTag("Player1"))
        {
            gameObject.GetComponent<SpriteRenderer>().color = mColor.mPlayer1Color;
        }
        else if (gameObject.CompareTag("Player2"))
        {
            gameObject.GetComponent<SpriteRenderer>().color = mColor.mPlayer2Color;
        }
        else if (gameObject.CompareTag("Player3"))
        {
            gameObject.GetComponent<SpriteRenderer>().color = mColor.mPlayer3Color;
        }
        else if (gameObject.CompareTag("Player4"))
        {
            gameObject.GetComponent<SpriteRenderer>().color = mColor.mPlayer4Color;
        }

    }
}
