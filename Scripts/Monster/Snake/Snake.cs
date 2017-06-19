using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Snake : MonoBehaviour {

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
	private int life = 100;
	public Slider xuetiao;
	private PlayerState ps;
	private float timer = 0;
	private Transform player;
	public float walkSpeed = 2.0f;

	// Use this for initialization
	void Start () {
		state = State.IDLE;
		anim = this.GetComponent<Animation> ();
		player = GameObject.FindGameObjectWithTag ("Player").transform;
		ps = GameObject.FindGameObjectWithTag ("Player").GetComponent<PlayerState> ();
	}
	
	// Update is called once per frame
	void Update () {
		AnimationControl();
		if (this.life <= 0) {
			state = State.DIE;
		}
		if(state == State.DIE){
			xuetiao.gameObject.SetActive (false);
			Destroy (this.gameObject, 2.3f);
			return;
		}

		xuetiao.value = this.life;
		timer += Time.deltaTime;
		float dis = Vector3.Distance(transform.position, player.position);
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
			if(timer > 1.0f){
				ps.GetDamage (30);
				ps.xuetiaoFadeIn ();
				timer = 0;
			}
		}
		//Debug.Log ("ssss---:" + life);
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
			anim["biteStandPoseAgressive"].speed = 0.3f;
			anim.Play ("biteStandPoseAgressive");
			break;
		case State.DIE:
			anim["goIdleFloorPose"].speed = 0.14f;
			anim.Play ("goIdleFloorPose");
			//anim.Play ("Take 001");
			break;
		}
	}

}
