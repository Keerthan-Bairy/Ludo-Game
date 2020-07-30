using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class YellowTokenScript : MonoBehaviour
{
    public GameObject mArrowAnimator;
    public YellowPlayerScript[] mYellowPlayers;
    public GameObject[] mInitialPositions;
    public List<string> mAllTokens;
    public SpriteRenderer mNumberRenderer;
    public int mFinalPositionIndex=0;
    public float mTokenCentreX;
    public float mTokenCentreY;
    public int mTokenOutCount = 0;
    private int mIndex = 0;

    private void Start()
    {
        for (int i = 0; i < 4; i++)
        {
            mYellowPlayers[i].mInitialPosition = mInitialPositions[i];
        }
        mTokenCentreX = (mYellowPlayers[0].transform.position.x + mYellowPlayers[2].transform.position.x) / 2;
        mTokenCentreY = (mYellowPlayers[0].transform.position.y + mYellowPlayers[2].transform.position.y) / 2;
    }

    private void Update()
    {
        if (GameManagerScript.mGameManager.mResumeButtonClicked == true && mIndex == 0)
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
        foreach (YellowPlayerScript player in mYellowPlayers)
        {
            player.mYellowCanMove = true;
        }
    }

    public void StopMove()
    {
        foreach (YellowPlayerScript player in mYellowPlayers)
        {
            player.mYellowCanMove = false;
        }
    }

    public void YellowMovePossible()
    {
        foreach (YellowPlayerScript YellowPlayer in mYellowPlayers)
        {
            if (YellowPlayer.mTokenOut == true && YellowPlayer.mPlayerFinished==false)
            {
                if (GameManagerScript.mGameManager.mNumberGot > YellowPlayer.mNumberOfStepsRemaining)
                {
                    GameManagerScript.mGameManager.mYellowCannotMove++;
                }
            }
        }
    }

    public void CircleEnable()
    {
        foreach (YellowPlayerScript Yellow in mYellowPlayers)
        {
            if (Yellow.mPlayerFinished == false && Yellow.mTokenOut == false)
            {
                Yellow.mChildObject.SetActive(true);
            }
            if (Yellow.mPlayerFinished == false && Yellow.mTokenOut == true)
            {
                if (GameManagerScript.mGameManager.mNumberGot <= Yellow.mNumberOfStepsRemaining)
                {
                    Yellow.mChildObject.SetActive(true);
                }
            }
        }
    }
    public void CircleDisable()
    {
        foreach (YellowPlayerScript Yellow in mYellowPlayers)
        {
            Yellow.mChildObject.SetActive(false);
        }
    }

    public void OrderInLayer(int inLayer)
    {
        foreach (YellowPlayerScript mSprite in mYellowPlayers)
        {
            mSprite.GetComponent<SpriteRenderer>().sortingOrder = inLayer;
            mSprite.transform.Find("Player3Color").GetComponent<SpriteRenderer>().sortingOrder = inLayer + 1;
        }
    }

    public void EnableCollider()
    {
        foreach(YellowPlayerScript mCollider in mYellowPlayers)
        {
            mCollider.GetComponent<CircleCollider2D>().enabled = true;
            mCollider.GetComponent<BoxCollider2D>().enabled = true;
        }
    }

    public void DisableCollider()
    {
        foreach (YellowPlayerScript mCollider in mYellowPlayers)
        {
            mCollider.GetComponent<CircleCollider2D>().enabled = false;
            mCollider.GetComponent<BoxCollider2D>().enabled = false;
        }
    }

    public void TokenOut()
    {
        mTokenOutCount = 0;
        foreach (YellowPlayerScript Yellow in mYellowPlayers)
        {
            if (Yellow.mTokenOut == true)
            {
                mTokenOutCount++;
            }
        }
    }

    public void AutoMoveYellowNotSix()
    {
        foreach (YellowPlayerScript Yellow in mYellowPlayers)
        {
            if (Yellow.mTokenOut == true && GameManagerScript.mGameManager.mYellowPlayerOut == 1)
            {
                Yellow.StartTokenMovement();
            }
        }
    }

    public void AutoMoveYellowForSix()
    {
        foreach (YellowPlayerScript Yellow in mYellowPlayers)
        {
            if (Yellow.mTokenOut == true && GameManagerScript.mGameManager.mYellowPlayerFinished == 3)
            {
                Yellow.StartTokenMovement();
            }
        }
    }

    public void SavePlayerDetails()
    {
        foreach(YellowPlayerScript mYellow in mYellowPlayers)
        {
            mYellow.SaveDetails();
        }
    }

    public void LoadToken1Details()
    {
        Player3Schema mPlayer3Token1 = JsonConvert.DeserializeObject<Player3Schema>(File.ReadAllText(Application.persistentDataPath + "/Player3Data1.json"));
        mYellowPlayers[0].mNumberOfStepsMoved = mPlayer3Token1.mNumberOfStepsMoved;
        mYellowPlayers[0].mLastPosition = mPlayer3Token1.mLastPosition;
        mYellowPlayers[0].mPositionMoved = mPlayer3Token1.mPositionMoved;
        mYellowPlayers[0].mFlag = mPlayer3Token1.mFlag;
        mYellowPlayers[0].mPlayerOutIndex = mPlayer3Token1.mPlayerOutIndex;
        mYellowPlayers[0].mTokenPosition = mPlayer3Token1.mTokenPosition;
        mYellowPlayers[0].mNumberOfStepsRemaining = mPlayer3Token1.mNumberOfStepsRemaining;
        mYellowPlayers[0].mIsReadyToMove = mPlayer3Token1.mIsReadyToMove;
        mYellowPlayers[0].mYellowCanMove = mPlayer3Token1.mYellowCanMove;
        mYellowPlayers[0].mAnotherChance = mPlayer3Token1.mAnotherChance;
        mYellowPlayers[0].mTokenMoved = mPlayer3Token1.mTokenMoved;
        mYellowPlayers[0].mYellowTokenMoving = mPlayer3Token1.mYellowTokenMoving;
        mYellowPlayers[0].mTokenOut = mPlayer3Token1.mTokenOut;
        mYellowPlayers[0].mMovePossible = mPlayer3Token1.mMovePossible;
        mYellowPlayers[0].mStartingPosition = mPlayer3Token1.mStartingPosition;
        mYellowPlayers[0].mPlayerFinished = mPlayer3Token1.mPlayerFinished;
        Vector3 mTokenPosition;
        mTokenPosition.x = mPlayer3Token1.mPosition[0];
        mTokenPosition.y = mPlayer3Token1.mPosition[1];
        mTokenPosition.z = mPlayer3Token1.mPosition[2];
        mYellowPlayers[0].gameObject.transform.position = mTokenPosition;
        Vector3 mTokenScale0;
        mTokenScale0.x = mPlayer3Token1.mScale[0];
        mTokenScale0.y = mPlayer3Token1.mScale[1];
        mTokenScale0.z = mPlayer3Token1.mScale[2];
        mYellowPlayers[0].gameObject.transform.localScale = mTokenScale0;
    }

    public void LoadToken2Details()
    {
        Player3Schema mPlayer3Token2 = JsonConvert.DeserializeObject<Player3Schema>(File.ReadAllText(Application.persistentDataPath + "/Player3Data2.json"));
        mYellowPlayers[1].mNumberOfStepsMoved = mPlayer3Token2.mNumberOfStepsMoved;
        mYellowPlayers[1].mLastPosition = mPlayer3Token2.mLastPosition;
        mYellowPlayers[1].mPositionMoved = mPlayer3Token2.mPositionMoved;
        mYellowPlayers[1].mFlag = mPlayer3Token2.mFlag;
        mYellowPlayers[1].mPlayerOutIndex = mPlayer3Token2.mPlayerOutIndex;
        mYellowPlayers[1].mTokenPosition = mPlayer3Token2.mTokenPosition;
        mYellowPlayers[1].mNumberOfStepsRemaining = mPlayer3Token2.mNumberOfStepsRemaining;
        mYellowPlayers[1].mIsReadyToMove = mPlayer3Token2.mIsReadyToMove;
        mYellowPlayers[1].mYellowCanMove = mPlayer3Token2.mYellowCanMove;
        mYellowPlayers[1].mAnotherChance = mPlayer3Token2.mAnotherChance;
        mYellowPlayers[1].mTokenMoved = mPlayer3Token2.mTokenMoved;
        mYellowPlayers[1].mYellowTokenMoving = mPlayer3Token2.mYellowTokenMoving;
        mYellowPlayers[1].mTokenOut = mPlayer3Token2.mTokenOut;
        mYellowPlayers[1].mMovePossible = mPlayer3Token2.mMovePossible;
        mYellowPlayers[1].mStartingPosition = mPlayer3Token2.mStartingPosition;
        mYellowPlayers[1].mPlayerFinished = mPlayer3Token2.mPlayerFinished;
        Vector3 mTokenPosition1;
        mTokenPosition1.x = mPlayer3Token2.mPosition[0];
        mTokenPosition1.y = mPlayer3Token2.mPosition[1];
        mTokenPosition1.z = mPlayer3Token2.mPosition[2];
        mYellowPlayers[1].gameObject.transform.position = mTokenPosition1;
        Vector3 mTokenScale1;
        mTokenScale1.x = mPlayer3Token2.mScale[0];
        mTokenScale1.y = mPlayer3Token2.mScale[1];
        mTokenScale1.z = mPlayer3Token2.mScale[2];
        mYellowPlayers[1].gameObject.transform.localScale = mTokenScale1;
    }

    public void LoadToken3Details()
    {
        Player3Schema mPlayer3Token3 = JsonConvert.DeserializeObject<Player3Schema>(File.ReadAllText(Application.persistentDataPath + "/Player3Data3.json"));
        mYellowPlayers[2].mNumberOfStepsMoved = mPlayer3Token3.mNumberOfStepsMoved;
        mYellowPlayers[2].mLastPosition = mPlayer3Token3.mLastPosition;
        mYellowPlayers[2].mPositionMoved = mPlayer3Token3.mPositionMoved;
        mYellowPlayers[2].mFlag = mPlayer3Token3.mFlag;
        mYellowPlayers[2].mPlayerOutIndex = mPlayer3Token3.mPlayerOutIndex;
        mYellowPlayers[2].mTokenPosition = mPlayer3Token3.mTokenPosition;
        mYellowPlayers[2].mNumberOfStepsRemaining = mPlayer3Token3.mNumberOfStepsRemaining;
        mYellowPlayers[2].mIsReadyToMove = mPlayer3Token3.mIsReadyToMove;
        mYellowPlayers[2].mYellowCanMove = mPlayer3Token3.mYellowCanMove;
        mYellowPlayers[2].mAnotherChance = mPlayer3Token3.mAnotherChance;
        mYellowPlayers[2].mTokenMoved = mPlayer3Token3.mTokenMoved;
        mYellowPlayers[2].mYellowTokenMoving = mPlayer3Token3.mYellowTokenMoving;
        mYellowPlayers[2].mTokenOut = mPlayer3Token3.mTokenOut;
        mYellowPlayers[2].mMovePossible = mPlayer3Token3.mMovePossible;
        mYellowPlayers[2].mStartingPosition = mPlayer3Token3.mStartingPosition;
        mYellowPlayers[2].mPlayerFinished = mPlayer3Token3.mPlayerFinished;
        Vector3 mTokenPosition2;
        mTokenPosition2.x = mPlayer3Token3.mPosition[0];
        mTokenPosition2.y = mPlayer3Token3.mPosition[1];
        mTokenPosition2.z = mPlayer3Token3.mPosition[2];
        mYellowPlayers[2].gameObject.transform.position = mTokenPosition2;
        Vector3 mTokenScale2;
        mTokenScale2.x = mPlayer3Token3.mScale[0];
        mTokenScale2.y = mPlayer3Token3.mScale[1];
        mTokenScale2.z = mPlayer3Token3.mScale[2];
        mYellowPlayers[2].gameObject.transform.localScale = mTokenScale2;
    }

    public void LoadToken4Details()
    {
        Player3Schema mPlayer3Token4 = JsonConvert.DeserializeObject<Player3Schema>(File.ReadAllText(Application.persistentDataPath + "/Player3Data4.json"));
        mYellowPlayers[3].mNumberOfStepsMoved = mPlayer3Token4.mNumberOfStepsMoved;
        mYellowPlayers[3].mLastPosition = mPlayer3Token4.mLastPosition;
        mYellowPlayers[3].mPositionMoved = mPlayer3Token4.mPositionMoved;
        mYellowPlayers[3].mFlag = mPlayer3Token4.mFlag;
        mYellowPlayers[3].mPlayerOutIndex = mPlayer3Token4.mPlayerOutIndex;
        mYellowPlayers[3].mTokenPosition = mPlayer3Token4.mTokenPosition;
        mYellowPlayers[3].mNumberOfStepsRemaining = mPlayer3Token4.mNumberOfStepsRemaining;
        mYellowPlayers[3].mIsReadyToMove = mPlayer3Token4.mIsReadyToMove;
        mYellowPlayers[3].mYellowCanMove = mPlayer3Token4.mYellowCanMove;
        mYellowPlayers[3].mAnotherChance = mPlayer3Token4.mAnotherChance;
        mYellowPlayers[3].mTokenMoved = mPlayer3Token4.mTokenMoved;
        mYellowPlayers[3].mYellowTokenMoving = mPlayer3Token4.mYellowTokenMoving;
        mYellowPlayers[3].mTokenOut = mPlayer3Token4.mTokenOut;
        mYellowPlayers[3].mMovePossible = mPlayer3Token4.mMovePossible;
        mYellowPlayers[3].mStartingPosition = mPlayer3Token4.mStartingPosition;
        mYellowPlayers[3].mPlayerFinished = mPlayer3Token4.mPlayerFinished;
        Vector3 mTokenPosition3;
        mTokenPosition3.x = mPlayer3Token4.mPosition[0];
        mTokenPosition3.y = mPlayer3Token4.mPosition[1];
        mTokenPosition3.z = mPlayer3Token4.mPosition[2];
        mYellowPlayers[3].gameObject.transform.position = mTokenPosition3;
        Vector3 mTokenScale3;
        mTokenScale3.x = mPlayer3Token4.mScale[0];
        mTokenScale3.y = mPlayer3Token4.mScale[1];
        mTokenScale3.z = mPlayer3Token4.mScale[2];
        mYellowPlayers[3].gameObject.transform.localScale = mTokenScale3;
    }
}
