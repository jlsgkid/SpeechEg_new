using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeechManager : MonoBehaviour {

	public static SpeechManager _instance;
	private XmlDocManager xm;

	void Awake(){
		if (_instance == null) {
			_instance = this;
		} else {
			Destroy(this);  
			_instance = null; 
		}
	}

	void Start () {
		xm = GameObject.FindGameObjectWithTag ("XML").GetComponent<XmlDocManager> ();
	}
		
	// Update is called once per frame
	void Update () {
		
	}
}
