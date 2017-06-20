﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeechManager : MonoBehaviour {

	public static SpeechManager _instance;
	AndroidJavaClass jc = null;
	AndroidJavaObject jo = null;
	private string curses = "";
	public string GetCurse(){
		return 	this.curses;
	}
	
	void Awake(){
		if (_instance == null) {
			_instance = this;
		} else {
			Destroy(this);  
			_instance = null; 
		}
		//事前準備
		jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
		jo = jc.GetStatic<AndroidJavaObject>("currentActivity");
	}
	
	//視線とかでトリガーする
	public void StartSpeech(){
		//AndroidJavaClass jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
		//AndroidJavaObject jo = jc.GetStatic<AndroidJavaObject>("currentActivity");
		jo.Call("StartActivity1");
	}
	
	public void speechLi(string str){
		//kuang.text = str;
		this.curses = str; 
	}

}
