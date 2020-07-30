using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceNumberScript : MonoBehaviour
{ 
    [SerializeField] Sprite[] mDiceFaces;
    [SerializeField] SpriteRenderer mDiceNumber;
    [SerializeField] Animator mRollingDiceAnimator;
    private GameManagerScript mGameManager;
    Coroutine mRollingDice;
    private int mRandomNumber;
   

    public int mNumberGot;
    private void Awake()
    {
        mRollingDiceAnimator.gameObject.SetActive(false);
    }

    private void Start()
    {
        mGameManager = FindObjectOfType<GameManagerScript>();
    }
    private void OnMouseDown()
    {
        if (GameManagerScript.mGameManager.mCanClick)
        {
            mRollingDice=StartCoroutine(IWaitDiceToRoll());
            mGameManager.mDiceRolled = false;
        }
        
    }
    IEnumerator IWaitDiceToRoll()
    {
        GameManagerScript.mGameManager.EnableCollider();
        mDiceNumber.gameObject.SetActive(false);
        mRollingDiceAnimator.gameObject.SetActive(true);
        GameManagerScript.mGameManager.mCanClick = false;
        SoundManagerScript.PlaySound("Dice");
        yield return new WaitForSeconds(1f);
        if (GameManagerScript.mGameManager.mCanRollSix == false)
        {
            mRandomNumber = Random.Range(0, 6);
            mNumberGot = mRandomNumber + 1;
            mDiceNumber.sprite = mDiceFaces[mRandomNumber];
        }
        else
        {
            mRandomNumber = 5;
            mNumberGot = mRandomNumber + 1;
            mDiceNumber.sprite = mDiceFaces[mRandomNumber];
            GameManagerScript.mGameManager.mCanRollSix = false;
        }
        GameManagerScript.mGameManager.mNumberGot = mNumberGot;
        GameManagerScript.mGameManager.mRolledDice = this;
        mRollingDiceAnimator.gameObject.SetActive(false);
        mDiceNumber.gameObject.SetActive(true);
        GameManagerScript.mGameManager.mCanClick = true;
        mGameManager.mDiceRolled = true;
        GameManagerScript.mGameManager.mRedCannotMove = 0;
        GameManagerScript.mGameManager.mBlueCannotMove = 0;
        GameManagerScript.mGameManager.mYellowCannotMove = 0;
        GameManagerScript.mGameManager.mGreenCannotMove = 0;
        if (mNumberGot != 6)
        {
            GameManagerScript.mGameManager.SixCountManager();
            GameManagerScript.mGameManager.AutoMoveTokensNotSix();
            GameManagerScript.mGameManager.mNumberOfSix = 0;
            GameManagerScript.mGameManager.SwitchToPlayer();
            GameManagerScript.mGameManager.ShiftConditionOne();
        }
        if (mNumberGot == 6)
        {
            GameManagerScript.mGameManager.SixCountInitialize();
            GameManagerScript.mGameManager.AutoMoveTokenForSix();
            GameManagerScript.mGameManager.mNumberOfSix++;
            GameManagerScript.mGameManager.AllowTokenMove();
            GameManagerScript.mGameManager.ShiftConditionTwo();
            GameManagerScript.mGameManager.ShiftConditionThree();
        }
        if (mRollingDice != null)
        {
            StopCoroutine(mRollingDice);
        }
    }
}
