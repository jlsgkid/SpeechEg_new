using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fox : MonoBehaviour {

	public enum State
	{
		IDLE,
		WALK,
		ATTACK,
		DIE
	}
	public State state;
	private Animation anim;
	[SerializeField]
	private Transform player;
	public float walkSpeed = 2.0f;
	[SerializeField]
	private int life = 100;
	[SerializeField]
	private bool isContrlEnter = false;
	public bool GetIsContrlEnter(){
		return this.isContrlEnter;
	}
	public Slider xuetiao;
	//public GameObject life_bar;
	private PlayerState ps;
	private float timer = 0;

	// Use this for initialization
	void Start () {
		state = State.IDLE;
		anim = this.GetComponent<Animation> ();
		player = GameObject.FindGameObjectWithTag ("Player").transform;
		ps = GameObject.FindGameObjectWithTag ("Player").GetComponent<PlayerState> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (this.life < 0) {
			state = State.DIE;
		}
		if(state == State.DIE){
			xuetiao.gameObject.SetActive (false);
			Destroy (this.gameObject, 3.0f);
			return;
		}
		//player and fox distance
		xuetiao.value = this.life;
		timer += Time.deltaTime;
		float dis = Vector3.Distance(transform.position, player.position);
		//Debug.Log (dis);
		if (dis < 10 && dis > 2.3) {
			state = State.WALK;
			xuetiao.gameObject.SetActive (true);
			//life_bar.SetActive (true);
			WalkToPlay ();
		} else if (dis < 2.3) {
			state = State.ATTACK;
		} else if(this.life > 0){
			state = State.IDLE;
		}

		//set animation
		AnimationControl();
			
		if(state == State.ATTACK){
			if(timer > 1.0f){
				ps.GetDamage (30);
				ps.xuetiaoFadeIn ();
				timer = 0;
			}
		}
//		else{
//			life_bar.gameObject.SetActive (false);
//		}
		//Debug.Log ("isContrlEnter: " + isContrlEnter);

	}
	public void vrContrlEnter(){
		isContrlEnter = true;
	}
	public void vrContrlExit(){
		isContrlEnter = false; 
	}

	void WalkToPlay(){
		transform.rotation = Quaternion.Slerp(transform.rotation, 
			Quaternion.LookRotation(player.position-transform.position), walkSpeed * Time.deltaTime);
		transform.position += transform.forward * walkSpeed * Time.deltaTime;
		transform.position = new Vector3 (transform.position.x, 0, transform.position.z);
	}

	public void GetDamage(int damage){
		if (this.life > 0) {
			this.life -= damage;
		} else {
			state = State.DIE;
		}
	}

	private void AnimationControl(){

		switch (state) {
		case State.IDLE:
			anim.Play ("idleLookAround");
			break;
		case State.WALK:
			anim.Play ("run");
			break;
		case State.ATTACK:
			//anim.Play ("agressiveJumpBite");
			anim.Play ("standAgressiveBite");
			break;
		case State.DIE:
			anim.Play ("death");
			break;
		}
	}


}
