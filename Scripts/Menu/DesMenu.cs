using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class DesMenu : MonoBehaviour {

	[SerializeField]
	private GameObject magicMenu;
	private bool isShow = false;
	//public Transform Camera;
	//private Vector3 offset = Vector3.zero;

//	void Awake() {
//		offset = transform.position - Camera.position;
//	}

	void Update () {
		//if(!isShow) FollowCamera ();
		if(GvrController.AppButtonDown){
			if (isShow) {
				Sequence mySequence = DOTween.Sequence ();  
				mySequence.PrependInterval (2)  
					.Insert (0, transform.DOScale (new Vector3 (0, 1, 0), mySequence.Duration ())); 
				isShow = false;
				Invoke ("SetGameObj", 3.0f);
			} else {
				magicMenu.SetActive (true);
				Sequence mySequence = DOTween.Sequence ();  
				mySequence.PrependInterval (2)  
					.Insert (0, transform.DOScale (new Vector3 (1, 1, 0.6f), mySequence.Duration ())); 
				isShow = true;
			}

		}
	}

//	void FollowCamera(){
//		Vector3 targetPos = Camera.position + offset;
//		transform.position = Vector3.Lerp(transform.position, targetPos, Time.deltaTime * 1.0f);
//		transform.rotation = Quaternion.Lerp(transform.rotation, Camera.rotation , 1.0f * Time.deltaTime);
//
//	}

	void SetGameObj(){
		magicMenu.SetActive (false);
	}
}
