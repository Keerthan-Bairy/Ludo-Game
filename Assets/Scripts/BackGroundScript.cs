using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class BackGroundScript : MonoBehaviour
{
	[SerializeField] Color mPlayer1;
	[SerializeField] Color mPlayer2;
	[SerializeField] Color mPlayer3;
	[SerializeField] Color mPlayer4;
	private ColorScript mColor;
	private string mJsonSavePath;

	void Start()
    {
		SaveGameEndData();
        BackGroundSize();
		mJsonSavePath = Application.persistentDataPath + "/Color.json";
		mColor = new ColorScript();
		mColor.mPlayer1Color = mPlayer1;
		mColor.mPlayer2Color = mPlayer2;
		mColor.mPlayer3Color = mPlayer3;
		mColor.mPlayer4Color = mPlayer4;
		mColor.mResumeButtonClicked = false;
		string mJsonData = JsonUtility.ToJson(mColor,true);
		File.WriteAllText(mJsonSavePath, mJsonData);

    }

    private void BackGroundSize()
    {
		SpriteRenderer renderer = GetComponent<SpriteRenderer>();
		if (renderer == null)
			return;


		Vector2 mSpriteSize = renderer.sprite.bounds.size;
		float mCameraHeight = Camera.main.orthographicSize * 2;
		float mCameraWidth = Camera.main.aspect * mCameraHeight;
		Vector2 mCameraSize = new Vector2(mCameraWidth, mCameraHeight);

		transform.localScale = new Vector3(1f, 1f, 1f);
		Vector2 mScale = new Vector2(mCameraSize.x / mSpriteSize.x, mCameraSize.y / mSpriteSize.y);

		if (mScale.x > mScale.y)
		{
			transform.localScale *= mScale.x;
		}
		else
		{
			transform.localScale *= mScale.y;
		}
	}
	public void SaveGameEndData()
    {
		GameEndData mData = new GameEndData();
		string mJson = JsonConvert.SerializeObject(mData, Formatting.Indented);
		File.WriteAllText(Application.persistentDataPath + "/GameEndValue.json", mJson);
    }
}
