using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class GreenTokenScript : MonoBehaviour
{
    public GameObject mArrowAnimator;
    public GreenPlayerScript[] mGreenPlayers;
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
            mGreenPlayers[i].mInitialPosition = mInitialPositions[i];
        }
        mTokenCentreX = (mGreenPlayers[0].transform.position.x + mGreenPlayers[2].transform.position.x) / 2;
        mTokenCentreY = (mGreenPlayers[0].transform.position.y + mGreenPlayers[2].transform.position.y) / 2;
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
        foreach (GreenPlayerScript player in mGreenPlayers)
        {
            player.mGreenCanMove = true;
        }
    }

    public void StopMove()
    {
        foreach (GreenPlayerScript player in mGreenPlayers)
        {
            player.mGreenCanMove = false;
        }
    }

    public void GreenMovePossible()
    {
        foreach (GreenPlayerScript GreenPlayer in mGreenPlayers)
        {
            if (GreenPlayer.mTokenOut == true && GreenPlayer.mPlayerFinished==false)
            {
                if (GameManagerScript.mGameManager.mNumberGot > GreenPlayer.mNumberOfStepsRemaining)
                {
                    GameManagerScript.mGameManager.mGreenCannotMove++;
                }
            }
        }
    }

    public void CircleEnable()
    {
        foreach (GreenPlayerScript Green in mGreenPlayers)
        {
            if (Green.mPlayerFinished == false && Green.mTokenOut == false)
            {
                Green.mChildObject.SetActive(true);
            }
            if (Green.mPlayerFinished == false && Green.mTokenOut == true)
            {
                if (GameManagerScript.mGameManager.mNumberGot <= Green.mNumberOfStepsRemaining)
                {
                    Green.mChildObject.SetActive(true);
                }
            }
        }
    }
    public void CircleDisable()
    {
        foreach (GreenPlayerScript Green in mGreenPlayers)
        {
            Green.mChildObject.SetActive(false);
        }
    }

    public void OrderInLayer(int inLayer)
    {
        foreach (GreenPlayerScript mSprite in mGreenPlayers)
        {
            mSprite.GetComponent<SpriteRenderer>().sortingOrder = inLayer;
            mSprite.transform.Find("Player4Color").GetComponent<SpriteRenderer>().sortingOrder = inLayer + 1; 
        }
    }

    public void EnableCollider()
    {
        foreach(GreenPlayerScript mCollider in mGreenPlayers)
        {
            mCollider.GetComponent<CircleCollider2D>().enabled = true;
            mCollider.GetComponent<BoxCollider2D>().enabled = true;
        }
    }

    public void DisableCollider()
    {
        foreach (GreenPlayerScript mCollider in mGreenPlayers)
        {
            mCollider.GetComponent<CircleCollider2D>().enabled = false;
            mCollider.GetComponent<BoxCollider2D>().enabled = false;
        }
    }

    public void TokenOut()
    {
        mTokenOutCount = 0;
        foreach (GreenPlayerScript Green in mGreenPlayers)
        {
            if (Green.mTokenOut == true)
            {
                mTokenOutCount++;
            }
        }
    }

    public void AutoMoveGreenNotSix()
    {
        foreach (GreenPlayerScript Green in mGreenPlayers)
        {
            if (Green.mTokenOut == true && GameManagerScript.mGameManager.mGreenPlayerOut == 1)
            {
                Green.StartTokenMovement();
            }
        }
    }

    public void AutoMoveGreenForSix()
    {
        foreach (GreenPlayerScript Green in mGreenPlayers)
        {
            if (Green.mTokenOut == true && GameManagerScript.mGameManager.mGreenPlayerFinished == 3)
            {
                Green.StartTokenMovement();
            }
        }
    }

    public void SavePlayerDetails()
    {
        foreach(GreenPlayerScript mGreen in mGreenPlayers)
        {
            mGreen.SaveDetails();
        }
    }

    public void LoadToken1Details()
    {
        Player4Schema mPlayer4Token1 = JsonConvert.DeserializeObject<Player4Schema>(File.ReadAllText(Application.persistentDataPath + "/Player4Data1.json"));
        mGreenPlayers[0].mNumberOfStepsMoved = mPlayer4Token1.mNumberOfStepsMoved;
        mGreenPlayers[0].mLastPosition = mPlayer4Token1.mLastPosition;
        mGreenPlayers[0].mPositionMoved = mPlayer4Token1.mPositionMoved;
        mGreenPlayers[0].mFlag = mPlayer4Token1.mFlag;
        mGreenPlayers[0].mPlayerOutIndex = mPlayer4Token1.mPlayerOutIndex;
        mGreenPlayers[0].mTokenPosition = mPlayer4Token1.mTokenPosition;
        mGreenPlayers[0].mNumberOfStepsRemaining = mPlayer4Token1.mNumberOfStepsRemaining;
        mGreenPlayers[0].mIsReadyToMove = mPlayer4Token1.mIsReadyToMove;
        mGreenPlayers[0].mGreenCanMove = mPlayer4Token1.mGreenCanMove;
        mGreenPlayers[0].mAnotherChance = mPlayer4Token1.mAnotherChance;
        mGreenPlayers[0].mTokenMoved = mPlayer4Token1.mTokenMoved;
        mGreenPlayers[0].mGreenTokenMoving = mPlayer4Token1.mGreenTokenMoving;
        mGreenPlayers[0].mTokenOut = mPlayer4Token1.mTokenOut;
        mGreenPlayers[0].mMovePossible = mPlayer4Token1.mMovePossible;
        mGreenPlayers[0].mStartingPosition = mPlayer4Token1.mStartingPosition;
        mGreenPlayers[0].mPlayerFinished = mPlayer4Token1.mPlayerFinished;
        Vector3 mTokenPosition;
        mTokenPosition.x = mPlayer4Token1.mPosition[0];
        mTokenPosition.y = mPlayer4Token1.mPosition[1];
        mTokenPosition.z = mPlayer4Token1.mPosition[2];
        mGreenPlayers[0].gameObject.transform.position = mTokenPosition;
        Vector3 mTokenScale0;
        mTokenScale0.x = mPlayer4Token1.mScale[0];
        mTokenScale0.y = mPlayer4Token1.mScale[1];
        mTokenScale0.z = mPlayer4Token1.mScale[2];
        mGreenPlayers[0].gameObject.transform.localScale = mTokenScale0;
    }

    public void LoadToken2Details()
    {
        Player4Schema mPlayer4Token2 = JsonConvert.DeserializeObject<Player4Schema>(File.ReadAllText(Application.persistentDataPath + "/Player4Data2.json"));
        mGreenPlayers[1].mNumberOfStepsMoved = mPlayer4Token2.mNumberOfStepsMoved;
        mGreenPlayers[1].mLastPosition = mPlayer4Token2.mLastPosition;
        mGreenPlayers[1].mPositionMoved = mPlayer4Token2.mPositionMoved;
        mGreenPlayers[1].mFlag = mPlayer4Token2.mFlag;
        mGreenPlayers[1].mPlayerOutIndex = mPlayer4Token2.mPlayerOutIndex;
        mGreenPlayers[1].mTokenPosition = mPlayer4Token2.mTokenPosition;
        mGreenPlayers[1].mNumberOfStepsRemaining = mPlayer4Token2.mNumberOfStepsRemaining;
        mGreenPlayers[1].mIsReadyToMove = mPlayer4Token2.mIsReadyToMove;
        mGreenPlayers[1].mGreenCanMove = mPlayer4Token2.mGreenCanMove;
        mGreenPlayers[1].mAnotherChance = mPlayer4Token2.mAnotherChance;
        mGreenPlayers[1].mTokenMoved = mPlayer4Token2.mTokenMoved;
        mGreenPlayers[1].mGreenTokenMoving = mPlayer4Token2.mGreenTokenMoving;
        mGreenPlayers[1].mTokenOut = mPlayer4Token2.mTokenOut;
        mGreenPlayers[1].mMovePossible = mPlayer4Token2.mMovePossible;
        mGreenPlayers[1].mStartingPosition = mPlayer4Token2.mStartingPosition;
        mGreenPlayers[1].mPlayerFinished = mPlayer4Token2.mPlayerFinished;
        Vector3 mTokenPosition1;
        mTokenPosition1.x = mPlayer4Token2.mPosition[0];
        mTokenPosition1.y = mPlayer4Token2.mPosition[1];
        mTokenPosition1.z = mPlayer4Token2.mPosition[2];
        mGreenPlayers[1].gameObject.transform.position = mTokenPosition1;
        Vector3 mTokenScale1;
        mTokenScale1.x = mPlayer4Token2.mScale[0];
        mTokenScale1.y = mPlayer4Token2.mScale[1];
        mTokenScale1.z = mPlayer4Token2.mScale[2];
        mGreenPlayers[1].gameObject.transform.localScale = mTokenScale1;
    }

    public void LoadToken3Details()
    {
        Player4Schema mPlayer4Token3 = JsonConvert.DeserializeObject<Player4Schema>(File.ReadAllText(Application.persistentDataPath + "/Player4Data3.json"));
        mGreenPlayers[2].mNumberOfStepsMoved = mPlayer4Token3.mNumberOfStepsMoved;
        mGreenPlayers[2].mLastPosition = mPlayer4Token3.mLastPosition;
        mGreenPlayers[2].mPositionMoved = mPlayer4Token3.mPositionMoved;
        mGreenPlayers[2].mFlag = mPlayer4Token3.mFlag;
        mGreenPlayers[2].mPlayerOutIndex = mPlayer4Token3.mPlayerOutIndex;
        mGreenPlayers[2].mTokenPosition = mPlayer4Token3.mTokenPosition;
        mGreenPlayers[2].mNumberOfStepsRemaining = mPlayer4Token3.mNumberOfStepsRemaining;
        mGreenPlayers[2].mIsReadyToMove = mPlayer4Token3.mIsReadyToMove;
        mGreenPlayers[2].mGreenCanMove = mPlayer4Token3.mGreenCanMove;
        mGreenPlayers[2].mAnotherChance = mPlayer4Token3.mAnotherChance;
        mGreenPlayers[2].mTokenMoved = mPlayer4Token3.mTokenMoved;
        mGreenPlayers[2].mGreenTokenMoving = mPlayer4Token3.mGreenTokenMoving;
        mGreenPlayers[2].mTokenOut = mPlayer4Token3.mTokenOut;
        mGreenPlayers[2].mMovePossible = mPlayer4Token3.mMovePossible;
        mGreenPlayers[2].mStartingPosition = mPlayer4Token3.mStartingPosition;
        mGreenPlayers[2].mPlayerFinished = mPlayer4Token3.mPlayerFinished;
        Vector3 mTokenPosition2;
        mTokenPosition2.x = mPlayer4Token3.mPosition[0];
        mTokenPosition2.y = mPlayer4Token3.mPosition[1];
        mTokenPosition2.z = mPlayer4Token3.mPosition[2];
        mGreenPlayers[2].gameObject.transform.position = mTokenPosition2;
        Vector3 mTokenScale2;
        mTokenScale2.x = mPlayer4Token3.mScale[0];
        mTokenScale2.y = mPlayer4Token3.mScale[1];
        mTokenScale2.z = mPlayer4Token3.mScale[2];
        mGreenPlayers[2].gameObject.transform.localScale = mTokenScale2;
    }

    public void LoadToken4Details()
    {
        Player4Schema mPlayer4Token4 = JsonConvert.DeserializeObject<Player4Schema>(File.ReadAllText(Application.persistentDataPath + "/Player4Data4.json"));
        mGreenPlayers[3].mNumberOfStepsMoved = mPlayer4Token4.mNumberOfStepsMoved;
        mGreenPlayers[3].mLastPosition = mPlayer4Token4.mLastPosition;
        mGreenPlayers[3].mPositionMoved = mPlayer4Token4.mPositionMoved;
        mGreenPlayers[3].mFlag = mPlayer4Token4.mFlag;
        mGreenPlayers[3].mPlayerOutIndex = mPlayer4Token4.mPlayerOutIndex;
        mGreenPlayers[3].mTokenPosition = mPlayer4Token4.mTokenPosition;
        mGreenPlayers[3].mNumberOfStepsRemaining = mPlayer4Token4.mNumberOfStepsRemaining;
        mGreenPlayers[3].mIsReadyToMove = mPlayer4Token4.mIsReadyToMove;
        mGreenPlayers[3].mGreenCanMove = mPlayer4Token4.mGreenCanMove;
        mGreenPlayers[3].mAnotherChance = mPlayer4Token4.mAnotherChance;
        mGreenPlayers[3].mTokenMoved = mPlayer4Token4.mTokenMoved;
        mGreenPlayers[3].mGreenTokenMoving = mPlayer4Token4.mGreenTokenMoving;
        mGreenPlayers[3].mTokenOut = mPlayer4Token4.mTokenOut;
        mGreenPlayers[3].mMovePossible = mPlayer4Token4.mMovePossible;
        mGreenPlayers[3].mStartingPosition = mPlayer4Token4.mStartingPosition;
        mGreenPlayers[3].mPlayerFinished = mPlayer4Token4.mPlayerFinished;
        Vector3 mTokenPosition3;
        mTokenPosition3.x = mPlayer4Token4.mPosition[0];
        mTokenPosition3.y = mPlayer4Token4.mPosition[1];
        mTokenPosition3.z = mPlayer4Token4.mPosition[2];
        mGreenPlayers[3].gameObject.transform.position = mTokenPosition3;
        Vector3 mTokenScale3;
        mTokenScale3.x = mPlayer4Token4.mScale[0];
        mTokenScale3.y = mPlayer4Token4.mScale[1];
        mTokenScale3.z = mPlayer4Token4.mScale[2];
        mGreenPlayers[3].gameObject.transform.localScale = mTokenScale3;
    }
}
