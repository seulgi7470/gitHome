﻿using UnityEngine;
using System.Collections;

public class UIResult : MonoBehaviour {
	public UILabel resultText;
	public GameObject nextBtn;

	private bool _win = false;
	private bool _refresh = false;
	// Use this for initialization
	void Start () {
		_refresh = true;
	}
	
	// Update is called once per frame
	void Update () {
		if(!_refresh)
			return;

		if(_win)
		{
			if(nextBtn)
				nextBtn.SetActive(true);
			if(resultText)
				resultText.text = " You win ";
		}
		else
		{
			if(nextBtn)
				nextBtn.SetActive(false);
			if(resultText)
				resultText.text = " You Lose ";
		}
		_refresh = false;
		
	}

	public void SetGameResult(bool win)
	{
		_win = win;
	}
}
