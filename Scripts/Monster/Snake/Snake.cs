using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Snake : MonoBehaviour {

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
	private int life = 100;
	public Slider xuetiao;
	private PlayerState ps;
	private float timer = 0;
	private Transform player;
	public float walkSpeed = 1.0f;
	//プレイヤーとの距離
	private float dis = 0.0f;
	[SerializeField] private bool isGazeIn = false;
	
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
		//消滅される判断
		if (this.life <= 0) {
			state = State.DIE;
			xuetiao.gameObject.SetActive (false);
			Destroy (this.gameObject, 2.4f);
			return;
		}
		//if(state == State.DIE){
		//}
		dis = Vector3.Distance(transform.position, player.position);
		//Debug.Log (dis);
		if (dis < 10 && dis > 3.5f) {
			state = State.WALK;
			xuetiao.gameObject.SetActive (true);
			WalkToPlay ();
		} else if (dis < 3.5f) {
			state = State.ATTACK;
		} else if(life > 0){
			state = State.IDLE;
		}

		if(state == State.ATTACK){
			//蛇の攻撃間
			if(timer > 0.5f){
				//プレイヤーに攻撃する
				ps.GetDamage (40);
				ps.xuetiaoFadeIn ();
				timer = 0;
			}
		}
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
		//Debug.Log (state);
		switch (state) {
		case State.IDLE:
			anim.Play ("idleStandPoseNormal");
			break;
		case State.WALK:
			anim.Play ("crawl");
			anim["crawl"].speed = 0.25f;
			break;
		case State.ATTACK:
			//anim.Play ("agressiveJumpBite");
			anim["biteStandPoseAgressive"].speed = 0.2f;
			anim.Play ("biteStandPoseAgressive");
			break;
		case State.DIE:
			anim["goIdleFloorPose"].speed = 0.14f;
			anim.Play ("goIdleFloorPose");
			//anim.Play ("Take 001");
			break;
		}
	}
	
	void ContrlPointerEnter(){
		isGazeIn = true;
	}
	void ContrlPointerExit(){
		isGazeIn = false;
	}
	public bool GetIsGazeIn(){
		return isGazeIn;
	}

}
