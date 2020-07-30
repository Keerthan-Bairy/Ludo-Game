using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class RedTokenScript : MonoBehaviour
{
    public GameObject mArrowAnimator;
    public GameObject[] mInitialPositions;
    public RedPlayerScript[] mRedPlayers;
    public List<string> mAllTokens;
    public float mTokenCentreX;
    public float mTokenCentreY;
    public int mTokenOutCount = 0;
    public int mFinalPositionIndex = 0;
    public SpriteRenderer mNumberRenderer;
    private int mIndex = 0;  
    

    private void Start()
    {
        for (int i = 0; i < 4; i++)
        {
            mRedPlayers[i].mInitialPosition = mInitialPositions[i];
        }
        mTokenCentreX = (mRedPlayers[0].transform.position.x + mRedPlayers[2].transform.position.x) / 2;
        mTokenCentreY = (mRedPlayers[0].transform.position.y + mRedPlayers[2].transform.position.y) / 2;
    }

    public void Update()
    {
        if(GameManagerScript.mGameManager.mResumeButtonClicked && mIndex == 0)
        {
            mIndex++;
            LoadToken1Details();
            LoadToken2Details();
            LoadToken3Details();
            LoadToken4Details();
        }
    }

    public void CanMove()
    {
        foreach(RedPlayerScript player in mRedPlayers)
        {
            player.mRedCanMove = true;
        }
    }

    public void StopMove()
    {
        foreach(RedPlayerScript player in mRedPlayers)
        {
            player.mRedCanMove = false;
        }
    }

    public void RedMovePossible()
    {
        foreach(RedPlayerScript RedPlayer in mRedPlayers)
        {
            if (RedPlayer.mTokenOut == true && RedPlayer.mPlayerFinished==false)
            {
                if (GameManagerScript.mGameManager.mNumberGot > RedPlayer.mNumberOfStepsRemaining)
                {
                    GameManagerScript.mGameManager.mRedCannotMove++;
                }
            }
        }
    }
    
    public void CircleEnable()
    {
        foreach(RedPlayerScript Red in mRedPlayers)
        {
            if(Red.mPlayerFinished==false && Red.mTokenOut == false)
            {
                Red.mChildObject.SetActive(true);
            }
            if(Red.mPlayerFinished==false && Red.mTokenOut == true)
            {
                if (GameManagerScript.mGameManager.mNumberGot <= Red.mNumberOfStepsRemaining)
                {
                    Red.mChildObject.SetActive(true);
                }
            }
            
        }
    }
    public void CircleDisable()
    {
        foreach (RedPlayerScript Red in mRedPlayers)
        {
            Red.mChildObject.SetActive(false);
        }
    }

    public void OrderInLayer(int inLayer)
    {
        foreach(RedPlayerScript mSprite in mRedPlayers)
        {
            mSprite.GetComponent<SpriteRenderer>().sortingOrder = inLayer;
            mSprite.transform.Find("Player1Color").GetComponent<SpriteRenderer>().sortingOrder = inLayer + 1;
        }
    }

    public void EnableCollider()
    {
       foreach(RedPlayerScript mCollider in mRedPlayers)
        {
            mCollider.GetComponent<CircleCollider2D>().enabled = true;
            mCollider.GetComponent<BoxCollider2D>().enabled = true;
        }
    }


    public void DisableCollider()
    {
        foreach (RedPlayerScript mCollider in mRedPlayers)
        {
            mCollider.GetComponent<CircleCollider2D>().enabled = false;
            mCollider.GetComponent<BoxCollider2D>().enabled = false;
        }
    }

    public void TokenOut()
    {
        mTokenOutCount = 0;
        foreach (RedPlayerScript Red in mRedPlayers)
        {
            if (Red.mTokenOut == true)
            {
                mTokenOutCount++;
            }
        }
    }

    public void AutoMoveRedNotSix()
    {
        foreach(RedPlayerScript Red in mRedPlayers)
        {
            if (Red.mTokenOut == true && GameManagerScript.mGameManager.mRedPlayerOut==1)
            {
                Red.StartTokenMovement();
            }
        }
    }

    public void AutoMoveRedForSix()
    {
        foreach(RedPlayerScript Red in mRedPlayers)
        {
            if(Red.mTokenOut==true && GameManagerScript.mGameManager.mRedPlayerFinished == 3)
            {
                Red.StartTokenMovement();
            }
        }
    }

    public void SavePlayerDetails()
    { 
        foreach (RedPlayerScript mRed in mRedPlayers)
        {
            mRed.SaveDetails();
        }
    }

    

    public void LoadToken1Details()
    {
        Player1Schema mPlayer1Token1 = JsonConvert.DeserializeObject<Player1Schema>(File.ReadAllText(Application.persistentDataPath + "/Player1Data1.json"));
        mRedPlayers[0].mNumberOfStepsMoved = mPlayer1Token1.mNumberOfStepsMoved;
        mRedPlayers[0].mLastPosition = mPlayer1Token1.mLastPosition;
        mRedPlayers[0].mPositionMoved = mPlayer1Token1.mPositionMoved;
        mRedPlayers[0].mFlag = mPlayer1Token1.mFlag;
        mRedPlayers[0].mPlayerOutIndex = mPlayer1Token1.mPlayerOutIndex;
        mRedPlayers[0].mTokenPosition = mPlayer1Token1.mTokenPosition;
        mRedPlayers[0].mNumberOfStepsRemaining = mPlayer1Token1.mNumberOfStepsRemaining;
        mRedPlayers[0].mIsReadyToMove = mPlayer1Token1.mIsReadyToMove;
        mRedPlayers[0].mRedCanMove = mPlayer1Token1.mRedCanMove;
        mRedPlayers[0].mAnotherChance = mPlayer1Token1.mAnotherChance;
        mRedPlayers[0].mTokenMoved = mPlayer1Token1.mTokenMoved;
        mRedPlayers[0].mRedTokenMoving = mPlayer1Token1.mRedTokenMoving;
        mRedPlayers[0].mTokenOut = mPlayer1Token1.mTokenOut;
        mRedPlayers[0].mMovePossible = mPlayer1Token1.mMovePossible;
        mRedPlayers[0].mStartingPosition = mPlayer1Token1.mStartingPosition;
        mRedPlayers[0].mPlayerFinished = mPlayer1Token1.mPlayerFinished;
        Vector3 mTokenPosition;
        mTokenPosition.x = mPlayer1Token1.mPosition[0];
        mTokenPosition.y = mPlayer1Token1.mPosition[1];
        mTokenPosition.z = mPlayer1Token1.mPosition[2];
        mRedPlayers[0].gameObject.transform.position = mTokenPosition;
        Vector3 mTokenScale0;
        mTokenScale0.x = mPlayer1Token1.mScale[0];
        mTokenScale0.y = mPlayer1Token1.mScale[1];
        mTokenScale0.z = mPlayer1Token1.mScale[2];
        mRedPlayers[0].gameObject.transform.localScale = mTokenScale0;
    }

    public void LoadToken2Details()
    {
        Player1Schema mPlayer1Token2 = JsonConvert.DeserializeObject<Player1Schema>(File.ReadAllText(Application.persistentDataPath + "/Player1Data2.json"));
        mRedPlayers[1].mNumberOfStepsMoved = mPlayer1Token2.mNumberOfStepsMoved;
        mRedPlayers[1].mLastPosition = mPlayer1Token2.mLastPosition;
        mRedPlayers[1].mPositionMoved = mPlayer1Token2.mPositionMoved;
        mRedPlayers[1].mFlag = mPlayer1Token2.mFlag;
        mRedPlayers[1].mPlayerOutIndex = mPlayer1Token2.mPlayerOutIndex;
        mRedPlayers[1].mTokenPosition = mPlayer1Token2.mTokenPosition;
        mRedPlayers[1].mNumberOfStepsRemaining = mPlayer1Token2.mNumberOfStepsRemaining;
        mRedPlayers[1].mIsReadyToMove = mPlayer1Token2.mIsReadyToMove;
        mRedPlayers[1].mRedCanMove = mPlayer1Token2.mRedCanMove;
        mRedPlayers[1].mAnotherChance = mPlayer1Token2.mAnotherChance;
        mRedPlayers[1].mTokenMoved = mPlayer1Token2.mTokenMoved;
        mRedPlayers[1].mRedTokenMoving = mPlayer1Token2.mRedTokenMoving;
        mRedPlayers[1].mTokenOut = mPlayer1Token2.mTokenOut;
        mRedPlayers[1].mMovePossible = mPlayer1Token2.mMovePossible;
        mRedPlayers[1].mStartingPosition = mPlayer1Token2.mStartingPosition;
        mRedPlayers[1].mPlayerFinished = mPlayer1Token2.mPlayerFinished;
        Vector3 mTokenPosition1;
        mTokenPosition1.x = mPlayer1Token2.mPosition[0];
        mTokenPosition1.y = mPlayer1Token2.mPosition[1];
        mTokenPosition1.z = mPlayer1Token2.mPosition[2];
        mRedPlayers[1].gameObject.transform.position = mTokenPosition1;
        Vector3 mTokenScale1;
        mTokenScale1.x = mPlayer1Token2.mScale[0];
        mTokenScale1.y = mPlayer1Token2.mScale[1];
        mTokenScale1.z = mPlayer1Token2.mScale[2];
        mRedPlayers[1].gameObject.transform.localScale = mTokenScale1;
    }

    public void LoadToken3Details()
    {
        Player1Schema mPlayer1Token3 = JsonConvert.DeserializeObject<Player1Schema>(File.ReadAllText(Application.persistentDataPath + "/Player1Data3.json"));
        mRedPlayers[2].mNumberOfStepsMoved = mPlayer1Token3.mNumberOfStepsMoved;
        mRedPlayers[2].mLastPosition = mPlayer1Token3.mLastPosition;
        mRedPlayers[2].mPositionMoved = mPlayer1Token3.mPositionMoved;
        mRedPlayers[2].mFlag = mPlayer1Token3.mFlag;
        mRedPlayers[2].mPlayerOutIndex = mPlayer1Token3.mPlayerOutIndex;
        mRedPlayers[2].mTokenPosition = mPlayer1Token3.mTokenPosition;
        mRedPlayers[2].mNumberOfStepsRemaining = mPlayer1Token3.mNumberOfStepsRemaining;
        mRedPlayers[2].mIsReadyToMove = mPlayer1Token3.mIsReadyToMove;
        mRedPlayers[2].mRedCanMove = mPlayer1Token3.mRedCanMove;
        mRedPlayers[2].mAnotherChance = mPlayer1Token3.mAnotherChance;
        mRedPlayers[2].mTokenMoved = mPlayer1Token3.mTokenMoved;
        mRedPlayers[2].mRedTokenMoving = mPlayer1Token3.mRedTokenMoving;
        mRedPlayers[2].mTokenOut = mPlayer1Token3.mTokenOut;
        mRedPlayers[2].mMovePossible = mPlayer1Token3.mMovePossible;
        mRedPlayers[2].mStartingPosition = mPlayer1Token3.mStartingPosition;
        mRedPlayers[2].mPlayerFinished = mPlayer1Token3.mPlayerFinished;
        Vector3 mTokenPosition2;
        mTokenPosition2.x = mPlayer1Token3.mPosition[0];
        mTokenPosition2.y = mPlayer1Token3.mPosition[1];
        mTokenPosition2.z = mPlayer1Token3.mPosition[2];
        mRedPlayers[2].gameObject.transform.position = mTokenPosition2;
        Vector3 mTokenScale2;
        mTokenScale2.x = mPlayer1Token3.mScale[0];
        mTokenScale2.y = mPlayer1Token3.mScale[1];
        mTokenScale2.z = mPlayer1Token3.mScale[2];
        mRedPlayers[2].gameObject.transform.localScale = mTokenScale2;
    }

    public void LoadToken4Details()
    {
        Player1Schema mPlayer1Token4 = JsonConvert.DeserializeObject<Player1Schema>(File.ReadAllText(Application.persistentDataPath + "/Player1Data4.json"));
        mRedPlayers[3].mNumberOfStepsMoved = mPlayer1Token4.mNumberOfStepsMoved;
        mRedPlayers[3].mLastPosition = mPlayer1Token4.mLastPosition;
        mRedPlayers[3].mPositionMoved = mPlayer1Token4.mPositionMoved;
        mRedPlayers[3].mFlag = mPlayer1Token4.mFlag;
        mRedPlayers[3].mPlayerOutIndex = mPlayer1Token4.mPlayerOutIndex;
        mRedPlayers[3].mTokenPosition = mPlayer1Token4.mTokenPosition;
        mRedPlayers[3].mNumberOfStepsRemaining = mPlayer1Token4.mNumberOfStepsRemaining;
        mRedPlayers[3].mIsReadyToMove = mPlayer1Token4.mIsReadyToMove;
        mRedPlayers[3].mRedCanMove = mPlayer1Token4.mRedCanMove;
        mRedPlayers[3].mAnotherChance = mPlayer1Token4.mAnotherChance;
        mRedPlayers[3].mTokenMoved = mPlayer1Token4.mTokenMoved;
        mRedPlayers[3].mRedTokenMoving = mPlayer1Token4.mRedTokenMoving;
        mRedPlayers[3].mTokenOut = mPlayer1Token4.mTokenOut;
        mRedPlayers[3].mMovePossible = mPlayer1Token4.mMovePossible;
        mRedPlayers[3].mStartingPosition = mPlayer1Token4.mStartingPosition;
        mRedPlayers[3].mPlayerFinished = mPlayer1Token4.mPlayerFinished;
        Vector3 mTokenPosition3;
        mTokenPosition3.x = mPlayer1Token4.mPosition[0];
        mTokenPosition3.y = mPlayer1Token4.mPosition[1];
        mTokenPosition3.z = mPlayer1Token4.mPosition[2];
        mRedPlayers[3].gameObject.transform.position = mTokenPosition3;
        Vector3 mTokenScale3;
        mTokenScale3.x = mPlayer1Token4.mScale[0];
        mTokenScale3.y = mPlayer1Token4.mScale[1];
        mTokenScale3.z = mPlayer1Token4.mScale[2];
        mRedPlayers[3].gameObject.transform.localScale = mTokenScale3;
    }
}
