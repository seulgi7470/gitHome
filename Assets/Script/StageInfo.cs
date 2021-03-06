﻿using UnityEngine;
using System.Collections;
using CommonData;

public class StageInfo {

	private static StageInfo mInstance;

	private int[,] mEnemyList;
	private int mStage;
	private EnumCharacterType mCharType;
	private int mEnemyNum;
	private int mEnemyDelay;

	private StageInfo()
	{
		mEnemyNum = 0;
		CreateEnemyList();
		SetEnemyType();
	}
	
	public static StageInfo GetInstance()
	{
		if (mInstance == null) {
			mInstance = new StageInfo();
		}
		return mInstance;
	}

	private void CreateEnemyList()
	{
		mEnemyList = new int[,] {
			{0, 1, 0, 0, 1, 0, 1, 0, 0, 1},	// 1-4
			{0, 1, 0, 0, 1, 0, 1, 1, 0, 1},	// 1-5
			{1, 1, 0, 1, 1, 0, 1, 1, 0, 1},	// 1-7
			{1, 0, 2, 0, 0, 1, 0, 1, 0, 1},	// 1-4 2-1
			{1, 0, 2, 0, 0, 1, 2, 0, 1, 1},	// 1-4 2-2
			{2, 1, 2, 0, 1, 0, 1, 1, 0, 1},	// 1-5 2-2
			{2, 1, 2, 0, 1, 0, 2, 1, 0, 1},	// 1-4 2-3
			{0, 1, 2, 0, 1, 2, 1, 2, 1, 1},	// 1-5 2-3
			{1, 1, 2, 1, 1, 2, 1, 2, 2, 1},	// 1-6 2-4
			{2, 1, 1, 2, 1, 2, 2, 1, 2, 1},	// 1-5 2-5
			{2, 1, 2, 2, 1, 2, 2, 1, 2, 2},	// 1-3 2-7
			{2, 2, 2, 2, 1, 2, 2, 2, 2, 2} 	// 1-1 2-9
		};
	}
	private void SetEnemyType()
	{
		switch(mEnemyNum)
		{
		case 1:
			mCharType = EnumCharacterType.CHARACTER_TYPE_ENEMY1;
			break;
		case 2:
			mCharType = EnumCharacterType.CHARACTER_TYPE_ENEMY2;
			break;
		}
	}

	public int CreateEnemyDelay(int enemyNum)
	{
		UnitData unitData ;

		switch(enemyNum)
		{
		case 0:
			mEnemyDelay = 3;
			break;
		case 1:
			unitData = DataMgr.GetInstance().GetUnitData(mCharType);
			mEnemyDelay = unitData.unitDelay;
			break;
		case 2:
			unitData = DataMgr.GetInstance().GetUnitData(mCharType);
			mEnemyDelay = unitData.unitDelay;
			break;
		}

		return mEnemyDelay;
	}

	public int[,] GetEnemyList() {
		return mEnemyList;
	}

	public EnumCharacterType GetEnemyType(int enemyNum) {
		switch(enemyNum)
		{
		case 0:
			mCharType = EnumCharacterType.CHARACTER_TYPE_NONE;
			break;
		case 1:
			mCharType = EnumCharacterType.CHARACTER_TYPE_ENEMY1;
			break;
		case 2:
			mCharType = EnumCharacterType.CHARACTER_TYPE_ENEMY2;
			break;
		}
		return mCharType;
	}
}
