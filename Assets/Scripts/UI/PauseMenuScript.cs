using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuScript : MonoBehaviour
{
    [SerializeField] GameObject mPauseMenu;
    [SerializeField] GameObject mGameOverMenu;
    [SerializeField] GameObject mWinnerMessage;
    [SerializeField] GameObject mPlayer;

    private Color mPlayer1Color;
    private Color mPlayer2Color;
    private Color mPlayer3Color;
    private Color mPlayer4Color;

    private PointsScript mData;
    private string jsonSavePath;
    private int mIndex = 0;
    private string mWinner;
    private int mRedPoints;
    private int mBluePoints;
    private int mYellowPoints;
    private int mGreenPoints;
    private bool mGameEnded;

    private void Awake()
    {
        mPauseMenu.SetActive(false);
        mGameOverMenu.SetActive(false);
        mWinnerMessage.SetActive(false);
        jsonSavePath = Application.persistentDataPath + "/Points.json";
        mData = new PointsScript();
    }
    private void Start()
    {
        LoadScore();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
                PauseGame();
        }
        GameOver();
    }

    public void ResumeGame()
    {
        mPauseMenu.SetActive(false);
        Time.timeScale = 1f;
    }

    public void PauseGame()
    {
        mPauseMenu.SetActive(true);
        Time.timeScale = 0f;
    }

    public void MenuButton()
    {
        PointsScript mPoints = new PointsScript();
        mPoints.mRedPoints = mRedPoints;
        mPoints.mBluePoints = mBluePoints;
        mPoints.mYellowPoints = mYellowPoints;
        mPoints.mGreenPoints = mGreenPoints;
        mPoints.mGameEnded = true;
        string mjson = JsonConvert.SerializeObject(mPoints, Formatting.Indented);
        File.WriteAllText(Application.persistentDataPath + "/Points.json", mjson); 
        Time.timeScale = 1f;
        SceneManager.LoadScene(1);
    }
    
    public void RestartMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(1);
    }

    public void OnCloseButton()
    {
        mWinnerMessage.SetActive(false);
        mGameOverMenu.SetActive(true);
    }

    public void GameOver()
    {
        if (GameManagerScript.mGameManager.mPlayingTokens.Count == 2)
        {
            if(GameManagerScript.mGameManager.mNumberIndex == 1)
            {
                GameManagerScript.mGameManager.mCanClick = false;
                mWinner = GameManagerScript.mGameManager.mPlayerFinished[0];
                mPlayer.GetComponent<TextMeshProUGUI>().text = mWinner;
                while (mIndex < 1)
                {
                    mWinnerMessage.SetActive(true);
                    SaveScore();
                    OnGameEnd();
                    GameManagerScript.mGameManager.SpawnFireWork();
                    SoundManagerScript.PlaySound("GameOver");
                    mIndex++;
                }             
            }
        }
        else if (GameManagerScript.mGameManager.mPlayingTokens.Count == 3)
        {
            if (GameManagerScript.mGameManager.mNumberIndex == 2)
            {
                GameManagerScript.mGameManager.mCanClick = false;
                mWinner = GameManagerScript.mGameManager.mPlayerFinished[0];
                mPlayer.GetComponent<TextMeshProUGUI>().text = mWinner;           
                while (mIndex < 1)
                {
                    mWinnerMessage.SetActive(true);
                    SaveScore();
                    OnGameEnd();
                    GameManagerScript.mGameManager.SpawnFireWork();
                    SoundManagerScript.PlaySound("GameOver");
                    mIndex++;
                }
            }
        }
        else if (GameManagerScript.mGameManager.mPlayingTokens.Count == 4)
        {
            if(GameManagerScript.mGameManager.mNumberIndex==3)
            {
                GameManagerScript.mGameManager.mCanClick = false;
                mWinner = GameManagerScript.mGameManager.mPlayerFinished[0];
                mPlayer.GetComponent<TextMeshProUGUI>().text = mWinner;
                while (mIndex < 1)
                {
                    mWinnerMessage.SetActive(true);
                    SaveScore();
                    OnGameEnd();
                    GameManagerScript.mGameManager.SpawnFireWork();
                    SoundManagerScript.PlaySound("GameOver");
                    mIndex++;
                }
            }
        }
    }

    public void OnQuit()
    {
        Application.Quit();
    }

    public void SaveScore()
    {
        if(mWinner=="Player 1")
        {
            PointsScript mPoints=new PointsScript();
            mRedPoints += 500;
            mPoints.mRedPoints = mRedPoints;
            mPoints.mBluePoints = mBluePoints;
            mPoints.mYellowPoints = mYellowPoints;
            mPoints.mGreenPoints = mGreenPoints;
            mPoints.mGameEnded = false;
            string jsonData = JsonUtility.ToJson(mPoints);
            File.WriteAllText(jsonSavePath, jsonData);
        }
        else if(mWinner=="Player 2")
        {
            PointsScript mPoints = new PointsScript();
            mBluePoints += 500;
            mPoints.mBluePoints = mBluePoints;
            mPoints.mRedPoints = mRedPoints;
            mPoints.mYellowPoints = mYellowPoints;
            mPoints.mGreenPoints = mGreenPoints;
            mPoints.mGameEnded = false;
            string jsonData = JsonUtility.ToJson(mPoints);
            File.WriteAllText(jsonSavePath, jsonData);
        }
        else if(mWinner=="Player 3")
        {
            PointsScript mPoints = new PointsScript();
            mYellowPoints += 500;
            mPoints.mBluePoints = mBluePoints;
            mPoints.mRedPoints = mRedPoints;
            mPoints.mYellowPoints = mYellowPoints;
            mPoints.mGreenPoints = mGreenPoints;
            mPoints.mGameEnded = false;
            string jsonData = JsonUtility.ToJson(mPoints);
            File.WriteAllText(jsonSavePath, jsonData);
        }
        else if(mWinner=="Player 4")
        {
            PointsScript mPoints = new PointsScript();
            mGreenPoints += 500;
            mPoints.mBluePoints = mBluePoints;
            mPoints.mRedPoints = mRedPoints;
            mPoints.mYellowPoints = mYellowPoints;
            mPoints.mGreenPoints = mGreenPoints;
            mPoints.mGameEnded = false;
            string jsonData = JsonUtility.ToJson(mPoints);
            File.WriteAllText(jsonSavePath, jsonData);
        }
    }

    public void OnSaveButton()
    {
        PointsScript mPoints = new PointsScript();
        mPoints.mRedPoints = mRedPoints;
        mPoints.mBluePoints = mBluePoints;
        mPoints.mYellowPoints = mYellowPoints;
        mPoints.mGreenPoints = mGreenPoints;
        mPoints.mGameEnded = true;
        string mJson = JsonConvert.SerializeObject(mPoints, Formatting.Indented);
        File.WriteAllText(Application.persistentDataPath + "/Points.json", mJson);
    }

    public void SavePauseButton()
    {
        GameEndData mGameEndData = new GameEndData();
        mGameEndData.mGameEnded = true;
    }

    public void OnGameEnd()
    {
        GameEndData mGameEndData = new GameEndData();
        mGameEndData.mGameEnded = false;
    }
    public void LoadScore()
    {
        mData = JsonUtility.FromJson<PointsScript>(File.ReadAllText(jsonSavePath));
        mRedPoints = mData.mRedPoints;
        mBluePoints = mData.mBluePoints;
        mGreenPoints = mData.mGreenPoints;
        mYellowPoints = mData.mYellowPoints;
    }
}
