using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuChangeText : MonoBehaviour {

	public Text title;
	public Text att_text;
	public Text des_text;
	public Text diff_text;
	public Text mp_text;

	void Start(){
		FireEnter ();
	}

	public void FireEnter(){
		//blaze
		title.text = "<color=yellow><size=45>Blaze</size></color>\n発音：ブレェィズ";
		att_text.text = "Attack <size=100>40</size>";
		des_text.text = "火が龍の如く相手に襲い掛かる非常に威力が高い魔法である";
		diff_text.text = "難易度";
		mp_text.text = "MP";
	}

	public void FlashEnter(){
		title.text = "<color=yellow><size=45>Flash</size></color>\n発音：ブレェィズ";
		att_text.text = "Attack <size=100>60</size>";
		des_text.text = "放電する術である。但し、使用には自身が持っているMP(チャクラ)の半分を消費する魔法である";
		diff_text.text = "難易度";
		mp_text.text = "MP";
	}

	public void LightEnter(){

	}
}
