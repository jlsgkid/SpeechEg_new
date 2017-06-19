using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxReact : MonoBehaviour {

	[SerializeField]
	private GameObject box;
	//public GameObject eplrPs;
	[SerializeField]
	private GameObject shake;
	private Animation sk_ani;
	[SerializeField] private Transform player;
	private AudioSource yell;

	// Use this for initialization
	void Start () {
		sk_ani = shake.GetComponent<Animation> ();
		yell = this.GetComponent<AudioSource> ();
	}
		
	public void OnContrlIn(){
		//Debug.Log ("asasa----");
		//box explore
		if(GvrController.ClickButtonDown){
			if(!yell.isPlaying){
				yell.Play ();
			}

			Destroy(box,1.5f);
			shake.SetActive (true);
			sk_ani["goStandPose"].speed = 0.2f;
			sk_ani.CrossFade ("goStandPose");
		}

	}
}
