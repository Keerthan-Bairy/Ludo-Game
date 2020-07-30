using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardlaoutScript : MonoBehaviour
{
	void Start()
	{
		ScaleToMatchScreen();
	}

	void ScaleToMatchScreen()
	{
		SpriteRenderer mRenderer = GetComponent<SpriteRenderer>();
		if (mRenderer == null)
			return;


		Vector2 mSpriteSize = mRenderer.sprite.bounds.size;
		Debug.Log(mSpriteSize.x);
		float mCameraHeight = Camera.main.orthographicSize * 2;
		float mCameraWidth = mCameraHeight * Camera.main.aspect;
		Debug.Log(mCameraWidth);
		Vector2 mCameraSize = new Vector2(mCameraWidth, mCameraHeight);

		Vector2 mScale = new Vector2(mCameraSize.x / mSpriteSize.x, mCameraSize.y / mSpriteSize.y);

		if (mScale.x > mScale.y)
		{
			transform.localScale *= mScale.x;
		}

	}

}
