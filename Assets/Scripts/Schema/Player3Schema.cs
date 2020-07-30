using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player3Schema
{
    public int mNumberOfStepsMoved;
    public int mLastPosition;
    public int mPositionMoved;
    public int mFlag;
    public int mPlayerOutIndex;
    public int mTokenPosition;
    public int mNumberOfStepsRemaining;
    public bool mIsReadyToMove;
    public bool mYellowCanMove;
    public bool mAnotherChance;
    public bool mTokenMoved;
    public bool mYellowTokenMoving;
    public bool mTokenOut;
    public bool mMovePossible;
    public bool mStartingPosition;
    public bool mPlayerFinished;
    public float[] mPosition = new float[3];
    public float[] mScale = new float[3];

}
