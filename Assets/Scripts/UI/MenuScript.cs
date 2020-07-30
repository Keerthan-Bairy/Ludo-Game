using DG.Tweening;
using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuScript : MonoBehaviour
{
    [SerializeField] Slider mSlider;
    [SerializeField] GameObject mMessageBox;
    [SerializeField] GameObject mLoadingScreen;
    [SerializeField] GameObject mTokenSelectScreen;
    [SerializeField] GameObject mMainMenu;
    [SerializeField] GameObject mRedScore;
    [SerializeField] GameObject mBlueScore;
    [SerializeField] GameObject mYellowScore;
    [SerializeField] GameObject mGreenScore;
    [SerializeField] GameObject mPlayer1Color;
    [SerializeField] GameObject mPlayer2Color;
    [SerializeField] GameObject mPlayer3Color;
    [SerializeField] GameObject mPlayer4Color;


    private string jsonSavePath;
    private int mNumberOfPlayers;
    private int mPlayerIndex;
    private Color mPlayer1;
    private Color mPlayer2;
    private Color mPlayer3;
    private Color mPlayer4;
    public bool mGameEnded;
    public bool mGameEnded1;
    public bool mResumeButtonClicked;
    public DataScript mData;
    private PointsScript mPoints;
    public ColorScript mColor;
    public List<int> mStorePlayers;

    private void Start()
    {
        mNumberOfPlayers = 0;
        mPlayerIndex = 0;
        jsonSavePath = Application.persistentDataPath + "/Data.json";
        mData = new DataScript();
        mPoints = new PointsScript();
        mColor = new ColorScript();
        mMainMenu.SetActive(true);
        LoadColor();
        Load();
        LoadGameEndData();
    }


    public void Save()
    {
        Scene scene = SceneManager.GetActiveScene();
        mData.mSceneName = scene.name;
        mData.mPlayers = mStorePlayers;
        string jsonData = JsonUtility.ToJson(mData);
        File.WriteAllText(jsonSavePath, jsonData);
    }

    public void RedSelect()
    {
        GameObject Red = GameObject.Find("ToggleRed");
        if (Red.GetComponent<Toggle>().isOn)
        {
            if (mPlayerIndex < mNumberOfPlayers)
            {
                mStorePlayers.Add(0);
                mPlayerIndex++;
            }
        }
        else
        {
            mStorePlayers.Remove(0);
            mPlayerIndex--;
        }
       
    }
    
    public void BlueSelect()
    {
        GameObject Blue = GameObject.Find("ToggleBlue");
        if (Blue.GetComponent<Toggle>().isOn)
        {
            if (mPlayerIndex < mNumberOfPlayers)
            {
                mStorePlayers.Add(1);
                mPlayerIndex++;
            }
        }
        else
        {
            mStorePlayers.Remove(1);
            mPlayerIndex--;
        }
    }

    public void YellowSelect()
    {
        GameObject Yellow = GameObject.Find("ToggleYellow");
        if (Yellow.GetComponent<Toggle>().isOn)
        {
            if (mPlayerIndex < mNumberOfPlayers)
            {
                mStorePlayers.Add(2);
                mPlayerIndex++;
            }
        }
        else
        {
            mStorePlayers.Remove(2);
            mPlayerIndex--;
        }
    }

    public void GreenSelect()
    {
        GameObject Green = GameObject.Find("ToggleGreen");
        if (Green.GetComponent<Toggle>().isOn)
        {
            if (mPlayerIndex < mNumberOfPlayers)
            {
                mStorePlayers.Add(3);
                mPlayerIndex++;
            }
        }
        else
        {
            mStorePlayers.Remove(3);
            mPlayerIndex--;
        }
    }

    public void OnTwoPlayerButton()
    {
        mNumberOfPlayers = 2;
    }

    public void OnThreePlayerButton()
    {
        mNumberOfPlayers = 3;
    }

    public void OnFourPlayerButton()
    {
        for(int i = 0; i < 4; i++)
        {
            mStorePlayers.Add(i);
        }
        Save();
        mLoadingScreen.SetActive(true);
        StartCoroutine(StartLoadingGame());
    }

    public void StartButton()
    {
        if (mPlayerIndex == mNumberOfPlayers)
        {
            Save();
            mTokenSelectScreen.SetActive(false);
            mLoadingScreen.SetActive(true);
            StartCoroutine(StartLoadingGame());
        }
        else
        {
            mMessageBox.SetActive(true);
        }       
    }


    IEnumerator StartLoadingGame()
    {
        AsyncOperation mLoading = SceneManager.LoadSceneAsync(2);
        while (!mLoading.isDone)
        {
            float progress = Mathf.Clamp01(mLoading.progress / 0.9f);
            mSlider.value = progress;
            yield return null;
        }
    }

    public void OnCloseButtonSelect()
    {
        mMessageBox.SetActive(false);
    }

    public void OnScoreBoardButtonDown()
    {
        Load();
    }

    public void OnPlayButtonClick()
    {
        if (mGameEnded == true)
        {
            transform.Find("MainMenu").gameObject.SetActive(false);
            transform.Find("PlayOptionsMenu").gameObject.SetActive(true);
      
        }
        else
        {
            transform.Find("MainMenu").gameObject.SetActive(false);
            transform.Find("PlayMenu").gameObject.SetActive(true);
        }
    }

    public void OnResumeButttonClick()
    {
        SaveColor();
        transform.Find("PlayOptionsMenu").gameObject.SetActive(false);
        mLoadingScreen.SetActive(true);
        StartCoroutine(StartLoadingGame());
 
    }
 
    public void Load()
    {
        mPoints = JsonUtility.FromJson<PointsScript>(File.ReadAllText(Application.persistentDataPath + "/Points.json"));
        mRedScore.GetComponent<TextMeshProUGUI>().text = mPoints.mRedPoints.ToString();
        mBlueScore.GetComponent<TextMeshProUGUI>().text = mPoints.mBluePoints.ToString();
        mYellowScore.GetComponent<TextMeshProUGUI>().text = mPoints.mYellowPoints.ToString();
        mGreenScore.GetComponent<TextMeshProUGUI>().text = mPoints.mGreenPoints.ToString();
        mGameEnded1 = mPoints.mGameEnded;
    }

    public void LoadGameEndData()
    {
        mGameEnded = mGameEnded1;
    }

    public void SaveColor()
    {
        ColorScript mNewColor = new ColorScript();
        mNewColor.mPlayer1Color = mPlayer1;
        mNewColor.mPlayer2Color = mPlayer2;
        mNewColor.mPlayer3Color = mPlayer3;
        mNewColor.mPlayer4Color = mPlayer4;
        mNewColor.mResumeButtonClicked = true;
        string mJsonString = JsonUtility.ToJson(mNewColor,true); 
        File.WriteAllText(Application.persistentDataPath + "/Color.json", mJsonString);
    }

    public void LoadColor()
    {
        ColorScript mColor =JsonUtility.FromJson<ColorScript>(File.ReadAllText(Application.persistentDataPath+"/Color.json"));
        mPlayer1Color.GetComponent<Image>().color = mColor.mPlayer1Color;
        mPlayer2Color.GetComponent<Image>().color = mColor.mPlayer2Color;
        mPlayer3Color.GetComponent<Image>().color = mColor.mPlayer3Color;
        mPlayer4Color.GetComponent<Image>().color = mColor.mPlayer4Color;
        mPlayer1 = mColor.mPlayer1Color;
        mPlayer2 = mColor.mPlayer2Color;
        mPlayer3 = mColor.mPlayer3Color;
        mPlayer4 = mColor.mPlayer4Color;
        mResumeButtonClicked = mColor.mResumeButtonClicked;
    }

    public void OnQuit()
    {
        Application.Quit();
    }
}
