using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using CommonData;
// BinaryFormatter Lib
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class PlayMgr {
	private List<int> mSelectedList;

	private List<int> mUnitList;
	private List<int> mOpenUnitList;

	private int mSproutValue;
	private int mStage;
	private int mOpenStage;
	private static PlayMgr mInstance;

	private PlayMgr()
	{
		mSelectedList = new List<int> ();

		mUnitList = new List<int> ();
		mOpenUnitList = new List<int> ();

		mUnitList.Add((int)EnumCharacterType.CHARACTER_TYPE_RAT);
		mUnitList.Add((int)EnumCharacterType.CHARACTER_TYPE_HORSE);
		mUnitList.Add((int)EnumCharacterType.CHARACTER_TYPE_RAT1);

	}

	public static PlayMgr GetInstance()
	{
		if (mInstance == null) {
			mInstance = new PlayMgr();
		}
		return mInstance;
	}

	public void AddSelectedUnit( EnumCharacterType charType )
	{
		//마리수 제한 체크도 필요
		if(mSelectedList.Contains((int)charType))
			return;
		mSelectedList.Add((int)charType);
	} 
	public void RemoveSelectedUnit( EnumCharacterType charType )
	{
		if(mSelectedList.Contains((int)charType))
		{
			mSelectedList.Remove((int)charType);
		}
	}
	public EnumCharacterType GetSelectedUnitAt(int index)
	{
		if( index < mSelectedList.Count )
			return (EnumCharacterType)mSelectedList[index];
		return EnumCharacterType.CHARACTER_TYPE_NONE;
	}
	public void ClearSelectedUnit()
	{
		mSelectedList.Clear();
	}
	public int CountSelectedUnit()
	{
		return mSelectedList.Count;
	}

	public List<int> GetUnitList()
	{
		return mUnitList;
	}

	public void SetOpenUnitList(EnumCharacterType charType)
	{
		if(mOpenUnitList.Count < 9)
		{
			mOpenUnitList.Add((int)charType);
			for(int i = mOpenUnitList.Count; i < 9; i++)
			{
				mOpenUnitList.Add ((int)EnumCharacterType.CHARACTER_TYPE_NONE);
			}
		}
		else
		{
			for(int i = 0; i < mOpenUnitList.Count; i++)
			{
				if(mOpenUnitList[i] == -1)
				{
					mOpenUnitList.RemoveAt(i);
					mOpenUnitList.Insert(i,(int)charType);
					break;
				}
			}
		}

		//Get a binary formatter
		var bf = new BinaryFormatter();
		//Create an in memory stream;
		var ms = new MemoryStream();
		//Save the List
		bf.Serialize(ms,mOpenUnitList);
		//Add it to playerprefs
		PlayerPrefs.SetString("mOpenUnitList", Convert.ToBase64String(ms.GetBuffer()));

	}
	public List<int> GetOpenUnitList()
	{
		//Get the data
		var data = PlayerPrefs.GetString("OpenUnitList");
		//if not blank then load it
		if(!string.IsNullOrEmpty(data))
		{
			//Binary formatter for loading back
			var bf = new BinaryFormatter();
			//Create a memory stream with the data
			var ms = new MemoryStream(Convert.FromBase64String(data));
			//Load back the List;
			mOpenUnitList = (List<int>)bf.Deserialize(ms);
		}
		return mOpenUnitList;
	}

	public int sproutValue { get { return mSproutValue; } set { mSproutValue = value; } }
	public int currentStageNo { get { mStage = PlayerPrefs.GetInt("currentStageNo");	return mStage; } 
		set { mStage = value;   PlayerPrefs.SetInt("currentStageNo", mStage);} }
	public int openStageNo { get { mOpenStage = PlayerPrefs.GetInt("OpenStageNo");	return mOpenStage; } 
		set { mOpenStage = value;   PlayerPrefs.SetInt("OpenStageNo", mOpenStage);} }


}
