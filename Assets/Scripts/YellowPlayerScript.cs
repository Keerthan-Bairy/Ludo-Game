using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;
using DG.Tweening;
using Newtonsoft.Json;
using System.IO;

public class YellowPlayerScript : MonoBehaviour
{
    [SerializeField] GameObject mTokenPath;
    public GameObject mChildObject;
    public GameObject mInitialPosition;
    private Transform mColliderInitialPosition;
    private Transform[] mPath;
    private Coroutine mMoveToken;
    private Coroutine mReInitialize;
    private CircleCollider2D mCircleCollider;
    private Collider2D mInCollider;
    private Collider2D mTriggerCollider;
    private RedTokenScript mRedToken;
    private BlueTokenScript mBlueToken;
    private YellowTokenScript mYellowToken;
    private GreenTokenScript mGreenToken;
    private BluePlayerScript mBluePlayer;
    private RedPlayerScript mRedPlayer;
    private GreenPlayerScript mGreenPlayer;
    private List<Collider2D> mColliderObject;
    private float[] mFinalPosition = { 0.2f, 0.10f, 0.0f, -0.10f };


    public int mNumberOfStepsMoved;
    public int mLastPosition;
    public int mPositionMoved;
    public int mFlag;
    public int mPlayerOutIndex;
    public int mTokenPosition;
    public int mNumberOfStepsRemaining;
    public int mNumberGot;

    public bool mIsReadyToMove;
    public bool mMoveToFirstPosition;
    public bool mYellowCanMove;
    public bool mAnotherChance;
    public bool mTokenMoved;
    public bool mYellowTokenMoving;
    public bool mMoveToFirst;
    public bool mTokenOut;
    public bool mMovePossible;
    public bool mStartingPosition;
    public bool mPlayerFinished;


    private void Start()
    {
        mPath = mTokenPath.GetComponentsInChildren<Transform>();
        mCircleCollider = GetComponent<CircleCollider2D>();
        mRedToken = FindObjectOfType<RedTokenScript>();
        mBlueToken = FindObjectOfType<BlueTokenScript>();
        mYellowToken = FindObjectOfType<YellowTokenScript>();
        mGreenToken = FindObjectOfType<GreenTokenScript>();
        if (!GameManagerScript.mGameManager.mResumeButtonClicked)
        {
            Init();
        }
        mChildObject.SetActive(false);
    }

    public void Init()
    {
        mPath = mTokenPath.GetComponentsInChildren<Transform>();
        mCircleCollider = GetComponent<CircleCollider2D>();
        mRedToken = FindObjectOfType<RedTokenScript>();
        mBlueToken = FindObjectOfType<BlueTokenScript>();
        mYellowToken = FindObjectOfType<YellowTokenScript>();
        mGreenToken = FindObjectOfType<GreenTokenScript>();
        mNumberOfStepsMoved = 2;
        mNumberOfStepsRemaining = 0;
        mLastPosition = 1;
        mPositionMoved = 0;
        mFlag = 0;
        mPlayerOutIndex = 0;
        mTokenPosition = 0;
        mIsReadyToMove = false;
        mYellowCanMove = false;
        mAnotherChance = false;
        mTokenMoved = false;
        mYellowTokenMoving = false;
        mMoveToFirst = false;
        mMoveToFirstPosition = false;
        mTokenOut = false;
        mStartingPosition = false;
        mPlayerFinished = false;
    }
    private void Update()
    {
        if (mMoveToFirst == true)
        {
            mInCollider.GetComponent<CircleCollider2D>().enabled = false;
        }
        CircleRotation();
    }


    //When a Player clicks the box collider OnMouseDown method will be triggered.
    private void OnMouseDown()
    {
        StartTokenMovement();
    }

    public void StartTokenMovement()
    {
        if (GameManagerScript.mGameManager.mRolledDice.CompareTag("Yellow") && mFlag == 0 && GameManagerScript.mGameManager.mNumberGot == 6)
        {
            MoveToken();
        }
        else if (mFlag == 1 && mTokenOut == true && GameManagerScript.mGameManager.mRolledDice.CompareTag("Yellow") && GameManagerScript.mGameManager.mNumberGot <= mNumberOfStepsRemaining)
        {
            MoveToken();
        }
    }

    public void MoveToken()
    {
        if (mYellowCanMove && GameManagerScript.mGameManager.mDiceRolled == true)
        {
            mYellowToken.CircleDisable();
            mPlayerOutIndex = 0;
            mMoveToken = StartCoroutine(IMoveToken());
            GameManagerScript.mGameManager.mDiceRolled = false;
        }
    }

    //Function to move the turn to the next player
    public void NextPlayer()
    {
        if (mAnotherChance == false && GameManagerScript.mGameManager.mLastToken == false && mTokenMoved == false)
        {
            GameManagerScript.mGameManager.MoveToNextPlayer();
        }
    }

    //Function to Check whether the player is last player or not.
    //If so move the turn to the first player by making a function call to the GameManager.
    public void FirstPlayer()
    {
        if (GameManagerScript.mGameManager.mLastToken == true && mAnotherChance == false)
        {
            GameManagerScript.mGameManager.mPlayerIndex = 0;
            GameManagerScript.mGameManager.StartGame(GameManagerScript.mGameManager.mPlayingTokens[GameManagerScript.mGameManager.mPlayerIndex]);
        }
    }

    //Function to move the token step by step based on the number appeared on the dice. 
    IEnumerator IMoveToken()
    {
        yield return new WaitForSeconds(.25f);
        mNumberGot = GameManagerScript.mGameManager.mNumberGot;
        if (mNumberGot == 6 && mIsReadyToMove == false)
        {
            //This is condition to move the token from Initial position to the first position.
            //mMoveToFirstPosition = true;
            transform.DOMove(mPath[1].transform.position, 1f).SetEase(Ease.InOutQuad).OnComplete(ShakeToken);
            GameManagerScript.mGameManager.mYellowPlayerOut++;
            GameManagerScript.mGameManager.mTokenClicked = false;
            mStartingPosition = true;
            mNumberGot = 2;
            mPositionMoved = 2;
            mFlag = 1;
            mIsReadyToMove = true;
            mAnotherChance = true;
            mTokenMoved = false;
            mTokenOut = true;
        }
        else if (mIsReadyToMove && mNumberGot <= 6 && GameManagerScript.mGameManager.mRolledDice.CompareTag("Yellow"))
        {
            //Checking whether number appeared on the dice is less than the number of steps remaining.
            //If not shift the turn to the next player.
            mChildObject.SetActive(false);
            mMoveToFirstPosition = false;
            if (mNumberGot <= mNumberOfStepsRemaining)
            {
                GameManagerScript.mGameManager.mCanClick = false;
                for (int i = mNumberOfStepsMoved; i < (mNumberGot + mNumberOfStepsMoved); i++)
                {
                    transform.DOMove(mPath[i].transform.position, .1f).SetEase(Ease.InOutQuad);
                    SoundManagerScript.PlaySound("Move");
                    GameManagerScript.mGameManager.mTokenMoving = true;
                    mYellowTokenMoving = true;
                    yield return new WaitForSeconds(.25f);
                    mLastPosition += 1;
                    mPositionMoved += 1;
                    mTokenPosition = i;
                }
                mYellowTokenMoving = false;
                GameManagerScript.mGameManager.mTokenMoving = false;
                if (mTokenPosition!=57 && mNumberGot == 6)
                {
                    //This is a condition to check whether a player got 6.
                    //If so he will get another chance to roll the dice.
                    mAnotherChance = true;
                    GameManagerScript.mGameManager.mCanClick = true;
                    mYellowCanMove = true;
                    mTokenMoved = false;
                }
                else if (mTokenPosition == 57)
                {
                    gameObject.transform.localScale = new Vector2(0.1f, 0.1f);
                    gameObject.transform.position = new Vector2(gameObject.transform.position.x - mFinalPosition[mYellowToken.mFinalPositionIndex], gameObject.transform.position.y);
                    mYellowToken.OrderInLayer(1);
                    SoundManagerScript.PlaySound("Home");
                    GameManagerScript.mGameManager.mYellowPlayerFinished++;
                    GameManagerScript.mGameManager.mYellowPlayerOut--;
                    mAnotherChance = true;
                    mYellowCanMove = false;
                    mPlayerFinished = true;
                    mYellowToken.mFinalPositionIndex++;
                }
                else
                {
                    mAnotherChance = false;
                    mYellowCanMove = false;
                    mTokenMoved = true;
                }
                mNumberOfStepsMoved += mNumberGot;
            }
        }
        mNumberOfStepsRemaining = mPath.Length - mPositionMoved;
        StartCoroutine(IShiftPlayer());
        AllPlayerFinished();
        mYellowToken.OrderInLayer(2);
        if (mMoveToken != null)
        {
            StopCoroutine(mMoveToken);
        }
    }


    //This is a trigger method to hit the other player's token.
    private void OnTriggerStay2D(Collider2D collision)
    {
        mInCollider = collision;
        mTriggerCollider = collision.gameObject.GetComponent<CircleCollider2D>();
        if (mCircleCollider.bounds.Intersects(mTriggerCollider.bounds) && GameManagerScript.mGameManager.mRolledDice.CompareTag("Yellow"))
        {
            FindInitialPositionRed();
            FindInitialPositionBlue();         
            FindInitialPositionGreen();
            if (mYellowTokenMoving == false && mPath[mTokenPosition].transform.position == collision.gameObject.transform.position)
            {
                if (mPath[mLastPosition].tag != "Star")
                {
                    if (gameObject.tag != collision.gameObject.tag)
                    {
                        mInCollider.gameObject.GetComponent<BoxCollider2D>().enabled = false;
                        mAnotherChance = true;
                        mMoveToFirst = true;
                        mInCollider.transform.DOMove(mColliderInitialPosition.position, 1f).SetEase(Ease.InOutQuad);
                        SoundManagerScript.PlaySound("Kill");
                        mReInitialize = StartCoroutine(ReInitializeFunction());
                    }
                    else
                    {
                            collision.transform.position = new Vector2(gameObject.transform.position.x, gameObject.transform.position.y);              
                    }
                }
                else
                {
                        if (gameObject.tag != collision.gameObject.tag)
                        {
                            collision.transform.position = new Vector2(gameObject.transform.position.x - 0.20f, gameObject.transform.position.y);
                        }
                        else
                        {
                            collision.transform.position = new Vector2(gameObject.transform.position.x, gameObject.transform.position.y);
                        }
                }

            }
        }
    }

    //Function to find the Initial Position of Yellow Token.
    private void FindInitialPositionRed()
    {
        if (mInCollider.gameObject.name== "Player1Token1")
        {
            mColliderInitialPosition = mRedToken.mInitialPositions[0].transform;
            mRedPlayer = GetComponent<RedPlayerScript>();
        }
        if (mInCollider.gameObject.name == "Player1Token2")
        {
            mColliderInitialPosition = mRedToken.mInitialPositions[1].transform;
            mRedPlayer = GetComponent<RedPlayerScript>();
        }
        if (mInCollider.gameObject.name == "Player1Token3")
        {
            mColliderInitialPosition = mRedToken.mInitialPositions[2].transform;
            mRedPlayer = GetComponent<RedPlayerScript>();
        }
        if (mInCollider.gameObject.name == "Player1Token4")
        {
            mColliderInitialPosition = mRedToken.mInitialPositions[3].transform;
            mRedPlayer = GetComponent<RedPlayerScript>();
        }

    }

    //Function to find the initial position of Blue token.
    private void FindInitialPositionBlue()
    {
        if (mInCollider.gameObject.name == "Player2Token1")
        {
            mColliderInitialPosition = mBlueToken.mInitialPositions[0].transform;
            mBluePlayer = GetComponent<BluePlayerScript>();
        }
        if (mInCollider.gameObject.name == "Player2Token2")
        {
            mColliderInitialPosition = mBlueToken.mInitialPositions[1].transform;
            mBluePlayer = GetComponent<BluePlayerScript>();
        }
        if (mInCollider.gameObject.name == "Player2Token3")
        {
            mColliderInitialPosition = mBlueToken.mInitialPositions[2].transform;
            mBluePlayer = GetComponent<BluePlayerScript>();
        }
        if (mInCollider.gameObject.name == "Player2Token4")
        {
            mColliderInitialPosition = mBlueToken.mInitialPositions[3].transform;
            mBluePlayer = GetComponent<BluePlayerScript>();
        }
    }

    //Function to find the Initial Position of Green Token.
    private void FindInitialPositionGreen()
        {
            if (mInCollider.gameObject.name == "Player4Token1")
            {
                mColliderInitialPosition = mGreenToken.mInitialPositions[0].transform;
                mGreenPlayer = GetComponent<GreenPlayerScript>();
            }
            if (mInCollider.gameObject.name == "Player4Token2")
            {
                mColliderInitialPosition = mGreenToken.mInitialPositions[1].transform;
                mGreenPlayer = GetComponent<GreenPlayerScript>();
            }
            if (mInCollider.gameObject.name == "Player4Token3")
            {
                mColliderInitialPosition = mGreenToken.mInitialPositions[2].transform;
                mGreenPlayer = GetComponent<GreenPlayerScript>();
            }
            if (mInCollider.gameObject.name == "player4Token4")
            {
                mColliderInitialPosition = mGreenToken.mInitialPositions[3].transform;
                mGreenPlayer = GetComponent<GreenPlayerScript>();
            }
        }

    //Interface function which Reinitializes the variables of a Token which gets out to the Initial value. 
    IEnumerator ReInitializeFunction()
    {
        yield return new WaitForSeconds(3f);
        if (mInCollider.gameObject.CompareTag("Red"))
        {
            mInCollider.gameObject.GetComponent<RedPlayerScript>().Init();
            mInCollider.gameObject.GetComponent<RedPlayerScript>().mMoveToFirst = false;
            while (mPlayerOutIndex < 1)
            {
                GameManagerScript.mGameManager.mRedPlayerOut--;
                mPlayerOutIndex++;
            }
            mMoveToFirst = false;
        }
        if (mInCollider.gameObject.CompareTag("Blue"))
        {
            mInCollider.gameObject.GetComponent<BluePlayerScript>().Init();
            mInCollider.gameObject.GetComponent<BluePlayerScript>().mMoveToFirst = false;
            while (mPlayerOutIndex < 1)
            {
                GameManagerScript.mGameManager.mBluePlayerOut--;
                mPlayerOutIndex++;
            }
            mMoveToFirst = false;
        }
        if (mInCollider.gameObject.CompareTag("Green"))
        {
            mInCollider.gameObject.GetComponent<GreenPlayerScript>().Init();
            mInCollider.gameObject.GetComponent<GreenPlayerScript>().mMoveToFirst = false;
            while (mPlayerOutIndex < 1)
            {
                GameManagerScript.mGameManager.mGreenPlayerOut--;
                mPlayerOutIndex++;
            }
            mMoveToFirst = false;
        }

    }

    //Interface function to move the dice to the next player.
    IEnumerator IShiftPlayer()
    {
        yield return new WaitForSeconds(1f);
        if (mTokenMoved == true)
        {
            mTokenMoved = false;
            NextPlayer();
        }
        FirstPlayer();
        GameManagerScript.mGameManager.mCanClick = true;
        mYellowCanMove = true;
    }

    private void CircleRotation()
    {
        if (GameManagerScript.mGameManager.mNumberGot == 6 && GameManagerScript.mGameManager.mRolledDice.CompareTag("Yellow") && GameManagerScript.mGameManager.mDiceRolled == true)
        {
            mYellowToken.CircleEnable();
        }
        else if (GameManagerScript.mGameManager.mNumberGot < 6 && GameManagerScript.mGameManager.mRolledDice.CompareTag("Yellow") && GameManagerScript.mGameManager.mDiceRolled == true)
        {
            if (mTokenOut == true && GameManagerScript.mGameManager.mTokenClicked == false && GameManagerScript.mGameManager.mNumberGot <= mNumberOfStepsRemaining)
            {
                mChildObject.transform.position = new Vector3(transform.position.x, transform.position.y, -7f);
                mChildObject.SetActive(true);
            }
        }
    }

    //Function that performs some operations when all the four players reaches the endpoint.
    public void AllPlayerFinished()
    {
        if (GameManagerScript.mGameManager.mYellowPlayerFinished == 4)
        {
            mYellowToken.StopMove();
            GameManagerScript.mGameManager.mPlayerFinished.Add("Player 3");
            mYellowToken.mNumberRenderer.sprite = GameManagerScript.mGameManager.mNumbers[GameManagerScript.mGameManager.mNumberIndex];
            mYellowToken.mNumberRenderer.transform.position = new Vector3(mYellowToken.mTokenCentreX, mYellowToken.mTokenCentreY, -7);
            mYellowToken.mNumberRenderer.transform.localScale = new Vector2(10f, 10f);
            GameManagerScript.mGameManager.SwitchToPlayer();
            GameManagerScript.mGameManager.mNumberIndex += 1;
        }
    }
    public void ShakeToken()
    {
        transform.DOShakeScale(1f, .1f, 5);
    }

    public void SaveDetails()
    {
        if (gameObject.name == "Player3Token1")
        {
            Player3Schema mData1 = new Player3Schema();
            mData1.mNumberOfStepsMoved = mNumberOfStepsMoved;
            mData1.mLastPosition = mLastPosition;
            mData1.mPositionMoved = mPositionMoved;
            mData1.mFlag = mFlag;
            mData1.mPlayerOutIndex = mPlayerOutIndex;
            mData1.mTokenPosition = mTokenPosition;
            mData1.mNumberOfStepsRemaining = mNumberOfStepsRemaining;
            mData1.mIsReadyToMove = mIsReadyToMove;
            mData1.mYellowCanMove = mYellowCanMove;
            mData1.mAnotherChance = mAnotherChance;
            mData1.mTokenMoved = mTokenMoved;
            mData1.mYellowTokenMoving = mYellowTokenMoving;
            mData1.mTokenOut = mTokenOut;
            mData1.mMovePossible = mMovePossible;
            mData1.mStartingPosition = mStartingPosition;
            mData1.mPlayerFinished = mPlayerFinished;
            mData1.mPosition[0] = transform.position.x;
            mData1.mPosition[1] = transform.position.y;
            mData1.mPosition[2] = transform.position.z;
            mData1.mScale[0] = transform.localScale.x;
            mData1.mScale[1] = transform.localScale.y;
            mData1.mScale[2] = transform.localScale.z;
            string mJson1 = JsonConvert.SerializeObject(mData1, Formatting.Indented);
            File.WriteAllText(Application.persistentDataPath + "/Player3Data1.json", mJson1);
        }
        if (gameObject.name == "Player3Token2")
        {
            Player3Schema mData2 = new Player3Schema();
            mData2.mNumberOfStepsMoved = mNumberOfStepsMoved;
            mData2.mLastPosition = mLastPosition;
            mData2.mPositionMoved = mPositionMoved;
            mData2.mFlag = mFlag;
            mData2.mPlayerOutIndex = mPlayerOutIndex;
            mData2.mTokenPosition = mTokenPosition;
            mData2.mNumberOfStepsRemaining = mNumberOfStepsRemaining;
            mData2.mIsReadyToMove = mIsReadyToMove;
            mData2.mYellowCanMove = mYellowCanMove;
            mData2.mAnotherChance = mAnotherChance;
            mData2.mTokenMoved = mTokenMoved;
            mData2.mYellowTokenMoving = mYellowTokenMoving;
            mData2.mTokenOut = mTokenOut;
            mData2.mMovePossible = mMovePossible;
            mData2.mStartingPosition = mStartingPosition;
            mData2.mPlayerFinished = mPlayerFinished;
            mData2.mPosition[0] = transform.position.x;
            mData2.mPosition[1] = transform.position.y;
            mData2.mPosition[2] = transform.position.z;
            mData2.mScale[0] = transform.localScale.x;
            mData2.mScale[1] = transform.localScale.y;
            mData2.mScale[2] = transform.localScale.z;
            string mJson2 = JsonConvert.SerializeObject(mData2, Formatting.Indented);
            File.WriteAllText(Application.persistentDataPath + "/Player3Data2.json", mJson2);
        }
        if (gameObject.name == "Player3Token3")
        {
            Player3Schema mData3 = new Player3Schema();
            mData3.mNumberOfStepsMoved = mNumberOfStepsMoved;
            mData3.mLastPosition = mLastPosition;
            mData3.mPositionMoved = mPositionMoved;
            mData3.mFlag = mFlag;
            mData3.mPlayerOutIndex = mPlayerOutIndex;
            mData3.mTokenPosition = mTokenPosition;
            mData3.mNumberOfStepsRemaining = mNumberOfStepsRemaining;
            mData3.mIsReadyToMove = mIsReadyToMove;
            mData3.mYellowCanMove = mYellowCanMove;
            mData3.mAnotherChance = mAnotherChance;
            mData3.mTokenMoved = mTokenMoved;
            mData3.mYellowTokenMoving = mYellowTokenMoving;
            mData3.mTokenOut = mTokenOut;
            mData3.mMovePossible = mMovePossible;
            mData3.mStartingPosition = mStartingPosition;
            mData3.mPlayerFinished = mPlayerFinished;
            mData3.mPosition[0] = transform.position.x;
            mData3.mPosition[1] = transform.position.y;
            mData3.mPosition[2] = transform.position.z;
            mData3.mScale[0] = transform.localScale.x;
            mData3.mScale[1] = transform.localScale.y;
            mData3.mScale[2] = transform.localScale.z;
            string mJson3 = JsonConvert.SerializeObject(mData3, Formatting.Indented);
            File.WriteAllText(Application.persistentDataPath + "/Player3Data3.json", mJson3);
        }
        if (gameObject.name == "Player3Token4")
        {
            Player3Schema mData4 = new Player3Schema();
            mData4.mNumberOfStepsMoved = mNumberOfStepsMoved;
            mData4.mLastPosition = mLastPosition;
            mData4.mPositionMoved = mPositionMoved;
            mData4.mFlag = mFlag;
            mData4.mPlayerOutIndex = mPlayerOutIndex;
            mData4.mTokenPosition = mTokenPosition;
            mData4.mNumberOfStepsRemaining = mNumberOfStepsRemaining;
            mData4.mIsReadyToMove = mIsReadyToMove;
            mData4.mYellowCanMove = mYellowCanMove;
            mData4.mAnotherChance = mAnotherChance;
            mData4.mTokenMoved = mTokenMoved;
            mData4.mYellowTokenMoving = mYellowTokenMoving;
            mData4.mTokenOut = mTokenOut;
            mData4.mMovePossible = mMovePossible;
            mData4.mStartingPosition = mStartingPosition;
            mData4.mPlayerFinished = mPlayerFinished;
            mData4.mPosition[0] = transform.position.x;
            mData4.mPosition[1] = transform.position.y;
            mData4.mPosition[2] = transform.position.z;
            mData4.mScale[0] = transform.localScale.x;
            mData4.mScale[1] = transform.localScale.y;
            mData4.mScale[2] = transform.localScale.z;
            string mJson4 = JsonConvert.SerializeObject(mData4, Formatting.Indented);
            File.WriteAllText(Application.persistentDataPath + "/Player3Data4.json", mJson4);
        }
    }
}
