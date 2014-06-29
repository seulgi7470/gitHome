using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SPSpriteAnimation : MonoBehaviour {

	public int FPS = 30;
	public string prefix = "";
	public bool loop = true;

	public GameObject target = null;
	public string endFunctionName = "OnAnimationEnd";

	UISprite mSprite;
	float mDelta = 0f;
	int mIndex = 0;
	bool mbActive = true;
	List<string> mSpriteNames = new List<string>();

	public int frames { get { return mSpriteNames.Count; } }

	public int framesPerSecond { get { return FPS; } set { FPS = value; } }


	public string namePrefix { get { return prefix; } set { if(prefix != value) { prefix = value; RebuildSpriteList(); } } }

	public bool isLoop { get { return loop; } set { loop = value; } }

	public bool isPlaying { get { return mbActive; } }

	// Use this for initialization
	void Start () {
		RebuildSpriteList ();
	}
	
	// Update is called once per frame
	void Update () {
		if (mbActive && mSpriteNames.Count > 1 && Application.isPlaying && FPS > 0f) {
			mDelta += Time.deltaTime;
			float rate = 1f / FPS;

			if (rate < mDelta) {
				mDelta = (rate > 0f) ? mDelta - rate : 0f;

				if (++mIndex >= mSpriteNames.Count) {
					mIndex = 0;
					mbActive = loop;
					if(!loop && target != null) {
						target.SendMessage(endFunctionName);
					}
				}

				if (mbActive) {
					mSprite.spriteName = mSpriteNames [mIndex];
					mSprite.MakePixelPerfect ();
				}
			}
		}
	}

	void RebuildSpriteList()
	{
		if (mSprite == null)
				mSprite = GetComponent<UISprite> ();
		mSpriteNames.Clear ();

		if (mSprite != null && mSprite.atlas != null) {
			List<UIAtlas.Sprite> sprites = mSprite.atlas.spriteList;

			for (int i = 0 , imax = sprites.Count; i <imax; ++i) {
				UIAtlas.Sprite sprite = sprites [i];

				if (string.IsNullOrEmpty (prefix) || sprite.name.StartsWith (prefix)) {
					mSpriteNames.Add (sprite.name);
				}
			}
		mSpriteNames.Sort ();
		}
	}

	public void Reset()
	{
		mbActive = true;
		mIndex = 0;

		if(mSprite != null && mSpriteNames.Count > 0)
		{
			mSprite.spriteName = mSpriteNames[mIndex];
			mSprite.MakePixelPerfect();
		}
	}
}
