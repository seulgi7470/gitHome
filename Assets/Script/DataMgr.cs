﻿using UnityEngine;
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
			unitData.name = "유닛타워";
			unitData.contents = "";
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
			unitData.name = "적타워";
			unitData.contents = "";
			unitData.maxHp = 300;
			unitData.speed = 0;
			unitData.power = 0;
			unitData.range = 0;
			unitData.bulletDelay = 0;
			unitData.bulletSpeed = 0;
			unitData.price = 0;
			unitData.plum = 300;
			unitData.unitDelay = 0;
			mUnitData.Add(unitData);
		}while(false);

		do // unit1 grayRat
		{
			UnitData unitData;
			unitData.name = "쥐";
			unitData.contents = "근거리 공격형 유닛\n파워는 약하지만\n빠르게 공격한다.";
			unitData.maxHp = 100;
			unitData.speed = 2;
			unitData.power = 25;
			unitData.range = 50;
			unitData.bulletDelay = 1;
			unitData.bulletSpeed = 3;
			unitData.price = 20;
			unitData.plum = 0;
			unitData.unitDelay = 50;
			mUnitData.Add(unitData);
		}while(false);

		do // unit2 Horse
		{
			UnitData unitData;
			unitData.name = "말";
			unitData.contents = "근거리 공격형 유닛\n체력이 높고\n스피드가 빠르다.";
			unitData.maxHp = 300;
			unitData.speed = 4;
			unitData.power = 40;
			unitData.range = 100;
			unitData.bulletDelay = 2;
			unitData.bulletSpeed = 2;
			unitData.price = 30;
			unitData.plum = 300;
			unitData.unitDelay = 100;
			mUnitData.Add(unitData);
		}while(false);

		do // unit3 Elephant
		{
			UnitData unitData;
			unitData.name = "코끼리";
			unitData.contents = "근거리 공격형 유닛\n스피드는 느리지만\n공격력이 세다.";
			unitData.maxHp = 200;
			unitData.speed = 2;
			unitData.power = 60;
			unitData.range = 150;
			unitData.bulletDelay = 1;
			unitData.bulletSpeed = 2;
			unitData.price = 50;
			unitData.plum = 1000;
			unitData.unitDelay = 200;
			mUnitData.Add(unitData);
		}while(false);

		do // unit4 Alpaca
		{
			UnitData unitData;
			unitData.name = "알파카";
			unitData.contents = "원거리 공격형 유닛\n공격력이 세고\n공격속도가 빠르다.";
			unitData.maxHp = 200;
			unitData.speed = 3;
			unitData.power = 60;
			unitData.range = 400;
			unitData.bulletDelay = 1;
			unitData.bulletSpeed = 2;
			unitData.price = 70;
			unitData.plum = 1500;
			unitData.unitDelay = 150;
			mUnitData.Add(unitData);
		}while(false);

		do // enemy1
		{
			UnitData unitData;
			unitData.name = "적1";
			unitData.contents = "";
			unitData.maxHp = 100;
			unitData.speed = -3;
			unitData.power = 30;
			unitData.range = 150;
			unitData.bulletDelay = 1;
			unitData.bulletSpeed = -2;
			unitData.price = 0;
			unitData.plum = 8;
			unitData.unitDelay = 4;
			mUnitData.Add(unitData);
		}while(false);

		do // enemy2
		{
			UnitData unitData;
			unitData.name = "적2";
			unitData.contents = "";
			unitData.maxHp = 120;
			unitData.speed = -2;
			unitData.power = 40;
			unitData.range = 400;
			unitData.bulletDelay = 2;
			unitData.bulletSpeed = -2;
			unitData.price = 0;
			unitData.plum = 14;
			unitData.unitDelay = 6;
			mUnitData.Add(unitData);
		}while(false);


	
	}

	public UnitData GetUnitData(EnumCharacterType charType)
	{
		int index = (int)charType;
		return mUnitData[index];
	

	}
}
