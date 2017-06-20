using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fox : MonoBehaviour {

	//ステータス
	public enum State
	{
		IDLE,
		WALK,
		ATTACK,
		DIE
	}
	public State state;
	//アニメ
	private Animation anim;
	//生命
	[SerializeField]
	private int life = 60;
	public Slider xuetiao;
	private Transform player;
	private float timer = 0;
	private PlayerState ps;
	public float walkSpeed = 3.0f;
	//プレイヤーとの距離
	private float dis = 0.0f;

	// Use this for initialization
	void Awake () {
		dis = Vector3.Distance(transform.position, player.position);
		state = State.IDLE;
		anim = this.GetComponent<Animation> ();
		GameObject playerObj =  GameObject.FindGameObjectWithTag ("Player") as GameObject;
		player = playerObj.transform;
		ps = playerObj.GetComponent<PlayerState> ();
	}
	
	// Update is called once per frame
	void Update () {
		AnimationControl();
		xuetiao.value = this.life;
		timer += Time.deltaTime;
		if (this.life < 0) {
			state = State.DIE;
			xuetiao.gameObject.SetActive (false);
			Destroy (this.gameObject, 2.5f);
			return;
		}
		dis = Vector3.Distance(transform.position, player.position);
		//Debug.Log (dis);
		if (dis < 10 && dis > 2.3) {
			state = State.WALK;
			xuetiao.gameObject.SetActive (true);
			WalkToPlay ();
		} else if (dis < 2.3) {
			state = State.ATTACK;
		} else if(this.life > 0){
			state = State.IDLE;
		}
		if(state == State.ATTACK){
			//狼の攻撃間
			if(timer > 1.0f){
				ps.GetDamage (30);
				ps.xuetiaoFadeIn ();
				timer = 0;
			}
		}
	}
	
	private void WalkToPlay(){
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
