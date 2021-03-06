﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathologicalGames;

public class PlayerAttack : MonoBehaviour {

	puboic enum CurseStr  
	{  
   	 	Blaze = 0,  
    		Flash = 1,  
    		Blink = 2,
		NONE  = 3,
		VALID = 4;
	} 
	public CurseStr curseStr = CurseStr.Blaze;
	//1.Monster
	[SerializeField] private Fox fox;
	[SerializeField] private Snake snake;
	//[SerializeField] private Spider spider;
	
	//2.Curse
	public GameObject incendio_ps;
	public LightTransform lightTrans;
	//public GameObject flash_ps;
	
	//public Transform ps_incendio;
	public GameObject prt;
	//Mana Bar
	public circleProcess mana_bar;
	private EnergyBar eb;
	public GameObject pos;
	//Ray
	private GvrPointerPhysicsRaycaster gvrRay;
	//Contrl is Down
	[SerializeField] private bool isAct = false;
	//音声入力文字列
	private string speechStr = "";
	//認識できない場合表示するための文字列
	StringBuilder textSpeech = new StringBuilder ();
	
	void Awake () {
		//Rey取得
		gvrRay = GameObject.FindGameObjectWithTag ("MainCamera").GetComponent<GvrPointerPhysicsRaycaster> ();
	}
	
	// Update is called once per frame
	void Update () {
		#if UNITY_EDITOR
		if((Input.GetKeyDown (KeyCode.X){
			isAct = true;
		}else{
			isAct = false;
		}
		#endif
		#if UNITY_ANDROID && !UNITY_EDITOR
		if (GvrController.ClickButtonDown == true) {
			isAct = true;
		}else{
			isAct = false;
		}
		if(isAct){
			//2017/06/20 PC test
			speechStr = "BLAZE";
			
			//StartCoroutine (beginSpeech());
			CurseStr curentStr = DoMana(speechStr);
			if(curentStr == CurseStr.NONE){
				//音声未認識
				//UI表示2s後消し
				textSpeech.text = textSpeech.Append();
			}else if(curentStr == CurseStr.VALID){
				//HP不足
				//UI表示2s後消し
			}else{
				Attack(curentStr);
			}
		}
		#endif
		//Mana Bar Show or Not
		if(mana_bar.GetCurrentAmout() >= 100){
			mana_bar.gameObject.SetActive(false);
		}
		    
	}
	IEnumerator beginSpeech(){
		SpeechManager.StartSpeech();
		yield return new WaitForSeconds(2.0f);
		speechStr = SpeechManager.GetCurse();
	}
		    
	void Attack(CurseStr curentStr){
		string objTag = gvrRay.getRayObjTag();
		if("Snake".Equals(objTag)){
			snake.GetDamage(GetRightDamageByCurse(curentStr));
		}else if("Fox".Equals(objTag)){
			fox.GetDamage(GetRightDamageByCurse(curentStr));	
		}else if("Spider".Equals(objTag)){
			spider.GetDamage(GetRightDamageByCurse(curentStr));
		}
	}
		    
	private int GetRightDamageByCurse(CurseStr curentStr){
		int rtnDamage = 0;
		swith(curentStr){
			case CurentStr.Blaze:
			rtnDamage = 30;
			break;
			case CurentStr.Flash:
			rtnDamage = 40;
			break;	
			default:
			rtnDamage = 0;
			break;					
		}
		return rtnDamage;
	}
		    
	void CurseStr DoMana(string speechWord){
		// get speech from speechManager
		//string speechWord = "BLINK";
		int currentMana =  mana_bar.GetCurrentAmout();
		if("BLAZE".Equals(speechWord)){
			//current mana
			if(currentMana <= 40){
				curseStr = CurseStr.VALID;
				return curseStr;
			}
			curseStr = CurseStr.Blaze;
			mana_bar.gameObject.SetActive(true);
			mana_bar.ChangeManaCir(40);
			incendio_ps.SetActive(true);
			StartCoroutine (SetManaDisper(incendio_ps));
		}else if("BLINK".Equals(speechWord)){
			if(currentMana <= 60){
				curseStr = CurseStr.VALID;
				return curseStr;
			}
			curseStr = CurseStr.Blink;
			mana_bar.gameObject.SetActive(true);
			mana_bar.ChangeManaCir(90);
			lightTrans.StartBlink ();
		}else if("FLASH".Equals(speechWord)){
			//current mana
			if(currentMana <= 80){
				curseStr = CurseStr.VALID;
				return curseStr;
			}
			curseStr = CurseStr.Flash;
			mana_bar.gameObject.SetActive(true);
			mana_bar.ChangeManaCir(60);
			//flash_ps.SetActive(true);
			//StartCoroutine (SetManaDisper(flash_ps));
		}else{
			curseStr = CurseStr.NONE;
		}
	}
	
	private IEnumerator SetManaDisper(GameObject obj_ps){
		yield return new WaitForSeconds(2.0f);
		obj_ps.SetActive(false);
	}

	private void SetManaInvalid(){
		mana_bar.gameObject.SetActive(false);
		incendio_ps.gameObject.SetActive(false);
	}


}
