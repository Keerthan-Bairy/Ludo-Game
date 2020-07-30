using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManagerScript : MonoBehaviour
{
    public static GameManagerScript mGameManager;
    [SerializeField] GameObject[] mAllTokens;
    [SerializeField] GameObject[] mArrows;
    private int mRedSixCount;
    private int mBlueSixCount;
    private int mYellowSixCount;
    private int mGreenSixCount;
    public DiceNumberScript mRolledDice;
    public GameObject[] mRollingDice;
    public Sprite[] mNumbers;
    public List<int> mPlayingTokens;
    public List<string> mPlayerFinished;
    public List<string> mPlayerNames;
    public DataScript mData;
    public ParticleSystem mParticleSystem;

    [SerializeField] GameObject mPlayer1;
    [SerializeField] GameObject mPlayer2;
    [SerializeField] GameObject mPlayer3;
    [SerializeField] GameObject mPlayer4;


    private RedTokenScript mRedToken;
    private BlueTokenScript mBlueToken;
    private YellowTokenScript mYellowToken;
    private GreenTokenScript mGreenToken;

    private RedPlayerScript mRedPlayer;
    private BluePlayerScript mBluePlayer;
    private YellowPlayerScript mYellowPlayer;
    private GreenPlayerScript mGreenPlayer;

    public bool mCanClick = false;
    public bool mDiceRolled = false;
    public bool mLastToken = false;
    public bool mTokenMoving;
    public bool mTokenClicked = false;
    public bool mCanRollSix = false;
    public bool mResumeButtonClicked;
    public bool mGameEnded;


    public int mNumberGot;
    public int mPlayerIndex;
    public int mRedPlayerOut=0;
    public int mBluePlayerOut=0;
    public int mYellowPlayerOut=0;
    public int mGreenPlayerOut=0;
    public int mRedPlayerFinished = 0;
    public int mBluePlayerFinished = 0;
    public int mYellowPlayerFinished = 0;
    public int mGreenPlayerFinished = 0;
    public int mRedCannotMove = 0;
    public int mBlueCannotMove = 0;
    public int mYellowCannotMove = 0;
    public int mGreenCannotMove = 0;
    public int mNumberIndex;
    public int mNumberOfSix = 0;
    private void Awake()
    {
        mGameManager = this;
    }
    private void Update()
    {
        if (mDiceRolled == true)
        {
            mCanClick = false;
        }

        //Condition to check whether the current player is last player or not.
        if (mPlayerIndex == mPlayingTokens.Count)
        {
            mLastToken = true;
        }
    }
    private void Start()
    {
        mRedToken = FindObjectOfType<RedTokenScript>();
        mBlueToken = FindObjectOfType<BlueTokenScript>();
        mYellowToken = FindObjectOfType<YellowTokenScript>();
        mGreenToken = FindObjectOfType<GreenTokenScript>();
        mRedPlayer = FindObjectOfType<RedPlayerScript>();
        SoundManagerScript.PlaySound("Load");
        mData = new DataScript();
        mTokenMoving = false;
        LoadColor();
        Load();
        if (mResumeButtonClicked == true)
        {
            LoadGameData();
            LoadTokenData();
            StartGame(mPlayingTokens[mPlayerIndex]);
        }
        else
        {
            for (int i = 0; i < mAllTokens.Length; i++)
            {
                if (mAllTokens[i].activeInHierarchy)
                {
                    mPlayingTokens.Add(i);
                }
            }
            mPlayerIndex = 0;
            mNumberIndex = 0;
            StartGame(mPlayingTokens[mPlayerIndex]);
        }
    }
   
    public void StartGame(int inChoice)
    {
        switch (inChoice) 
        {
            case 0:
                if (mRedPlayerFinished != 4)
                {
                    mRollingDice[0].SetActive(true);
                    mRollingDice[1].SetActive(false);
                    mRollingDice[2].SetActive(false);
                    mRollingDice[3].SetActive(false);
                    mArrows[0].SetActive(true);
                    mArrows[1].SetActive(false);
                    mArrows[2].SetActive(false);
                    mArrows[3].SetActive(false);
                    if (mRedSixCount == 4)
                    {
                        mCanRollSix = true;
                    }

                    mRedToken.OrderInLayer(3);
                    mNumberOfSix = 0;
                    mCanClick = true;
                    mPlayerIndex++;
                    mLastToken = false;
                    mDiceRolled = false;
                    mRedToken.CanMove();
                }
                else
                {
                    mPlayerIndex++;
                    StartGame(mPlayingTokens[mPlayerIndex]);
                }
                break;
            case 1:
                if (mBluePlayerFinished != 4)
                {
                    mRollingDice[0].SetActive(false);
                    mRollingDice[1].SetActive(true);
                    mRollingDice[2].SetActive(false);
                    mRollingDice[3].SetActive(false);
                    mArrows[0].SetActive(false);
                    mArrows[1].SetActive(true);
                    mArrows[2].SetActive(false);
                    mArrows[3].SetActive(false);
                    if (mBlueSixCount == 4)
                    {
                        mCanRollSix = true;
                    }

                    mBlueToken.OrderInLayer(3);
                    mNumberOfSix = 0;
                    mCanClick = true;
                    mPlayerIndex++;
                    mLastToken = false;
                    mDiceRolled = false;
                    mBlueToken.CanMove();
                }
                else
                {
                    mPlayerIndex++;
                    StartGame(mPlayingTokens[mPlayerIndex]);
                }
                break;
            case 2:
                if (mYellowPlayerFinished != 4)
                {
                    mRollingDice[0].SetActive(false);
                    mRollingDice[1].SetActive(false);
                    mRollingDice[2].SetActive(true);
                    mRollingDice[3].SetActive(false);
                    mArrows[0].SetActive(false);
                    mArrows[1].SetActive(false);
                    mArrows[2].SetActive(true);
                    mArrows[3].SetActive(false);
                    if (mYellowSixCount == 4)
                    {
                        mCanRollSix = true;
                    }

                    mYellowToken.OrderInLayer(3);
                    mNumberOfSix = 0;
                    mCanClick = true;
                    mPlayerIndex++;
                    mLastToken = false;
                    mDiceRolled = false;
                    mYellowToken.CanMove();
                }
                else
                {
                    mPlayerIndex++;
                    StartGame(mPlayingTokens[mPlayerIndex]);
                }             
                break;
            case 3:
                if (mGreenPlayerFinished != 4)
                {
                    mRollingDice[0].SetActive(false);
                    mRollingDice[1].SetActive(false);
                    mRollingDice[2].SetActive(false);
                    mRollingDice[3].SetActive(true);
                    mArrows[0].SetActive(false);
                    mArrows[1].SetActive(false);
                    mArrows[2].SetActive(false);
                    mArrows[3].SetActive(true);
                    if (mGreenSixCount == 4)
                    {
                        mCanRollSix = true;
                    }

                    mYellowToken.OrderInLayer(3);
                    mNumberOfSix = 0;
                    mCanClick = true;
                    mPlayerIndex++;
                    mLastToken = false;
                    mDiceRolled = false;
                    mGreenToken.CanMove();
                }
                else
                {
                    mPlayerIndex++;
                    StartGame(mPlayingTokens[mPlayerIndex]);
                }          
                break;
        }
    }
    IEnumerator IMoveToNextPlayer()
    {
        yield return new WaitForSeconds(.75f);
        mRedToken.CircleDisable();
        mBlueToken.CircleDisable();
        mYellowToken.CircleDisable();
        mGreenToken.CircleDisable();
        if (mLastToken == true)
        {
            mPlayerIndex = 0;
            StartGame(mPlayingTokens[mPlayerIndex]);
        }
        else
        {
            MoveToNextPlayer();
        }
       
    }

    //Function to start the game from the first player.
    public void MoveToNextPlayer()
    {
            StartGame(mPlayingTokens[mPlayerIndex]);
    }

    //Function to shift the dice to the next player when no players are ready to move.
    public void SwitchToPlayer()
    {
        if (mRolledDice.CompareTag("Red") && mRedPlayerOut==0)
        {
            StartCoroutine(IMoveToNextPlayer());
        }
        else if (mRolledDice.CompareTag("Blue") && mBluePlayerOut == 0)
        {
            StartCoroutine(IMoveToNextPlayer());
        }
        else if (mRolledDice.CompareTag("Yellow") && mYellowPlayerOut == 0)
        {
            StartCoroutine(IMoveToNextPlayer());
        }
        else if(mRolledDice.CompareTag("Green") && mGreenPlayerOut == 0)
        {
            StartCoroutine(IMoveToNextPlayer());
        }
    }

    //This is a function to Shift the player When the mNumberGot is less than mNumberOfStepsRemaining.
    public void ShiftConditionOne()
    {
        if (mRolledDice.CompareTag("Red") && mRedPlayerOut>0)
        {
            mRedToken.RedMovePossible();
            if (mRedCannotMove == mRedPlayerOut)
            {
                StartCoroutine(IMoveToNextPlayer());
            }
        }
        if (mRolledDice.CompareTag("Blue") && mBluePlayerOut > 0)
        {
            mBlueToken.BlueMovePossible();
            if (mBlueCannotMove == mBluePlayerOut)
            {
                StartCoroutine(IMoveToNextPlayer());
            }
        }
        if (mRolledDice.CompareTag("Yellow") && mYellowPlayerOut > 0)
        {
            mYellowToken.YellowMovePossible();
            if (mYellowCannotMove == mYellowPlayerOut)
            {
                StartCoroutine(IMoveToNextPlayer());
            }
        }
        if (mRolledDice.CompareTag("Green") && mGreenPlayerOut > 0)
        {
            mGreenToken.GreenMovePossible();
            if (mGreenCannotMove == mGreenPlayerOut)
            {
                StartCoroutine(IMoveToNextPlayer());
            }
        }
    }

    public void Players()
    {
        foreach(int number in mPlayingTokens)
        {
            if (number == 0)
            {
                mPlayerNames.Add("Red");
            }
            else if (number == 1)
            {
                mPlayerNames.Add("Blue");
            }
            else if (number == 2)
            {
                mPlayerNames.Add("Yellow");

            }
            else
            {
                mPlayerNames.Add("Green");
            }
        }
    }
    //Functio to shift the player when mnUmberGot is 6 and Token's of respective player has reached home is greater than 0.
    public void ShiftConditionTwo()
    {
        if (mRolledDice.CompareTag("Red") && mRedPlayerFinished>0)
        {
            mRedToken.RedMovePossible();
            mRedToken.TokenOut();
            if (mRedCannotMove == mRedPlayerOut && mRedToken.mTokenOutCount==4)
            {
                StartCoroutine(IMoveToNextPlayer());
            }
        }
        if (mRolledDice.CompareTag("Blue") && mBluePlayerFinished>0)
        {
            mBlueToken.BlueMovePossible();
            mBlueToken.TokenOut();
            if (mBlueCannotMove == mBluePlayerOut && mBlueToken.mTokenOutCount==4)
            {
                StartCoroutine(IMoveToNextPlayer());
            }
        }
        if (mRolledDice.CompareTag("Yellow") && mYellowPlayerFinished>0)
        {
            mYellowToken.YellowMovePossible();
            mYellowToken.TokenOut();
            if (mYellowCannotMove == mYellowPlayerOut && mYellowToken.mTokenOutCount==4)
            {
                StartCoroutine(IMoveToNextPlayer());
            }
        }
        if (mRolledDice.CompareTag("Green") && mGreenPlayerFinished>0)
        {
            mGreenToken.GreenMovePossible();
            mGreenToken.TokenOut();
            if (mGreenCannotMove == mGreenPlayerOut && mGreenToken.mTokenOutCount==4)
            {
                StartCoroutine(IMoveToNextPlayer());
            }
        }
    }
    //Function to shift the dice when we get three 6s continuously
    public void ShiftConditionThree()
    {
        if (mNumberOfSix == 3)
        {
            StartCoroutine(IMoveToNextPlayer());
        }
    }

    //Functio to load the data from Json file.
    public void Load()
    {
        mData = JsonUtility.FromJson<DataScript>(File.ReadAllText(Application.persistentDataPath + "/Data.json"));

        for(int i = 0; i <4; i++)
        {
            if (!mData.mPlayers.Contains(i))
            {
                mAllTokens[i].SetActive(false);
            }
        }
    }
    //Function to activate firework animation when the game ends.
    public void SpawnFireWork()
    {
        StartCoroutine(ISpawnFireWork());
    }

    IEnumerator ISpawnFireWork()
    {
        for (int i = 0; i < 30; i++)
        {
            yield return new WaitForSeconds(0.5f);
            Vector3 mScreenPosition = Camera.main.ScreenToWorldPoint(new Vector3(Random.Range(0, Screen.width), Random.Range(0, Screen.height), Camera.main.farClipPlane / 2));
            Instantiate(mParticleSystem, new Vector3(mScreenPosition.x, mScreenPosition.y, -7), Quaternion.identity);
        }
    }
    //Function to enable all the colliders.
    public void EnableCollider()
    {   
        mRedToken.EnableCollider();
        mBlueToken.EnableCollider();
        mYellowToken.EnableCollider();
        mGreenToken.EnableCollider();
    }
    //Function to disable all the colliders.
    public void DisableCollider()
    {
        mRedToken.DisableCollider();
        mBlueToken.DisableCollider();
        mYellowToken.DisableCollider();
        mGreenToken.DisableCollider();
    }

    public void AllowTokenMove()
    {
        if (mRolledDice.CompareTag("Red"))
        {
            mRedToken.CanMove();
        }
        else if (mRolledDice.CompareTag("Blue"))
        {
            mBlueToken.CanMove();
        }
        else if (mRolledDice.CompareTag("Yellow"))
        {
            mYellowToken.CanMove();
        }
        else if (mRolledDice.CompareTag("Green"))
        {
            mGreenToken.CanMove();
        }
    }

    public void AutoMoveTokensNotSix()
    {
        if (mRolledDice.CompareTag("Red"))
        {
            mRedToken.AutoMoveRedNotSix();
        }
        else if (mRolledDice.CompareTag("Blue"))
        {
            mBlueToken.AutoMoveBlueNotSix();
        }
        else if (mRolledDice.CompareTag("Yellow"))
        {
            mYellowToken.AutoMoveYellowNotSix();
        }
        else if (mRolledDice.CompareTag("Green"))
        {
            mGreenToken.AutoMoveGreenNotSix();
        }
    }

    public void AutoMoveTokenForSix()
    {
        if (mRolledDice.CompareTag("Red"))
        {
            mRedToken.AutoMoveRedForSix();
        }
        else if (mRolledDice.CompareTag("Blue"))
        {
            mBlueToken.AutoMoveBlueForSix();
        }
        else if (mRolledDice.CompareTag("Yellow"))
        {
            mYellowToken.AutoMoveYellowForSix();
        }
        else if (mRolledDice.CompareTag("Green"))
        {
            mGreenToken.AutoMoveGreenForSix();
        }
    }

    public void SixCountInitialize()
    {
        if (mRolledDice.CompareTag("Red"))
        {
            mRedSixCount = 0;
        }
        else if (mRolledDice.CompareTag("Blue"))
        {
            mBlueSixCount = 0;
        }
        else if (mRolledDice.CompareTag("Yellow"))
        {
            mYellowSixCount = 0;
        }
        else if (mRolledDice.CompareTag("Green"))
        {
            mGreenSixCount = 0;
        }
    }

    public void SixCountManager()
    {
        if (mRolledDice.CompareTag("Red") && mRedPlayerOut == 0)
        {
            mRedSixCount += 1;
        }
        else if (mRolledDice.CompareTag("Blue") && mBluePlayerOut == 0)
        {
            mBlueSixCount += 1;
        }
        else if (mRolledDice.CompareTag("Yellow") && mYellowPlayerOut == 0)
        {
            mYellowSixCount += 1;
        }
        else if (mRolledDice.CompareTag("Green") && mGreenPlayerOut == 0)
        {
            mGreenSixCount += 1;
        }
    }


    public void LoadColor()
    {
        ColorScript mColor = JsonUtility.FromJson<ColorScript>(File.ReadAllText(Application.persistentDataPath + "/Color.json")); 
        mPlayer1.GetComponent<SpriteRenderer>().color = mColor.mPlayer1Color;
        mPlayer2.GetComponent<SpriteRenderer>().color = mColor.mPlayer2Color;
        mPlayer3.GetComponent<SpriteRenderer>().color = mColor.mPlayer3Color;
        mPlayer4.GetComponent<SpriteRenderer>().color = mColor.mPlayer4Color;
        mResumeButtonClicked = mColor.mResumeButtonClicked;
    }


    public void SaveGameData()
    {
        GameManagerSchema mGameData = new GameManagerSchema();
        mGameData.mPlayerIndex = mPlayerIndex;
        mGameData.mRedPlayersOut = mRedPlayerOut;
        mGameData.mBluePlayersOut = mBluePlayerOut;
        mGameData.mYellowPlayersOut = mYellowPlayerOut;
        mGameData.mGreenPlayersOut = mGreenPlayerOut;
        mGameData.mRedPlayersFinished = mRedPlayerFinished;
        mGameData.mBluePlayersFinished = mBluePlayerFinished;
        mGameData.mYellowPlayersFinished = mYellowPlayerFinished;
        mGameData.mGreenPlayersFinished = mGreenPlayerFinished;
        mGameData.mRedCannotMove = mRedCannotMove;
        mGameData.mBlueCannotMove = mBlueCannotMove;
        mGameData.mYellowCannotMove = mYellowCannotMove;
        mGameData.mGreenCannotMove = mGreenCannotMove;
        mGameData.mRedSixCount = mRedSixCount;
        mGameData.mBlueSixCount = mBlueSixCount;
        mGameData.mYellowSixCount = mYellowSixCount;
        mGameData.mGreenSixCount = mGreenSixCount;
        mGameData.mPlayingTokens = mPlayingTokens;
        string mJsonData = JsonConvert.SerializeObject(mGameData, Formatting.Indented);
        File.WriteAllText(Application.persistentDataPath + "/GameManagerData.json", mJsonData);

        mRedToken.SavePlayerDetails();
        mBlueToken.SavePlayerDetails();
        mYellowToken.SavePlayerDetails();
        mGreenToken.SavePlayerDetails();
        SaveTokenData();
    }

    public void SaveTokenData()
    {
        TokenDataSchema mTokenData = new TokenDataSchema();
        mTokenData.mPlayer1FinalPositionIndex = mRedToken.mFinalPositionIndex;
        mTokenData.mPlayer2FinalPositionIndex = mBlueToken.mFinalPositionIndex;
        mTokenData.mPlayer3FinalPositionIndex = mYellowToken.mFinalPositionIndex;
        mTokenData.mPlayer4FinalPositionIndex = mGreenToken.mFinalPositionIndex;
        string mPositionData = JsonConvert.SerializeObject(mTokenData, Formatting.Indented);
        File.WriteAllText(Application.persistentDataPath + "/TokenData.json", mPositionData);
    }

    public void LoadTokenData()
    {
        TokenDataSchema mLoadTokenData = JsonConvert.DeserializeObject<TokenDataSchema>(File.ReadAllText(Application.persistentDataPath + "/TokenData.json"));
        mRedToken.mFinalPositionIndex = mLoadTokenData.mPlayer1FinalPositionIndex;
        mBlueToken.mFinalPositionIndex = mLoadTokenData.mPlayer2FinalPositionIndex;
        mYellowToken.mFinalPositionIndex = mLoadTokenData.mPlayer3FinalPositionIndex;
        mGreenToken.mFinalPositionIndex = mLoadTokenData.mPlayer4FinalPositionIndex;
    }

    public void LoadGameData()
    {
        GameManagerSchema mLoadData = JsonConvert.DeserializeObject<GameManagerSchema>(File.ReadAllText(Application.persistentDataPath + "/GameManagerData.json"));
        mPlayerIndex = mLoadData.mPlayerIndex-1;
        mRedPlayerOut = mLoadData.mRedPlayersOut;
        mBluePlayerOut = mLoadData.mBluePlayersOut;
        mYellowPlayerOut = mLoadData.mYellowPlayersOut;
        mGreenPlayerOut = mLoadData.mGreenPlayersOut;
        mRedPlayerFinished = mLoadData.mRedPlayersFinished;
        mBluePlayerFinished = mLoadData.mBluePlayersFinished;
        mYellowPlayerFinished = mLoadData.mYellowPlayersFinished;
        mGreenPlayerFinished = mLoadData.mGreenPlayersFinished;
        mRedCannotMove = mLoadData.mRedCannotMove;
        mBlueCannotMove = mLoadData.mBlueCannotMove;
        mYellowCannotMove = mLoadData.mYellowCannotMove;
        mGreenCannotMove = mLoadData.mGreenCannotMove;
        mRedSixCount = mLoadData.mRedSixCount;
        mBlueSixCount = mLoadData.mBlueSixCount;
        mYellowSixCount = mLoadData.mYellowSixCount;
        mGreenSixCount = mLoadData.mGreenSixCount;
        mPlayingTokens = mLoadData.mPlayingTokens;
    }
}
