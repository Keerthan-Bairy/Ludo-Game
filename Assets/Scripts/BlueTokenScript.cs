using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class BlueTokenScript : MonoBehaviour
{
    public BluePlayerScript[] mBluePlayers;
    public GameObject[] mInitialPositions;
    public GameObject mArrowAnimator;
    public float mTokenCentreX;
    public float mTokenCentreY;
    public SpriteRenderer mNumberRenderer;
    public int mFinalPositionIndex=0;
    public int mTokenOutCount=0;
    public int mIndex=0;

    private void Start()
    {
        for (int i = 0; i < 4; i++)
        {
            mBluePlayers[i].mInitialPosition = mInitialPositions[i];
        }
        mTokenCentreX = (mBluePlayers[0].transform.position.x + mBluePlayers[2].transform.position.x) / 2;
        mTokenCentreY = (mBluePlayers[0].transform.position.y + mBluePlayers[2].transform.position.y) / 2;
    }

    public void Update()
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
        foreach (BluePlayerScript player in mBluePlayers)
        {
            player.mBlueCanMove = true;
        }
    }

    public void StopMove()
    {
        foreach (BluePlayerScript player in mBluePlayers)
        {
            player.mBlueCanMove = false;
        }
    }

    public void BlueMovePossible()
    {
        foreach (BluePlayerScript BluePlayer in mBluePlayers)
        {
            if (BluePlayer.mTokenOut == true&& BluePlayer.mPlayerFinished==false)
            {
                if (GameManagerScript.mGameManager.mNumberGot > BluePlayer.mNumberOfStepsRemaining)
                {
                    GameManagerScript.mGameManager.mBlueCannotMove++;
                }
            }
        }
    }

    public void CircleEnable()
    {
        foreach (BluePlayerScript Blue in mBluePlayers)
        {
            if (Blue.mPlayerFinished == false && Blue.mTokenOut == false)
            {
                Blue.mChildObject.SetActive(true);
            }
            if (Blue.mPlayerFinished == false && Blue.mTokenOut == true)
            {
                if (GameManagerScript.mGameManager.mNumberGot <= Blue.mNumberOfStepsRemaining)
                {
                    Blue.mChildObject.SetActive(true);
                }
            }
        }
    }
    public void CircleDisable()
    {
        foreach (BluePlayerScript Blue in mBluePlayers)
        {
            Blue.mChildObject.SetActive(false);
        }
    }

    public void OrderInLayer(int inLayer)
    {
        foreach (BluePlayerScript mSprite in mBluePlayers)
        {
            mSprite.GetComponent<SpriteRenderer>().sortingOrder = inLayer;
            mSprite.transform.Find("Player2Color").GetComponent<SpriteRenderer>().sortingOrder = inLayer + 1;
        }
    }

    public void EnableCollider()
    {
        foreach(BluePlayerScript mCollider in mBluePlayers)
        {
            mCollider.GetComponent<CircleCollider2D>().enabled = true;
            mCollider.GetComponent<BoxCollider2D>().enabled = true;
        }
    }

    public void DisableCollider()
    {
        foreach (BluePlayerScript mCollider in mBluePlayers)
        {
            mCollider.GetComponent<CircleCollider2D>().enabled = false;
            mCollider.GetComponent<BoxCollider2D>().enabled = false;
        }
    }

    public void TokenOut()
    {
        mTokenOutCount = 0;
        foreach (BluePlayerScript Blue in mBluePlayers)
        {
            if (Blue.mTokenOut == true)
            {
                mTokenOutCount++;
            }
        }
    }

    public void AutoMoveBlueNotSix()
    {
        foreach (BluePlayerScript Blue in mBluePlayers)
        {
            if (Blue.mTokenOut == true && GameManagerScript.mGameManager.mBluePlayerOut == 1)
            {
                Blue.StartTokenMovement();
            }
        }
    }

    public void AutoMoveBlueForSix()
    {
        foreach (BluePlayerScript Blue in mBluePlayers)
        {
            if (Blue.mTokenOut == true && GameManagerScript.mGameManager.mBluePlayerFinished == 3)
            {
                Blue.StartTokenMovement();
            }
        }
    }

    public void SavePlayerDetails()
    {
        foreach(BluePlayerScript mBlue in mBluePlayers)
        {
            mBlue.SaveDetails();
        }
    }

    public void LoadToken1Details()
    {
        Player2Schema mPlayer2Token1 = JsonConvert.DeserializeObject<Player2Schema>(File.ReadAllText(Application.persistentDataPath + "/Player2Data1.json"));
        mBluePlayers[0].mNumberOfStepsMoved = mPlayer2Token1.mNumberOfStepsMoved;
        mBluePlayers[0].mLastPosition = mPlayer2Token1.mLastPosition;
        mBluePlayers[0].mPositionMoved = mPlayer2Token1.mPositionMoved;
        mBluePlayers[0].mFlag = mPlayer2Token1.mFlag;
        mBluePlayers[0].mPlayerOutIndex = mPlayer2Token1.mPlayerOutIndex;
        mBluePlayers[0].mTokenPosition = mPlayer2Token1.mTokenPosition;
        mBluePlayers[0].mNumberOfStepsRemaining = mPlayer2Token1.mNumberOfStepsRemaining;
        mBluePlayers[0].mIsReadyToMove = mPlayer2Token1.mIsReadyToMove;
        mBluePlayers[0].mBlueCanMove = mPlayer2Token1.mBlueCanMove;
        mBluePlayers[0].mAnotherChance = mPlayer2Token1.mAnotherChance;
        mBluePlayers[0].mTokenMoved = mPlayer2Token1.mTokenMoved;
        mBluePlayers[0].mBlueTokenMoving = mPlayer2Token1.mBlueTokenMoving;
        mBluePlayers[0].mTokenOut = mPlayer2Token1.mTokenOut;
        mBluePlayers[0].mMovePossible = mPlayer2Token1.mMovePossible;
        mBluePlayers[0].mStartingPosition = mPlayer2Token1.mStartingPosition;
        mBluePlayers[0].mPlayerFinished = mPlayer2Token1.mPlayerFinished;
        Vector3 mTokenPosition0;
        mTokenPosition0.x = mPlayer2Token1.mPosition[0];
        mTokenPosition0.y = mPlayer2Token1.mPosition[1];
        mTokenPosition0.z = mPlayer2Token1.mPosition[2];
        mBluePlayers[0].gameObject.transform.position = mTokenPosition0;
        Vector3 mTokenScale0;
        mTokenScale0.x = mPlayer2Token1.mScale[0];
        mTokenScale0.y = mPlayer2Token1.mScale[1];
        mTokenScale0.z = mPlayer2Token1.mScale[2];
        mBluePlayers[0].gameObject.transform.localScale = mTokenScale0;
    }

    public void LoadToken2Details()
    {
        Player2Schema mPlayer2Token2 = JsonConvert.DeserializeObject<Player2Schema>(File.ReadAllText(Application.persistentDataPath + "/Player2Data2.json"));
        mBluePlayers[1].mNumberOfStepsMoved = mPlayer2Token2.mNumberOfStepsMoved;
        mBluePlayers[1].mLastPosition = mPlayer2Token2.mLastPosition;
        mBluePlayers[1].mPositionMoved = mPlayer2Token2.mPositionMoved;
        mBluePlayers[1].mFlag = mPlayer2Token2.mFlag;
        mBluePlayers[1].mPlayerOutIndex = mPlayer2Token2.mPlayerOutIndex;
        mBluePlayers[1].mTokenPosition = mPlayer2Token2.mTokenPosition;
        mBluePlayers[1].mNumberOfStepsRemaining = mPlayer2Token2.mNumberOfStepsRemaining;
        mBluePlayers[1].mIsReadyToMove = mPlayer2Token2.mIsReadyToMove;
        mBluePlayers[1].mBlueCanMove = mPlayer2Token2.mBlueCanMove;
        mBluePlayers[1].mAnotherChance = mPlayer2Token2.mAnotherChance;
        mBluePlayers[1].mTokenMoved = mPlayer2Token2.mTokenMoved;
        mBluePlayers[1].mBlueTokenMoving = mPlayer2Token2.mBlueTokenMoving;
        mBluePlayers[1].mTokenOut = mPlayer2Token2.mTokenOut;
        mBluePlayers[1].mMovePossible = mPlayer2Token2.mMovePossible;
        mBluePlayers[1].mStartingPosition = mPlayer2Token2.mStartingPosition;
        mBluePlayers[1].mPlayerFinished = mPlayer2Token2.mPlayerFinished;
        Vector3 mTokenPosition1;
        mTokenPosition1.x = mPlayer2Token2.mPosition[0];
        mTokenPosition1.y = mPlayer2Token2.mPosition[1];
        mTokenPosition1.z = mPlayer2Token2.mPosition[2];
        mBluePlayers[1].gameObject.transform.position = mTokenPosition1;
        Vector3 mTokenScale1;
        mTokenScale1.x = mPlayer2Token2.mScale[0];
        mTokenScale1.y = mPlayer2Token2.mScale[1];
        mTokenScale1.z = mPlayer2Token2.mScale[2];
        mBluePlayers[1].gameObject.transform.localScale = mTokenScale1;
    }

    public void LoadToken3Details()
    {
        Player2Schema mPlayer2Token3 = JsonConvert.DeserializeObject<Player2Schema>(File.ReadAllText(Application.persistentDataPath + "/Player2Data3.json"));
        mBluePlayers[2].mNumberOfStepsMoved = mPlayer2Token3.mNumberOfStepsMoved;
        mBluePlayers[2].mLastPosition = mPlayer2Token3.mLastPosition;
        mBluePlayers[2].mPositionMoved = mPlayer2Token3.mPositionMoved;
        mBluePlayers[2].mFlag = mPlayer2Token3.mFlag;
        mBluePlayers[2].mPlayerOutIndex = mPlayer2Token3.mPlayerOutIndex;
        mBluePlayers[2].mTokenPosition = mPlayer2Token3.mTokenPosition;
        mBluePlayers[2].mNumberOfStepsRemaining = mPlayer2Token3.mNumberOfStepsRemaining;
        mBluePlayers[2].mIsReadyToMove = mPlayer2Token3.mIsReadyToMove;
        mBluePlayers[2].mBlueCanMove = mPlayer2Token3.mBlueCanMove;
        mBluePlayers[2].mAnotherChance = mPlayer2Token3.mAnotherChance;
        mBluePlayers[2].mTokenMoved = mPlayer2Token3.mTokenMoved;
        mBluePlayers[2].mBlueTokenMoving = mPlayer2Token3.mBlueTokenMoving;
        mBluePlayers[2].mTokenOut = mPlayer2Token3.mTokenOut;
        mBluePlayers[2].mMovePossible = mPlayer2Token3.mMovePossible;
        mBluePlayers[2].mStartingPosition = mPlayer2Token3.mStartingPosition;
        mBluePlayers[2].mPlayerFinished = mPlayer2Token3.mPlayerFinished;
        Vector3 mTokenPosition2;
        mTokenPosition2.x = mPlayer2Token3.mPosition[0];
        mTokenPosition2.y = mPlayer2Token3.mPosition[1];
        mTokenPosition2.z = mPlayer2Token3.mPosition[2];
        mBluePlayers[2].gameObject.transform.position = mTokenPosition2;
        Vector3 mTokenScale2;
        mTokenScale2.x = mPlayer2Token3.mScale[0];
        mTokenScale2.y = mPlayer2Token3.mScale[1];
        mTokenScale2.z = mPlayer2Token3.mScale[2];
        mBluePlayers[2].gameObject.transform.localScale = mTokenScale2;
    }

    public void LoadToken4Details()
    {
        Player2Schema mPlayer2Token4 = JsonConvert.DeserializeObject<Player2Schema>(File.ReadAllText(Application.persistentDataPath + "/Player2Data4.json"));
        mBluePlayers[3].mNumberOfStepsMoved = mPlayer2Token4.mNumberOfStepsMoved;
        mBluePlayers[3].mLastPosition = mPlayer2Token4.mLastPosition;
        mBluePlayers[3].mPositionMoved = mPlayer2Token4.mPositionMoved;
        mBluePlayers[3].mFlag = mPlayer2Token4.mFlag;
        mBluePlayers[3].mPlayerOutIndex = mPlayer2Token4.mPlayerOutIndex;
        mBluePlayers[3].mTokenPosition = mPlayer2Token4.mTokenPosition;
        mBluePlayers[3].mNumberOfStepsRemaining = mPlayer2Token4.mNumberOfStepsRemaining;
        mBluePlayers[3].mIsReadyToMove = mPlayer2Token4.mIsReadyToMove;
        mBluePlayers[3].mBlueCanMove = mPlayer2Token4.mBlueCanMove;
        mBluePlayers[3].mAnotherChance = mPlayer2Token4.mAnotherChance;
        mBluePlayers[3].mTokenMoved = mPlayer2Token4.mTokenMoved;
        mBluePlayers[3].mBlueTokenMoving = mPlayer2Token4.mBlueTokenMoving;
        mBluePlayers[3].mTokenOut = mPlayer2Token4.mTokenOut;
        mBluePlayers[3].mMovePossible = mPlayer2Token4.mMovePossible;
        mBluePlayers[3].mStartingPosition = mPlayer2Token4.mStartingPosition;
        mBluePlayers[3].mPlayerFinished = mPlayer2Token4.mPlayerFinished;
        Vector3 mTokenPosition3;
        mTokenPosition3.x = mPlayer2Token4.mPosition[0];
        mTokenPosition3.y = mPlayer2Token4.mPosition[1];
        mTokenPosition3.z = mPlayer2Token4.mPosition[2];
        mBluePlayers[3].gameObject.transform.position = mTokenPosition3;
        Vector3 mTokenScale3;
        mTokenScale3.x = mPlayer2Token4.mScale[0];
        mTokenScale3.y = mPlayer2Token4.mScale[1];
        mTokenScale3.z = mPlayer2Token4.mScale[2];
        mBluePlayers[3].gameObject.transform.localScale = mTokenScale3;
    }
}

