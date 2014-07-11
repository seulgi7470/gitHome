using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using CommonData;


public class DataMgr {

	private static DataMgr mInstance;
	private List<UnitData> mUnitData;

	private DataMgr()
	{
		Init ();
	}
	
	public static DataMgr GetInstance()
	{
		if (mInstance == null) {
			mInstance = new DataMgr();
		}
		return mInstance;
	}

	void Init()
	{
		LoadUnitData();
	}

	void LoadUnitData()
	{
		if(mUnitData == null)
			mUnitData = new List<UnitData>();
		mUnitData.Clear();

		do // unitTower
		{
			UnitData unitData;
			unitData.maxHp = 300;
			unitData.speed = 0;
			unitData.power = 0;
			unitData.range = 0;
			unitData.bulletDelay = 0;
			unitData.bulletSpeed = 0;
			unitData.price = 0;
			unitData.plum = 0;
			unitData.unitDelay = 0;
			mUnitData.Add(unitData);
		}while(false);
		
		
		do // enemyTower
		{
			UnitData unitData;
			unitData.maxHp = 300;
			unitData.speed = 0;
			unitData.power = 0;
			unitData.range = 0;
			unitData.bulletDelay = 0;
			unitData.bulletSpeed = 0;
			unitData.price = 0;
			unitData.plum = 200;
			unitData.unitDelay = 0;
			mUnitData.Add(unitData);
		}while(false);

		do // unit1 grayRat
		{
			UnitData unitData;
			unitData.maxHp = 150;
			unitData.speed = 2;
			unitData.power = 20;
			unitData.range = 50;
			unitData.bulletDelay = 1;
			unitData.bulletSpeed = 3;
			unitData.price = 30;
			unitData.plum = 0;
			unitData.unitDelay = 50;
			mUnitData.Add(unitData);
		}while(false);

		do // unit2 Horse
		{
			UnitData unitData;
			unitData.maxHp = 200;
			unitData.speed = 4;
			unitData.power = 60;
			unitData.range = 100;
			unitData.bulletDelay = 2;
			unitData.bulletSpeed = 2;
			unitData.price = 50;
			unitData.plum = 300;
			unitData.unitDelay = 100;
			mUnitData.Add(unitData);
		}while(false);

		do // unit3 Rat1
		{
			UnitData unitData;
			unitData.maxHp = 100;
			unitData.speed = 3;
			unitData.power = 30;
			unitData.range = 100;
			unitData.bulletDelay = 3;
			unitData.bulletSpeed = 3;
			unitData.price = 30;
			unitData.plum = 0;
			unitData.unitDelay = 100;
			mUnitData.Add(unitData);
		}while(false);

		do // unit4 Elephant
		{
			UnitData unitData;
			unitData.maxHp = 300;
			unitData.speed = 1;
			unitData.power = 50;
			unitData.range = 300;
			unitData.bulletDelay = 1;
			unitData.bulletSpeed = 2;
			unitData.price = 50;
			unitData.plum = 1000;
			unitData.unitDelay = 200;
			mUnitData.Add(unitData);
		}while(false);

		do // enemy1
		{
			UnitData unitData;
			unitData.maxHp = 100;
			unitData.speed = -3;
			unitData.power = 30;
			unitData.range = 150;
			unitData.bulletDelay = 1;
			unitData.bulletSpeed = -2;
			unitData.price = 0;
			unitData.plum = 8;
			unitData.unitDelay = 5;
			mUnitData.Add(unitData);
		}while(false);

		do // enemy2
		{
			UnitData unitData;
			unitData.maxHp = 120;
			unitData.speed = -2;
			unitData.power = 40;
			unitData.range = 500;
			unitData.bulletDelay = 2;
			unitData.bulletSpeed = -2;
			unitData.price = 0;
			unitData.plum = 14;
			unitData.unitDelay = 7;
			mUnitData.Add(unitData);
		}while(false);


	
	}

	public UnitData GetUnitData(EnumCharacterType charType)
	{
		int index = (int)charType;
		return mUnitData[index];
	

	}
}
